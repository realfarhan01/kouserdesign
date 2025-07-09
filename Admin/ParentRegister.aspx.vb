
Partial Class Admin_ParentRegister
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindLocality()
            BindState()
            If Not Request.QueryString("Pid") Is Nothing Then
                Dim dt As DataTable = BLL.GetParentList(Request.QueryString("Pid"), "")
                If dt.Rows.Count > 0 Then
                    txtFname.Text = dt.Rows(0)("fatherName")
                    txtMname.Text = dt.Rows(0)("mothername")
                    txtaddress.Text = dt.Rows(0)("Address")
                    txtContactno.Text = dt.Rows(0)("Contactno")
                    txtEmailid.Text = dt.Rows(0)("Emailid")
                    ddlState.SelectedValue = dt.Rows(0)("State")
                    BindCity(ddlState.SelectedValue)
                    ddlCity.SelectedValue = dt.Rows(0)("City")
                    ddlNationality.SelectedValue = dt.Rows(0)("Nationality")
                    ddlLocality.SelectedValue = dt.Rows(0)("LocalityId")
                    ddlactive.SelectedValue = dt.Rows(0)("Isblock")
                    hfId.Value = Request.QueryString("Pid")
                Else
                    litmsg.Text = Notifications.ErrorMessage("No Data Found")
                End If
            End If

        End If
    End Sub

    Sub BindLocality()
        ddlLocality.DataSource = BLL.BindLocality()

        ddlLocality.DataTextField = "Locality"
        ddlLocality.DataValueField = "LocalityId"
        ddlLocality.DataBind()
    End Sub
    Sub BindState()
        ddlState.DataSource = BLL.BindState()

        ddlState.DataTextField = "State"
        ddlState.DataValueField = "State"
        ddlState.DataBind()
    End Sub
    Sub BindCity(ByVal State As String)
        ddlCity.DataSource = BLL.BindCity(State)

        ddlCity.DataTextField = "City"
        ddlCity.DataValueField = "City"
        ddlCity.DataBind()
    End Sub

    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlState.SelectedIndexChanged
        BindCity(ddlState.SelectedValue)
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim res As String = ""
        If hfId.Value = "" Then
            res = BLL.AddParents(hfId.Value, txtFname.Text, txtMname.Text, ddlNationality.SelectedValue, "", txtaddress.Text, ddlCity.SelectedValue, ddlState.Text, ddlLocality.SelectedValue, txtContactno.Text, txtEmailid.Text, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                Dim id As String = res.Split(":")(1)
                Response.Redirect("StudentRegistration.aspx?Pid=" & id)


            End If
        Else
            res = BLL.AddParents(hfId.Value, txtFname.Text, txtMname.Text, ddlNationality.SelectedValue, "", txtaddress.Text, ddlCity.SelectedValue, ddlState.Text, ddlLocality.SelectedValue, txtContactno.Text, txtEmailid.Text, ddlactive.SelectedValue)
            If res.Chars(0) = "#" Then
                litmsg.Text = Notifications.SuccessMessage(res)
                hfId.Value = ""
            End If

        End If
       
    End Sub
End Class
