using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class TagForNews
    {
        public TagForNews()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetNewsByTag(String tag, int currentPage, int pageSite)
        {
            try
            {
                int beginRow = (currentPage - 1) * pageSite + currentPage;
                int endRow = (currentPage - 1) * pageSite + currentPage + pageSite;
                SqlParameter[] paramList = new SqlParameter[3];
                paramList[0] = new SqlParameter("@tag", SqlDbType.VarChar, 200);
                paramList[0].Value = tag;
                paramList[1] = new SqlParameter("@beginRow", SqlDbType.Int);
                paramList[1].Value = beginRow;
                paramList[2] = new SqlParameter("@EndRow", SqlDbType.Int);
                paramList[2].Value = endRow;

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getNewByTag", paramList);
                return tables;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable getTagByTag(String tag)
        {
            try
            {
                checkValid check = new checkValid();
                tag = check.removeHtmlTangs(tag);
                SqlParameter[] paramList = new SqlParameter[1];

                paramList[0] = new SqlParameter("@tag", SqlDbType.VarChar, 200);
                paramList[0].Value = tag;
                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getTagByTag", paramList);
                return tables;

            }
            catch (Exception)
            {

                throw;
            }
        }     

        public DataTable getRanDomTag()
        {
            try
            {

                Dal.DatabaseAccess ds = new Dal.DatabaseAccess();
                DataTable tables = ds.executeSelect("getRandomTags");
                return tables;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
