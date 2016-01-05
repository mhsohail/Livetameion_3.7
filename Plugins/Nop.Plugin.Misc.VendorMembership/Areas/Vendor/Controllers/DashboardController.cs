using Nop.Plugin.Misc.VendorMembership.Areas.Vendor.ViewModels;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Areas.Vendor.Controllers
{
    public class DashboardController : BasePluginController
    {
        private readonly IProductService _productService;

        public DashboardController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var model = new DashboardModel();
            PrepareDashboardModel(model);
            return View(model);
        }

        protected virtual void PrepareDashboardModel(DashboardModel model)
        {
            var groupDealProducts = _productService.SearchProducts(
                productType: Core.Domain.Catalog.ProductType.GroupDeal);
            model.ActiveGroupDeals = groupDealProducts;
        }
    }
}
