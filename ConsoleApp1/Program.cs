using Services;
using Data;

var service = new SearchService(new ConnectorFactory());

var result = await service.SearchItemsInAllConnectionsWithoutRestrictions(new List<string> { "suco", "500ml" });


foreach (var item in result)
{
    Console.WriteLine(item.Name + " " + item.Price.ToString());
}