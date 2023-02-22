using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using System;

namespace AddressBookMVC.DAL
{
    public class CON_DALBase
    {
        #region CON_SelectAll

        #region CON_Contact_SelectAll
        public DataTable dbo_PR_CON_Contact_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        #endregion


        #region MST_Contact_Category_SelectAll
        public DataTable dbo_PR_MST_ContactCategory_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        #endregion

        #endregion


        #region CON_DELETE

        #region CON_Contact_DeleteByPK
        public bool dbo_PR_CON_Contact_DeleteByPK(string conn, int ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region MST_ContactCategory_DeleteByPK
        public bool dbo_PR_MST_ContactCategory_SelectAll(string conn, int ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #endregion


        #region CON_SelectByPK

        #region CON_Contact_SelectByPK



        #endregion

        #region MST_ContactCategory_SelectByPK

        #endregion

        #endregion

    }
}
