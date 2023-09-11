using Services;
using Data.SupermarketConnections;
using Data.GoogleSheetsConnection;

var searchService = new SearchService(new SupermarketConnectorFactory());

var spreadsheetService = new SpreadsheetService(new GoogleSheetsConnector(@""));
StreamWriter writer = new StreamWriter(@"C:\Users\Trilogo\Desktop\pesquisa.txt");

var items = spreadsheetService.GetItemsFromSpreadsheet("Banco de Dados!B5:C640", "");

searchService.CreateFilter(items);

for(int i=0; i < items.Count; i++)
{
    var result = await searchService
        .SearchFilteredItemsInAllConnections(items[i][1]
        //.Replace("un)", "")
        .Replace("(", "")
        .Replace(")", "")
        .ToLower()
        .Split(" ")
        .ToList());

    if (i >= 21)
    {
        
    }
    
    string match = "";
    
    if(result == null)
    {
        try
        {
            match = " (Match imperfeito)";
            result = await searchService
            .SearchItemsInAllConnectionsWithoutRestrictions(items[i][1]
            .Replace("un)", "")
            .Replace("(", "")
            .Replace(")", "")
            .ToLower()
            .Split(" ")
            .ToList());
        }
        catch (Exception e) { }
    }

    if(result != null) { 
        Console.WriteLine(items[i][0] + match  + " " + items[i][1] + ": " + result[0].DomainName + " " + result[0].Name + " " + result[0].Price + "R$");
        writer.WriteLine(items[i][0] + match + " " + items[i][1] + ": " + result[0].DomainName + " " + result[0].Name + " " + result[0].Price + "R$");
        writer.Flush();

        spreadsheetService.AppendToSpreadsheet(result, items[i], match, "d!A:G", "");
    }
}