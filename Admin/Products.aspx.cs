using System;
using System.Web;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

public partial class Admin_Products :  System.Web.UI.Page
{
    public class ResultSet
    {
        public Boolean Success { get; set; }
        public Int32 Total { get; set; }
        public Object Result { get; set; }
    }
    BusinessLogicLayer bll = new BusinessLogicLayer();
    #region PAGE FUNCTIONS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindProducts();

        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResultSet res = new ResultSet();
            try
            {
                res.Result = bll.GetCategories(3, 0);
                res.Success = true;
                res.Total = 1;
            }
            catch (Exception ex)
            {
                res.Total = 0;
                res.Success = false;
            }
            DataTable CategoryListing = (DataTable)res.Result;
            ddlCat.DataSource = CategoryListing;
            ddlCat.DataTextField = "CategoryName";
            ddlCat.DataValueField = "CategoryCode";
            ddlCat.DataBind();
          
            HttpContext.Current.Session["CategoryListing"] = CategoryListing;
            try
            {
                res.Result = bll.GetColors(0, "", "", "GetAllColors");
                res.Success = true;
                res.Total = 1;
            }
            catch (Exception ex)
            {
                res.Total = 0;
                res.Success = false;
            }
            DataTable ColorListing = (DataTable)res.Result;
            HttpContext.Current.Session["ColorListing"] = ColorListing;

            DataTable brandListing = bll.GetBrands(0, "", "", "", "select");
            HttpContext.Current.Session["BrandListing"] = brandListing;
        }
    }
    #endregion

    #region Bind Products List
    protected void BindProducts()
    {
        ddlpage.Items.Insert(0, "1");
        DataTable dt = bll.GetProductsListing(txtproductcode.Text, ddlCat.SelectedValue, Convert.ToInt16(ddlpage.SelectedValue) , 100);
        rptData.DataSource = dt;
        rptData.DataBind();
        if (Convert.ToInt16(ddlpage.SelectedValue) == 1)
        {
            int TotalPage = bll.GetProductsListingPagesCount(txtproductcode.Text, ddlCat.SelectedValue, Convert.ToInt16(ddlpage.SelectedValue), 100);
            ddlpage.Items.Clear();
            int cnt = 0;
            while(cnt<TotalPage)
            {
                ddlpage.Items.Insert( cnt, (cnt+1).ToString());
                cnt = cnt + 1;
            }
        }
    }
    #endregion

    //--------------ADD PRODUCT-------------------------------------------------------------
    #region ADD PRODUCTS
    [WebMethod()]
    public static string AddCatalogue(int CategoryId, string CatalogueTitle, string CatalogueDescription, string VideoURL, string MoreDetailURL, string ThumbnailURL, string CatalogueURL, string CatalogueSizeMB, string CatalogueTags, int BrandId, string ProductSize, string Color, string Price, string Weight, string SKUCode)
    {
        try
        {
            if (Price == "")
            {
                Price = "0";
            }
            try
            {
                if (HttpContext.Current.Session["MultiFiles"] != null)
                {
                    DataTable dt = (DataTable)HttpContext.Current.Session["MultiFiles"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ThumbnailURL = ThumbnailURL + dt.Rows[i]["ImageSize"] + "|" + dt.Rows[i]["ImageId"] + "|" + dt.Rows[i]["IsDefault"] + ",";
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            BusinessLogicLayer bll = new BusinessLogicLayer();
            ResultSet res = new ResultSet();

            if (HttpContext.Current.Session["Loginid"] != null)
            {
                var ReturnPostResponse = "";
                try
                {
                    res.Result = bll.AddEditProducts(0, Convert.ToString(HttpContext.Current.Session["CatalogueURLcateCode"]), 1, CategoryId, CatalogueTitle, CatalogueDescription, ThumbnailURL, "Files/Catalogue/" + HttpContext.Current.Session["CatalogueURL"] + CatalogueURL, Convert.ToDecimal(CatalogueSizeMB), CatalogueTags, VideoURL, MoreDetailURL, HttpContext.Current.Session["Loginid"].ToString(), BrandId, ProductSize, Color, Convert.ToDecimal(Price), Weight,SKUCode);
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                ReturnPostResponse = JsonConvert.SerializeObject(res);

                HttpContext.Current.Session["CatalogueURLcateCode"] = null;
                HttpContext.Current.Session["MultiFiles"] = null;
                HttpContext.Current.Session["extensionThumbnailURL"] = CatalogueURL; //ThumbnailURL;
                HttpContext.Current.Session["tagsInputAdd"] = CatalogueTags;

                JObject obj1 = JObject.Parse(ReturnPostResponse.ToString());
                bool Success = Convert.ToBoolean(obj1["Result"][0]["Success"]);
                int CatalogueId = Convert.ToInt32(obj1["Result"][0]["CatalogueId"].ToString());
                HttpContext.Current.Session["CatalogueId"] = CatalogueId;
                string CatCode = obj1["Result"][0]["CatCode"].ToString();

                return ReturnPostResponse;
            }
            else
            {
                HttpContext.Current.Session["CatalogueURLcateCode"] = null;
                return "SessionOut";
            }
        }
        catch (Exception ac)
        {
            return "Error";
        }

    }
    [WebMethod()]
    public static string UploadCatalogue(string CatalogueURL, decimal CatalogueSizeMB, string extension)
    {
        try
        {
            if (HttpContext.Current.Session["UserSNo"] != null)
            {
                var ReturnPostResponse = "";
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ResultSet res = new ResultSet();
                try
                {
                    res.Result = bll.UploadProduct(0, Convert.ToInt32(HttpContext.Current.Session["UserSNo"]), CatalogueURL, CatalogueSizeMB, HttpContext.Current.Session["Loginid"].ToString());
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                ReturnPostResponse = JsonConvert.SerializeObject(res);

                JObject obj1 = JObject.Parse(ReturnPostResponse.ToString());
                var CatalogueURL1 = obj1["Result"][0]["CatalogueURL"].ToString();
                //object dec1 = JsonConvert.DeserializeObject(ReturnPostResponse);
                //if (HttpContext.Current.Session["CatalogueURL"] == null && HttpContext.Current.Session["CatalogueURLcateCode"] == null)
                //{

                HttpContext.Current.Session["CatalogueURL"] = obj1["Result"][0]["CatalogueURL"].ToString();
                HttpContext.Current.Session["CatalogueURLcateCode"] = obj1["Result"][0]["CatalogueURL"].ToString();

                //}
                HttpContext.Current.Session["extension"] = extension.ToString();
                return ReturnPostResponse;
            }
            else
            {
                return "SessionOut";
            }

        }
        catch (Exception ac)
        {
            return "Error";
        }
    }
    [WebMethod()]
    public static string UploadThumbnail(string CatalogueURL, decimal CatalogueSizeMB)
    {
        try
        {

            if (HttpContext.Current.Session["MemberSNo"] != null)
            {

                var ReturnPostResponse = "";
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ResultSet res = new ResultSet();
                try
                {
                    res.Result = bll.UploadProduct(0, Convert.ToInt32(HttpContext.Current.Session["UserSNo"]), CatalogueURL, CatalogueSizeMB, HttpContext.Current.Session["Loginid"].ToString());
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                ReturnPostResponse = JsonConvert.SerializeObject(res);

                JObject obj1 = JObject.Parse(ReturnPostResponse.ToString());
                var CatalogueURL1 = obj1["Result"][0]["CatalogueURL"].ToString();

                if (HttpContext.Current.Session["CatalogueURL"] == null && HttpContext.Current.Session["CatalogueURLcateCode"] == null)
                {

                    HttpContext.Current.Session["CatalogueURL"] = obj1["Result"][0]["CatalogueURL"].ToString();
                    HttpContext.Current.Session["CatalogueURLcateCode"] = obj1["Result"][0]["CatalogueURL"].ToString();

                }

                return ReturnPostResponse;
            }
            else
            {
                return "SessionOut";
            }

        }
        catch (Exception ac)
        {
            return "Error";
        }
    }
    [WebMethod]
    public static string clearSessionData()
    {

        try
        {
            HttpContext.Current.Session["CatalogueURLcateCode"] = "";
            HttpContext.Current.Session["CatalogueURL"] = "";

            return "";

        }
        catch (Exception ex)
        {

            return ex.Message.ToString();
        }

    }
    #endregion

    #region DELETE PRODUCT
    [WebMethod()]
    public static string DeleteCatalogue(int CatalogueId)
    {
        try
        {
            if (HttpContext.Current.Session["LoginId"] != null)
            {
                var ReturnPostResponse = "";
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ResultSet res = new ResultSet();
                try
                {
                    res.Result = bll.MemberProductDelete(1, CatalogueId);
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                ReturnPostResponse = JsonConvert.SerializeObject(res);

                return ReturnPostResponse;
            }
            else
            {
                return "SessionOut";
            }

        }
        catch (Exception ac)
        {
            return "Error";
        }
    }

    #endregion

    #region DELETE THUMBNAIL
    [WebMethod()]
    public static string DeleteMoreThumbnail(int CatalogueImageId)
    {
        try
        {
            if (HttpContext.Current.Session["LoginId"] != null)
            {
                var ReturnPostResponse = "";
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ResultSet res = new ResultSet();
                try
                {
                    res.Result = bll.DeleteCatalogueImagesThumbnail("CatalogueImages", CatalogueImageId, 2, "", HttpContext.Current.Session["LoginId"].ToString());
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                ReturnPostResponse = JsonConvert.SerializeObject(res);

                return ReturnPostResponse;
            }
            else
            {
                return "SessionOut";
            }
        }
        catch (Exception ac)
        {
            return "Error";
        }
    }

    [WebMethod]
    public static string DeleteThumbnailFromDataTable(string ImageSNo)
    {
        try
        {
            DataTable dtFiles = (DataTable)HttpContext.Current.Session["MultiFiles"];
            DataRow[] dtr = dtFiles.Select("SNo=" + ImageSNo);
            foreach (var drow in dtr)
            {
                drow.Delete();
            }
            dtFiles.AcceptChanges();
            HttpContext.Current.Session["MultiFiles"] = dtFiles;
            return "success";
        }
        catch (Exception ex)
        {
            return "failed";
        }
    }


    [WebMethod]
    public static string SetDefaultThumbnailAddCat(string ImageSNo)
    {
        try
        {
            DataTable dtFiles = (DataTable)HttpContext.Current.Session["MultiFiles"];
            DataRow[] dtPreDefault = dtFiles.Select("IsDefault=1");
            foreach (var drow in dtPreDefault)
            {
                drow["IsDefault"] = 0;

            }
            DataRow[] dtr = dtFiles.Select("SNo=" + ImageSNo);
            foreach (var drow in dtr)
            {
                drow["IsDefault"] = 1;
            }
            dtFiles.AcceptChanges();
            HttpContext.Current.Session["MultiFiles"] = dtFiles;
            return "success";
        }
        catch (Exception ex)
        {
            return "failed";
        }
    }

    #endregion

    #region Set Default Image

    [WebMethod()]
    public static string SetCatalogueDefaultImage(int CatalogueId, int CatalogueImageId)
    {
        try
        {
            var ReturnPostResponse = "";
            BusinessLogicLayer bll = new BusinessLogicLayer();
            ResultSet res = new ResultSet();
            try
            {
                res.Result = bll.SetProductDefaultImage(CatalogueId, 1, CatalogueImageId);
                res.Success = true;
                res.Total = 1;
            }
            catch (Exception ex)
            {
                res.Total = 0;
                res.Success = false;
            }
            ReturnPostResponse = JsonConvert.SerializeObject(res);
            return ReturnPostResponse;

        }
        catch (Exception ac)
        {
            return "Error";
        }
    }

    #endregion

    #region EDIT PRODUCT
    [WebMethod()]
    public static string CheckAddUpdateCataloguePermission()
    {
        HttpContext.Current.Session["MultiFiles"] = null;
        if (HttpContext.Current.Session["LoginId"] != null)
        {
            return "True";
        }
        else
        {
            return "SessionOut";
        }
    }
    [WebMethod()]
    public static string GetMoreThumbnail(int CatalogueId)
    {
        try
        {
            if (HttpContext.Current.Session["LoginId"] != null)
            {
                var GetRequestResponse = "";
                BusinessLogicLayer bll = new BusinessLogicLayer();
                ResultSet res = new ResultSet();
                try
                {
                    res.Result = bll.GetProductImages(CatalogueId, 3);
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                GetRequestResponse = JsonConvert.SerializeObject(res);

                return GetRequestResponse;

            }
            else
            {
                return "SessionOut";
            }
        }
        catch (Exception ac)
        {
            return "Error";
        }
    }

    [WebMethod()]
    public static string GetIPAddress()
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
                                             // Get the IP
                                             //string myIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
        return myIP;
    }

    [WebMethod]
    public static string GetUploadedThumbnailMultiple()
    {
        string ThumbnailURL = "";
        try
        {
            if (HttpContext.Current.Session["MultiFiles"] != null)
            {
                DataTable dt = (DataTable)HttpContext.Current.Session["MultiFiles"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ThumbnailURL = ThumbnailURL + dt.Rows[i]["ImageSize"] + "|" + dt.Rows[i]["ImageId"] + "|" + dt.Rows[i]["IsDefault"] + ",";
                }
            }

            return ThumbnailURL;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
    [WebMethod()]
    public static string EditUploadCatalogue(string CatalogueURL, string extension)
    {
        try
        {
            if (HttpContext.Current.Session["LoginId"] != null)
            {
                var ReturnPostResponse = "";

                if (HttpContext.Current.Session["CatalogueURL"] == null)
                {
                    HttpContext.Current.Session["CatalogueURL"] = CatalogueURL.ToString();
                }
                HttpContext.Current.Session["extension"] = extension.ToString();
                return ReturnPostResponse;
            }
            else
            {
                return "SessionOut";
            }

        }
        catch (Exception ac)
        {
            return "Error";
        }
    }
    [WebMethod()]
    public static string UpdateCatalogue(int CatalogueId, string CatCode, int CategoryId, string CatalogueTitle, string CatalogueDescription, string VideoURL, string MoreDetailURL, string CatalogueURL, string Logo, string CatalogueSizeMB, string ThumbnailURL, int BrandId, string ProductSize, string Color, string Price, string Weight, string SKUCode)
    {
        try
        {
            BusinessLogicLayer bll = new BusinessLogicLayer();
            ResultSet res = new ResultSet();
            if (Price == "")
            {
                Price = "0";
            }
            if (CatalogueSizeMB == "")
                CatalogueSizeMB = "0";

            if (HttpContext.Current.Session["LoginId"] != null)
            {
                var ReturnPostResponse = "";
                if (CatalogueURL != "")
                {
                    CatalogueURL = "Files/Catalogue/" + HttpContext.Current.Session["CatalogueURL"] + CatalogueURL;
                }
                try
                {
                    res.Result = bll.AddEditProducts(CatalogueId, System.Web.HttpUtility.UrlDecode(CatCode), 1, CategoryId, HttpUtility.UrlDecode(CatalogueTitle), HttpUtility.UrlDecode(CatalogueDescription), ThumbnailURL, CatalogueURL, Convert.ToDecimal(CatalogueSizeMB), "", VideoURL, MoreDetailURL, HttpContext.Current.Session["Loginid"].ToString(), BrandId, ProductSize, Color, Convert.ToDecimal(Price), Weight, SKUCode);
                    res.Success = true;
                    res.Total = 1;
                }
                catch (Exception ex)
                {
                    res.Total = 0;
                    res.Success = false;
                }
                ReturnPostResponse = JsonConvert.SerializeObject(res);
                

                return ReturnPostResponse;
            }
            else
            {
                return "SessionOut";
            }
        }
        catch (Exception ac)
        {
            return "Error";
        }
    }

    #endregion

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindProducts();
    }
}