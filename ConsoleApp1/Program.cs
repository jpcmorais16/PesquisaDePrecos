using Services;
using Data;
using Data.SupermarketConnections;
using Data.GoogleSheetsConnection;

var service = new SearchService(new SupermarketConnectorFactory());

var service2 = new SpreadsheetService(new GoogleSheetsConnector(@"C:\Users\Trilogo\Desktop\credentials"));
StreamWriter writer = new StreamWriter(@"C:\Users\Trilogo\Desktop\pesquisa.txt");




var teste = service2.GetItemsFromSpreadsheet("Banco de Dados!B5:C640", "");

service.CreateFilter(teste);


for(int i=0; i < teste.Count; i++)
{
    if (i == 4)
    {
        
    }
    var result = await service
        .SearchFilteredItemsInAllConnections(teste[i][1]
        //.Replace("un)", "")
        .Replace("(", "")
        .Replace(")", "")
        .ToLower()
        .Split(" ")
        .ToList());
    string match = "";
    

    if(result == null)
    {
        try
        {
            match = " (Match imperfeito)";
            result = await service
            .SearchItemsInAllConnectionsWithoutRestrictions(teste[i][1]
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
        Console.WriteLine(teste[i][0] + match  + " " + teste[i][1] + ": " + result[0].DomainName + " " + result[0].Name + " " + result[0].Price.ToString() + "R$");
        writer.WriteLine(teste[i][0] + match + " " + teste[i][1] + ": " + result[0].DomainName + " " + result[0].Name + " " + result[0].Price.ToString() + "R$");
        writer.Flush();

        service2.AppendToSpreadsheet(result, teste[i], match, "", "");
    }

   
    /*string str = "";
    for(int j=0; j < 2; j++)
    {
        str += teste[i][j] + " ";
    }
    Console.WriteLine(str);*/

}