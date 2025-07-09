
Partial Class CompanyMaster
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            BindState()


            Dim dr As SqlDataReader = BLL.ExecDataReader("select * from dbo.SchoolMaster")
            If dr.Read() Then
                txtname.Text = dr("SchoolName")
                txtaddress.Text = dr("Address")
                txtContactno.Text = dr("Contactno")
                txtEmailid.Text = dr("Emailid")
                ddlState.SelectedValue = dr("State").ToString()
                BindCity(dr("State").ToString())
                ddlCity.SelectedValue = dr("City").ToString()
                txtdomain.Text = dr("DomainName")
                txtfax.Text = dr("FaxNo")
                txtprefix.Text = dr("Prefix")
                txtfbpage.Text = dr("FBPage")

            Else
                litmsg.Text = Notifications.ErrorMessage("No Data Found")
            End If
        End If

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

    Protected Sub ddlState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlState.SelectedIndexChanged
        BindCity(ddlState.SelectedValue)
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click


        BLL.AddSchool(txtname.Text, txtaddress.Text, ddlCity.SelectedValue, ddlState.SelectedValue, txtContactno.Text, txtEmailid.Text, txtdomain.Text, txtprefix.Text, txtfax.Text, txtfbpage.Text)
        litmsg.Text = Notifications.SuccessMessage("Updated Successsfully.")



    End Sub
End Class
