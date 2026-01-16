using AModul.Product;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultil.Cache;
namespace AModul.Common
{
    public class MainAnalytic
    {
        ContactAnalyticControl contactControl = new ContactAnalyticControl();
        ProductAnalyticControl productControl = new ProductAnalyticControl();
        Dal.SysNotify sn = new Dal.SysNotify();
        public HomeDashboard SetUpMainAnalytic()
        {
            HomeDashboard dashboard = new HomeDashboard();
            
            var AllSynNotify = sn.GetSysNotify("%", "%", "-1")??new List<Models.Notify.SysNotify>();            
            var contactModel = contactControl.ContactAnalytic();

            dashboard.AllSynNotify = AllSynNotify.Take(10).ToList();
            dashboard.WaitingSynNotify = AllSynNotify.Count();
            dashboard.WaitingContact = contactModel.WaitingCount;

            var productAnalytic = productControl.GetProductAnalytic()??new Models.Modul.Product.ProductAnalyticItem();
            dashboard.OrderWaiting = productAnalytic.OrderWaiting;
            dashboard.RequestToday = productAnalytic.RequestToday;
            dashboard.OrderPaider = productAnalytic.OrderPaider;

            return dashboard;


        }
    }
}
