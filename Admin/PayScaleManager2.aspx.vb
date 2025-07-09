Imports System.Web.Services

Partial Class Admin_PayScaleManager2
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            bind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.ExecDataTable("Select * from vwPayScaleMaster order by PSID desc")
        btnExport.Visible = False
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                btnExport.Visible = True
            End If
        End If

        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

    <WebMethod> _
    Public Shared Function PayScaleManager(PayScaleName As String, DA As String, DAPer As Decimal, DAFix As Decimal, HRA As String, HRAPer As Decimal, HRAFix As Decimal, OtherAllowance As String, OAPer As Decimal, OAFix As Decimal, PF As String, PFPer As Decimal, PFDA As Decimal, PFFix As Decimal, TDS As String, TDSPer As Decimal, TDSFix As Decimal, OD As String, ODPer As Decimal, ODFix As Decimal, ESI As String, ESIPer As Decimal, ESIDA As Decimal, ESIFix As Decimal, PaidLeaves As Decimal) As String

        Try
            Dim Result As String = (New BusinessLogicLayer).AddUpdatePayScaleMaster(0, PayScaleName, DA, DAPer, DAFix, HRA, HRAPer, HRAFix, OtherAllowance, OAPer, OAFix, PF, PFPer, PFDA, PFFix, TDS, TDSPer, TDSFix, OD, ODPer, ODFix, ESI, ESIPer, ESIDA, ESIFix, PaidLeaves)
            Return "Error"
        Catch ac As Exception
            Return "Error"
        End Try
    End Function

End Class
