using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AddressBookMVC.Models;
using System;
using System.Collections.Generic;

namespace AddressBookMVC.Controllers
{
    public class CON_ContactController : Controller
    {
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)

        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_CON_Contact_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            conn.Close();

            return View("CON_ContactList", dt);

        }
        public IActionResult Add(int? ContactID)
        {

            #region Country Drop Down

            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt1 = new DataTable();

            SqlConnection conn1 = new SqlConnection(connectionstr1);

            conn1.Open();

            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);



            List<LOC_Country_SelectForDropDownModel> list = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_Country_SelectForDropDownModel vlst = new LOC_Country_SelectForDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            conn1.Close();
            #endregion

            #region State Drop Down

            string connectionstr2 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt2 = new DataTable();

            SqlConnection conn2 = new SqlConnection(connectionstr2);

            conn2.Open();

            SqlCommand objCmd2 = conn2.CreateCommand();
            objCmd2.CommandType = CommandType.StoredProcedure;
            objCmd2.CommandText = "PR_LOC_State_SelectForDropDown";
            SqlDataReader objSDR2 = objCmd2.ExecuteReader();
            dt2.Load(objSDR2);



            List<LOC_State_SelectForDropDownModel> list2 = new List<LOC_State_SelectForDropDownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                LOC_State_SelectForDropDownModel vlst2 = new LOC_State_SelectForDropDownModel();
                vlst2.StateID = Convert.ToInt32(dr["StateID"]);
                vlst2.StateName = dr["StateName"].ToString();
                list2.Add(vlst2);
            }
            ViewBag.StateList = list2;
            conn2.Close();
            #endregion

            #region City Drop Down
            string connectionstr3 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt3 = new DataTable();

            SqlConnection conn3 = new SqlConnection(connectionstr3);

            conn3.Open();

            SqlCommand objCmd3 = conn3.CreateCommand();
            objCmd3.CommandType = CommandType.StoredProcedure;
            objCmd3.CommandText = "PR_LOC_City_SelectForDropDown";
            SqlDataReader objSDR3 = objCmd3.ExecuteReader();
            dt3.Load(objSDR3);



            List<LOC_City_SelectForDropDownModel> list3 = new List<LOC_City_SelectForDropDownModel>();
            foreach (DataRow dr in dt3.Rows)
            {
                LOC_City_SelectForDropDownModel vlst3 = new LOC_City_SelectForDropDownModel();
                vlst3.CityID = Convert.ToInt32(dr["CityID"]);
                vlst3.CityName = dr["CityName"].ToString();
                list3.Add(vlst3);
            }
            ViewBag.CityList = list3;
            conn3.Close();
            #endregion

            #region Contact Category Drop Down
            string connectionstr4 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt4 = new DataTable();

            SqlConnection conn4 = new SqlConnection(connectionstr4);

            conn4.Open();

            SqlCommand objCmd4 = conn4.CreateCommand();
            objCmd4.CommandType = CommandType.StoredProcedure;
            objCmd4.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            SqlDataReader objSDR4 = objCmd4.ExecuteReader();
            dt4.Load(objSDR4);



            List<MST_ContactCategory_SelectForDropDownModel> list4 = new List<MST_ContactCategory_SelectForDropDownModel>();
            foreach (DataRow dr in dt4.Rows)
            {
                MST_ContactCategory_SelectForDropDownModel vlst4 = new MST_ContactCategory_SelectForDropDownModel();
                vlst4.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                vlst4.ContactCategoryName = dr["ContactCategoryName"].ToString();
                list4.Add(vlst4);
            }
            ViewBag.ContactCategoryList = list4;
            conn4.Close();
            #endregion


            if (ContactID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_CON_Contact_SelectByPK";
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                CON_ContactModel modelCON_ContactModel = new CON_ContactModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelCON_ContactModel.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelCON_ContactModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelCON_ContactModel.StateID = Convert.ToInt32(dr["StateID"]);
                    modelCON_ContactModel.CityID = Convert.ToInt32(dr["CityID"]);
                    modelCON_ContactModel.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelCON_ContactModel.ContactName = dr["ContactName"].ToString();
                    modelCON_ContactModel.Address = dr["Address"].ToString();
                    modelCON_ContactModel.PinCode = dr["PinCode"].ToString();
                    modelCON_ContactModel.MobileNo = dr["MobileNo"].ToString();
                    modelCON_ContactModel.AlternetContact = dr["AlternetContact"].ToString();
                    modelCON_ContactModel.Email = dr["Email"].ToString();
                    modelCON_ContactModel.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                    modelCON_ContactModel.LinkedIn = dr["LinkedIn"].ToString();
                    modelCON_ContactModel.Twitter = dr["Twitter"].ToString();
                    modelCON_ContactModel.Insta = dr["Insta"].ToString();
                    modelCON_ContactModel.Gender = dr["Gender"].ToString();

                    modelCON_ContactModel.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelCON_ContactModel.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("CON_ContactAddEdit", modelCON_ContactModel);
                }
                conn.Close();
            }
            return View("CON_ContactAddEdit");
        }

        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (modelCON_Contact.ContactID == null)
            {
                objCmd.CommandText = "PR_CON_Contact_Insert";

            }
            else
            {
                objCmd.CommandText = "PR_CON_Contact_UpdateByPK";
                objCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelCON_Contact.ContactID;

            }

            objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCON_Contact.CountryID;
            objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCON_Contact.StateID;
            objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCON_Contact.CityID;
            objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelCON_Contact.ContactCategoryID;
            objCmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = modelCON_Contact.ContactName;
            objCmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelCON_Contact.Address;
            objCmd.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = modelCON_Contact.PinCode;
            objCmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar).Value = modelCON_Contact.MobileNo;
            objCmd.Parameters.Add("@AlternetContact", SqlDbType.NVarChar).Value = modelCON_Contact.AlternetContact;
            objCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = modelCON_Contact.Email;
            objCmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = modelCON_Contact.BirthDate;
            objCmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = modelCON_Contact.LinkedIn;
            objCmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = modelCON_Contact.Twitter;
            objCmd.Parameters.Add("@Insta", SqlDbType.NVarChar).Value = modelCON_Contact.Insta;
            objCmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = modelCON_Contact.Gender;


            objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelCON_Contact.CreationDate;
            objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelCON_Contact.ModificationDate;

            if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if (modelCON_Contact.ContactID == null)
                    TempData["ContactInsertMessage"] = "Record Insert Successfully";
                else
                    TempData["ContactInsertMessage"] = "Record Update Successfully";
            }
            conn.Close();
            return RedirectToAction("Add");
        }
        
        public IActionResult Delete(int ContactID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_CON_Contact_DeleteByPK";

            objCmd.Parameters.AddWithValue("@ContactID", ContactID);

            objCmd.ExecuteNonQuery();


            conn.Close();

            return RedirectToAction("Index");
        }

        public IActionResult CON_ContactList()
        {


            return View();
        }
    }
}
