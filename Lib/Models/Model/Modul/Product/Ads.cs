using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class Ads
    {
        public Ads()
        {
            ClickUrl = string.Empty;
            Description = string.Empty;
            ImagesUrl = string.Empty;
            Prioty = 0;
            ReferenceId = string.Empty;
        }
        public string ReferenceId { get; set; }
        public string ClickUrl { get; set; }
        public string Description { get; set; }
        public string ImagesUrl { get; set; }
        public int Id { get; set; }
        public int Prioty { get; set; }
    }
}
