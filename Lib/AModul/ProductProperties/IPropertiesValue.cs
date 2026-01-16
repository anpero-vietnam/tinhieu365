using Models;
using Models.Modul.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AModul.ProductProperties
{
    public interface IPropertiesValue
    {
        AtributeValue GetValueById(int id);
        int AddProductPropertyValue(AtributeValue models, int productId);
        int ResetProductPropertyValue(int productId);
        List<AtributeValue> GetAllPropertyByProduct(int productId);
        List<AtributeValue> GetValueProductCategoryId(int categoryId);
        int Delete(int id);
        
        //int UpdateProductAtribute(int atribiteId, int productId, int price, bool isInstock, int agenId);       

    }
}
