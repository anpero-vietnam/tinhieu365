using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ultil
{
    public static class UrlHelper
    {
        public static string GetRootUrlReferrer()
        {
            try
            {
                 Uri myReferrer = HttpContext.Current.Request.UrlReferrer;
              //  Uri myReferrer= System.Web.HttpContext.Current.Request.Url;
                string actual = myReferrer.PathAndQuery.ToString();
                String[] spitActual = actual.Split('/');
                return spitActual[0] + @"//" + spitActual[2];
            }
            catch (Exception)
            {
 
                return string.Empty;
            }

        }
        public static string GetProductLink(string productName, int productId)
        {
            if (!string.IsNullOrEmpty(productName) && productId > 0)
            {
                return "/project/" + StringHelper.ToURLgach(productName).ToLower() + "-pr" + productId;
            }
            else
            {
                return "/";
            }
        }
        public static string GetProductGroupLink(string productName, int productId)
        {
            if (!string.IsNullOrEmpty(productName) && productId > 0)
            {
                return "/" + StringHelper.ToURLgach(productName) + "-g" + productId;
            }
            else
            {
                return "/";
            }
        }
        public static string GetProductLink(string parentCatName, string productName, int productId)
        {
            if (!string.IsNullOrEmpty(parentCatName) && !string.IsNullOrEmpty(productName) && productId > 0)
            {
                return "/" + StringHelper.ToURLgach(parentCatName) + "/" + StringHelper.ToURLgach(productName) + "-p" + productId;
            }
            else
            {
                return "/";
            }

        }
        public static string GetDistricLink(string districName, int CategoryId)
        {
            if (!string.IsNullOrEmpty(districName) && !string.IsNullOrEmpty(districName) && CategoryId > 0)
            {
                return "/" + StringHelper.ToURLgach(districName) + "-d" + CategoryId;
            }
            else
            {
                return "/";
            }

        }
        public static string GetCategoryLink(string categoryName, int CategoryId)
        {
            if (!string.IsNullOrEmpty(categoryName) && !string.IsNullOrEmpty(categoryName) && CategoryId > 0)
            {
                return "/" + StringHelper.ToURLgach(categoryName) + "-c" + CategoryId;
            }
            else
            {
                return "/";
            }

        }
        public static string GetBlogCategoryLink(string categoryName, int CategoryId)
        {
            if (!string.IsNullOrEmpty(categoryName) && !string.IsNullOrEmpty(categoryName) && CategoryId > 0)
            {
                return "/" + StringHelper.ToURLgach(categoryName) + "-b" + CategoryId;
            }
            else
            {
                return "/";
            }

        }
        public static string GetArticleLink(string ArticleTitle, int ArticleId)
        {
            if (!string.IsNullOrEmpty(ArticleTitle) && !string.IsNullOrEmpty(ArticleTitle) && ArticleId > 0)
            {
                return "/" + StringHelper.ToURLgach(ArticleTitle) + "-a" + ArticleId;
            }
            else
            {
                return "/";
            }

        }

        public static string GetParentCategoryLink(string parentCategoryName, int parentCategoryId)
        {
            if (!string.IsNullOrEmpty(parentCategoryName) && !string.IsNullOrEmpty(parentCategoryName) && parentCategoryId > 0)
            {
                return "/" + StringHelper.ToURLgach(parentCategoryName) + "-c" + parentCategoryId;
            }
            else
            {
                return "/";
            }
        }

    }
}
