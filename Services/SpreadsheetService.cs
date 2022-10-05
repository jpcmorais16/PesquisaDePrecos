﻿using Data.Interfaces;
using Data.GoogleSheetsConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

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

        public void AppendToSpreadsheet(List<Product> products, List<string> data, string match)
        {
            string range = "a!A:E";
            string id = "";

            List<object> list = new List<object>
            {
                data[0],
                data[1],
                products[0].DomainName,
                match + " " + products[0].Name,
                products[0].Price.ToString() + "R$",

            };

            _connector.Append(range, id, list);

        }
    }
}
