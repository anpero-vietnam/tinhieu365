using Models;
using System.Collections.Generic;
using System.Data;

namespace AModul.Dapper
{
    //repository 
   // public class ConnectionProxy<TEntity> : IConnection<TEntity> where TEntity : class
   public class ConnectionProxy<TEntity> where TEntity : class
    {
        Connection<TEntity> cn = null;
        protected int ExecuteProc(string storeName, string outPutParamName, out int outPut, Dictionary<string, object> paramlist = null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.ExecuteProc(storeName, outPutParamName, out outPut,paramlist);
        }

        protected int DeleteById(int id)
        {

            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.DeleteById(id);
        }
        //tester
        protected object ExecuteScalar(string storeName, Dictionary<string,object> paramList =null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.ExecuteScalar(storeName,  paramList);
        }

        protected int ExecQuery(string query)
        {
            if(cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.ExecQuery(query);
        }
        protected List<TEntity> ExeQuery(string query,Dictionary<string, object> paramlist = null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.ExeQuery(query, paramlist);            
        }
        protected DataTable SelectDataTable(string query,  Dictionary<string, object> paramlist = null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.SelectDataTable(query,paramlist);         
        }

        //tested
        protected TEntity SelectSingle(string storeName,Dictionary<string, object> paramlist = null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();

            }
            return cn.SelectSingle(storeName,paramlist);
        }
        //tested
        protected int ExecuteProc(string storeName, Dictionary<string, object> paramlist = null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.ExecuteProc(storeName, paramlist);
        }
      
        //tested
        protected List<TEntity> Select(string storeName, Dictionary<string, object> paramlist = null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.Select(storeName, paramlist);
        }
        
        //tested
        protected List<TEntity> Select(string storeName,  string outPutParamName, out int outPut,Dictionary<string,object> paramlist=null)
        {
            if (cn == null)
            {
                cn = new Connection<TEntity>();
            }
            return cn.Select(storeName,outPutParamName, out outPut,paramlist);
        }
       
    }
}
