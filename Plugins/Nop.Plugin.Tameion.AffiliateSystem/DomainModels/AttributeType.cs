using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public enum AttributeType
    {
        [Display(Name = "Cart Attributes")]
        Cart,
        [Display(Name = "Customer Attributes")]
        Customer
    }
}
