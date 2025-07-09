
Partial Class BusRoutePickupPoints
    Inherits BasePage
    Dim BLL As New BusinessLogicLayer
    Dim arraylist1 As New ArrayList()
    Dim arraylist2 As New ArrayList()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindClass()
        End If
    End Sub
    Sub BindClass()
        ddlRoute.DataSource = BLL.ExecDataTable("Select RouteId,RouteName from BusRouteMaster Where Deactivated is Null order by RouteName")
        ddlRoute.DataTextField = "RouteName"
        ddlRoute.DataValueField = "RouteId"
        ddlRoute.DataBind()
    End Sub
    Protected Sub ddlRoute_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRoute.SelectedIndexChanged
        ListBox1.DataSource = BLL.ExecDataTable("select * from tbl_PickupLocation order by Amount")
        ListBox1.DataTextField = "PickupPoint"
        ListBox1.DataValueField = "PickupPointId"
        ListBox1.DataBind()
        'Dim subjectid As String = BLL.ExecScalar("select subjectids from classmaster where classid=@classid", "@classid", ddlClass.SelectedValue)
        'subjectid = Left(subjectid, subjectid.ToString.Length - 1)

        ListBox2.DataSource = BLL.ExecDataTableProc("Get_BusRoutePickupPoints", "@RouteId", ddlRoute.SelectedValue)
        ListBox2.DataTextField = "PickupPoint"
        ListBox2.DataValueField = "PickupPointId"
        ListBox2.DataBind()

    End Sub
    Protected Sub btn1_Click(ByVal sender As Object, ByVal e As EventArgs)
        lbltxt.Visible = False

        If ListBox1.SelectedIndex >= 0 Then
            For i As Integer = 0 To ListBox1.Items.Count - 1
                If ListBox1.Items(i).Selected Then
                    If Not arraylist1.Contains(ListBox1.Items(i)) Then

                        arraylist1.Add(ListBox1.Items(i))
                    End If

                End If
            Next
            For i As Integer = 0 To arraylist1.Count - 1
                If Not ListBox2.Items.Contains(DirectCast(arraylist1(i), ListItem)) Then
                    ListBox2.Items.Add(DirectCast(arraylist1(i), ListItem))
                End If
                ListBox1.Items.Remove(DirectCast(arraylist1(i), ListItem))
            Next
            ListBox2.SelectedIndex = -1
        Else
            lbltxt.Visible = True
            lbltxt.Text = Notifications.ErrorMessage("Please Select atleast one in Pickup Point Listbox to move.")
        End If
    End Sub
    'Protected Sub btn2_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    lbltxt.Visible = False
    '    While ListBox1.Items.Count <> 0
    '        For i As Integer = 0 To ListBox1.Items.Count - 1
    '            ListBox2.Items.Add(ListBox1.Items(i))
    '            ListBox1.Items.Remove(ListBox1.Items(i))
    '        Next
    '    End While
    'End Sub
    Protected Sub btn3_Click(ByVal sender As Object, ByVal e As EventArgs)
        lbltxt.Visible = False
        If ListBox2.SelectedIndex >= 0 Then
            For i As Integer = 0 To ListBox2.Items.Count - 1
                If ListBox2.Items(i).Selected Then
                    If Not arraylist2.Contains(ListBox2.Items(i)) Then

                        arraylist2.Add(ListBox2.Items(i))
                    End If

                End If
            Next
            For i As Integer = 0 To arraylist2.Count - 1
                If Not ListBox1.Items.Contains(DirectCast(arraylist2(i), ListItem)) Then
                    ListBox1.Items.Add(DirectCast(arraylist2(i), ListItem))
                End If
                ListBox2.Items.Remove(DirectCast(arraylist2(i), ListItem))
            Next
            ListBox1.SelectedIndex = -1
        Else
            lbltxt.Visible = True
            lbltxt.Text = Notifications.ErrorMessage("Please Select atleast one in Pickup Point Listbox to move.")
        End If

    End Sub
    'Protected Sub btn4_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    lbltxt.Visible = False
    '    While ListBox2.Items.Count <> 0
    '        For i As Integer = 0 To ListBox2.Items.Count - 1
    '            ListBox1.Items.Add(ListBox2.Items(i))
    '            ListBox2.Items.Remove(ListBox2.Items(i))

    '        Next
    '    End While


    'End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        lbltxt.Visible = True
        Try
            Dim subjstr As String = ""
            If ListBox2.Items.Count > 0 Then
                For i As Integer = 0 To ListBox2.Items.Count - 1
                    subjstr = subjstr + ListBox2.Items(i).Value + ","
                Next
            End If
            BLL.ExecNonQueryProc("Prc_AddUpdateBusRoute", "@RouteId", ddlRoute.SelectedValue, "@RouteName", "", "@PickupPoints", subjstr)
            lbltxt.Text = Notifications.SuccessMessage("Pickup Points Submitted Successfully")
        Catch ex As Exception
            lbltxt.Text = Notifications.ErrorMessage("Please Try Later")
        End Try
    End Sub
End Class
