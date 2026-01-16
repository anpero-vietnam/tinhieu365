using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AModul.Dapper;
using Models;
using Models.Modul.Common;

namespace StoreMng
{
    public class Turning : ConnectionProxy<TurningModel>
    {
        
        public List<TurningModel> GetTurningTable()
        {
            
            return base.Select("sp_AnalyticRecord").ToList();
        }
        public int TurningRequesAnalyticTable()
        {
            
            return Convert.ToInt32(base.ExecuteScalar("sp_ArtiveRequesAnalytic"));
        }
    }
}
