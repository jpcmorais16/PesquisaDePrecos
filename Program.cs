using Data;

var query = new AtacadaoItemQuery();

var result = await query.SearchProducts(new List<string> { "suco", "500ml" });

foreach (var item in result)
{
    Console.WriteLine(item);
}