using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Filter
{
    public class YearAnalyticFilter
    {
        int day, month, year;
        string type;
        public int Year { get => year; set => year = value; }
        public int Day { get => day; set => day = value; }
        public int Month { get => month; set => month = value; }
        public string Type { get => type; set => type = value; }
    }
}
