Imports System.Security.Policy

Public Class options
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim url As String = "https://github.com/drobin04/repo-issues-tracker"
        ' Use Process.Start with UseShellExecute = True to open the default web browser
        Dim psi As New ProcessStartInfo()
        psi.UseShellExecute = True
        psi.FileName = Url

        Try
            Process.Start(psi)
        Catch ex As Exception
            MessageBox.Show($"Unable to open the issue in the browser. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim url As String = "https://www.linkedin.com/in/douglas-robinson-653900a9/"
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