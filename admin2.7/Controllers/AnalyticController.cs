using AModul.Common;
using Dal;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Controllers;

namespace admin.Controllers
{
    public class AnalyticController : BaseController
    {

        public ActionResult Index(int month, int day, string type)
        {

            if (type == null) { type = "pr"; }
            if (Security.Store.IsInRole(AEnum.UserRole.CanViewAnalytic))
            {
                SetUpAll();
                ViewBag.DefaultTable = GetAnalyticHtmlTable(month, day, type);
            }
            return View();
        }        
        public ActionResult Export3(int month, int day, string type)
        {
            if (type == null) { type = "pr"; }
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, AEnum.UserRole.CanViewAnalytic))
            {
                Analytic an = new Analytic();
                var model = an.GetRequesAnalytic(DateTime.Now.Year, month, day, type);
                DateTime nowDate = DateTime.Now;
                Ultil.IConvertHelper<Models.Modul.Common.RequesAnalyticModel> cv = new Ultil.IConvertHelper<Models.Modul.Common.RequesAnalyticModel>();
                var table = cv.CreateDataTable(model);
                if (table != null)
                {
                    if (day == 0 && month == 0)
                    {


                        string MapingColumName = "PrName>Tiêu đề,M1>Tháng 1,M2>T2,M3>T3,M4>T4,M5>T5,M6>T6,M7>T7,M8>T8,M9>T9,M10>T10,M11>T11,T12";
                        DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                        base.Export("Report-Year-" + DateTime.Now.Year, RebuldTableToYearTable(table), firstDayCurrentYear, nowDate, null, MapingColumName);
                    }
                    else if (day == 0 && month > 0)
                    {
                        string MapingColumName = "PrName>Tiêu đề";
                        DateTime firstDayCurrentMonth = new DateTime(nowDate.Year, nowDate.Month, 1);
                        base.Export("Report-Month-" + DateTime.Now.Month + " năm" + DateTime.Now.Year, RebuldTableToMonthTable(table), firstDayCurrentMonth, nowDate, null, MapingColumName);
                    }
                }
              
            }
            return View();
        }
        public string Get(int month, int day, string type, int st)
        {
            if (type == null) { type = "pr"; }
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, AEnum.UserRole.CanViewAnalytic))
            {

                return GetAnalyticHtmlTable(month, day, type);
            }
            else
            {
                return "";
            }

        }
        public string GetAnalyticHtmlTable(int month, int day, string type)
        {
            string rs = "";
            int year = DateTime.Now.Year;
            Analytic an = new Analytic();
            if (month == 0)
            {
                var currentDate = DateTime.Now;                
                month = currentDate.Month;
                year = currentDate.Year;
            }
            var model  = an.GetRequesAnalytic(DateTime.Now.Year, month, day, type);
            Ultil.IConvertHelper<Models.Modul.Common.RequesAnalyticModel> convert = new Ultil.IConvertHelper<Models.Modul.Common.RequesAnalyticModel>();
            var table = convert.CreateDataTable(model);
            if (table != null && table.Rows.Count > 0)
            {
                if (day == 0 && month == 0)
                {

                    string MapingColumName = "PrName>Tiêu đề,MTD>Lượt tháng trước";
                    rs = Ultil.StringHelper.AutoCreateHtmlTable(RebuldTableToYearTable(table), MapingColumName, null);
                }
                else if (day == 0 && month > 0)
                {
                    string MapingColumName = "PrName>Tiêu đề,YTD>Lượt xem từ đầu tháng";
                    rs = Ultil.StringHelper.AutoCreateHtmlTable(RebuldTableToMonthTable(table), MapingColumName, null);
                }
            }
            return rs;
        }
        private DataTable RebuldTableToMonthTable(DataTable table)
        {
            DataTable tempTable = new DataTable();
            DataColumn cl1 = new DataColumn();
            cl1.ColumnName = "PrName";
            tempTable.Columns.Add(cl1);
            var ReferrerIDList = (from r in table.AsEnumerable()
                                  select r["ReferrerID"]).Distinct().ToList();
            //for (int i = 0; i < 30; i++)
            //{
            //    DataColumn cl2 = new DataColumn();
            //    cl2.ColumnName = "D" + (i + 1).ToString();
            //    tempTable.Columns.Add(cl2);
            //}
            DataColumn cl2 = new DataColumn();
            cl2.ColumnName = "YTD";
            tempTable.Columns.Add(cl2);
            for (int i = 0; i < ReferrerIDList.Count; i++)
            {
                DataRow r = tempTable.NewRow();
                r["PrName"] = table.Select("ReferrerID=" + ReferrerIDList[i].ToString())[0]["Tittle"];
                for (int j = 0; j < 30; j++)
                {
                    int sum = 0;
                    //String query = "ReferrerID=" + ReferrerIDList[i].ToString() + " and DD=" + (j + 1).ToString();
                    String query = "ReferrerID=" + ReferrerIDList[i].ToString();
                    DataRow[] result = table.Select(query);
                    if (result != null && result.Count() > 0)
                    {
                        for (int k = 0; k < result.Count(); k++)
                        {
                            sum += Convert.ToInt32(result[k]["RequestCount"]);
                        }
                    }
                    r["YTD"] = sum.ToString();
                }
                tempTable.Rows.Add(r);
            }
            return tempTable;
        }
        public void ExportToExcel(int month, int day, string type)
        {

            if (type == null) { type = "pr"; }
            if (UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId,AEnum.UserRole.CanViewAnalytic))
            {
                Analytic an = new Analytic();
                var model = an.GetRequesAnalytic(DateTime.Now.Year, month, day, type);
                Ultil.IConvertHelper<Models.Modul.Common.RequesAnalyticModel> cv = new Ultil.IConvertHelper<Models.Modul.Common.RequesAnalyticModel>();
                var table = cv.CreateDataTable(model);
                DateTime nowDate = DateTime.Now;
                if (day == 0 && month == 0)
                {
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    string MapingColumName = "PrName>Tiêu đề,M1>T1,M2>T2,M3>T3,M4>T4,M5>T5,M6>T6,M7>T7,M8>T8,M9>T9,M10>T10,M11>T11,T12>Tháng 12";
                    base.Export("Report-Year-" + DateTime.Now.Year, RebuldTableToYearTable(table), firstDayCurrentYear, nowDate, null, MapingColumName);
                }
                else if (day == 0 && month > 0)
                {

                    DateTime firstDayCurrentMonth = new DateTime(nowDate.Year, nowDate.Month, 1);
                    string MapingColumName = "PrName>Tiêu đề";
                    base.Export("Report-Month-" + DateTime.Now.Month + "-" + DateTime.Now.Year, RebuldTableToMonthTable(table), firstDayCurrentMonth, nowDate, null, MapingColumName);
                }
            }
        }
        private DataTable RebuldTableToYearTable(DataTable table)
        {
            DataTable tempTable = new DataTable();
            DataColumn cl1 = new DataColumn();
            cl1.ColumnName = "PrName";
            tempTable.Columns.Add(cl1);
            var ReferrerIDList = (from r in table.AsEnumerable()
                                  select r["ReferrerID"]).Distinct().ToList();
            //for (int i = 0; i < 12; i++)
            //{
            //    DataColumn cl2 = new DataColumn();
            //    cl2.ColumnName = "M" + (i + 1).ToString();
            //    tempTable.Columns.Add(cl2);
            //}
            DataColumn cl2 = new DataColumn();
            cl2.ColumnName = "MTD";
            tempTable.Columns.Add(cl2);
            for (int i = 0; i < ReferrerIDList.Count; i++)
            {
                DataRow r = tempTable.NewRow();
                r["PrName"] = table.Select("ReferrerID=" + ReferrerIDList[i].ToString())[0]["Tittle"];

                int sum = 0;
                String query = "ReferrerID=" + ReferrerIDList[i].ToString();
                DataRow[] result = table.Select(query);
                if (result != null && result.Count() > 0)
                {
                    for (int k = 0; k < result.Count(); k++)
                    {
                        sum += Convert.ToInt32(result[k]["RequestCount"]);
                    }
                }
                r["MTD"] = sum.ToString();
                tempTable.Rows.Add(r);
            }



            return tempTable;
        }

    }
}