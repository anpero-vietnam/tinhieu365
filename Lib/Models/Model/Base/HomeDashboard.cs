using Models.Notify;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HomeDashboard
    {   
        public int OrderWaiting { get; set; }
        public int WaitingContact { get; set; }
        public int ViewToday { get; set; }
        public int OrderPaider { get; set; }
        public List<SysNotify> AllSynNotify { get; set; }
        public int RequestToday { get; set; }
        public List<UserMessenge> UserMessengeList { get; set; }
        public int WaitingSynNotify { get; set; }
        public int WaitingMessage { get; set; }
        public HomeDashboard()
        {
            WaitingMessage = 0;
            WaitingSynNotify = 0;
            UserMessengeList = new List<UserMessenge>();
            AllSynNotify = new List<SysNotify>();
            RequestToday = 0;
            OrderPaider = 0;
            ViewToday = 0;
            WaitingContact = 0;
            OrderWaiting = 0;
        }
    }
   
}
