using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Modul.Product
{
    public class AtributeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Images { get; set; }
        public int Rank { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string AtributeValueJson { get; set; }
        
        public List<AtributeValue> Values {
            get {
                if (!string.IsNullOrEmpty(AtributeValueJson))
                {
                    return JsonConvert.DeserializeObject<List<AtributeValue>>(AtributeValueJson);
                }
                else
                {
                    return new List<AtributeValue>();
                }
                
            }
          
        }
        public AtributeModel()
        {
            Id = 0;
            Images = string.Empty;
            Rank = 0;
            Description = string.Empty;
            Keywords = string.Empty;
            
        }
    }
}
