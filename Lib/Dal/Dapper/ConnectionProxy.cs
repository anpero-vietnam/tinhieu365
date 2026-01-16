using Models;
using System.Collections.Generic;
using System.Data;

namespace Dal.Dapper
{
    //repository 
    public class ConnectionProxy<TEntity>  where TEntity : class
    {
        Connection<TEntity> cn = null;
        protected string ServerName = "";
        protected string ModulName = string.Empty;        
        protected int ExecuteProc(string storeName, Dictionary<string, object> ParamList, string outPutParamName, out int outPut)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            cn.ServerName = ServerName;
            
            cn.ModulName = ModulName;
            var result= cn.ExecuteProc(storeName,ParamList, outPutParamName, out outPut);
            Reset();
            return result;
        }

       protected int ExecuteProc(string storeName, Dictionary<string, object> paramList)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            cn.ServerName = ServerName;
            cn.ModulName = ModulName;
            var result= cn.ExecuteProc(storeName, paramList);
            Reset();
            return result;
        }
        //tester
        protected object ExecuteScalar(string storeName, Dictionary<string, object> ParamList)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            cn.ServerName = ServerName;            
            cn.ModulName = ModulName;
            var result = cn.ExecuteScalar(storeName,ParamList);
            Reset();
            return result;
        }
        protected int ExecQuery(string query)
        {
            //if (cn == null)
            //{
            //    cn = new Connection<TEntity>();
            //}
            //cn.ServerName = ServerName;
            //cn.ParamList = ParamList;
            //var result = cn.ExecQuery(query);

            //ParamList = new Dictionary<string, object>();
            //ServerName = string.Empty;
            //return result;
            return 0;
        }
        protected List<TEntity> ExecQuery(string query, Dictionary<string, object> ParamList)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }            
            var result= cn.ExeQuery(query,ParamList);
            Reset();
            return result;
        }
        protected DataTable SelectDataTable(string query, Dictionary<string, object> ParamList)
        {
            if (cn == null)
            { 
                cn = new Connection<TEntity>();
            }
            cn.ServerName = ServerName;            
            cn.ModulName = ModulName;
            var result = cn.SelectDataTable(query,ParamList);
            Reset();
            return result;
        }

        //tested
        protected TEntity SelectSingle(string storeName, Dictionary<string, object> ParamList)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();                
                
            }
            
            cn.ServerName = ServerName;
            
            cn.ModulName = ModulName;
            var resoult= cn.SelectSingle(storeName,ParamList);
            Reset();
            return resoult;
        }

        //tested
        //protected int ExecuteProc(string storeName, Dictionary<string, object> ParamList)
        //{
        //    if (cn == null)
        //    {
        //        cn = new Connection<TEntity>();                
        //    }
        //    cn.ServerName = ServerName;
            
        //    cn.ModulName = ModulName;
        //    var result= cn.ExecuteProc(storeName,ParamList);
        //    Reset();
        //    return result;
        //}

        //tested
        public List<TEntity> Select(string storeName, Dictionary<string, object> ParamList)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            cn.ServerName = ServerName;            
            cn.ModulName = ModulName;
            var resoult = cn.Select(storeName,ParamList);
            Reset();
            return resoult;
        }
        //tested
        public List<TEntity> Select(string storeName, Dictionary<string, object> ParamList, string outPutParamName, out int outPut)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            cn.ServerName = ServerName;
            
            cn.ModulName = ModulName;
            var result = cn.Select(storeName,ParamList, outPutParamName, out outPut);
            Reset();
            return result;
        }
        private void Reset()
        {
            ServerName = string.Empty;
            ModulName = string.Empty;
        }
    }
}
