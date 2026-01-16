using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.Modul.WareHouse
{
    public class SeriaModel 
    {
        
        public SeriaModel()
        {
            Id = new Guid();
            ProductId = 0;
            Seria = string.Empty;
            Warranty = 0;
            IsOneTimeUsing = true;
            AdminSearchOnly = true;
            AgenId = 0;
            CreateDate = DateTime.Now;
            IsUsed = false;
        }
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Seria { get; set; }
        public int Warranty { get; set; }
        public bool IsOneTimeUsing { get; set; }
        public bool AdminSearchOnly { get; set; }
        public int AgenId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsUsed { get; set; }
        
    }
}
