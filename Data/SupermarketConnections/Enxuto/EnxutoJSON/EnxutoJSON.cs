using Domain;
using Domain.Interfaces;

namespace Data.SupermarketConnections.Enxuto.EnxutoJSON
{
    public class EnxutoJSON : IProduct
    {
        public double preco { get; set; }
        public string descricao { get; set; }
        public bool em_oferta { get; set; }

        public int CompareTo(object? obj)
        {
            if (obj == null) return -1;
            EnxutoJSON product = obj as EnxutoJSON;

            return preco < preco ? -1 : 1;
        }

        public string getPrice()
        {
            return preco.ToString();
        }

        public Product GetProduct()
        {
            return new Product
            {
                Name = descricao,
                Price = preco,
                HasDiscount = em_oferta,
                DomainName = "Enxuto",
                Type = ""
            };
        }
    }
}