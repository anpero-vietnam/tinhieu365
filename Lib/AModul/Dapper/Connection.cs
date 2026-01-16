using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AModul.Dapper
{
    internal class Connection<TEntity> : IConnection<TEntity> where TEntity : class
    {

        
        public Connection()
        {
        }
        
       
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
   
        private DynamicParameters GetParam(Dictionary<string, object> param)
        {
            DynamicParameters _param = new DynamicParameters();
            if (param != null)
            {
                foreach (KeyValuePair<string, object> entry in param)
                {
                    _param.Add(entry.Key, entry.Value);
                }
            }
            return _param;
        }
        public int ExecQuery(string query, Dictionary<string, object> paramlist = null)
        {
            int rs = 0;
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                
                    try
                    {
                        rs = db.Execute(query, GetParam(paramlist));
                    }

                    catch (Exception)
                    {

                    }
                    db.Close();
                    db.Dispose();
                
                

            }
            return rs;
        }
  

        public List<TEntity> ExeQuery(string query,Dictionary<string, object> paramlist = null)
        {
            List<TEntity> rs = new List<TEntity>();
            using (SqlConnection db = new SqlConnection(GetConnectionString()))
            {
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        rs = db.Query<TEntity>(query, GetParam(paramlist)).ToList();
                        db.Close();
                        db.Dispose();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        return rs;
                    }

                }

            }
            return rs;
        }
        public int DeleteById(int id)
        {
            int rs = 1;
            var connection = GetConnectionString();
            string entityName = typeof(TEntity).Name.ToString();
            entityName = entityName.Replace("Model", string.Empty);
            string delteQuery = "delete " + entityName + " where id=" + id;
            using (SqlConnection db = new SqlConnection(connection))
            {
                try
                {
                    db.Execute(delteQuery, commandType: CommandType.Text);

                    db.Close();
                    db.Dispose();
                }
                catch (Exception)
                {
                    rs = 0;
                }

            }
            return rs;
        }


        public List<TEntity> Select(string storeName,  Dictionary<string, object> paramlist = null)
        {
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                using (SqlConnection db = new SqlConnection(connectionString))
                {

                    db.Open();
                    List<TEntity> result = db.Query<TEntity>(storeName, GetParam(paramlist), commandType: CommandType.StoredProcedure).ToList();

                    db.Close();
                    db.Dispose();
                    return result;
                }
            }
            else
            {
                return null;
            }

        }
        public DataTable SelectDataTable(string storeName, Dictionary<string, object> paramlist = null)
        {
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                using (SqlConnection db = new SqlConnection(connectionString))
                {

                    db.Open();
                    var dataReader = db.ExecuteReader(storeName, GetParam(paramlist), commandType: CommandType.StoredProcedure);
                    DataTable dt = new DataTable();
                    dt.Load(dataReader);
                    db.Close();
                    db.Dispose();
                    return dt;
                }
            }
            else { return null; }

        }

        public List<TEntity> Select(string storeName,  string outPutParamName, out int outPut, Dictionary<string, object> paramlist = null)
        {
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                DynamicParameters _param = GetParam(paramlist);
                _param.Add(outPutParamName, 0, DbType.Int32, direction: ParameterDirection.InputOutput);
                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    db.Open();
                    List<TEntity> result = db.Query<TEntity>(storeName, _param, commandType: CommandType.StoredProcedure).ToList();
                    outPut = _param.Get<int>(outPutParamName);
                    db.Close();
                    db.Dispose();
                    return result;
                }
            }
            else
            {
                outPut = 0;
                return null;
            }
        }
       
        public int ExecuteProc(string storeName,string outPutParamName, out int outPut, Dictionary<string, object> paramlist = null)
        {
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                int affectedRows = 0;
                DynamicParameters _param = GetParam(paramlist);
                _param.Add(outPutParamName, 0, DbType.Int32, direction: ParameterDirection.InputOutput);
                using (SqlConnection db = new SqlConnection(connectionString))
                {

                    try
                    {
                        affectedRows = db.Execute(storeName, _param, commandType: CommandType.StoredProcedure);
                        outPut = _param.Get<int>(outPutParamName);
                        db.Close();
                        db.Dispose();
                    }
                    catch (Exception ex)
                    {
                        outPut = 0;
                        db.Close();
                        db.Dispose();
                    }
                    return affectedRows;
                }
            }
            else
            {
                outPut = 0;
                return 0;
            }

        }

        //tested
        public int ExecuteProc(string storeName, Dictionary<string, object> paramlist = null)
        {
            int result = 0;
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    db.Open();
                   
                    try
                    {

                        result = db.Execute(storeName, GetParam(paramlist), commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception)
                    {

                    }
                    db.Close();
                    db.Dispose();

                    return result;
                    //}

                }

            }
            else
            {
                return 0;
            }

        }
        public object ExecuteScalar(string storeName,  Dictionary<string, object> paramlist = null)
        {
            object result = 0;
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    db.Open();
                    //using (var transaction = db.BeginTransaction())
                    //{
                    try
                    {
                        result = db.ExecuteScalar(storeName, GetParam(paramlist), commandType: CommandType.StoredProcedure);
                        //    transaction.Commit();
                    }
                    catch (Exception)
                    {
                        //    transaction.Rollback();
                    }
                    db.Close();
                    db.Dispose();
                    return result;
                    //}

                }
            }
            else
            {
                return null;
            }

        }
        //tested

        //tested
        public TEntity SelectSingle(string storeName, Dictionary<string, object> paramlist = null)
        {
            string connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    TEntity result=null;
                    db.Open();
                    try
                    {
                        result = db.QuerySingle<TEntity>(storeName, GetParam(paramlist), commandType: CommandType.StoredProcedure);

                    }
                    catch (Exception ex)
                    {
                        
                    }
                    db.Close();
                    db.Dispose();
                    return result;
                    //}

                }
            }
            else
            {
                return null;
            }


        }
    }
}
