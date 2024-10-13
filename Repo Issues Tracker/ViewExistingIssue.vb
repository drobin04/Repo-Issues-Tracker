Imports System.Net.Http
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Newtonsoft.Json.Linq
Imports System.Diagnostics
Public Class ViewExistingIssue
    Private Const GitHubApiUrl As String = "https://api.github.com/repos/{0}/{1}/issues/{2}"
    Private AccessToken As String
    Private RepoOwner As String
    Private RepoName As String
    Private IssueId As Integer
    Private ParentForm As Form1
    ' Constructor to initialize form with repository owner, repo name, and issue ID
    Public Sub New(repoName As String, issueId As Integer, parent As Form1)
        InitializeComponent()
        Me.RepoOwner = My.Settings.GitHubUserName
        Me.RepoName = repoName
        Me.IssueId = issueId
        Me.AccessToken = My.Settings.GitHubAccessToken
        Me.ParentForm = parent
        If Me.RepoName.Contains("/") Then Me.RepoName = Me.RepoName.TextAfter("/")
    End Sub

    ' Function to load comments for the specified issue
    Public Async Sub LoadComments()
        Dim client = New HttpClient()
        client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("token", AccessToken)
        client.DefaultRequestHeaders.UserAgent.ParseAdd("request")

        Try


            ' Construct the API URL to get the issue details
            Dim issueApiUrl As String = String.Format(GitHubApiUrl, RepoOwner, RepoName, IssueId)
            Dim issue
            Debug.Write(issueApiUrl)
            Try
                ' Fetch the issue details
                Dim issueResponse = Await client.GetStringAsync(issueApiUrl)
                Debug.Write(issueResponse.ToString)

                issue = JObject.Parse(issueResponse)
                Dim issueDetails As String = issue("body").ToString()

            Catch ex As Exception
                MsgBox("Error fetching issue body content" & ex.ToString)
            End Try

            ' Prepare the output
            Dim commentsText As New System.Text.StringBuilder()
            txtTitle.Text = issue("title")
            'commentsText.AppendLine($"Created by: {issue("user")("login")} at: {issue("created_at")}")
            commentsText.AppendLine(issue("body").ToString())
            commentsText.AppendLine("----------")

            ' Prepare comments for display
            Dim formattedComments As New System.Text.StringBuilder()
            Try
                ' Fetch the comments for the specified issue
                Dim response = Await client.GetStringAsync(String.Format(GitHubApiUrl & "/comments", RepoOwner, RepoName, IssueId))
                Dim comments = JArray.Parse(response)
                For Each comment In comments
                    Dim body As String = comment("body").ToString()
                    Dim createdAt As String = comment("created_at").ToString()
                    Dim user As String = comment("user")("login").ToString()

                    commentsText.AppendLine($"User: {user}")
                    commentsText.AppendLine($"Date: {createdAt}")
                    commentsText.AppendLine(body)
                    commentsText.AppendLine("----------")
                    commentsText.AppendLine() ' Add a line break
                Next
            Catch ex As Exception
                MessageBox.Show("Error loading comments: " & ex.Message)
            End Try

            ' Display comments in the TextBox
            TextBox2.Text = commentsText.ToString()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub


    Private Sub ViewExistingIssue_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComments()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CloseIssue(My.Settings.GitHubUserName, Me.RepoName, Me.IssueId, My.Settings.GitHubAccessToken)

    End Sub

    Public Async Function CloseIssue(owner As String, repo As String, issueNumber As Integer, accessToken As String) As Task
        ' Construct the API URL for closing the issue
        Dim apiUrl As String = $"https://api.github.com/repos/{owner}/{repo}/issues/{issueNumber}"

        Using client As New HttpClient()
            client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("token", accessToken)
            client.DefaultRequestHeaders.UserAgent.ParseAdd("request")

            ' Prepare the JSON content to close the issue
            Dim issueData = New JObject(New JProperty("state", "closed"))
            Dim content = New StringContent(issueData.ToString(), Encoding.UTF8, "application/json")

            ' Send the PATCH request to close the issue
            Dim response = Await client.PatchAsync(apiUrl, content)

            If response.IsSuccessStatusCode Then
                'MessageBox.Show("Issue closed successfully!")

                Me.Close()
                Me.ParentForm.LoadIssues(RepoName)

                Me.Close()
            Else
                Dim errorResponse = Await response.Content.ReadAsStringAsync()
                MessageBox.Show($"Error closing issue: {response.StatusCode} - {errorResponse}")
            End If
        End Using
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim x As New AddCommentToIssue(RepoName, IssueId, Me)
        x.Show()

    End Sub

    Private Sub btnViewOnGithub_Click(sender As Object, e As EventArgs) Handles btnViewOnGithub.Click
        OpenIssueInBrowser(RepoOwner, RepoName, IssueId)
    End Sub

    Public Sub OpenIssueInBrowser(owner As String, repo As String, issueNumber As Integer)
        ' Construct the GitHub issue URL
        Dim url As String = $"https://github.com/{owner}/{repo}/issues/{issueNumber}"

        ' Use Process.Start with UseShellExecute = True to open the default web browser
        Dim psi As New ProcessStartInfo()
        psi.UseShellExecute = True
        psi.FileName = url

        Try
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"Unable to open the issue in the browser. Error: {ex.Message}")
        End Try
    End Sub

End Class
