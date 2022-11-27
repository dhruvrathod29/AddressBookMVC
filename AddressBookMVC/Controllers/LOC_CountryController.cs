using AddressBookMVC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookMVC.Controllers
{
    public class LOC_CountryController : Controller
    {
        #region Configuration

        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        
        {
            Configuration = _configuration;
        }
        #endregion

        #region Index

        #region Select All
        public IActionResult Index()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_Country_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            conn.Close();
            
            return View("LOC_CountryList",dt);
        }
        #endregion

        #endregion

        #region Add
        public IActionResult Add(int? CountryID)
        {
            #region Select By PK
            if (CountryID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_Country_SelectByPK";
                objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt= new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_CountryModel modelLOC_CountryModel = new LOC_CountryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_CountryModel.CountryID = Convert.ToInt32(dr["CountryID"]); 
                    modelLOC_CountryModel.CountryName = dr["CountryName"].ToString(); 
                    modelLOC_CountryModel.CountryCode = dr["CountryCode"].ToString(); 
                    modelLOC_CountryModel.CreationDate = Convert.ToDateTime(dr["CreationDate"]); 
                    modelLOC_CountryModel.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("LOC_CountryAddEdit", modelLOC_CountryModel);
                }
                conn.Close();
            }
            #endregion

            return View("LOC_CountryAddEdit");  
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (ModelState.IsValid)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                if (modelLOC_Country.CountryID == null)
                {

                    objCmd.CommandText = "PR_LOC_Country_Insert";

                }
                else
                {
                    objCmd.CommandText = "PR_LOC_Country_UpdateByPK";
                    objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
                }

                objCmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = modelLOC_Country.CountryName;
                objCmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = modelLOC_Country.CountryCode;
                objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_Country.CreationDate;
                objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_Country.ModificationDate;


                if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
                {
                    if (modelLOC_Country.CountryID == null)
                        TempData["CountryInsertMessage"] = "Record Insert Successfully";

                    else
                        TempData["CountryInsertMessage"] = "Record Update Successfully";

                }

                conn.Close();
            }
           
            return View("LOC_CountryAddEdit");  
        }
        #endregion
sdfsfsfsfsf
        #region Delete
        public IActionResult Delete(int CountryID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_Country_DeleteByPK";
           
            objCmd.Parameters.AddWithValue("@CountryID", CountryID);

            objCmd.ExecuteNonQuery();

            
            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

       /* public IActionResult LOC_CountryList()
        {


            return View();
        }*/

    }
}
