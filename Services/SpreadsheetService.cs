using Data.Interfaces;
using Data.GoogleSheetsConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SpreadsheetService
    {
        public List<List<string>> _spreadsheet { get; set; }
        public ISheetsConnector _connector = new GoogleSheetsConnector(@"");

        public List<List<string>> GetNFirstItemsFromSpreadsheet(int N)
        {
            string range = "Banco de Dados!B5:C640";
            string id = "";

            var response = _connector.ReadDataFromSpreadsheet(range, id);

            return response;


        }
    }
}
