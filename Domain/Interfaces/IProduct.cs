using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProduct: IComparable
    {
        public string getPrice();
        //Cria um mapeamento entre o tipo especifico de produto para o produto geral
        public Product GetProduct();
    }
}
