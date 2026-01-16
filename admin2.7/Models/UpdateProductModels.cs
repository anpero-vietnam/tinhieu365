using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace admin.Models
{
    public class UpdateProductModels
    {
        public int St { get; set; }
        public int IsSales { get; set; }
        public int CategoryId { get; set; }
        public int IsParent { get; set; }
        public int ParentCatId { get; set; }
        public int Warranty { get; set; }
        public int Price { get; set; }
        public int SalePice { get; set; }

        public int Id { get; set; }
        public int StatusId { get; set; }
        public int Location { get; set; }
        public int SubLocation { get; set; }
        public int Prioty { get; set; }

        private List<ProductPropertiesValue> property;
        string prioties, script, origin, thumbLink, detail, name, keywords, shortDesc, saleEndTime, tag, specifications;

       public bool Sale
        {
            get
            {
                return IsSales == 0 ? false : true;

            }
        }

        public string Prioties { get => prioties; set => prioties = value; }
        public string Script { get => script; set => script = value; }
        public string Origin { get => origin; set => origin = value; }
        public string ThumbLink { get => thumbLink; set => thumbLink = value; }
        public string Detail { get => detail; set => detail = value; }
        public string Name { get => name; set => name = value; }
        public string Keywords { get => keywords; set => keywords = value; }
        public string ShortDesc { get => shortDesc; set => shortDesc = value; }
        public string SaleEndTime { get => saleEndTime; set => saleEndTime = value; }
        public string Tag { get => tag; set => tag = value; }
        public string Specifications { get => specifications; set => specifications = value; }
        public List<ProductPropertiesValue> Property { get => property; set => property = value; }

        public UpdateProductModels()
        {
            Warranty = 0;
            StatusId = 0;
            Location = 0;
            SubLocation = 0;
            Prioty = 0;
            Origin = string.Empty;
            Tag = string.Empty;
            SaleEndTime = string.Empty;
            Keywords = string.Empty;
            Name = string.Empty;
            Script = string.Empty;
            Prioties = string.Empty;
            Specifications = string.Empty;
            Property = new List<ProductPropertiesValue>();
        }
    }
}