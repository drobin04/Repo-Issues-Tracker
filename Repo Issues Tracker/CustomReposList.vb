Public Class CustomReposList
    Private Parentform1 As Form1

    Public Sub New(ParentForm As Form1)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Parentform1 = ParentForm

    End Sub

    Private Sub CustomReposList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.CustomReposList

    End Sub

    Private Sub CustomReposList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.CustomReposList = TextBox1.Text
        ' Reload the list of repo's on the main form.
        Parentform1.LoadRepositories(My.Settings.GitHubAccessToken, True)
        Me.Close()
    End Sub
End Class