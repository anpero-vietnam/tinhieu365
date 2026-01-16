using Dal.Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ultil.Cache;
namespace StoreMng
{
    public class UseControl : ConnectionProxy<UserProfile>
    {
        
     
     
        //public UserProfile GetUserRole(int uid)
        //{

        //    ParamList.Add("@uid", uid);
        //    ParamList.Add("@agenID", storeId);
        //    return base.SelectSingle("[sp_GET_ALL_ROLE_OF_USE]");            
        //}
        public int RemoveUOutAllRole(int uid)
        {
            ClearCacheData(uid);
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("@UID", SqlDbType.Int);
            paramList[0].Value = uid;            
            DatabaseAccess ds = new DatabaseAccess();
            return ds.executeUpdate("DEL_ALL_ROLE_STORE", paramList);
        }
        public int RemoveUOutRole(int uid, int storeId, int role)
        {
            ClearCacheData(uid);
            Dictionary<string, object> ParamList = new Dictionary<string, object>();
            ParamList.Add("@UID", uid);
            ParamList.Add("@STOREID", storeId);
            ParamList.Add("@STOREROLE", role);
            return  base.ExecuteProc("DEL_U_ROLE_STORE",ParamList);          
        }
 
        public static bool ClearCacheData(int uid)
        {
            string cacheKey = "ROLE_OF_USER_" + uid.ToString() + "_";            
            return CacheHelper.Remove(cacheKey);

        }
    }
}
