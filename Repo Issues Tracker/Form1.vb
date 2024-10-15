Imports System.Data.SqlTypes
Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Form1
    ' VARIABLE DECLARATIONS
    Private Const RedirectUri As String = "http://localhost:5000/"

    Private isTokenRequested As Boolean = False


#Region "Deprecated / Code To Be Removed"




#End Region


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
                'btnAuthenticate.BackColor = SystemColors.Info
                LoadRepositories(My.Settings.GitHubAccessToken, True)
            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try

        End If
        If My.Settings.GitHubAccessToken = "" Then
            'GetOAuthInfo()
            UpdatePAT()

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
        Try
            ' Save dropdown for repo selection to a setting, to be retrieved on next launch.
            If ComboBox1.SelectedItem.ToString <> "" Then
                My.Settings.RepoSelection = ComboBox1.SelectedItem.ToString()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim x As New CustomReposList(Me)
        x.Show()

    End Sub


    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        UpdatePAT()

    End Sub

    Private Sub UpdatePAT()
        My.Settings.GitHubAccessToken = InputBox("Enter GitHub Personal Access Token - You can get this by navigating to Settings > Developer Settings > Personal Access Tokens on Github. It will need read/write permissions on Repos/Issues in order to work properly.")
        LoadRepositories(My.Settings.GitHubAccessToken)

        MsgBox("API Token Changed To : " & My.Settings.GitHubAccessToken)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim x As New options
        x.Show()

    End Sub
End Class
