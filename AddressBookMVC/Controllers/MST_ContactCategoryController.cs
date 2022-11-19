﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using AddressBookMVC.Models;
using System;

namespace AddressBookMVC.Controllers
{
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)

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
            objCmd.CommandText = "PR_MST_ContactCategory_SelectAll";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);

            conn.Close();

            return View("MST_ContactCategoryList", dt);
            
        }

        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
                SqlConnection conn = new SqlConnection(connectionstr);

                conn.Open();

                SqlCommand objCmd = conn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();
                dt.Load(objSDR);

                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelMST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelMST_ContactCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
                }
                conn.Close();
            }
            return View("MST_ContactCategoryAddEdit");
        }

        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                objCmd.CommandText = "PR_MST_ContactCategory_Insert";

            }
            else
            {
                objCmd.CommandText = "PR_MST_ContactCategory_UpdateByPK";
                objCmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ContactCategory.ContactCategoryID;

            }
            objCmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelMST_ContactCategory.ContactCategoryName;
            objCmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelMST_ContactCategory.CreationDate;
            objCmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelMST_ContactCategory.ModificationDate;

            if (Convert.ToBoolean(objCmd.ExecuteNonQuery()))
            {
                if(modelMST_ContactCategory.ContactCategoryID == null)
                    TempData["ContactCategoryInsertMessage"] = "Record Insert Successfully";
                else
                    TempData["ContactCategoryInsertMessage"] = "Record Update Successfully";
            }
            conn.Close();
            return View("MST_ContactCategoryAddEdit");
        }
        public IActionResult Delete(int ContactCategoryID)
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionStrings");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);

            conn.Open();

            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_MST_ContactCategory_DeleteByPK";

            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);

            objCmd.ExecuteNonQuery();


            conn.Close();

            return RedirectToAction("Index");
        }

        public IActionResult MST_ContactCategoryList()
        {


            return View();
        }
    }
}
