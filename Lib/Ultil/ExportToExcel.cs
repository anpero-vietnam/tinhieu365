using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;


namespace Ultil
{
    public static class ExportToExcel
    {
        /// <summary>
        /// Export DataTable to Excel file
        /// </summary>
        /// <param name="DataTable">Source DataTable</param>
        /// <param name="ExcelFilePath">Path to result file name</param>
        public static string ExportToFiles(System.Data.DataTable Table, string filePath, string extenstion)
        {
            String DirectorysPath = HttpContext.Current.Server.MapPath(filePath + "/");

            try
            {

                if (!System.IO.Directory.Exists(DirectorysPath))
                {
                    System.IO.Directory.CreateDirectory(DirectorysPath);

                }
                if (System.IO.File.Exists(DirectorysPath + "/data" + extenstion))
                {
                    System.IO.File.WriteAllText(DirectorysPath + "/data" + extenstion, String.Empty);
                }
                else
                {

                    System.IO.File.Create(DirectorysPath + "/data" + extenstion);
                }

                if (Table != null && Table.Rows.Count > 0)
                {
                    string s = "";

                    foreach (DataColumn column in Table.Columns)
                    {

                        s += column.ColumnName + "\t";
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(DirectorysPath + "/data" + extenstion, false, Encoding.UTF8))
                    {
                        file.WriteLine(s);
                        for (int i = 0; i < Table.Rows.Count; i++)
                        {
                            string z = "";
                            for (int j = 0; j < Table.Columns.Count; j++)
                            {
                                string value = "";
                                if (DBNull.Value != Table.Rows[i][j] && !string.IsNullOrEmpty(Table.Rows[i][j].ToString()))
                                {
                                    value = Table.Rows[i][j].ToString();
                                }
                                z += Ultil.StringHelper.RemoveHtmlTangs(Table.Rows[i][j].ToString()) + "\t";
                            }
                            file.WriteLine(z);
                        }
                        file.Close();
                        file.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
            return "";
        }
        public static void AutoCreateSheet(ExcelWorksheet ws, string reportName, DataTable DataTables, DateTime fromDate, DateTime toDate, string mapPingColunmName, List<string> ignoreColunm)
        {
            ws.Cells["B1"].Value = reportName;
            ws.Cells["B2"].Value = string.Format("Report Dates: {0} - {1}", fromDate.ToString("d.M.yyyy"), toDate.ToString("d.M.yyyy"));
            int startRow = 4;
            List<string> columnNames = new List<string>();
            int ignoreCell = 0;
            for (int i = 0; i < DataTables.Columns.Count; i++)
            {
                if (ignoreColunm != null)
                {
                    if (!ignoreColunm.Contains(DataTables.Columns[i].ColumnName))
                    {

                        columnNames.Add(DataTables.Columns[i].ColumnName);
                        ws.Cells[startRow - 1, i + 1 - ignoreCell].Value = Ultil.StringHelper.MultiTextReplace(DataTables.Columns[i].ColumnName, mapPingColunmName);
                        ws.Cells[startRow - 1, i + 1 - ignoreCell].Style.FillPattern.SetPattern(FillPatternStyle.Solid, System.Drawing.Color.Aqua, System.Drawing.Color.Black);
                        ws.Cells[startRow - 1, i + 1 - ignoreCell].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        ws.Cells[startRow - 1, i + 1 - ignoreCell].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        ws.Cells[startRow - 1, i + 1 - ignoreCell].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        ws.Cells[startRow - 1, i + 1 - ignoreCell].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Chocolate, LineStyle.Thin);
                    }
                    else
                    {
                        ignoreCell += 1;
                    }
                }
                else
                {
                    columnNames.Add(DataTables.Columns[i].ColumnName);
                    ws.Cells[startRow - 1, i + 1].Value = Ultil.StringHelper.MultiTextReplace(DataTables.Columns[i].ColumnName, mapPingColunmName);

                    ws.Cells[startRow - 1, i + 1].Style.FillPattern.SetPattern(FillPatternStyle.Solid, System.Drawing.Color.Aqua, System.Drawing.Color.Black);
                    ws.Cells[startRow - 1, i + 1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Chocolate, LineStyle.Thin);
                    ws.Cells[startRow - 1, i + 1].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Chocolate, LineStyle.Thin);
                    ws.Cells[startRow - 1, i + 1].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Chocolate, LineStyle.Thin);
                    ws.Cells[startRow - 1, i + 1].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Chocolate, LineStyle.Thin);
                }
            }
            if (DataTables != null && DataTables.Rows.Count > 0)
            {
                for (int i = 0; i < DataTables.Rows.Count; i++)
                {
                    for (int j = 0; j < columnNames.Count; j++)
                    {

                        if (DBNull.Value == DataTables.Rows[i][columnNames[j]])
                        {
                            if (!columnNames[j].ToLower().Contains("date"))
                            {
                                ws.Cells[startRow + i, j + 1].Value = "no";
                            }

                        }
                        else
                        {
                            ws.Cells[startRow + i, j + 1].Value = DataTables.Rows[i][columnNames[j]].ToString();
                        }
                        #region style
                        if ((startRow + i) % 2 == 0)
                        {
                            ws.Cells[startRow + i, j + 1].Style.FillPattern.SetPattern(FillPatternStyle.Solid, System.Drawing.Color.White, System.Drawing.Color.Black);

                        }
                        else
                        {
                            ws.Cells[startRow + i, j + 1].Style.FillPattern.SetPattern(FillPatternStyle.Solid, System.Drawing.Color.FromArgb(219, 229, 241), System.Drawing.Color.Black);
                        }
                        ws.Cells[startRow + i, j + 1].Style.Borders.SetBorders(MultipleBorders.Bottom, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        ws.Cells[startRow + i, j + 1].Style.Borders.SetBorders(MultipleBorders.Left, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        ws.Cells[startRow + i, j + 1].Style.Borders.SetBorders(MultipleBorders.Right, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        ws.Cells[startRow + i, j + 1].Style.Borders.SetBorders(MultipleBorders.Top, System.Drawing.Color.Chocolate, LineStyle.Thin);
                        #endregion style
                    }
                }
            }
        }
              
    }
}
