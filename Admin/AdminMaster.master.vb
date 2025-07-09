
Partial Class Admin_AdminMaster
    Inherits System.Web.UI.MasterPage
    Public BLL As New BusinessLogicLayer

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim pageurl As String = BLL.GetCurrentPageName()
        litpagename.Text = BLL.ExecScalar("select menuname from dynamicmenu where url=@url", "@url", pageurl)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If (Session("Menustr") Is Nothing Or Session("Menustr") = "") Then '  If (String.IsNullOrEmpty(Session("Menustr"))) And (Session("User") = "Admin" Or Session("User") = "SUser") Then
        '    Response.Redirect("/")
        'End If
        If Session("LoginId") IsNot Nothing Then
            If Not BLL.IsValidForPage() Then
                Response.Redirect("Home.aspx")
            End If
        Else
            Response.Redirect("../Login.aspx")
        End If


        'If Not IsPostBack Then
        '    If Session("Menu") Is Nothing Then
        '        Session("Menu") = BLL.CreateMenuNew()
        '    End If
        '    litmenu.Text = Session("Menu")
        'End If
        'enc.EncryptConnString()

    End Sub
End Class

