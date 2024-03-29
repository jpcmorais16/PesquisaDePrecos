﻿using Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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

        public GoogleSheetsConnector(string credPath)
        {


            var json = File.ReadAllText(credPath);
            PersonalServiceAccountCred cr = JsonConvert.DeserializeObject<PersonalServiceAccountCred>(json);
            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cr.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(cr.private_key));


            _sheetsService = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                }
            ); 
        }

        public List<List<string>> ReadDataFromSpreadsheet(string range, string spreadsheetId)
        {

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

        public void Append(string range, string spreadsheetId, List<object> values)
        {
            //var range = "a!A:B";
            var valuerange = new ValueRange();
            //var objectList = new List<object>() { "kk", "sarve" };

            valuerange.Values = new List<IList<object>>() { values };
            var appendRequest = _sheetsService.Spreadsheets.Values.Append(valuerange, spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var response = appendRequest.Execute();
        }

    }
}
