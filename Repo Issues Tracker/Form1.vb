Imports System.Data.SqlTypes
Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Form1
    ' VARIABLE DECLARATIONS
    Private Const RedirectUri As String = "http://localhost:5000/"

    Private isTokenRequested As Boolean = False

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAuthenticate.Click
        Dim ClientId As String = My.Settings.GitHubClientID
        Dim authorizeUrl As String = $"https://github.com/login/oauth/authorize?client_id={ClientId}&redirect_uri={RedirectUri}&scope=repo"

        Dim startInfo As New ProcessStartInfo()
        startInfo.UseShellExecute = True
        startInfo.FileName = authorizeUrl

        Try
            Process.Start(startInfo)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to open the browser: " & ex.Message)
        End Try

        ' Start listening for the callback using HttpListener
        Task.Run(AddressOf HandleCallback)
    End Sub

    Private Async Function HandleCallback() As Task
        Dim listener As New HttpListener()
        listener.Prefixes.Add(RedirectUri)
        listener.Start()

        Try
            ' Wait for the callback
            Debug.WriteLine("Waiting for GitHub OAuth callback...")

            Dim context = Await listener.GetContextAsync()
            Dim code = context.Request.QueryString("code")

            If Not String.IsNullOrEmpty(code) Then
                ' Respond to the request
                Dim response = context.Response
                Dim responseString = "Authorization successful. You can close this window."
                Dim buffer = System.Text.Encoding.UTF8.GetBytes(responseString)

                response.ContentLength64 = buffer.Length
                Using output = response.OutputStream
                    Await output.WriteAsync(buffer, 0, buffer.Length)
                End Using

                ' Proceed to exchange the authorization code for an access token
                Await GetAccessToken(code)
            Else
                Debug.WriteLine("Authorization code not found in the request.")
            End If

        Catch ex As Exception
            Debug.WriteLine("Error in HTTP listener: " & ex.Message)
        Finally
            listener.Stop()
        End Try
    End Function

    ' Function to get access token
    Private Async Function GetAccessToken(code As String) As Task
        Dim client = New HttpClient()
        Dim values = New Dictionary(Of String, String) From {
            {"client_id", My.Settings.GitHubClientID},
            {"client_secret", My.Settings.GitHubClientSecret},
            {"code", code},
            {"redirect_uri", RedirectUri}
        }

        Dim content = New FormUrlEncodedContent(values)

        Try
            Dim response = Await client.PostAsync("https://github.com/login/oauth/access_token", content)
            Dim responseString = Await response.Content.ReadAsStringAsync()

            ' Log the full response for debugging purposes
            Debug.WriteLine("Full response: " & responseString)

            If response.IsSuccessStatusCode Then
                Dim token As String = Nothing

                ' Check if the response is JSON or query string
                If responseString.StartsWith("{") Then
                    ' Handle JSON response
                    Dim jsonResponse = JObject.Parse(responseString)
                    token = jsonResponse("access_token")?.ToString()
                Else
                    ' Handle query string response
                    Dim queryResponse = System.Web.HttpUtility.ParseQueryString(responseString)
                    token = queryResponse("access_token")
                End If

                If Not String.IsNullOrEmpty(token) Then
                    My.Settings.GitHubAccessToken = token
                    My.Settings.Save() ' Save the settings
                    Debug.WriteLine("Access Token saved in settings.")
                    Await LoadRepositories(token)
                Else
                    Debug.WriteLine("Failed to retrieve access token from response.")
                End If
            Else
                Debug.WriteLine($"Error retrieving access token: {response.StatusCode} - {responseString}")
                MessageBox.Show($"Error retrieving access token: {response.StatusCode} - {responseString}")
            End If
        Catch ex As Exception
            Debug.WriteLine("Exception during token exchange: " & ex.Message)
            MessageBox.Show("Exception during token exchange: " & ex.Message)
        End Try
        btnAuthenticate.BackColor = SystemColors.Info

    End Function

    Private Sub AddCustomReposToCombobox()
        For Each line As String In My.Settings.CustomReposList.Split(Environment.NewLine)
            'MsgBox(line.ToString)
            ComboBox1.Items.Add(line)
        Next
    End Sub

    ' Function to load repositories into combobox
    Public Async Function LoadRepositories(token As String, Optional RunningAsFormLoad As Boolean = False) As Task
        Dim client = New HttpClient()
        client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("token", token)
        client.DefaultRequestHeaders.UserAgent.ParseAdd("request")

        Try
            Dim response = Await client.GetStringAsync("https://api.github.com/user/repos")
            Dim repos = JArray.Parse(response)

            ' Update the ComboBox on the UI thread
            If ComboBox1.InvokeRequired Then
                ComboBox1.Invoke(Sub()
                                     ComboBox1.Items.Clear() ' Clear existing items
                                     For Each repo In repos
                                         ComboBox1.Items.Add(repo("name").ToString())
                                     Next
                                     AddCustomReposToCombobox()
                                 End Sub)
            Else
                ComboBox1.Items.Clear() ' Clear existing items
                For Each repo In repos
                    ComboBox1.Items.Add(repo("name").ToString())
                Next
                AddCustomReposToCombobox()
            End If

            ' Select Saved Repo From Last Session If it exists
            If RunningAsFormLoad Then
                If My.Settings.RepoSelection <> "" Then

                    ' Check if combobox has the item we had in our setting value. If a match is found, select it.
                    For Each item In ComboBox1.Items
                        'MsgBox(item.ToString())
                        If item.ToString = My.Settings.RepoSelection Then
                            ComboBox1.SelectedItem = My.Settings.RepoSelection

                        End If
                    Next

                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading repositories: " & ex.Message)
        End Try
    End Function

    ' Event handler for when a repository is selected
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim selectedRepo As String = ComboBox1.SelectedItem.ToString()
        If Not String.IsNullOrEmpty(selectedRepo) Then
            ' Fetch issues for the selected repository
            Task.Run(Function() LoadIssues(selectedRepo))
        End If
    End Sub

    ' Function to load issues for the selected repository into DataGridView
    Public Async Function LoadIssues(repoName As String) As Task
        Dim username ' Will hold either current user, or repo owner.
        ' Correct for repo name potentially having a custom 3rd party owner in it.
        Dim ThirdPartyRepo As Boolean = False
        If repoName.Contains("/") Then
            username = repoName.TextBefore("/") ' Dont move this in front of the next line where we set reponame.
            repoName = repoName.TextAfter("/") 'Naw seriously don't move it in front of me. 
            My.Settings.GitHubUserName = username
            ThirdPartyRepo = True

        End If
        Dim client = New HttpClient()
        Dim token = My.Settings.GitHubAccessToken ' Retrieve the token from settings
        client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("token", token)
        client.DefaultRequestHeaders.UserAgent.ParseAdd("request")

        Try
            ' Retrieve the current user's username
            If ThirdPartyRepo Then
                'Already set username above, do nothing
            Else ' Not 3rd party repo, use current user for username / repo owner.
                Dim userResponse = Await client.GetStringAsync("https://api.github.com/user")
                Dim user = JObject.Parse(userResponse)
                username = user("login").ToString()
                My.Settings.GitHubUserName = username
            End If

            Dim response = Await client.GetStringAsync($"https://api.github.com/repos/{username}/{repoName}/issues?state=open")
            Dim issues = JArray.Parse(response)

            ' Prepare DataGridView update on UI thread
            If DataGridView1.InvokeRequired Then
                DataGridView1.Invoke(Sub() PopulateDataGridView(issues))
            Else
                PopulateDataGridView(issues)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading issues: " & ex.Message)
        End Try
    End Function


    ' Function to populate DataGridView with issue details
    Private Sub PopulateDataGridView(issues As JArray)
        DataGridView1.Rows.Clear() ' Clear existing rows
        'DataGridView1.Columns.Clear() ' Clear existing columns

        ' Define columns for DataGridView
        'DataGridView1.Columns.Add("IssueNumber", "Issue Number")
        'DataGridView1.Columns.Add("Title", "Title")
        'DataGridView1.Columns.Add("State", "State")
        'DataGridView1.Columns.Add("CreatedAt", "Created At")

        ' Populate rows with issue data
        For Each issue In issues
            Dim issueNumber As Integer = issue("number")
            Dim title As String = issue("title").ToString()
            Dim state As String = issue("state").ToString()
            Dim createdAt As String = issue("created_at").ToString()
            'Dim details As String = issue("body")?.ToString() ' Get issue details

            DataGridView1.Rows.Add(issueNumber, title, state, createdAt)
        Next
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'InitializeComponent() ' Putting this here causes something to break with the githab auth, the button becomes yellow and nothing happens
        ' after the button is clicked. Not going to bother investigating for now. 10/12/2024 drobinson

        'If setting for githubaccesstoken is populated, load cbox for repos list.
        If My.Settings.GitHubAccessToken <> "" Then
            Try
                btnAuthenticate.BackColor = SystemColors.Info
                LoadRepositories(My.Settings.GitHubAccessToken, True)
            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try

        End If
        If My.Settings.GitHubClientID = "" Then
            GetOAuthInfo()

        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem IsNot Nothing Then
            Dim selectedRepo As String = ComboBox1.SelectedItem.ToString()
            Dim accessToken As String = My.Settings.GitHubAccessToken
            Dim newIssueForm As New NewIssue(selectedRepo, accessToken, Me)
            newIssueForm.Show()
        Else
            MessageBox.Show("Please select a repository first.")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoadIssues(ComboBox1.SelectedItem.ToString)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        GetOAuthInfo()
    End Sub
    Private Sub GetOAuthInfo()
        My.Settings.GitHubClientID = InputBox("Enter your Github Client ID - This should be for a Github OAuth App:")
        My.Settings.GitHubClientSecret = InputBox("Enter your Github Client Secret:")
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        ' Check if the double-clicked cell is valid
        If e.RowIndex >= 0 Then
            ' Get the currently selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Retrieve the IssueNumber from the specified column
            Dim IssueNumber As Integer = Convert.ToInt32(selectedRow.Cells("IssueNumber").Value)

            ' Now you can use the IssueNumber variable as needed
            'MessageBox.Show($"You selected Issue Number: {IssueNumber}")
            Dim x As New ViewExistingIssue(ComboBox1.SelectedItem.ToString, IssueNumber, Me)
            x.Show()
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Save dropdown for repo selection to a setting, to be retrieved on next launch.
        If ComboBox1.SelectedItem.ToString <> "" Then
            My.Settings.RepoSelection = ComboBox1.SelectedItem.ToString()
        End If


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim x As New CustomReposList(Me)
        x.Show()

    End Sub
End Class
