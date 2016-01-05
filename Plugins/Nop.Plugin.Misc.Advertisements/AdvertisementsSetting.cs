using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Advertisements
{
    public class AdvertisementsSetting : ISettings
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int VendorId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public string SeName { get; set; }
    }
}
