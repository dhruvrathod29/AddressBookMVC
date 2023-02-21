using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection.Emit;

namespace AddressBookMVC.DAL
{
    public abstract class LOC_DALBase
    {
        #region LOC_SelectAll

        #region dbo.PR_LOC_Country_SelectAll
        public DataTable dbo_PR_LOC_Country_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectAll");

                DataTable dt = new DataTable();
                using(IDataReader dr = sqlDB.ExecuteReader(dbCMD))
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

        #region dbo.PR_LOC_State_SelectAll
        public DataTable dbo_PR_LOC_State_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");
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

        #region dbo.PR_LOC_City_SelectAll
        public DataTable dbo_PR_LOC_City_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectAll");
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

        #region LOC_Delete

        #region dbo.PR_LOC_Country_Delete

        public bool dbo_PR_LOC_Country_DeleteByPK(string conn, int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_DeleteByPK");
                sqlDB.AddInParameter(dbCMD,"CountryID",SqlDbType.Int,CountryID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;   
            }
        }


        #endregion

        #region PR_LOC_State_Delete
        public bool dbo_PR_LOC_State_DeleteByPK(string conn, int StateID)
        {
            try
            {
                SqlDatabase sqlDB =  new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPK");
                sqlDB.AddInParameter(dbCMD,"StateID",SqlDbType.Int ,StateID);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return(vReturnValue == -1 ? false : true);  
            }
            catch(Exception ex) 
            {
                return false;
            }
        }
        #endregion

        #region PR_LOC_City_Delete
        public bool dbo_PR_LOC_City_DeleteByPK(string conn, int CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, CityID);
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

    }
}
