using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Name = "";
            id = 0;
            Images = "";
            childCategory = new List<ProductCategory>();
        }        
        public int Rank { get; set; }
        
        List<ProductCategory> childCategory;
        string name, images, keywords, description;
        int id;
        string defaultLink;
        public string DefaultLink
        {
            get
            {
                if (string.IsNullOrEmpty(defaultLink))
                {
                    return "/" + Ultil.StringHelper.ToSplitURLgach(Name) + "-c" + Id;
                }
                else
                {
                    return defaultLink;
                }
                
            }
            set
            {
                defaultLink = value;
            }

        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public List<ProductCategory> ChildCategory
        {
            get
            {
                return childCategory;
            }

            set
            {
                childCategory = value;
            }
        }

        public string Images
        {
            get
            {
                return images;
            }

            set
            {
                images = value;
            }
        }

        public string Keywords
        {
            get
            {
                return keywords;
            }

            set
            {
                keywords = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
    }
}
