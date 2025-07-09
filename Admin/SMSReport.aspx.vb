
Partial Class Admin_SMSReport
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("Select SM.StudentName,Sm.FatherName,(Select top 1 ClassName From ClassMaster Where MainClassID=SM.MainClassID)ClassName,SM.PArentID,SMS.Mobile,SMS.Text_Message,SMS.Timing  From dbo.tbl_Sms SMS inner join StudentMaster SM on SMS.Emp_Parent_ID=SM.PArentID")
        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
       
    End Sub
End Class
