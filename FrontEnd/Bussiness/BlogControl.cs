using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Bussiness
{
    public class BlogControl
    {
        public BlogItem GetArticle(int id, int st)
        {
            Dal.News blogMng = new Dal.News();
            return GetBlogItem(blogMng.GetNewsByIdFrontEnd(id, st));
        }
        public SearchArticleResults SearchArticle(int subCat, int minPrioty, int currentPage, int pageSite, int st)
        {
            int rsCount = 0;
            SearchArticleResults rs = new SearchArticleResults();
            rs.ItemList = GetBlogItemList(subCat, minPrioty, currentPage, pageSite, st, out rsCount);
            rs.ResultsCount = rsCount;
            return rs;
        }
        /// <summary>
        /// set 0 or -1 to get all category
        /// </summary>
        /// <param name="subCat">set 0 or -1 to get all category</param>        
        /// <returns></returns>
        private List<BlogItem> GetBlogItemList(int subCat, int minPrioty, int currentPage, int pageSite, int st, out int count)
        {
            Dal.News blogMng = new Dal.News();
            List<BlogItem> rs = new List<BlogItem>();
            DataTable table = blogMng.getNewTableFrontEnd(subCat, minPrioty, currentPage, pageSite, st, out count);
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
                {
                    rs.Add(GetBlogItem(item));
                }
            }
            return rs;
        }
        public List<BlogCategory> GetBlogCategoryList(int st)
        {
            Dal.SubCategory blogMng = new Dal.SubCategory();
            List<BlogCategory> rs = new List<BlogCategory>();
            DataTable table = blogMng.GetArticleCateGory(st);
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow item in table.Rows)
                {
                    rs.Add(GetBlogCategory(item));
                }
            }
            return rs;
        }
        private BlogCategory GetBlogCategory(DataRow row)
        {
            if (row != null)
            {
                BlogCategory item = new BlogCategory();
                item.Name = row["mieuta"].ToString();
                item.Thumb = row["images"].ToString();
                item.Id = Convert.ToInt32(row["id"]);

                return item;
            }
            else
            {
                return null;
            }

        }
        private BlogItem GetBlogItem(DataRow row)
        {
            if (row != null)
            {
                try
                {
                    BlogItem item = new BlogItem();
                    item.ShotDesc = row["shortDesc"].ToString();
                    item.Title = row["Tittle"].ToString();
                    item.ViewTime = Convert.ToInt32(row["viewTime"]);
                    item.Thumb = row["thumb"].ToString();
                    item.Content = row["content"].ToString();
                    item.CreateDate = row["datepost"].ToString();
                    item.Id = Convert.ToInt32(row["Id"]);
                    item.CategoryId = Convert.ToInt32(row["subcategory"]);
                    item.Tag = row["tag"].ToString();
                    item.TagKhongDau = row["tagKhongDau"].ToString();
                    item.CategoryName = row["subcatname"].ToString();
                    return item;
                }
                catch (Exception)
                {

                    return null;
                }

            }
            else
            {
                return null;
            }

        }
    }
    public class SearchArticleResults
    {
        public SearchArticleResults()
        {
            itemList = new List<BlogItem>();
            ResultsCount = 0;
        }
        List<BlogItem> itemList;
        int resultsCount;

        public List<BlogItem> ItemList
        {
            get
            {
                return itemList;
            }

            set
            {
                itemList = value;
            }
        }

        public int ResultsCount
        {
            get
            {
                return resultsCount;
            }

            set
            {
                resultsCount = value;
            }
        }
    }
    public class BlogItem
    {
        string title, createDate, shotDesc, content, thumb, categoryName, tag, tagKhongDau;
        int id, viewTime, categoryId;

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

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string CreateDate
        {
            get
            {
                return createDate;
            }

            set
            {
                createDate = value;
            }
        }

        public string ShotDesc
        {
            get
            {
                return shotDesc;
            }

            set
            {
                shotDesc = value;
            }
        }


        public string Thumb
        {
            get
            {
                return thumb;
            }

            set
            {
                thumb = value;
            }
        }


        public string Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
            }
        }

        public string TagKhongDau
        {
            get
            {
                return tagKhongDau;
            }

            set
            {
                tagKhongDau = value;
            }
        }

        public int ViewTime
        {
            get
            {
                return viewTime;
            }

            set
            {
                viewTime = value;
            }
        }
        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }



        public int CategoryId
        {
            get
            {
                return categoryId;
            }

            set
            {
                categoryId = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }

        public BlogItem()
        {
            CategoryName = "";
            CategoryId = 0;
            Thumb = "";
            Content = "";
            ShotDesc = "";
            CreateDate = "";
            Title = "";
            Thumb = "";
            Id = 0;
            ViewTime = 0;
            TagKhongDau = "";
            Tag = "";
        }
    }
    public class BlogCategory
    {
        int id;
        string name, thumb;
        public BlogCategory()
        {
            id = 0;
            Name = "";
            Thumb = "";
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

        public string Thumb
        {
            get
            {
                return thumb;
            }

            set
            {
                thumb = value;
            }
        }
    }
}