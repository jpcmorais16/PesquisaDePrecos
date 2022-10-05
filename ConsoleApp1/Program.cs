﻿using Services;
using Data;
using Data.SupermarketConnections;

var service = new SearchService(new SupermarketConnectorFactory());

var service2 = new SpreadsheetService();
StreamWriter writer = new StreamWriter(@"C:\Users\Trilogo\Desktop\pesquisa.txt");




var teste = service2.GetNFirstItemsFromSpreadsheet(2);
/*try{
    foreach (var item in result)
    {
        Console.WriteLine(item.DomainName + ": " + item.Name + " " + item.Price.ToString() + "R$");
    }
}
catch
{
    return;
}*/

for(int i=0; i < teste.Count; i++)
{
    if (i == 4)
    {
        
    }
    var result = await service
        .SearchItemsInAllConnectionsWithoutRestrictionsContainingAllWords(teste[i][1]
        .Replace("(", "")
        .Replace(")", "")
        .Split(" ")
        .ToList());
    string match = "";
    

    if(result == null)
    {
        match = " (Match perfeito nao encontrado)" ;
        result = await service
        .SearchItemsInAllConnectionsWithoutRestrictions(teste[i][1]
        .Replace("(", "")
        .Replace(")", "")
        .Split(" ")
        .ToList());

    }

    if(result != null) { 
        Console.WriteLine(teste[i][0] + match  + " " + teste[i][1] + ": " + result[0].DomainName + " " + result[0].Name + " " + result[0].Price.ToString() + "R$");
        writer.WriteLine(teste[i][0] + match + " " + teste[i][1] + ": " + result[0].DomainName + " " + result[0].Name + " " + result[0].Price.ToString() + "R$");
        writer.Flush();
    }
   
    /*string str = "";
    for(int j=0; j < 2; j++)
    {
        str += teste[i][j] + " ";
    }
    Console.WriteLine(str);*/

}