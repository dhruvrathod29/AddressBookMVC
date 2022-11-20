using AddressBookMVC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookMVC.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)

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
            objCmd.CommandText = "PR_LOC_State_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            
            dt.Load(objSDR);
            
            conn.Close();

            return View("LOC_StateList",dt);

        }



        public IActionResult Add(int? StateID)
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
            



            if (StateID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPK";
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);


                
                LOC_StateModel modelLOC_State = new LOC_StateModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("LOC_StateAddEdit", modelLOC_State);
                }
               
               
                conn.Close();
            }
            return View("LOC_StateAddEdit");
        }

     

        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (modelLOC_State.StateID == null)
            {
                objCmd.CommandText = "PR_LOC_State_Insert";

            }
            else
            {
                objCmd.CommandText = "PR_LOC_State_UpdateByPK";
                objCmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;


            }
            objCmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;
            objCmd.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = modelLOC_State.StateName;
            objCmd.Parameters.Add("@StateCode", SqlDbType.NVarChar).Value = modelLOC_State.StateCode;
            objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_State.CreationDate;
            objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_State.ModificationDate;


            if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if (modelLOC_State.StateID == null)
                    TempData["StateInsertMessage"] = "Record Insert Successfully";
                else
                    TempData["StateInsertMessage"] = "Record Update Successfully";

            }

            conn.Close();
            return RedirectToAction("Add");
        }

        public IActionResult Delete(int StateID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_State_DeleteByPK";

            objCmd.Parameters.AddWithValue("@StateID", StateID);

            objCmd.ExecuteNonQuery();

            conn.Close();


            return RedirectToAction("Index");
        }


        public IActionResult LOC_StateList()
        {
            return View();
        }
    }
}
