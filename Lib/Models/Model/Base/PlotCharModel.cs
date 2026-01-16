using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PlotCharModel
    {       
        // GET api/<controller>
        public string label { get; set; }
        public List<int[]> data { get; set; }
        public PlotCharModel()
        {
            data = new List<int[]>();
            label = string.Empty;
        }
    }
}
