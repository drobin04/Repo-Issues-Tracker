<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        btnAuthenticate = New Button()
        DataGridView1 = New DataGridView()
        IssueNumber = New DataGridViewTextBoxColumn()
        Title = New DataGridViewTextBoxColumn()
        State = New DataGridViewTextBoxColumn()
        CreatedAt = New DataGridViewTextBoxColumn()
        Button2 = New Button()
        ComboBox1 = New ComboBox()
        lblRepoName = New Label()
        Button1 = New Button()
        Button3 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnAuthenticate
        ' 
        btnAuthenticate.BackColor = Color.Yellow
        btnAuthenticate.Location = New Point(12, 29)
        btnAuthenticate.Name = "btnAuthenticate"
        btnAuthenticate.Size = New Size(109, 23)
        btnAuthenticate.TabIndex = 1
        btnAuthenticate.Text = "Authenticate"
        btnAuthenticate.UseVisualStyleBackColor = False
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {IssueNumber, Title, State, CreatedAt})
        DataGridView1.Location = New Point(12, 56)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.Size = New Size(776, 164)
        DataGridView1.TabIndex = 2
        ' 
        ' IssueNumber
        ' 
        IssueNumber.HeaderText = "Issue Number"
        IssueNumber.Name = "IssueNumber"
        IssueNumber.ReadOnly = True
        ' 
        ' Title
        ' 
        Title.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Title.HeaderText = "Title"
        Title.Name = "Title"
        Title.ReadOnly = True
        ' 
        ' State
        ' 
        State.HeaderText = "State"
        State.Name = "State"
        State.ReadOnly = True
        State.Width = 50
        ' 
        ' CreatedAt
        ' 
        CreatedAt.HeaderText = "Created At"
        CreatedAt.Name = "CreatedAt"
        CreatedAt.ReadOnly = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button2.Location = New Point(713, 27)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 3
        Button2.Text = "Refresh"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(127, 27)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(265, 23)
        ComboBox1.TabIndex = 4
        ' 
        ' lblRepoName
        ' 
        lblRepoName.AutoSize = True
        lblRepoName.Location = New Point(127, 9)
        lblRepoName.Name = "lblRepoName"
        lblRepoName.Size = New Size(37, 15)
        lblRepoName.TabIndex = 0
        lblRepoName.Text = "Repo:"
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button1.Location = New Point(632, 27)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 6
        Button1.Text = "New Issue"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(12, 5)
        Button3.Name = "Button3"
        Button3.Size = New Size(109, 23)
        Button3.TabIndex = 7
        Button3.Text = "GitHub Client ID"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 232)
        Controls.Add(Button3)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(Button2)
        Controls.Add(DataGridView1)
        Controls.Add(btnAuthenticate)
        Controls.Add(lblRepoName)
        MinimumSize = New Size(600, 270)
        Name = "Form1"
        Text = "Repo Issues Tracker"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents btnAuthenticate As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents lblRepoName As Label
    Friend WithEvents IssueNumber As DataGridViewTextBoxColumn
    Friend WithEvents Title As DataGridViewTextBoxColumn
    Friend WithEvents State As DataGridViewTextBoxColumn
    Friend WithEvents CreatedAt As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button

End Class
