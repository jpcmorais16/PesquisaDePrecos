using Services;
using Data;

var service = new SearchService(new ConnectorFactory());

var result = await service.SearchItemsInAllConnectionsWithoutRestrictions(new List<string> { "jgdsngjidfiugsdg" });


try{
    foreach (var item in result)
    {
        Console.WriteLine(item.DomainName + ": " + item.Name + " " + item.Price.ToString() + "R$");
    }
}
catch
{
    return;
}