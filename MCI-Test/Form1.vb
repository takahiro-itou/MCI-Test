﻿Imports System.Runtime.CompilerServices

Public Class Form1

    <System.Runtime.InteropServices.DllImport("winmm.dll",
    CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function mciSendString(ByVal command As String,
    ByVal buffer As System.Text.StringBuilder,
    ByVal bufferSize As Integer, ByVal hwndCallback As IntPtr) As Integer
    End Function

    <System.Runtime.InteropServices.DllImport("winmm.dll",
    CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Shared Function mciGetErrorString(ByVal fdwError As Integer, ByVal lpszErrorText As System.Text.StringBuilder, ByVal cchErrorText As Integer) As Integer

    End Function

    Private aliasName As String = "MediaFile"

    Private Function getMciError(ByVal fdwError As Integer) As String
        Dim resText As System.Text.StringBuilder
        Dim retErr As String

        resText = New System.Text.StringBuilder(512)
        mciGetErrorString(fdwError, resText, resText.Capacity)
        retErr = resText.ToString()
        Return retErr
    End Function


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '再生するファイル名
        Dim curDir = Application.StartupPath
        Dim fileName = "Test.wmv"

        Dim cmd As String
        Dim result As Integer
        Dim errMsg As String
        Dim cs = PictureBox1.ClientSize

        'ファイルを開く
        cmd = "open """ + fileName + """ alias " + aliasName
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If

        cmd = "set " & aliasName & " time format milliseconds"
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If

        cmd = "window " & aliasName & " handle " & PictureBox1.Handle.ToString
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If

        cmd = String.Format("put {2} destination at 0 0 {0} {1}", cs.Width, cs.Height, aliasName)
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cmd As String
        Dim result As Integer
        Dim errMsg As String

        '再生する
        cmd = "play " + aliasName
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cmd As String
        Dim result As Integer
        Dim errMsg As String

        cmd = "stop " & aliasName
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim cmd As String
        Dim result As Integer
        Dim errMsg As String

        cmd = "resume " & aliasName
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If
    End Sub

    Public Function getValue(ByVal resStr As String) As Long
        If Not IsNumeric(resStr) Then resStr = "0"

        If Long.Parse(resStr) < 0 Then
            Return ((Long.Parse(resStr) And &H7FFF) + 2 ^ 15)
        Else
            Return Long.Parse(resStr)
        End If
    End Function

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' 現在位置と長さ
        Dim ret As Integer
        Dim cmd As String
        Dim resText As System.Text.StringBuilder
        Dim filePos As Long, fileLen As Long
        Dim result As Integer
        Dim errMsg As String

        resText = New System.Text.StringBuilder(512)
        cmd = "status " & aliasName & " position"
        ret = mciSendString(cmd, resText, resText.Capacity, IntPtr.Zero)
        If ret = 0 Then
            filePos = getValue(resText.ToString())
        End If

        resText = New System.Text.StringBuilder(512)
        cmd = "status " & aliasName & " length"
        ret = mciSendString(cmd, resText, resText.Capacity, IntPtr.Zero)
        If ret = 0 Then
            fileLen = getValue(resText.ToString())
        End If

        Label1.Text = String.Format("{0:#,##0} / {1:#,##0}", filePos, fileLen)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' シーク
        Dim cmd As String
        Dim result As Integer
        Dim errMsg As String

        cmd = "seek " & aliasName & " to 90000"
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim cmd As String
        Dim result As Integer
        Dim errMsg As String

        cmd = "pause " & aliasName
        result = mciSendString(cmd, Nothing, 0, IntPtr.Zero)
        If result <> 0 Then
            errMsg = getMciError(result)
            MessageBox.Show(errMsg)
            Return
        End If
    End Sub
End Class
