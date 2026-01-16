using Models;
using System.Collections.Generic;
using System.Data;

namespace AModul.Dapper
{
    public interface IConnection<T> where T : class
    {

        int ExecuteProc(string storeName,  string outPutParamName,out int outPut, Dictionary<string, object> paramlist = null);
        int ExecuteProc(string storeName,Dictionary<string, object> paramlist = null);
        List<T> Select(string storeName, Dictionary<string, object> paramlist = null);
        int ExecQuery(string query, Dictionary<string, object> paramlist = null);
        List<T> ExeQuery(string query, Dictionary<string, object> paramlist = null);
        List<T> Select(string storeName, string outPutParamName, out int outPut, Dictionary<string, object> paramlist = null);
        T SelectSingle(string storeName,Dictionary<string, object> paramlist = null);
        object ExecuteScalar(string storeName, Dictionary<string, object> paramlist = null);
        DataTable SelectDataTable(string storeNamee, Dictionary<string, object> paramlist = null);
    }
}
