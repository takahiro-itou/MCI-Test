Public Class Form1

    <System.Runtime.InteropServices.DllImport("winmm.dll",
    CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function mciSendString(ByVal command As String,
    ByVal buffer As System.Text.StringBuilder,
    ByVal bufferSize As Integer, ByVal hwndCallback As IntPtr) As Integer
    End Function

    Private aliasName As String = "MediaFile"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '再生するファイル名
        Dim curDir = Application.StartupPath
        Dim fileName = "Test.wmv"

        Dim cmd As String

        'ファイルを開く
        cmd = "open """ + fileName + """ alias " + aliasName
        If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
            Return
        End If

        cmd = "window " & aliasName & " handle " & PictureBox1.Handle.ToString
        If mciSendString(cmd, Nothing, 0, IntPtr.Zero) <> 0 Then
            Return
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cmd As String

        '再生する
        cmd = "play " + aliasName
        mciSendString(cmd, Nothing, 0, IntPtr.Zero)
    End Sub
End Class
