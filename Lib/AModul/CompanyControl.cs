using AModul.Dapper;
using Models;
using System.Collections.Generic;


namespace AModul
{
    public class CompanyControl : ConnectionProxy<CompanyModel>
    {
        public List<CompanyModel> SearchCompany(string tiker)
        {
            if (tiker != null && tiker.Length > 1)
            {
                Dictionary<string, object> paramlist = new Dictionary<string, object>();

                paramlist.Add("@tiker", tiker);
                return base.Select("sp_searchCompany", paramlist);
            }
            else
            {
                return new List<CompanyModel>();
            }
        }
    }
}
