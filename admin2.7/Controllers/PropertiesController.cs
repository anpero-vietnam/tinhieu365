using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AModul.ProductProperties;

using Models;
using Models.Modul.Product;

namespace Web.Mvc.Controllers
{
    public class PropertiesController : BaseController
    {
        IProperties property = new PropertiesControl();
        IPropertiesValue propertyValue = new PropertiesValueControl();
        
        // GET: Properties
        [IsAuthenlication]
        public ActionResult Index()
        {
            SetUpAll();
            return View();
        }
        [IsAuthenlication]
        [HttpPost]
        public JsonResult GetData()
        {
            return Json(property.GetAll());
        }
      
        //[IsAuthenlication]
        //[HttpPost]
        //public JsonResult GetAllDataByProducrId(int productId)
        //{
        //    return Json(property.GetAllDataByProducrId(AnperoClient, productId));
        //}
        [IsAuthenlication]
        [HttpPost]
        public JsonResult GetDataById(int id)
        {
            return Json(property.GetAll().Where(x=>x.Id==id).FirstOrDefault());
        }
        [IsAuthenlication]
        [HttpPost]
        public JsonResult GetValueById(int id)
        {
            
            return Json(propertyValue.GetValueById(id));
        }
        [IsAuthenlication]
        [HttpPost]
        public int Delete(int id)
        {
            return property.Delete(id);
        }
        [IsAuthenlication]
        [HttpPost]
        public JsonResult GetValueByProductId(int productId)
        {

            return Json(propertyValue.GetAllPropertyByProduct(productId));
        }
        [IsAuthenlication]
        [ValidateInput(false)]
        [HttpPost]
        public int AddorUpdateValue(AtributeValue model)
        {
            return property.AddOrUpdateValue(model);
        }
        [IsAuthenlication]
        [HttpPost]
        public int AddorUpdate(AtributeModel model)
        {
            return property.AddOrUpdate(model);
        }
        [IsAuthenlication]
        [HttpPost]
        public int UpdatePriotyRank(int rank,int id)
        {
            return property.UpdatePriotyRank(rank,id);
        }
        [IsAuthenlication]
        [HttpPost]
        public int UpdatePriotyValueRank(int rank, int id)
        {
            return property.UpdatePriotyValueRank(rank, id);
        }
        
        [IsAuthenlication]
        [HttpPost]
        public int DeleteValById(int id)
        {
            return property.DeleteValById(id);
        }       
    }
}