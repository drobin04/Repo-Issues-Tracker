Imports System.Net
Imports System.IO
Imports System.Text

Public Class AddCommentToIssue

    Private AccessToken As String
    Private RepoOwner As String
    Private RepoName As String
    Private IssueId As Integer
    ' Constructor to initialize form with repository owner, repo name, and issue ID
    Public Sub New(repoName As String, issueId As Integer)
        InitializeComponent()
        Me.RepoOwner = My.Settings.GitHubUserName
        Me.RepoName = repoName
        Me.IssueId = issueId
        Me.AccessToken = My.Settings.GitHubAccessToken
    End Sub


    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        AddCommentToIssue(RepoOwner, RepoName, IssueId, AccessToken, TextBox1.Text)
        MsgBox("Comment submitted!")
        Me.Close()

    End Sub

    Public Function AddCommentToIssue(owner As String, repo As String, issueNumber As Integer, accessToken As String, comment As String) As String
        Try
            ' The GitHub API URL for adding a comment
            Dim url As String = $"https://api.github.com/repos/{owner}/{repo}/issues/{issueNumber}/comments"

            ' Create the web request
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.UserAgent = "YourAppName" ' GitHub requires a User-Agent header
            request.Headers("Authorization") = $"Bearer {accessToken}"

            ' Prepare the JSON body with the comment text
            Dim jsonData As String = $"{{""body"": ""{comment.Replace("""", "\""")}""}}"
            Dim dataBytes As Byte() = Encoding.UTF8.GetBytes(jsonData)
            request.ContentLength = dataBytes.Length

            ' Write the request body
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(dataBytes, 0, dataBytes.Length)
            End Using

            ' Get the response from GitHub
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()
                    Return $"Comment added successfully: {responseText}"
                End Using
            End Using

        Catch ex As WebException
            ' If the request failed, try to read the error message from the response
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim errorResponse As String = reader.ReadToEnd()
                    Return $"Error adding comment: {errorResponse}"
                End Using
            End If
            ' Return the exception message if no response is available
            Return $"Exception occurred: {ex.Message}"
        End Try
    End Function

End Class