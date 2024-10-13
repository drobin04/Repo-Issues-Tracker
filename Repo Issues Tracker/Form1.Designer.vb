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
        DataGridView1 = New DataGridView()
        IssueNumber = New DataGridViewTextBoxColumn()
        Title = New DataGridViewTextBoxColumn()
        State = New DataGridViewTextBoxColumn()
        CreatedAt = New DataGridViewTextBoxColumn()
        Button2 = New Button()
        ComboBox1 = New ComboBox()
        lblRepoName = New Label()
        Button1 = New Button()
        Button4 = New Button()
        LinkLabel1 = New LinkLabel()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
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
        ComboBox1.Location = New Point(12, 27)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(265, 23)
        ComboBox1.TabIndex = 4
        ' 
        ' lblRepoName
        ' 
        lblRepoName.AutoSize = True
        lblRepoName.Location = New Point(12, 9)
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
        ' Button4
        ' 
        Button4.Location = New Point(283, 26)
        Button4.Name = "Button4"
        Button4.Size = New Size(132, 23)
        Button4.TabIndex = 8
        Button4.Text = "Custom Repos List"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' LinkLabel1
        ' 
        LinkLabel1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        LinkLabel1.AutoSize = True
        LinkLabel1.Location = New Point(686, 5)
        LinkLabel1.Name = "LinkLabel1"
        LinkLabel1.Size = New Size(108, 15)
        LinkLabel1.TabIndex = 9
        LinkLabel1.TabStop = True
        LinkLabel1.Text = "Update GitHub PAT"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 232)
        Controls.Add(LinkLabel1)
        Controls.Add(Button4)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(Button2)
        Controls.Add(DataGridView1)
        Controls.Add(lblRepoName)
        MinimumSize = New Size(600, 270)
        Name = "Form1"
        Text = "Repo Issues Tracker"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents lblRepoName As Label
    Friend WithEvents IssueNumber As DataGridViewTextBoxColumn
    Friend WithEvents Title As DataGridViewTextBoxColumn
    Friend WithEvents State As DataGridViewTextBoxColumn
    Friend WithEvents CreatedAt As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents LinkLabel1 As LinkLabel

End Class
