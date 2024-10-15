<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class options
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
        Button3 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        Button1 = New Button()
        Button2 = New Button()
        SuspendLayout()
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(12, 12)
        Button3.Name = "Button3"
        Button3.Size = New Size(168, 23)
        Button3.TabIndex = 11
        Button3.Text = "Clear Auth Settings / PAT"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(196, 27)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(305, 137)
        TextBox1.TabIndex = 12
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(196, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(155, 15)
        Label1.TabIndex = 13
        Label1.Text = "Debug Log (For Future Use):"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point)
        Label3.Location = New Point(583, 25)
        Label3.Name = "Label3"
        Label3.Size = New Size(157, 21)
        Label3.TabIndex = 15
        Label3.Text = "Repo Issues Tracker"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(527, 59)
        Label4.Name = "Label4"
        Label4.Size = New Size(258, 15)
        Label4.TabIndex = 16
        Label4.Text = "Open Source GitHub Issues Management Utility"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(581, 86)
        Label5.Name = "Label5"
        Label5.Size = New Size(161, 15)
        Label5.TabIndex = 17
        Label5.Text = "Written by Douglas Robinson"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(572, 114)
        Button1.Name = "Button1"
        Button1.Size = New Size(89, 50)
        Button1.TabIndex = 18
        Button1.Text = "View Project On GitHub"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(667, 114)
        Button2.Name = "Button2"
        Button2.Size = New Size(88, 50)
        Button2.TabIndex = 19
        Button2.Text = "View Author On LinkedIn"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' options
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(797, 176)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label1)
        Controls.Add(TextBox1)
        Controls.Add(Button3)
        Name = "options"
        Text = "Options"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button3 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
