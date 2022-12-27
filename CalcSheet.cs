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

        private const string _acceptedWordBitsFileName = "Lookups\\originalWordBits.json";
        private List<string> _acceptedWordBits;

        public CalcSheet(string fileName) 
        {
            _fileName = fileName;
            _excelApp = new Application();
            _workbook = _excelApp.Workbooks.Open(fileName);
            _excelApp.Visible = false;

            // Load accepted original word bits .json file
            GetAcceptedOriginalWordBits(_acceptedWordBitsFileName);
        }

        private void GetAcceptedOriginalWordBits(string fileName)
        {
            FileStream file = File.OpenRead(fileName);

            JsonNode js = JsonObject.Parse(file);

            JsonNode jsonWordBits = js["originalWordBits"];

            List<string> words = new List<string>();

            foreach (string word in jsonWordBits.AsArray())
            {
                words.Add(word);
            }

            _acceptedWordBits = words;
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
            }
        }

        public void Close()
        {
            _workbook.Close();
            _excelApp.Quit();
        }
    }
}
