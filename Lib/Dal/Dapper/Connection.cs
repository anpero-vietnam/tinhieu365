using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dal.Dapper
{
    internal class Connection<TEntity> : IConnection<TEntity> where TEntity : class
    {
        string serverName;
        string ConnectionStrings;
        public Connection()
        {
            serverName = string.Empty;
            ConnectionStrings = string.Empty;
            ModulName = string.Empty;
        }

        public string ModulName{get;set;}
        public string ServerName
        {
            get
            {
                return serverName;
            }
            set
            {
                serverName = value;
            }
        }
      
        private string GetConnectionString()
        {   
              return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private DynamicParameters GetParam(Dictionary<string, object> param)
        {
            DynamicParameters _param= new DynamicParameters(); 
            if (param != null)
            {
                foreach (KeyValuePair<string, object> entry in param)
                {
                    _param.Add(entry.Key, entry.Value);
                }
            }
            return _param;
        }
        public int ExecQuery(string query, Dictionary<string, object> ParamList)
        {
            int rs = 0;         
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        rs = db.Execute(query, GetParam(ParamList));
                    }
                    
                    catch (Exception)
                    {

                    }

                    ServerName = string.Empty;
                    ModulName = string.Empty;
                    db.Close();
                    db.Dispose();
                    
                }
               
            }
            return rs;
        }
     
    
        public List<TEntity> ExeQuery(string query,Dictionary<string, object> ParamList)
        {
            List<TEntity> rs = new List<TEntity>();        
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {                
                rs = db.Query<TEntity>(query,GetParam(ParamList)).ToList();                
                db.Close();
                db.Dispose();
            }
            return rs;
        }
        public List<TEntity> Select(string storeName,Dictionary<string,object> ParamList)
        {       
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
             
                
                db.Open();
                List<TEntity> result=db.Query<TEntity>(storeName, GetParam(ParamList), commandType: CommandType.StoredProcedure).ToList();
                ServerName = string.Empty;
                ModulName = string.Empty;
                db.Close();
                db.Dispose();
                return result;
            }

        }
        public DataTable SelectDataTable(string storeName, Dictionary<string, object> ParamList)
        {
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {

                db.Open();
                var dataReader = db.ExecuteReader(storeName, GetParam(ParamList), commandType: CommandType.StoredProcedure);
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                ServerName = string.Empty;
                ModulName = string.Empty;
                db.Close();
                db.Dispose();
                return dt;
            }

        }
        
        public List<TEntity> Select(string storeName, Dictionary<string, object> ParamList, string outPutParamName, out int outPut)
        {
            DynamicParameters _param = GetParam(ParamList);
            _param.Add(outPutParamName, 0, DbType.Int32, direction: ParameterDirection.InputOutput);
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Open();
                List<TEntity> result = db.Query<TEntity>(storeName, _param, commandType: CommandType.StoredProcedure).ToList();
                outPut = _param.Get<int>(outPutParamName);
                ServerName = string.Empty;
                ModulName = string.Empty;
                db.Close();
                db.Dispose();
                return result;
            }

        }
        public int ExecuteProc(string storeName, Dictionary<string, object> ParamList, string outPutParamName, out int outPut)
        {
            int affectedRows = 0;
            DynamicParameters _param = GetParam(ParamList);            
            _param.Add(outPutParamName, 0, DbType.Int32, direction: ParameterDirection.InputOutput);
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                
                    try
                    {
                        affectedRows = db.Execute(storeName, _param, commandType: CommandType.StoredProcedure);
                        outPut = _param.Get<int>(outPutParamName);
                        db.Close();
                        db.Dispose();
                        
                    }
                    catch (Exception)
                    {
                        outPut = 0;
                        db.Close();
                        db.Dispose();
                    }
                ServerName = string.Empty;
                ModulName = string.Empty;
                return affectedRows;

              
                    
            }
        }
        
        //tested
        public int ExecuteProc(string storeName, Dictionary<string, object> ParamList)
        {
            int result = 0;            
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Open();
                //using (var transaction = db.BeginTransaction())
                //{
                    try
                    {
                    
                        result = db.Execute(storeName, GetParam(ParamList), commandType: CommandType.StoredProcedure);
                    //    transaction.Commit();
                    }
                    catch (Exception)
                    {
                    //    transaction.Rollback();
                    }
                ServerName = string.Empty;
                ModulName = string.Empty;
                db.Close();
                    db.Dispose();

                    return result;
                //}
             
            }
        }
        public object ExecuteScalar(string storeName, Dictionary<string, object> ParamList)
        {
            object result = 0;
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                db.Open();
                //using (var transaction = db.BeginTransaction())
                //{
                try
                {
                    result = db.ExecuteScalar(storeName, GetParam(ParamList), commandType: CommandType.StoredProcedure);
                    //    transaction.Commit();
                }
                catch (Exception)
                {
                    //    transaction.Rollback();
                }
                db.Close();
                db.Dispose();
                ServerName = string.Empty;
                ModulName = string.Empty;
                return result;
                //}

            }
        }
        //tested

        //tested
        public TEntity SelectSingle(string storeName, Dictionary<string, object> ParamList)
        {
            
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                TEntity result;
                db.Open();
                //using (var transaction = db.BeginTransaction())
                //{
                try
                {
                    result = db.QuerySingle<TEntity>(storeName, GetParam(ParamList), commandType: CommandType.StoredProcedure);
                    //    transaction.Commit();
                }
                catch (Exception)
                {
                    throw;                    
                }
                db.Close();
                db.Dispose();
                ServerName = string.Empty;
                ModulName = string.Empty;
                return result;
                //}

            }
        }
    }
}
