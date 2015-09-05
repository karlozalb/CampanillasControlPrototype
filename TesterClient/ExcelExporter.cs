using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesterClient
{
    class ExcelExporter
    {
        public ExcelExporter()
        {

        }

        public void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + ";";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + ";";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        public void toXLS(string ptitle,string pworksheetname, DataGridView dGV, string filename)
        {
            FileInfo newFile = new FileInfo(filename);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(filename);
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(pworksheetname);

                worksheet.View.PageLayoutView = true;

                package.Workbook.Properties.Title = ptitle;


                for (int j = 0; j < dGV.Columns.Count; j++)
                {
                    worksheet.Cells[1, j + 1].Value = dGV.Columns[j].HeaderText;
                    worksheet.Cells[1, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, j + 1].Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    worksheet.Cells[1, j + 1].Style.Font.Color.SetColor(Color.White);
                }

                for (int i = 0; i < dGV.RowCount; i++)
                {                    
                    for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dGV.Rows[i].Cells[j].Value;
                        if (j == 0)
                        {
                            worksheet.Cells[i + 2, j + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[i + 2, j + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }                        
                    }                       
                }

                worksheet.Cells.AutoFitColumns(0);
                worksheet.HeaderFooter.OddHeader.CenteredText = "&12&U&\"Arial,Regular Bold\"" + ptitle;

                package.Save();
            }
        }        
    }
}


