using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public enum LogicalOperation
    {
        [Display(Name = "is")]
        Is,
        [Display(Name = "is not")]
        IsNot,
        [Display(Name = "greater than")]
        GreaterThan,
        [Display(Name = "less than")]
        LessThan,
        [Display(Name = "greater than or equals")]
        GreaterThanOrEquals,
        [Display(Name = "less than or equals")]
        LessThanOrEquals,
        [Display(Name = "is one of")]
        IsOneOf,
        [Display(Name = "is not one of")]
        IsNotOneOf
    }
}
