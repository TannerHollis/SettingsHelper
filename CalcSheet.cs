using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private const string _acceptedWordBitsFileName = "originalWordBits.json";
        private List<string> _acceptedWordBits;

        public CalcSheet(string fileName) 
        {
            _fileName = fileName;
            _excelApp= new Application();
            _workbook = _excelApp.Workbooks.Open(fileName);
            _excelApp.Visible = false;

            // Load accepted original word bits .json file
            GetAcceptedOriginalWordBits(_acceptedWordBitsFileName);
        }

        private void GetAcceptedOriginalWordBits(string fileName)
        {
            FileStream file = File.OpenRead(fileName);

            JsonNode js = JsonObject.Parse(file);

            _acceptedWordBits = js["originalWordBits"].GetValue<List<string>>();
        }

        public void PrintNames()
        {
            Names names = _workbook.Names;
            foreach (Name name in names)
            {
                if (_acceptedWordBits.Contains(name.Name))
                {
                    Console.WriteLine(name.Name + ": " + name.RefersToRange.Text);
                }

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
