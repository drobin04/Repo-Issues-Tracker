<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewExistingIssue
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        txtTitle = New TextBox()
        TextBox2 = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        btnViewOnGithub = New Button()
        SuspendLayout()
        ' 
        ' txtTitle
        ' 
        txtTitle.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtTitle.BackColor = SystemColors.Window
        txtTitle.Location = New Point(12, 23)
        txtTitle.Name = "txtTitle"
        txtTitle.ReadOnly = True
        txtTitle.Size = New Size(501, 23)
        txtTitle.TabIndex = 0
        ' 
        ' TextBox2
        ' 
        TextBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox2.BackColor = SystemColors.Window
        TextBox2.Location = New Point(12, 72)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.ReadOnly = True
        TextBox2.ScrollBars = ScrollBars.Vertical
        TextBox2.Size = New Size(501, 152)
        TextBox2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 5)
        Label1.Name = "Label1"
        Label1.Size = New Size(32, 15)
        Label1.TabIndex = 2
        Label1.Text = "Title:"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(12, 49)
        Label2.Name = "Label2"
        Label2.Size = New Size(45, 15)
        Label2.TabIndex = 3
        Label2.Text = "Details:"
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button1.Location = New Point(12, 230)
        Button1.Name = "Button1"
        Button1.Size = New Size(188, 23)
        Button1.TabIndex = 4
        Button1.Text = "CLOSE AND COMPLETE  ISSUE"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button2.Location = New Point(206, 230)
        Button2.Name = "Button2"
        Button2.Size = New Size(121, 23)
        Button2.TabIndex = 5
        Button2.Text = "Add Comment"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' btnViewOnGithub
        ' 
        btnViewOnGithub.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnViewOnGithub.Location = New Point(333, 230)
        btnViewOnGithub.Name = "btnViewOnGithub"
        btnViewOnGithub.Size = New Size(116, 23)
        btnViewOnGithub.TabIndex = 6
        btnViewOnGithub.Text = "View On Github"
        btnViewOnGithub.UseVisualStyleBackColor = True
        ' 
        ' ViewExistingIssue
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(528, 259)
        Controls.Add(btnViewOnGithub)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(TextBox2)
        Controls.Add(txtTitle)
        Name = "ViewExistingIssue"
        Text = "ViewExistingIssue"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtTitle As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents btnViewOnGithub As Button
End Class
