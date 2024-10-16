﻿Imports System.Net
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class AddCommentToIssue

    Private AccessToken As String
    Private RepoOwner As String
    Private RepoName As String
    Private IssueId As Integer
    Private ParentForm As ViewExistingIssue

    ' Constructor to initialize form with repository owner, repo name, and issue ID
    Public Sub New(repoName As String, issueId As Integer, Parent As ViewExistingIssue)
        InitializeComponent()
        Me.RepoOwner = My.Settings.GitHubUserName
        Me.RepoName = repoName
        Me.IssueId = issueId
        Me.AccessToken = My.Settings.GitHubAccessToken
        Me.ParentForm = Parent
        If Me.RepoName.Contains("/") Then Me.RepoName = Me.RepoName.TextAfter("/")
    End Sub


    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        AddCommentToIssue(RepoOwner, RepoName, IssueId, AccessToken, TextBox1.Text)

        Me.ParentForm.LoadComments()

        Me.Close()

    End Sub

    Public Sub AddCommentToIssue(owner As String, repo As String, issueNumber As Integer, accessToken As String, comment As String)
        Try
            ' The GitHub API URL for adding a comment
            Dim url As String = $"https://api.github.com/repos/{owner}/{repo}/issues/{issueNumber}/comments"

            ' Create the web request
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.UserAgent = "YourAppName" ' GitHub requires a User-Agent header
            request.Headers("Authorization") = $"Bearer {accessToken}"

            ' Prepare the JSON body with the comment text, using JObject to escape properly
            Dim jsonData As New JObject(New JProperty("body", comment))
            Dim dataBytes As Byte() = Encoding.UTF8.GetBytes(jsonData.ToString())
            request.ContentLength = dataBytes.Length

            ' Write the request body
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(dataBytes, 0, dataBytes.Length)
            End Using

            ' Get the response from GitHub
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()

                End Using
            End Using

        Catch ex As WebException
            ' If the request failed, try to read the error message from the response
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim errorResponse As String = reader.ReadToEnd()
                    MsgBox($"Error adding comment: {errorResponse}")
                End Using
            End If
            ' Return the exception message if no response is available
            MsgBox($"Exception occurred: {ex.Message}")
        End Try
    End Sub

End Class