using Services;
using Data;

var service = new SearchService(new ConnectorFactory());

var result = await service.SearchItemsInAllConnectionsWithWordRestrictions(new List<string> { "suco", "500ml" }, new List<string> { "concentrado"});


foreach (var item in result)
{
    Console.WriteLine(item.full_display + " " + item.price.getPrice().ToString());
}