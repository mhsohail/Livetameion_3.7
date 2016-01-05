using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.DomainModels
{
    public class ConditionAttribute : BaseEntity
    {
        public string Name { get; set; }
        public int AttributeTypeId { get; set; }
        public AttributeType AttributeType { get { return (AttributeType)AttributeTypeId; } set { AttributeTypeId = (int)value; } }
    }
}
