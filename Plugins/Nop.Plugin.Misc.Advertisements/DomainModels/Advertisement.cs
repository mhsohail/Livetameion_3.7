using Nop.Core;
using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Advertisements.DomainModels
{
    public class Advertisement : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int VendorId { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool Deleted { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        
        public Advertisement()
        {
            CreatedOnUtc = DateTime.MinValue;
            UpdatedOnUtc = DateTime.MinValue;
        }
    }
}
