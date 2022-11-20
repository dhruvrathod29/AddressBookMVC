using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AddressBookMVC.Models;
using System;

namespace AddressBookMVC.Controllers
{
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)

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
            objCmd.CommandText = "PR_LOC_City_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            conn.Close();

            return View("LOC_CityList", dt);
           
        }

        public IActionResult Add(int? CityID)
        {
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

                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.PinCode = dr["PinCode"].ToString();
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("LOC_CityAddEdit", modelLOC_City);
                }
                conn.Close();
            }

            return View("LOC_CityAddEdit");
        }

        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (modelLOC_City.CityID == null)
            {
                objCmd.CommandText = "PR_LOC_City_Insert";

            }
            else
            {
                objCmd.CommandText = "PR_LOC_City_UpdateByPK";
                objCmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;


            }
            objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            objCmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
            objCmd.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = modelLOC_City.PinCode;
            objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_City.CreationDate;
            objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_City.ModificationDate;


            if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if (modelLOC_City.CityID == null)
                    TempData["CityInsertMessage"] = "Record Insert Successfully";
                else
                    TempData["CityInsertMessage"] = "Record Update Successfully";

            }

            conn.Close();


            return View("LOC_CityAddEdit");
        }

       

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

        public IActionResult LOC_CityList()
        {


            return View();
        }

    }
}
