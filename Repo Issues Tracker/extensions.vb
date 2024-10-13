' Items copied from my VB.NET extensions repo: https://github.com/drobin04/VB.NET-Extensions/blob/main/VB%20Extensions/extensions.vb



Imports System.Runtime.CompilerServices

Module extensions
    '<summary>
    ' Returns the text occurring after a specific string, as opposed to needing to get text after a given index using substring()
    '</summary>
    <Extension()>
    Public Function TextAfter(Text As String, StartingPoint As String) As String
        Dim IndexOfStartingPoint As Integer

        IndexOfStartingPoint = Text.IndexOf(StartingPoint)
        Try
            Return Text.Substring(IndexOfStartingPoint + StartingPoint.Length)
        Catch
            Return ""
        End Try
    End Function

    '<summary>
    ' Returns the text occurring before a specific string
    '</summary>
    <Extension()>
    Public Function TextBefore(Text As String, TextDelimiter As String) As String
        Dim IndexOfDelimiter As Integer = 0
        Dim TextBeforeDelimiter As String = ""
        Try
            If Text.Contains(TextDelimiter) Then
                IndexOfDelimiter = Text.IndexOf(TextDelimiter)
                TextBeforeDelimiter = Text.Substring(0, IndexOfDelimiter)

            Else
                TextBeforeDelimiter = Text

            End If

        Catch

        End Try

        Return TextBeforeDelimiter
    End Function

End Module
