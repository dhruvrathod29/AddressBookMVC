using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AddressBookMVC.Models;
using System;

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
            return View("CON_ContactAddEdit");
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
