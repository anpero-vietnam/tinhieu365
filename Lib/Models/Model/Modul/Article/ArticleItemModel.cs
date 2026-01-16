using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Article
{
    public class ArticleItem
    {
        public int CategoryId { get; set; }
        public string Tag { get; set; }
        public int CreateBy { get; set; }
        
        public string TagKhoDau { get; set; }
        public int Id { get; set; }
        public string ShortDesc { get; set; }
        public string Tittle { get; set; }
        public string NewsDesc { get; set; }

        public string Thumb { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Prioty { get; set; }
        public int ViewTime { get; set; }
        public string RaTe { get; set; }
        public string Author { get; set; }
        public bool Publish { get; set; }
        public string DatePost { get; set; }
        public string CateGoryName { get; set; }        
        
        public string Lang { get; set; }
        public ArticleItem()
        {
            CategoryId = 0;
            Tag = string.Empty;
            TagKhoDau = string.Empty;
            Id = 0;
            ShortDesc = string.Empty;
            Tittle = string.Empty;
            NewsDesc = string.Empty;
            Thumb = string.Empty;
            
            Prioty = 0;
            ViewTime =0;
            RaTe = string.Empty;
            Author = string.Empty;
            Publish = true;
            DatePost = string.Empty;
            CateGoryName = string.Empty;
            Lang = string.Empty;
        }

    }
}
