using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Dapper;
namespace Dal
{
    public  class Turning:ConnectionProxy<Models.PlotCharModel>
    {
        public  int ClearRequesAnalytic()
        {
            
            return 0;
        }
    }
}
