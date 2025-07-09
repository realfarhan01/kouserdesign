Partial Class CreateUser
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Dim enc As New Encryption64
    Protected Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim cv As New CustomValidator
        If Page.IsValid Then
            If Not validation.isID(txtUserid.Text) Then
                cv.IsValid = False
                cv.ErrorMessage = "<ul>invalid User Id Format</ul>"
                cv.ValidationGroup = "UserRegistration"
                Page.Validators.Add(cv)
                Exit Sub
            End If
            If BLL.isOperator(txtUserid.Text) And hfId.Value Is Nothing Then
                cv.IsValid = False
                cv.ErrorMessage = "<ul>Login Id already exists</ul>"
                cv.ValidationGroup = "UserRegistration"
                Page.Validators.Add(cv)
                Exit Sub
            End If
            Dim mStr As String
            mStr = BLL.CreateOperator(txtName.Text, txtUserid.Text, txtemail.Text, txtAddress.Text, txtPwd.Text, txtMobile.Text)
            If mStr.Chars(0) = "!" Then
            Else
                Session("uid") = mStr
                Response.Redirect("Userrights.aspx")
            End If
        End If


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dr As SqlDataReader
            Dim Loginid As String = ""
            If Not Request.QueryString("ID") Is Nothing Then
                Loginid = HttpUtility.HtmlDecode(enc.Decrypt(Request.QueryString("ID")))
            End If

            dr = BLL.ExecDataReader("select SNo, UserName, LoginId, Password, UserType, Email, Address,Mobile from adminlogins where Loginid=@LoginId", "@LoginId", Loginid)
            While dr.Read
                hfId.Value = Loginid
                txtUserid.Text = dr("Loginid")
                txtUserid.ReadOnly = True
                txtemail.Text = dr("email")
                txtAddress.Text = dr("address")
                txtName.Text = dr("UserName")
                txtPwd.Attributes.Add("value", IIf(dr("password").ToString() Is DBNull.Value, String.Empty, dr("password").ToString()))
                txtMobile.Text = dr("Mobile")
            End While

        End If
    End Sub
End Class
