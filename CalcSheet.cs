using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Excel;

namespace SettingsHelper
{
    public class CalcSheet
    {
        private string _fileName;
        private Application _excelApp;
        private Workbook _workbook;
        
        public CalcSheet(string fileName) 
        {
            _fileName = fileName;
            _excelApp= new Application();
            _workbook = _excelApp.Workbooks.Open(fileName);
            _excelApp.Visible = false;
        }

        public void PrintNames()
        {
            Names names = _workbook.Names;
            foreach (Name name in names)
            {
                Console.WriteLine(name.Name + ": " + name.RefersTo);
                // Range range = name.RefersToRange;
                // Console.WriteLine(" - " + range.Text);
            }
        }

        public void Close()
        {
            _workbook.Close();
        }
    }
}
