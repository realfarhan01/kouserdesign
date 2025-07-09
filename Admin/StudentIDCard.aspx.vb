Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Partial Class Admin_StudentIDCard
    Inherits System.Web.UI.Page

    Dim BLL As New BusinessLogicLayer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindStudentData()
            ltrSchool.Text = BLL.BindSchoolHeader()

        End If
        If Not Request.QueryString("CID") Is Nothing Then
            HiddenField1.Value = Request.QueryString("CID").ToString()
            btnUploadPhoto.Visible = True
            bind(0)
        End If

    End Sub

    Sub BindStudentData()
        ddlsearch.DataSource = BLL.ExecDataTableProc("Prc_StudentData")
        ddlsearch.DataTextField = "StudentData"
        ddlsearch.DataValueField = "StudentId"
        ddlsearch.DataBind()
    End Sub


    Protected Sub ddlsearch_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlsearch.SelectedIndexChanged
        HiddenField1.Value = ddlsearch.SelectedValue
        btnUploadPhoto.Visible = True
        pnlStudentdetails.Visible = True
        bind(0)
    End Sub

    Sub bind(ByVal Type As Integer)
        Dim dt As DataTable = BLL.GetStudentList(HiddenField1.Value, "", 0, "", "", Type, "", "", "", "")
        If dt.Rows.Count > 0 Then
            lblName.Text = dt.Rows(0)("StudentName")
            lblFName.Text = dt.Rows(0)("FatherName")
            lblMName.Text = dt.Rows(0)("MotherName")
            lblDOB.Text = dt.Rows(0)("DOB")
            lblgender.Text = dt.Rows(0)("Gender")
            lblAddress.Text = dt.Rows(0)("Address")
            lblPhone.Text = dt.Rows(0)("ContactNo")
            lblBlood.Text = dt.Rows(0)("Bloodgroup")
            lblAdm.Text = dt.Rows(0)("StudentId")
            lblSec.Text = dt.Rows(0)("Section")
            lblClass.Text = dt.Rows(0)("ClassName")
            If dt.Rows(0)("StudentPic").ToString() <> "" Then

                ' you can specify app-root-relative URLs ("~/...") here
                imgStudent.ImageUrl = dt.Rows(0)("StudentPic")
            Else

                imgStudent.ImageUrl = "~/upload/Studentphoto/student_nopic.png"
            End If

            lblFormNo.Text = dt.Rows(0)("RegFormNo")
            lblRegClass.Text = dt.Rows(0)("ClassName")
            lblRegistrationNo.Text = dt.Rows(0)("StudentId")
            lblAdmdate.Text = dt.Rows(0)("RegDate")
            lblSection.Text = dt.Rows(0)("Section")
            lblrollno.Text = dt.Rows(0)("RollNo")

            lblStuName.Text = dt.Rows(0)("StudentName")
            lblGen.Text = dt.Rows(0)("Gender")
            lblDOBS.Text = dt.Rows(0)("DOB")
            lblPOB.Text = dt.Rows(0)("PlaceofBirth")
            lblTongue.Text = dt.Rows(0)("MotherTongue")
            lblSecLang.Text = dt.Rows(0)("SecondLanguage")
            lblBGroop.Text = dt.Rows(0)("BloodGroup")


            lblfaname.Text = dt.Rows(0)("FatherFirstName") & " " & dt.Rows(0)("FatherLastName")
            lblOccup.Text = dt.Rows(0)("FatherOccupation")
            lblDesignation.Text = dt.Rows(0)("FatherDesignation")
            lblQuali.Text = dt.Rows(0)("FatherQualification")
            lblInCome.Text = dt.Rows(0)("FatherIncome")
            lblMobile.Text = dt.Rows(0)("ContactNo")


            lblMoName.Text = dt.Rows(0)("MotherFirstName") & " " & dt.Rows(0)("MotherLastName")
            lblmOccup.Text = dt.Rows(0)("MotherOccupation")
            lblMdesignation.Text = dt.Rows(0)("MotherDesignation")
            lblMoQuali.Text = dt.Rows(0)("MotherQualification")
            lblMoIncome.Text = dt.Rows(0)("MotherIncome")
            lblMoMobile.Text = dt.Rows(0)("ContactNo")





            lblAddres.Text = dt.Rows(0)("Address")
            lblState.Text = dt.Rows(0)("State")
            lblCity.Text = dt.Rows(0)("City")
            lblLocalty.Text = dt.Rows(0)("Locality")
            lblPin.Text = dt.Rows(0)("PinCode")
            lblEmail.Text = dt.Rows(0)("EMailId")
            lblAddressmobile.Text = dt.Rows(0)("ContactNo")

            lblAddressmobile.Text = dt.Rows(0)("ContactNo")

            lblSchoolName.Text = dt.Rows(0)("LastSchool")
            lblPClass.Text = dt.Rows(0)("LastClassId")
            lblPSession.Text = dt.Rows(0)("LastSession")
            lblPercent.Text = dt.Rows(0)("LastPercentage")
            lblCGPA.Text = dt.Rows(0)("LastCGPA")


            lblBoard.Text = dt.Rows(0)("LastBoard")
            lblResult.Text = dt.Rows(0)("LastResult")
            lblLeavingschool.Text = dt.Rows(0)("LastLeavingReason")


            lblGDetails.Text = dt.Rows(0)("GFirstName") & " " & dt.Rows(0)("GLastName")
            lblGoccup.Text = dt.Rows(0)("GOccupation")
            lblGDesig.Text = dt.Rows(0)("GDesignation")
            lblGQuali.Text = dt.Rows(0)("GQualification")
            lblGIncome.Text = dt.Rows(0)("GIncome")
            lblGAddress.Text = dt.Rows(0)("GAddress")
            lblGState.Text = dt.Rows(0)("GState")
            lblDCity.Text = dt.Rows(0)("GCity")
            lblPinCode.Text = dt.Rows(0)("GPinCode")
            lblGEmail.Text = dt.Rows(0)("GEmail")
            lblGMob.Text = dt.Rows(0)("GMobile")

            lblOtherFacility.Text = dt.Rows(0)("isTransport")
            lblOtherReligion.Text = dt.Rows(0)("Religion")
            lblOtherRate.Text = dt.Rows(0)("RTE")
            lblOthAdm.Text = dt.Rows(0)("isAdmissionFeesFree")
            lblOthTution.Text = dt.Rows(0)("isTuitionFeesFree")
            lblCategory.Text = dt.Rows(0)("Category")
            lblhouse.Text = dt.Rows(0)("House")
           
            tblCard.Visible = True
            pnlStudentdetails.Visible = True


        Else
            tblCard.Visible = False
            pnlStudentdetails.Visible = False
        End If




    End Sub

    Protected Sub btnUploadPhoto_Click(sender As Object, e As EventArgs)
        Dim Images As String = ""
      
            If FileUpload1.HasFile Then

                Dim filename As String = Path.GetFileName(FileUpload1.FileName)
                ' Dim fname = Server.MapPath("~/upload/") & filename
            FileUpload1.SaveAs(Server.MapPath("~/upload/Studentphoto/") & filename)
                'ResizeImageAndSave(121, 200, fname, "~/Files/Thumbnail/" + filename)

            End If
        Images = "~/upload/Studentphoto/" + FileUpload1.FileName.ToString()
            BLL.ExecNonQuery("update studentmaster set studentpic=@studentpic where studentid =@studentid", "@studentpic", Images, "@studentid", HiddenField1.Value)
            imgStudent.ImageUrl = Images

    End Sub






    'Public Function ResizeImageAndSave(Width As Integer, Height As Integer, imageUrl As String, destPath As String) As String

    '    Dim fullsizeImage As System.Drawing.Image = System.Drawing.Image.FromFile(imageUrl)
    '    Dim newWidth As Integer = Width
    '    Dim maxHeight As Integer = Height

    '    ' Prevent using images internal thumbnail
    '    fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone)
    '    fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone)

    '    If fullsizeImage.Width <= newWidth Then
    '        newWidth = fullsizeImage.Width
    '    End If

    '    Dim newHeight As Integer = fullsizeImage.Height * newWidth / fullsizeImage.Width
    '    If newHeight > maxHeight Then
    '        ' Resize with height instead
    '        newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height
    '        newHeight = maxHeight
    '    End If

    '    Dim newImage As System.Drawing.Image = fullsizeImage.GetThumbnailImage(newWidth, newHeight, Nothing, IntPtr.Zero)
    '    newImage.Save(HttpContext.Current.Server.MapPath(destPath), ImageFormat.Jpeg)
    '    fullsizeImage.Dispose()
    '    Return ""
    'End Function

    'Public Function ThumbnailCallback() As Boolean
    '    Return False
    'End Function

    'Public ReadOnly Property IsReusable() As Boolean
    '    Get
    '        Return False
    '    End Get
    'End Property

End Class
