using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Ultil
{
    public partial class IConvertHelper<T> 
    {
        public  DataTable CreateDataTable(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        //public I DeepClone<I>(I source)
        //{
        //    var serialized = JsonConvert.SerializeObject(source);
        //    return JsonConvert.DeserializeObject<I>(serialized);
        //}
        public static I DeepClone<I>(I source)
        {

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(I);
            }

            var result = JsonConvert.DeserializeObject<I>(JsonConvert.SerializeObject(source, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

            return result;

        }
        public  Dictionary<string, string> ObjectToDictionary(object obj)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (obj != null)
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    string propName = prop.Name;
                    var val = obj.GetType().GetProperty(propName).GetValue(obj, null);
                    if (val != null)
                    {
                        ret.Add(propName, val.ToString());
                    }
                    else
                    {
                        ret.Add(propName, "");
                    }
                }
            }
            return ret;
        }
    }
}
