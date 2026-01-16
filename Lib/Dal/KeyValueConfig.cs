using Dal.Dapper;
using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class KeyValueConfigControl : ConnectionProxy<KeyValueConfigModel>  
    {
        public int UpdateKeyValueConfig(string key,string value,string description)
        {   
            Dictionary<string, object> paramlist = new Dictionary<string, object>();
            paramlist.Add("@key",key);
            paramlist.Add("@Value",value);
            paramlist.Add("@description", description);
            return base.ExecuteProc("sp_AddOrUpdateKeyValue", paramlist);
        }
        public List<KeyValueConfigModel> GetAllKeyValueConfig()
        {
            return base.ExecQuery("Select * from KeyValueCongfig ",null);
        }
    }
}
