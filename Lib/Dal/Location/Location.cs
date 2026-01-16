using Models.Modul.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class LocationControl
    {
        public List<Location> GetLocation(int parentId)
        {
            List<Location> rs = new List<Location>();
            if (!Ultil.Cache.CacheHelper.TryGet("location_cache_" + parentId, out rs))
            {
                rs = new List<Location>();
                SqlParameter[] paramList = new SqlParameter[1];
                paramList[0] = new SqlParameter("@parentId", SqlDbType.Int, 32);
                paramList[0].Value = parentId;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("Getlocation", paramList);
                if (tables != null && tables.Rows.Count > 0)
                {
                    foreach (DataRow item in tables.Rows)
                    {
                        Location l = new Location();
                        l.Id = Convert.ToInt32(item["id"]);
                        l.ParentId = Convert.ToInt32(item["parent"]);
                        l.Name = item["name"].ToString();
                        List<Location> _chidLocation = new List<Location>();
                        if (!Ultil.Cache.CacheHelper.TryGet("location_cache_" + l.Id, out _chidLocation))
                        {
                            _chidLocation = GetLocation(l.Id);
                            Ultil.Cache.CacheHelper.Set("location_cache_" + l.Id, rs);
                        }
                        l.ChildLocation = _chidLocation;
                        rs.Add(l);
                    }
                }
                Ultil.Cache.CacheHelper.Set("location_cache_" + parentId, rs);
            }
            return rs;
        }
    }


}
