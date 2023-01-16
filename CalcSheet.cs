using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
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
            _excelApp = new Application();
            _workbook = _excelApp.Workbooks.Open(fileName);
            _excelApp.Visible = true;
        }

        public string GetName(string name)
        {
            string ret = string.Empty;
            Names workBookNames = _workbook.Names;
            foreach (Name workBookName in workBookNames)
            {
                if (workBookName.Name.Equals(name))
                {
                    ret = workBookName.RefersToRange.Text;
                    break;
                }
            }
            return ret;
        }

        public void SetName(string name, string value)
        {
            Names workBookNames = _workbook.Names;
            foreach (Name workBookName in workBookNames)
            {
                if (workBookName.Name.Equals(name))
                {
                    Range range = workBookName.RefersToRange;
                    range.Value2 = value;
                    break;
                }
            }
        }

        public void Save()
        {
            _workbook.Save();
        }

        public void Close()
        {
            _workbook.Close(false);
            _excelApp.Quit();
            while (Marshal.ReleaseComObject(_workbook) != 0) { }
            while (Marshal.ReleaseComObject(_excelApp) != 0) { }
        }
    }
}
