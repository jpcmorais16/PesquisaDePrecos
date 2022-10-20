using Services.Interfaces;
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

        private readonly ISheetsConnector _connector;

        public SpreadsheetService(ISheetsConnector connector)
        {
            _connector = connector;
        }

        public List<List<string>> GetItemsFromSpreadsheet(string range, string id)
        {
            

            var response = _connector.ReadDataFromSpreadsheet(range, id);

            return response;


        }

        public void AppendToSpreadsheet(List<Product> products, List<string> data, string match, string range, string id)
        {
            

            List<object> list = new List<object>
            {
                data[0],
                data[1],
                products[0].DomainName,
                match + " " + products[0].Name,
                "R$" + products[0].Price.ToString(),
                data[0],
                match.Equals("") ? products[0].Name : "",

            };

            _connector.Append(range, id, list);

        }
    }
}
