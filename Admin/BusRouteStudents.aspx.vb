Imports System.Web.Services

Partial Class Admin_BusRouteStudents
    Inherits System.Web.UI.Page
    Dim BLL As New BusinessLogicLayer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'bind()
            ltrSchool.Text = BLL.BindSchoolHeader()
            ddlClass.DataSource = BLL.BindMainClass()
            ddlClass.DataTextField = "MainClassName"
            ddlClass.DataValueField = "MainClassId"
            ddlClass.DataBind()

            ddlRoute.DataSource = BLL.BindBusRouteMaster()
            ddlRoute.DataTextField = "RouteName"
            ddlRoute.DataValueField = "RouteId"
            ddlRoute.DataBind()

            ddlPickupPoints.DataSource = BLL.BindBusRoutePickupPoints(0)
            ddlPickupPoints.DataTextField = "PickupPoint"
            ddlPickupPoints.DataValueField = "PickupPointId"
            ddlPickupPoints.DataBind()
        End If
    End Sub
    Sub bind()
        Dim dt As DataTable = BLL.GetConvenceStudents(txtStudentid.Text, txtstuname.Text, ddlClass.SelectedValue, ddlSection.SelectedValue, ddlRoute.SelectedValue, ddlPickupPoints.SelectedValue)
        reportheader.Visible = False
        If dt IsNot Nothing Then
            If dt.Rows.Count > 0 Then
                reportheader.Visible = True
            End If
        End If

        DataDisplay.DataSource = dt
        DataDisplay.DataBind()
    End Sub

    Protected Sub ddlRoute_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoute.SelectedIndexChanged
        ddlPickupPoints.Items.Clear()
        ddlPickupPoints.Items.Add(New ListItem("All Pickup Points", "0"))
        ddlPickupPoints.DataSource = BLL.BindBusRoutePickupPoints(0)
        ddlPickupPoints.DataTextField = "PickupPoint"
        ddlPickupPoints.DataValueField = "PickupPointId"
        ddlPickupPoints.DataBind()
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        bind()
    End Sub
    Protected Sub btnexportpage_Click(sender As Object, e As EventArgs) Handles btnexportpage.Click
        Response.Redirect("BusRouteStudentsExport.aspx?sid=" & txtStudentid.Text & "&name=" & txtstuname.Text & "&class=" & ddlClass.SelectedValue & "&section=" & ddlSection.SelectedValue & "&pid=" & ddlPickupPoints.SelectedValue & "&rid=" & ddlRoute.SelectedValue)
    End Sub
End Class
