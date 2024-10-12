Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class NewIssue
    Private Const GitHubApiUrl As String = "https://api.github.com/repos/{0}/issues"
    Private AccessToken As String
    Private RepoName As String
    Private ParentForm As Form1
    ' Constructor to initialize form with repo name and access token
    Public Sub New(repoName As String, accessToken As String, parent As Form1)
        InitializeComponent()
        Me.RepoName = repoName
        Me.AccessToken = accessToken
        Me.ParentForm = parent
        'MsgBox(Me.RepoName & Me.AccessToken)
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim title As String = TextBox1.Text
        Dim details As String = TextBox2.Text
        'MsgBox("Repo name: " & RepoName)
        'MsgBox("Access token: " & AccessToken)


        If String.IsNullOrWhiteSpace(title) OrElse String.IsNullOrWhiteSpace(details) Then
            MessageBox.Show("Please enter both a title and details for the issue.")
            Return
        End If

        Await CreateIssue(title, details)
    End Sub

    Private Async Function CreateIssue(title As String, details As String) As Task
        Dim client = New HttpClient()
        Dim token = My.Settings.GitHubAccessToken ' Assuming you stored the token in Settings
        client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("token", token)
        client.DefaultRequestHeaders.UserAgent.ParseAdd("request")

        Dim issueData = New JObject(New JProperty("title", title), New JProperty("body", details))
        Dim content = New StringContent(issueData.ToString(), Encoding.UTF8, "application/json")
        Dim username As String = ""

        Try
            Dim client2 = New HttpClient()
            Dim token2 = My.Settings.GitHubAccessToken ' Retrieve the token from settings
            client2.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("token", token)
            client2.DefaultRequestHeaders.UserAgent.ParseAdd("request")

            ' Retrieve the current user's username
            Dim userResponse = Await client.GetStringAsync("https://api.github.com/user")
            Dim user = JObject.Parse(userResponse)
            username = user("login").ToString()
        Catch ex As Exception
            MsgBox("Failed to populate username. " & ex.ToString)
        End Try


        Dim response = Await client.PostAsync($"https://api.github.com/repos/{username}/{RepoName}/issues", content)

        If response.IsSuccessStatusCode Then
            'MessageBox.Show("Issue created successfully!")
        Else
            Dim errorResponse = Await response.Content.ReadAsStringAsync()
            MessageBox.Show($"Error creating issue: {response.StatusCode} - {errorResponse}")
        End If
        Me.ParentForm.LoadIssues(RepoName)

        Me.Close()

    End Function
End Class
