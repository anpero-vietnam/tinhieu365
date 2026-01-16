using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Article
{
    public class WebContentModel
    {
        public  WebContentModel()
        {
            Type = string.Empty;
            TextContent = string.Empty;
        }
        public string Type { get; set; }
        public string TextContent { get; set; }
    }
}
