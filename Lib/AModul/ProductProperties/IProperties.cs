using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Modul;
using Models.Modul.Product;

namespace AModul.ProductProperties
{
    public interface IProperties
    {
        //List<Models.ProductProperties> GetAllDataByProducrId(AnperoClient client, int productId);
        int AddOrUpdate(AtributeModel model);
        int AddOrUpdateValue(AtributeValue model);
        int UpdatePriotyRank(int rank, int id);
        int UpdatePriotyValueRank(int rank, int id);
        int Delete(int id);
        List<AtributeModel> GetAll();
        
        int DeleteValById(int id);
        List<AtributeModel> GetDataOfProductOnly(int productId);
    }
}
