using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace SettingsHelper
{
    public class SettingsTranslator
    {
        private string _relayType;
        private RelayLookup _relayLookup;

        public static void ExtractFromXLSX(string fileName)
        {
            // Open Excel and workbook
            Application app = new Application();
            app.Visible = false;
            Workbook workbook = app.Workbooks.Open(fileName);

            // Declare list of worksheets
            List<Worksheet> worksheets = new List<Worksheet>();

            for (int i = 2; i < workbook.Sheets.Count + 1; i++)
            {
                Worksheet currentWorksheet = workbook.Sheets[i];
                worksheets.Add(currentWorksheet);   
            }

            // Clear relay file associations before adding new ones
            RelayFiles.ClearRelayAssociations();

            foreach(Worksheet worksheet in worksheets)
            {
                worksheet.Select();

                string filePath = Path.Combine(FileManager.LookupsFolder, worksheet.Name);

                if(File.Exists(filePath + ".csv"))
                    File.Delete(filePath + ".csv");

                // Save file
                worksheet.SaveAs(filePath, XlFileFormat.xlCSV);

                // Convert .csv to .json
                RelayLookup lookup = RelayLookup.FromCSVFile(filePath + ".csv");
                lookup.ToFile(filePath + ".json");

                // Create new relay file association
                RelayFileAssociation assoc = new RelayFileAssociation();
                assoc.RelayType = worksheet.Name;
                assoc.RDBTemplate = Path.Combine(FileManager.RDBTemplatesFolder, worksheet.Name + ".rdb");
                assoc.TranslationLayerFile = filePath + ".json";

                // Add relay file association
                RelayFiles.AddRelayAssociation(assoc);
            }

            workbook.Close();
            app.Quit();
        }

        public SettingsTranslator(string relayType)
        {
            _relayType = relayType;
            _relayLookup = RelayLookup.FromFile(FileManager.GetTranslationLayerFile(_relayType));
        }

        public string[] LookupWordBit(string genericWordBit)
        {
            return _relayLookup.GetRelayWordBits(genericWordBit);
        }

        public WordBitLookup[] GetWordBitLookupReverse(string relayWordBit)
        {
            return _relayLookup.GetWordBitLookupReverse(relayWordBit);
        }

        public WordBitLookup GetRelayWordBitLookup(string relayWordBit)
        {
            return _relayLookup.GetRelayWordBitLookup(relayWordBit);
        }
    }
}
