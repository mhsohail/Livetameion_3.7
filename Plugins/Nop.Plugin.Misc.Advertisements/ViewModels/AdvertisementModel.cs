using Nop.Admin.Models.Stores;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.Advertisements.ViewModels
{
    public class AdvertisementModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Url { get; set; }        
        public string Description { get; set; }
        [Display(Name = "Vendor")]
        public int VendorId { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Deleted { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsLoggedInAsVendor { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }
        [Display(Name = "Banner")]
        public string BannerThumbnailUrl { get; set; }

        public AdvertisementModel()
        {
            CreatedOn = DateTime.MinValue;
            UpdatedOn = DateTime.MinValue;
            AvailableVendors = new List<SelectListItem>();
        }

        //Store mapping
        [Display(Name = "LimitedToStores")]
        public bool LimitedToStores { get; set; }
        [Display(Name = "AvailableStores")]
        public List<StoreModel> AvailableStores { get; set; }
        public int[] SelectedStoreIds { get; set; }
    }
}
