using Data.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GoogleSheetsConnection
{
    public class GoogleSheetsConnector: ISheetsConnector
    {
        private SheetsService _sheetsService;
        private string _spreadSheetId;

        public GoogleSheetsConnector(string credPath)
        {
            //string credPath = @"C:\Users\Trilogo\Desktop\credentials\credentials.json";

            var json = File.ReadAllText(credPath);
            PersonalServiceAccountCred cr = JsonConvert.DeserializeObject<PersonalServiceAccountCred>(json); // "personal" service account credential

            // Create an explicit ServiceAccountCredential credential
            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cr.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(cr.private_key));

            // Create the service
            _sheetsService = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                }
            ); 
        }

        public List<List<string>> ReadDataFromSpreadsheet(string range, string spreadsheetId)
        {
            //var range = "a!A1:A2";
            //"1_7qcTh67UXl3_kV-osvVJts8Owv-PUt5KuIdJnGaPXc"
            var request = _sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            var a = response.Values[0];

            
            List<List<string>> result = new List<List<string>>();

            foreach(List<object> row in response.Values)
            {
                List<string> data = new List<string>
                {
                     row[0].ToString(), row[1].ToString() 
                };

                /*foreach (object value in column)
                {
                    data.Add(value.ToString());
                }*/
                result.Add(data);
            }

            return result;

        }

    }
}
