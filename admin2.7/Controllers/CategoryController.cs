using AModul.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace admin.Controllers
{
  
    public class CategoryController : BaseController
    {
        // GET: Category
        [IsAuthenlication]
        public JsonResult GetCategoryById(int id)
        {
            Category cat = new Category();
          return  Json(cat.GetCatById(id));
        }
        [IsAuthenlication]
        [ValidateInput(false)]
        public int UpdateCateGory(int id,int prioty, string desc, string keyword, string name,string thumb,int parentId)
        {
            Category ca = new Category();
            int rs = ca.UpdateCategory(Convert.ToInt32(id), name, Convert.ToInt32(prioty), desc, keyword, thumb, parentId);
            return rs;
        }
        [IsAuthenlication]
        [ValidateInput(false)]

        public int DeleteCateGory(int id)
        {

            try
            {
                Category cat = new Category();
                cat.DeleteCategory(id).ToString();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
          
    }
}