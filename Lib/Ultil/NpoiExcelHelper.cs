using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Ultil
{
    public class NpoiExcelHelper
    {
        public DataTable GetDataTableFromExcel(Stream stream, string fileName)
        {
            DataTable dataTable = new DataTable();
            ISheet sh = null;
            //HSSFWorkbook hssfwb;
            if (stream != null)
            {
                try
                {
                    string Ext = System.IO.Path.GetExtension(fileName); //<-Extension del archivo
                    switch (Ext.ToLower())
                    {
                        case ".xls":
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream);
                            sh = hssfwb.GetSheetAt(0);
                            break;

                        case ".xlsx":
                            XSSFWorkbook xssfwb = new XSSFWorkbook(stream);
                            sh = xssfwb.GetSheetAt(0);
                            break;
                    }
                    if (sh != null)
                    {

                        var excelRows = sh.LastRowNum + 1;
                        var maxColunmExcel = 0;
                        for (int k = 0; k < excelRows; k++)
                        {
                            if (sh.GetRow(k) != null && sh.GetRow(k).Cells != null)
                            {
                                int tempMax = sh.GetRow(k).Cells.Count;
                                if (tempMax > maxColunmExcel)
                                {
                                    maxColunmExcel = tempMax;
                                }
                            }

                        }
                        for (int i = 0; i < maxColunmExcel; i++)
                        {
                            dataTable.Columns.Add("", typeof(string));
                        }
                        for (int i = 0; i < excelRows; i++)
                        {
                            dataTable.Rows.Add();
                        }
                        // add row

                        for (int i = 0; i < excelRows; i++)
                        {
                            // add neccessary columns

                            // write row value
                            for (int j = 0; j < maxColunmExcel; j++)
                            {
                                try
                                {
                                    var cell = sh.GetRow(i).GetCell(j);

                                    if (cell != null)
                                    {
                                        // TODO: you can add more cell types capatibility, e. g. formula
                                        switch (cell.CellType)
                                        {
                                            case NPOI.SS.UserModel.CellType.Numeric:
                                                dataTable.Rows[i][j] = sh.GetRow(i).GetCell(j).NumericCellValue;
                                                //dataGridView1[j, i].Value = sh.GetRow(i).GetCell(j).NumericCellValue;

                                                break;
                                            case NPOI.SS.UserModel.CellType.String:
                                                dataTable.Rows[i][j] = sh.GetRow(i).GetCell(j).StringCellValue;

                                                break;
                                        }
                                    }
                                    else
                                    {
                                        dataTable.Rows[i][j] = string.Empty;
                                    }
                                }
                                catch (Exception)
                                {
                                    dataTable.Rows[i][j] = string.Empty;

                                }

                            }


                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            stream.Close();
            stream.Dispose();
            return dataTable;
        }
        public static void DataTableToExcel(DataTable pDatos, string pFilePath, string sheetName, string mapPingColunmName, List<string> ignoreColunm)
        {
            ignoreColunm = ignoreColunm ?? new List<string>();
            try
            {
                if (pDatos != null && pDatos.Rows.Count > 0)
                {
                    IWorkbook workbook = null;
                    ISheet worksheet = null;


                    using (FileStream stream = new FileStream(pFilePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        string Ext = System.IO.Path.GetExtension(pFilePath); //<-Extension del archivo
                        switch (Ext.ToLower())
                        {
                            case ".xls":
                                HSSFWorkbook workbookH = new HSSFWorkbook();
                                NPOI.HPSF.DocumentSummaryInformation
                                dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                                dsi.Company = "Anpero"; dsi.Manager = "Anpero co";
                                workbookH.DocumentSummaryInformation = dsi;
                                workbook = workbookH;
                                break;

                            case ".xlsx": workbook = new XSSFWorkbook(); break;
                        }

                        worksheet = workbook.CreateSheet(sheetName); //<-Usa el nombre de la tabla como nombre de la Hoja    
                        ICellStyle hearderStyle = CreateHearderStyle(workbook);
                        ICellStyle cellStyle = CreateCellStyle(workbook);
                        
                        //CREAR EN LA PRIMERA FILA LOS TITULOS DE LAS COLUMNAS
                        int iRow = 0;
                        if (pDatos.Columns.Count > 0)
                        {
                            int iCol = 0;
                            IRow fila = worksheet.CreateRow(iRow);

                            foreach (DataColumn columna in pDatos.Columns)
                            {
                             
                                
                                if (!ignoreColunm.Contains(columna.ColumnName))
                                {
                                    ICell cell = fila.CreateCell(iCol, CellType.String);
                                    cell.CellStyle = hearderStyle;
                                    cell.SetCellValue(Ultil.StringHelper.MultiTextReplace(columna.ColumnName, mapPingColunmName));
                                    iCol++;
                                }
                            }
                            iRow++;
                        }

                        //FORMATOS PARA CIERTOS TIPOS DE DATOS
                        ICellStyle _doubleCellStyle = CreateCellStyle(workbook); ;
                        _doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.###");

                        ICellStyle _intCellStyle = CreateCellStyle(workbook); ;
                        _intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

                        ICellStyle _boolCellStyle = CreateCellStyle(workbook); ;
                        _boolCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("BOOLEAN");

                        ICellStyle _dateCellStyle = CreateCellStyle(workbook); ;
                        _dateCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-MM-yyyy");

                        ICellStyle _dateTimeCellStyle = CreateCellStyle(workbook); ;
                        _dateTimeCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd-MM-yyyy HH:mm:ss");

                        //AHORA CREAR UNA FILA POR CADA REGISTRO DE LA TABLA
                        foreach (DataRow row in pDatos.Rows)
                        {
                            IRow fila = worksheet.CreateRow(iRow);
                            int iCol = 0;
                            for (int i = 0; i < pDatos.Columns.Count; i++)
                            {
                                DataColumn column = pDatos.Columns[i];
                            //}
                            //foreach (DataColumn column in pDatos.Columns)
                            //{
                              

                                if (!ignoreColunm.Contains(column.ColumnName))
                                {
                                    ICell cell = null; //<-Representa la celda actual                               
                                    object cellValue = row[i]; //<- El valor actual de la celda

                                    switch (column.DataType.ToString())
                                    {
                                        case "System.Boolean":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.Boolean);

                                                if (Convert.ToBoolean(cellValue)) { cell.SetCellFormula("TRUE()"); }
                                                else { cell.SetCellFormula("FALSE()"); }

                                                cell.CellStyle = _boolCellStyle;
                                            }
                                            break;

                                        case "System.String":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.String);
                                                cell.SetCellValue(Convert.ToString(cellValue));
                                                cell.CellStyle = cellStyle;
                                            }
                                            break;

                                        case "System.Int32":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.Numeric);
                                                cell.SetCellValue(Convert.ToInt32(cellValue));
                                                cell.CellStyle = _intCellStyle;
                                            }
                                            break;
                                        case "System.Int64":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.Numeric);
                                                cell.SetCellValue(Convert.ToInt64(cellValue));
                                                cell.CellStyle = _intCellStyle;
                                            }
                                            break;
                                        case "System.Decimal":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.Numeric);
                                                cell.SetCellValue(Convert.ToDouble(cellValue));
                                                cell.CellStyle = _doubleCellStyle;
                                            }
                                            break;
                                        case "System.Double":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.Numeric);
                                                cell.SetCellValue(Convert.ToDouble(cellValue));
                                                cell.CellStyle = _doubleCellStyle;
                                            }
                                            break;

                                        case "System.DateTime":
                                            if (cellValue != DBNull.Value)
                                            {
                                                cell = fila.CreateCell(iCol, CellType.Numeric);
                                                cell.SetCellValue(Convert.ToDateTime(cellValue));

                                                //Si No tiene valor de Hora, usar formato dd-MM-yyyy
                                                DateTime cDate = Convert.ToDateTime(cellValue);
                                                if (cDate != null && cDate.Hour > 0) { cell.CellStyle = _dateTimeCellStyle; }
                                                else { cell.CellStyle = _dateCellStyle; }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                    iCol++;
                                }
                                
                            }
                            iRow++;
                        }

                        workbook.Write(stream);
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static ICellStyle CreateHearderStyle(IWorkbook workbook)
        {
            ICellStyle cusTomstyle = workbook.CreateCellStyle();
            var font = workbook.CreateFont();
            font.FontHeightInPoints = 10;            
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;


            cusTomstyle.BorderBottom = BorderStyle.Thin;
            cusTomstyle.BorderLeft = BorderStyle.Thin;
            cusTomstyle.BorderRight = BorderStyle.Thin;
            cusTomstyle.BorderTop = BorderStyle.Thin;
            cusTomstyle.TopBorderColor = IndexedColors.Grey80Percent.Index;
            cusTomstyle.LeftBorderColor = IndexedColors.Grey80Percent.Index;
            cusTomstyle.RightBorderColor = IndexedColors.Grey80Percent.Index;
            cusTomstyle.BottomBorderColor = IndexedColors.Black.Index;

            cusTomstyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;            
            cusTomstyle.FillPattern = FillPattern.SolidForeground;
            
            
            cusTomstyle.SetFont(font);
            return cusTomstyle;
        }

        private static ICellStyle CreateCellStyle(IWorkbook workbook)
        {
            ICellStyle cusTomstyle = workbook.CreateCellStyle();
            var font = workbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;

            cusTomstyle.BorderBottom = BorderStyle.Thin;
            cusTomstyle.BorderLeft = BorderStyle.Thin;
            cusTomstyle.BorderRight = BorderStyle.Thin;
            cusTomstyle.BorderTop = BorderStyle.Thin;
            cusTomstyle.TopBorderColor = IndexedColors.Grey50Percent.Index;
            cusTomstyle.LeftBorderColor = IndexedColors.Grey50Percent.Index;
            cusTomstyle.RightBorderColor = IndexedColors.Grey50Percent.Index;
            cusTomstyle.BottomBorderColor = IndexedColors.Grey50Percent.Index;
            //cusTomstyle.FillForegroundColor = IndexedColors.Grey50Percent.Index;
            //cusTomstyle.FillPattern = FillPattern.SolidForeground;
            
            cusTomstyle.SetFont(font);

            return cusTomstyle;
        }


    }
}


