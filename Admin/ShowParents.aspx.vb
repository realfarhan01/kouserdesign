﻿
Partial Class Admin_ShowParents
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub
    Sub bind()
        DataDisplay.DataSource = BLL.GetParentList(txtParentId.Text, txtStudentid.Text)
        DataDisplay.DataBind()
    End Sub
End Class
