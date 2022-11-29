using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AddressBookMVC.Models;
using System;
using System.Collections.Generic;
using AddressBookMVC.DAL;

namespace AddressBookMVC.Controllers
{

    public class LOC_CityController : Controller
    {
        #region Configuration
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)

        {
            Configuration = _configuration;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            #region SelectAll
           
            
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_City_SelectAll(connectionstr);
            return View("LOC_CityList", dt);

            /* DataTable dt = new DataTable();
             SqlConnection conn = new SqlConnection(connectionstr);

             conn.Open();

             SqlCommand objCmd = conn.CreateCommand();
             objCmd.CommandType = CommandType.StoredProcedure;
             objCmd.CommandText = "PR_LOC_City_SelectAll";
             SqlDataReader objSDR = objCmd.ExecuteReader();
             dt.Load(objSDR);

             conn.Close();*/

            #endregion
        }
        #endregion

        #region Add
        public IActionResult Add(int? CityID)
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
            conn1.Close();


            List<LOC_Country_SelectForDropDownModel> list = new List<LOC_Country_SelectForDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_Country_SelectForDropDownModel vlst = new LOC_Country_SelectForDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            

            List<LOC_State_SelectForDropDownModel> list1 = new List<LOC_State_SelectForDropDownModel>();
            ViewBag.StateList = list1;



            #endregion


            #region Select By PK

            if (CityID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_City_SelectByPK";
                objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                LOC_CityModel  modelLOC_City = new LOC_CityModel();

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.PinCode = dr["PinCode"].ToString();
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("LOC_CityAddEdit", modelLOC_City);
                }
                conn.Close();

                // Aya Levanu baki chhe
            }
            #endregion

            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {

            if (ModelState.IsValid)
            {
                #region Insert
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;

                if (modelLOC_City.CityID == null)
                {
                    objCmd.CommandText = "PR_LOC_City_Insert";

                }
                #endregion

                #region Update By PK
                else
                {
                    objCmd.CommandText = "PR_LOC_City_UpdateByPK";
                    objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;


                }
                #endregion

                objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
                objCmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
                objCmd.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = modelLOC_City.PinCode;
                objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = DBNull.Value;
                objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = DBNull.Value;


                if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
                {
                    if (modelLOC_City.CityID == null)
                        TempData["CityInsertMessage"] = "Record Insert Successfully";
                    else
                        TempData["CityInsertMessage"] = "Record Update Successfully";

                }

                conn.Close();

            }


            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_City_DeleteByPK";

            objCmd.Parameters.AddWithValue("@CityID", CityID);

            objCmd.ExecuteNonQuery();


            conn.Close();

            return RedirectToAction("Index");
        }
        #endregion

        #region DropDownByCountry
        [HttpPost]
        public IActionResult DropDownByCountry(int CountryID)
        {
            #region State Drop Down

            string connectionstr1 = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt1 = new DataTable();

            SqlConnection conn1 = new SqlConnection(connectionstr1);

            conn1.Open();

            SqlCommand objCmd1 = conn1.CreateCommand();
            objCmd1.CommandType = CommandType.StoredProcedure;
            objCmd1.CommandText = "PR_LOC_State_SelectForDropDown";
            objCmd1.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader objSDR1 = objCmd1.ExecuteReader();
            dt1.Load(objSDR1);

            conn1.Close();

            List<LOC_State_SelectForDropDownModel> list1 = new List<LOC_State_SelectForDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_State_SelectForDropDownModel vlst = new LOC_State_SelectForDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = dr["StateName"].ToString();
                list1.Add(vlst);
            }
            ViewBag.StateList = list1;
            var vModel = list1;
            return Json(vModel);
            
            #endregion
        }
        #endregion

        /*public IActionResult LOC_CityList()
        {


            return View();
        }*/

    }
}
