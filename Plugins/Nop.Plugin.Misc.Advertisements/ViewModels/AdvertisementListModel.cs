using Nop.Web.Framework.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.Advertisements.ViewModels
{
    public class AdvertisementListModel : BaseNopModel
    {
        public AdvertisementListModel()
        {
            //AvailableCategories = new List<SelectListItem>();
        }

        [Display(Name = "Advertisement Name")]
        [AllowHtml]
        public string SearchAdvertisementName { get; set; }
        [Display(Name = "Store")]
        public int SearchStoreId { get; set; }
        [Display(Name = "Vendor")]
        public int SearchVendorId { get; set; }
    }
}
