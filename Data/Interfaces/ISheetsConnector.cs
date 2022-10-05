using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISheetsConnector
    {
        public List<List<string>> ReadDataFromSpreadsheet(string range, string spreadsheetId);
        public void Append(string range, string spreadsheetId, List<object> values);
    }
}
