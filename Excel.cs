using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _excel = Microsoft.Office.Interop.Excel;

namespace Charger
{
    class Excel
    {
        String path = "";
        _Application excel = new  _excel.Application();

        Workbook wb;
        Worksheet ws;

        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = excel.Worksheets[sheet];
        }

        public void WriteStrToCell(int str, int colum, string WriteStr)
        {
            ws.Cells[str, colum].Value2 = WriteStr;
        }

        public void Save()
        {
            wb.Save();
        }

        public void  SaveAs(string path)
        {
            wb.SaveAs(path);
        }


    }
}
