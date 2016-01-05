using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.ViewModels
{
    public class ProductSpecificationAttributeModel : BaseNopEntityModel
    {
        public ProductSpecificationAttributeModel()
        {
            this.AvailableOptions = new List<SelectListItem>();
        }

        //ProductSpecificationAttributeModel core model fields
        [AllowHtml]
        public string AttributeTypeName { get; set; }
        [AllowHtml]
        public string AttributeName { get; set; }
        [AllowHtml]
        public string ValueRaw { get; set; }

        //ProductSpecificationAttribute core model fields
        public int AttributeTypeId { get; set; }
        public int SpecificationAttributeOptionId { get; set; }
        public int ProductId { get; set; }
        public string CustomValue { get; set; }

        //common fields between both two models above
        public bool AllowFiltering { get; set; }
        public bool ShowOnProductPage { get; set; }
        public int DisplayOrder { get; set; }

        //new fields
        public List<SelectListItem> AvailableOptions { get; set; }
        public SpecificationAttributeOption SpecificationAttributeOption { get; set; }
    }
}
