﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Atacadao;
using Domain;

namespace Data.Interfaces
{
    public interface IHttpConnector
    {
        public Task<List<Product>> SearchProducts(List<string> searchTerms);
        public string GetName();
    }
}
