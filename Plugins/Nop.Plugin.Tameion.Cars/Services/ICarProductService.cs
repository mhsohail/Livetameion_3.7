using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.Cars.Services
{
    public interface ICarProductService
    {
        IList<Product> SearchCarProducts();
    }
}
