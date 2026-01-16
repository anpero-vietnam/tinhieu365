using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Article
{
    public class ArticleCategoryModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public int ParentCategory { get; set; }
        public ArticleCategoryModel()
        {
            Id = 0;
            Description = string.Empty;
            Images = string.Empty;
            ParentCategory = 0;
        }
    }
}
