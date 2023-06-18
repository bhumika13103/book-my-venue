﻿Imports System.Data.OleDb
Public Class childCHANGEPASSWORDcustomer
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Dim id As Integer
    Private Sub BtnCANCEL_Click(sender As Object, e As EventArgs) Handles BtnCANCEL.Click
        Me.Close()
    End Sub

    Private Sub BtnRESET_Click(sender As Object, e As EventArgs) Handles BtnRESET.Click
        TxtOLDPWD.Text = Nothing
        TxtNEWPWD.Text = Nothing
        TxtRENEWPWD.Text = Nothing
    End Sub
    Private Sub hideshow_pwd(txtbox As TextBox, btn As Button)
        If txtbox.UseSystemPasswordChar = True Then
            txtbox.UseSystemPasswordChar = False
            btn.Image = Image.FromFile("C:\Users\HP\OneDrive\Documents\Visual Studio 2015\Projects\BookMyVenue\png\show.png")
        Else
            txtbox.UseSystemPasswordChar = True
            btn.Image = Image.FromFile("C:\Users\HP\OneDrive\Documents\Visual Studio 2015\Projects\BookMyVenue\png\hide.png")
        End If
    End Sub

    Private Sub BtnOLDPWD_Click(sender As Object, e As EventArgs) Handles BtnOLDPWD.Click
        hideshow_pwd(TxtOLDPWD, BtnOLDPWD)
    End Sub

    Private Sub BtnNEWPWD_Click(sender As Object, e As EventArgs) Handles BtnNEWPWD.Click
        hideshow_pwd(TxtNEWPWD, BtnNEWPWD)
    End Sub

    Private Sub BtnRENEWPWD_Click(sender As Object, e As EventArgs) Handles BtnRENEWPWD.Click
        hideshow_pwd(TxtRENEWPWD, BtnRENEWPWD)
    End Sub

    Private Sub BtnCONFIRM_Click(sender As Object, e As EventArgs) Handles BtnCONFIRM.Click
        Dim sql1 As String
        Dim pass As String
        If String.Equals(TxtNEWPWD.Text, TxtRENEWPWD.Text) Then
            id = CUSTOMERdashboard.custid
            sql1 = "SELECT PASSWORD FROM CustomerDETAILS WHERE password = '" & TxtOLDPWD.Text & "' AND CUSTOMER_ID = " & id
            cmd = New OleDbCommand(sql1, dbcon)
            dr = cmd.ExecuteReader()
            If dr.Read() = True Then
                If String.Equals(TxtOLDPWD.Text, TxtNEWPWD.Text) Then
                    MessageBox.Show("Old and New password cannot be same", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                Else
                    Dim sql2 As String
                    Dim rowsaffected As Integer
                    sql2 = "UPDATE CustomerDETAILS SET [PASSWORD] = '" & TxtNEWPWD.Text & "'" & " WHERE [CUSTOMER_ID] = " & id
                    cmd = New OleDbCommand(sql2, dbcon)

                    rowsaffected = cmd.ExecuteNonQuery()
                    If rowsaffected = 1 Then
                        MessageBox.Show("Password Changed Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                        TxtOLDPWD.Text = Nothing
                        TxtNEWPWD.Text = Nothing
                        TxtRENEWPWD.Text = Nothing
                    End If
                End If
            Else
                MessageBox.Show("Wrong old password entered", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("New passswords doesn't match", "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
        End If
        Me.Close()
        pDisconnectDB()
    End Sub

    Private Sub childCHANGEPASSWORDcustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pConnectDB()
    End Sub
End Class