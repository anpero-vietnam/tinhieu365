using Models;
using System.Collections.Generic;
using System.Data;

namespace Dal.Dapper
{
    public interface IConnection<T> where T : class
    {
        
        int ExecuteProc(string storeName, Dictionary<string, object> ParamList, string outPutParamName,out int outPut);
        int ExecuteProc(string storeName, Dictionary<string, object> ParamList);
        List<T> Select(string storeName, Dictionary<string, object> ParamList);
        int ExecQuery(string query, Dictionary<string, object> ParamList);
        List<T> ExeQuery(string query, Dictionary<string, object> ParamList);
        List<T> Select(string storeName, Dictionary<string, object> ParamList, string outPutParamName, out int outPut);
        T SelectSingle(string storeName, Dictionary<string, object> ParamList);
        object ExecuteScalar(string storeName, Dictionary<string, object> ParamList);
        DataTable SelectDataTable(string storeNamee, Dictionary<string, object> ParamList);
    }
}
