﻿using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Plugin.Misc.GroupDeals.ViewModels;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Services.Media;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Nop.Services.Seo;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Plugin.Misc.GroupDeals.Helpers;
using System.ComponentModel.DataAnnotations;
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using Nop.Services.Localization;
using Nop.Core.Domain.Catalog;
using Nop.Services.Customers;
using Nop.Services.Tax;
using Nop.Services.Common;
using Nop.Services.ExportImport;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using Nop.Core.Domain.Common;
using Nop.Services.Discounts;
using Nop.Core.Domain.Discounts;
using Nop.Admin.Extensions;
using System.Diagnostics;
using Nop.Web.Framework;
using Nop.Plugin.Misc.GroupDeals.Extensions;

namespace Nop.Plugin.Misc.GroupDeals.Areas
{
    public class BaseGroupDealsController : BasePluginController
    {
        protected readonly IProductService _productService;
        protected readonly IProductTemplateService _productTemplateService;
        protected readonly ICategoryService _categoryService;
        protected readonly IManufacturerService _manufacturerService;
        protected readonly ICustomerService _customerService;
        protected readonly IUrlRecordService _urlRecordService;
        protected readonly IWorkContext _workContext;
        protected readonly ILanguageService _languageService;
        protected readonly ILocalizationService _localizationService;
        protected readonly ILocalizedEntityService _localizedEntityService;
        protected readonly ISpecificationAttributeService _specificationAttributeService;
        protected readonly IPictureService _pictureService;
        protected readonly ITaxCategoryService _taxCategoryService;
        protected readonly IProductTagService _productTagService;
        protected readonly ICopyProductService _copyProductService;
        protected readonly IPdfService _pdfService;
        protected readonly IExportManager _exportManager;
        protected readonly IImportManager _importManager;
        protected readonly ICustomerActivityService _customerActivityService;
        protected readonly IPermissionService _permissionService;
        protected readonly IAclService _aclService;
        protected readonly IStoreService _storeService;
        protected readonly IOrderService _orderService;
        protected readonly IStoreMappingService _storeMappingService;
        protected readonly IVendorService _vendorService;
        protected readonly IShippingService _shippingService;
        protected readonly IShipmentService _shipmentService;
        protected readonly ICurrencyService _currencyService;
        protected readonly CurrencySettings _currencySettings;
        protected readonly IMeasureService _measureService;
        protected readonly MeasureSettings _measureSettings;
        protected readonly AdminAreaSettings _adminAreaSettings;
        protected readonly IDateTimeHelper _dateTimeHelper;
        protected readonly IDiscountService _discountService;
        protected readonly IProductAttributeService _productAttributeService;
        protected readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
        protected readonly IShoppingCartService _shoppingCartService;
        protected readonly IProductAttributeFormatter _productAttributeFormatter;
        protected readonly IProductAttributeParser _productAttributeParser;
        protected readonly IDownloadService _downloadService;
        protected readonly IRepository<GroupdealPicture> _groupdealPictureRepo;
        protected readonly IGroupDealService _groupdealService;
        protected readonly IGenericAttributeService _genericAttributeService;

        public BaseGroupDealsController(
            IProductService productService,
            IProductTemplateService productTemplateService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICustomerService customerService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            ISpecificationAttributeService specificationAttributeService,
            IPictureService pictureService,
            ITaxCategoryService taxCategoryService,
            IProductTagService productTagService,
            ICopyProductService copyProductService,
            IPdfService pdfService,
            IExportManager exportManager,
            IImportManager importManager,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IAclService aclService,
            IStoreService storeService,
            IOrderService orderService,
            IStoreMappingService storeMappingService,
            IVendorService vendorService,
            IShippingService shippingService,
            IShipmentService shipmentService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            IMeasureService measureService,
            MeasureSettings measureSettings,
            AdminAreaSettings adminAreaSettings,
            IDateTimeHelper dateTimeHelper,
            IDiscountService discountService,
            IProductAttributeService productAttributeService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            IShoppingCartService shoppingCartService,
            IProductAttributeFormatter productAttributeFormatter,
            IProductAttributeParser productAttributeParser,
            IDownloadService downloadService,
            IRepository<GroupDeal> groupDealRepo,
            IRepository<GroupdealPicture> groupdealPictureRepo,
            IGroupDealService groupdealService,
            IGenericAttributeService genericAttributeService)
        {
            this._productService = productService;
            this._productTemplateService = productTemplateService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._customerService = customerService;
            this._urlRecordService = urlRecordService;
            this._workContext = workContext;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._specificationAttributeService = specificationAttributeService;
            this._pictureService = pictureService;
            this._taxCategoryService = taxCategoryService;
            this._productTagService = productTagService;
            this._copyProductService = copyProductService;
            this._pdfService = pdfService;
            this._exportManager = exportManager;
            this._importManager = importManager;
            this._customerActivityService = customerActivityService;
            this._permissionService = permissionService;
            this._aclService = aclService;
            this._storeService = storeService;
            this._orderService = orderService;
            this._storeMappingService = storeMappingService;
            this._vendorService = vendorService;
            this._shippingService = shippingService;
            this._shipmentService = shipmentService;
            this._currencyService = currencyService;
            this._currencySettings = currencySettings;
            this._measureService = measureService;
            this._measureSettings = measureSettings;
            this._adminAreaSettings = adminAreaSettings;
            this._dateTimeHelper = dateTimeHelper;
            this._discountService = discountService;
            this._productAttributeService = productAttributeService;
            this._backInStockSubscriptionService = backInStockSubscriptionService;
            this._shoppingCartService = shoppingCartService;
            this._productAttributeFormatter = productAttributeFormatter;
            this._productAttributeParser = productAttributeParser;
            this._downloadService = downloadService;
            this._groupdealPictureRepo = groupdealPictureRepo;
            this._groupdealService = groupdealService;
            this._genericAttributeService = genericAttributeService;
        }

        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            var gdvm = new GroupDealViewModel();
            var area = ControllerContext.RouteData.Values["area"];
            if (area.ToString().ToLower() == "vendor")
            {
                if (_workContext.CurrentVendor != null)
                    gdvm.IsLoggedInAsVendor = true;
            }
            else
            {
                if (_workContext.CurrentVendor != null)
                    gdvm.IsLoggedInAsVendor = false;
            }

            return RedirectToAction("List", new { area = "Admin" });
        }

        public ActionResult List()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var model = new GroupDealListModel();
            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.GetFormattedBreadCrumb(categories), Value = c.Id.ToString() });

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
                model.AvailableManufacturers.Add(new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //warehouses
            model.AvailableWarehouses.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var wh in _shippingService.GetAllWarehouses())
                model.AvailableWarehouses.Add(new SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var v in _vendorService.GetAllVendors(showHidden: true))
                model.AvailableVendors.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString() });

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"), Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"), Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"), Value = "2" });
            
            return View(model);
        }

        [AcceptVerbs("POST")]
        public ActionResult GroupDealList(DataSourceRequest command, GroupDealListModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                //model.SearchVendorId = _workContext.CurrentVendor.Id;
            }

            var categoryIds = new List<int> { model.SearchCategoryId };
            //include subcategories
            if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
                categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            var groupDeals = _groupdealService.SearchGroupDeals(
                categoryIds: categoryIds,
                manufacturerId: model.SearchManufacturerId,
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                warehouseId: model.SearchWarehouseId,
                productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true,
                overridePublished: overridePublished
            );
            var gridModel = new DataSourceResult();
            gridModel.Data = groupDeals.Select(x =>
            {
                //var productModel = x.ToModel();
                var groupDealModel = new ModelsMapper().CreateMap<Product, GroupDealViewModel>(x);
                //little hack here:
                //ensure that product full descriptions are not returned
                //otherwise, we can get the following error if products have too long descriptions:
                //"Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property. "
                //also it improves performance
                groupDealModel.FullDescription = "";

                //picture
                var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                groupDealModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                //product type
                groupDealModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                //friendly stock qantity
                //if a simple product AND "manage inventory" is "Track inventory", then display
                if (x.ProductType == ProductType.SimpleProduct && x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                    groupDealModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();
                if (x.AvailableStartDateTimeUtc < x.AvailableEndDateTimeUtc)
                {
                    groupDealModel.GroupdealStatusName = PluginHelper.GetAttribute<DisplayAttribute>(GroupdealStatus.Running).Name;
                }
                else
                {
                    groupDealModel.GroupdealStatusName = PluginHelper.GetAttribute<DisplayAttribute>(GroupdealStatus.Ended).Name;
                }
                return groupDealModel;
            });
            gridModel.Total = groupDeals.TotalCount;

            return Json(gridModel);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //var groupDeals = _groupdealService.GetAllGroupdeals().ToList();
            //var groupDeals = _groupdealService.GetAllGroupDealProducts().ToList();

            //var groupDealViewModels = new List<GroupDealViewModel>();
            //foreach (var gd in groupDeals)
            //{
            //    //var gdvm = new ModelsMapper().CreateMap<GroupDeal, GroupDealViewModel>(gd);
            //    var gdvm = new ModelsMapper().CreateMap<Product, GroupDealViewModel>(gd);
            //    if (gd.AvailableStartDateTimeUtc < gd.AvailableEndDateTimeUtc)
            //    {
            //        gdvm.GroupdealStatusName = PluginHelper.GetAttribute<DisplayAttribute>(GroupdealStatus.Running).Name;
            //    }
            //    else
            //    {
            //        gdvm.GroupdealStatusName = PluginHelper.GetAttribute<DisplayAttribute>(GroupdealStatus.Ended).Name;
            //    }
            //    groupDealViewModels.Add(gdvm);
            //}

            //var gridModel = new DataSourceResult
            //{
            //    Data = groupDealViewModels,
            //    Total = groupDealViewModels.Count
            //};

            //return Json(gridModel);
        }

        [NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var categoriesIds = new List<int>();
            var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetChildCategoryIds(category.Id));
            }
            return categoriesIds;
        }

        [AcceptVerbs("GET")]
        public ActionResult Details(int id)
        {
            return View();
        }

        //[AcceptVerbs("GET")]
        //[ActionName("Edit")]
        //public ActionResult Edit(int id)
        //{
        //    if (id == null)
        //    {
        //        throw new ArgumentNullException("id");
        //    }
        //    var groupdeal = _groupdealService.GetGroupDealById(id);
        //    var model = new ModelsMapper().CreateMap<GroupDeal, GroupDealViewModel>(groupdeal);
        //    model.GroupdealPictureViewModel = new GroupdealPictureViewModel();

        //    var vendors = _vendorService.GetAllVendors();
        //    model.AvailableVendors = new List<SelectListItem>();
        //    foreach (var vendor in vendors)
        //    {
        //        model.AvailableVendors.Add(new SelectListItem
        //        {
        //            Text = vendor.Name,
        //            Value = vendor.Id.ToString()
        //        });
        //    }
        //    PrepareGroupdealViewModel(model, groupdeal, false, false);

        //    return View("EditGroupdeal", model);
        //}

        [AcceptVerbs("GET")]
        [ActionName("Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var groupdeal = _productService.GetProductById((int)id);
            var model = new ModelsMapper().CreateMap<Product, GroupDealViewModel>(groupdeal);

            var vendors = _vendorService.GetAllVendors();
            model.AvailableVendors = new List<SelectListItem>();
            foreach (var vendor in vendors)
            {
                model.AvailableVendors.Add(new SelectListItem
                {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }
            PrepareGroupdealViewModel(model, groupdeal, false, false);

            return View("EditGroupDeal", model);
        }

        //[AcceptVerbs("POST"), ActionName("Edit"), ValidateAntiForgeryToken]
        //[ParameterBasedOnFormName("save-continue", "continueEditing")]
        //public ActionResult Edit(GroupDealViewModel model, bool continueEditing)
        //{
        //    //var groupDeal = _groupdealService.GetGroupDealById(model.Id);

        //    //groupDeal.Name = model.Name;
        //    //groupDeal.CreatedOnUtc = model.CreatedOnUtc;
        //    //groupDeal.UpdatedOnUtc = model.UpdatedOnUtc;
        //    //groupDeal.Country = model.Country;
        //    //groupDeal.StateOrProvince = model.StateOrProvince;
        //    //_groupdealService.UpdateGroupdeal(groupDeal);

        //    //return new NullJsonResult();

        //    //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
        //    //    return AccessDeniedView();

        //    var groupdeal = _groupdealService.GetGroupDealById(model.Id);
        //    if (groupdeal == null || groupdeal.Deleted)
        //        //No product found with the specified id
        //        return RedirectToAction("Index", new { area = "Admin" });

        //    //a vendor should have access only to his products
        //    if (_workContext.CurrentVendor != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
        //        return RedirectToAction("Index", new { area = "Admin" });

        //    if (ModelState.IsValid)
        //    {
        //        //a vendor should have access only to his products
        //        if (_workContext.CurrentVendor != null)
        //        {
        //            model.VendorId = _workContext.CurrentVendor.Id;
        //        }
        //        //vendors cannot edit "Show on home page" property
        //        if (_workContext.CurrentVendor != null && model.ShowOnHomePage != groupdeal.ShowOnHomePage)
        //        {
        //            model.ShowOnHomePage = groupdeal.ShowOnHomePage;
        //        }
        //        var prevStockQuantity = groupdeal.GetTotalStockQuantity();

        //        //groupdeal
        //        //groupdeal = model.ToEntity(groupdeal);
        //        model.CreatedOn = groupdeal.CreatedOnUtc;
        //        groupdeal = new ModelsMapper().CreateMap<GroupDealViewModel, GroupDeal>(model);
        //        groupdeal.UpdatedOnUtc = DateTime.UtcNow;
        //        _groupdealService.UpdateGroupdeal(groupdeal);
        //        //search engine name
        //        model.SeName = groupdeal.ValidateSeName(model.SeName, "groupdeal.Name", true);
        //        _urlRecordService.SaveSlug(groupdeal, model.SeName, 0);
        //        ////locales
        //        //UpdateLocales(groupdeal, model);
        //        ////tags
        //        //SaveProductTags(groupdeal, ParseProductTags(model.ProductTags));
        //        ////warehouses
        //        //SaveProductWarehouseInventory(groupdeal, model);
        //        ////ACL (customer roles)
        //        //SaveProductAcl(groupdeal, model);
        //        ////Stores
        //        //SaveStoreMappings(groupdeal, model);
        //        ////picture seo names
        //        //UpdatePictureSeoNames(groupdeal);
        //        ////discounts
        //        //var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);
        //        //foreach (var discount in allDiscounts)
        //        //{
        //        //    if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
        //        //    {
        //        //        //new discount
        //        //        if (groupdeal.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
        //        //            groupdeal.AppliedDiscounts.Add(discount);
        //        //    }
        //        //    else
        //        //    {
        //        //        //remove discount
        //        //        if (groupdeal.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
        //        //            groupdeal.AppliedDiscounts.Remove(discount);
        //        //    }
        //        //}
        //        //_productService.UpdateProduct(groupdeal);
        //        //_productService.UpdateHasDiscountsApplied(groupdeal);
        //        ////back in stock notifications
        //        //if (groupdeal.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
        //        //    groupdeal.BackorderMode == BackorderMode.NoBackorders &&
        //        //    groupdeal.AllowBackInStockSubscriptions &&
        //        //    groupdeal.GetTotalStockQuantity() > 0 &&
        //        //    prevStockQuantity <= 0 &&
        //        //    groupdeal.Published &&
        //        //    !groupdeal.Deleted)
        //        //{
        //        //    _backInStockSubscriptionService.SendNotificationsToSubscribers(groupdeal);
        //        //}

        //        ////activity log
        //        //_customerActivityService.InsertActivity("EditProduct", _localizationService.GetResource("ActivityLog.EditProduct"), groupdeal.Name);

        //        //SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Updated"));

        //        if (continueEditing)
        //        {
        //            //selected tab
        //            //SaveSelectedTabIndex();

        //            return RedirectToAction("Edit", new { id = groupdeal.Id, area = "Admin" });
        //        }
        //        return RedirectToAction("Index", new { area = "Admin" });
        //    }

        //    //If we got this far, something failed, redisplay form
        //    //PrepareProductModel(model, groupdeal, false, true);
        //    //PrepareAclModel(model, groupdeal, true);
        //    //PrepareStoresMappingModel(model, groupdeal, true);
        //    return View("EditGroupdeal", model);
        //}

        [AcceptVerbs("POST"), ActionName("Edit"), ValidateAntiForgeryToken]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(GroupDealViewModel model, bool continueEditing)
        {
            var groupDeal = _productService.GetProductById(model.Id);
            if (groupDeal == null || groupDeal.Deleted)
                //No product found with the specified id
                return RedirectToAction("Index", new { area = "Admin" });

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && groupDeal.VendorId != _workContext.CurrentVendor.Id)
                return RedirectToAction("Index", new { area = "Admin" });

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (_workContext.CurrentVendor != null)
                {
                    model.VendorId = _workContext.CurrentVendor.Id;
                }
                //vendors cannot edit "Show on home page" property
                if (_workContext.CurrentVendor != null && model.ShowOnHomePage != groupDeal.ShowOnHomePage)
                {
                    model.ShowOnHomePage = groupDeal.ShowOnHomePage;
                }
                var prevStockQuantity = groupDeal.GetTotalStockQuantity();

                //groupdeal
                //groupdeal = model.ToEntity(groupdeal);
                model.CreatedOn = groupDeal.CreatedOnUtc;
                groupDeal = new ModelsMapper().CreateMap<GroupDealViewModel, Product>(model, groupDeal);
                groupDeal.UpdatedOnUtc = DateTime.UtcNow;
                _productService.UpdateProduct(groupDeal);
                //search engine name
                model.SeName = groupDeal.ValidateSeName(model.SeName, "groupdeal.Name", true);
                _urlRecordService.SaveSlug(groupDeal, model.SeName, 0);
                ////locales
                //UpdateLocales(groupdeal, model);
                ////tags
                //SaveProductTags(groupdeal, ParseProductTags(model.ProductTags));
                ////warehouses
                //SaveProductWarehouseInventory(groupdeal, model);
                ////ACL (customer roles)
                //SaveProductAcl(groupdeal, model);
                ////Stores
                //SaveStoreMappings(groupdeal, model);
                ////picture seo names
                //UpdatePictureSeoNames(groupdeal);
                ////discounts
                //var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);
                //foreach (var discount in allDiscounts)
                //{
                //    if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                //    {
                //        //new discount
                //        if (groupdeal.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
                //            groupdeal.AppliedDiscounts.Add(discount);
                //    }
                //    else
                //    {
                //        //remove discount
                //        if (groupdeal.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
                //            groupdeal.AppliedDiscounts.Remove(discount);
                //    }
                //}
                //_productService.UpdateProduct(groupdeal);
                //_productService.UpdateHasDiscountsApplied(groupdeal);
                ////back in stock notifications
                //if (groupdeal.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
                //    groupdeal.BackorderMode == BackorderMode.NoBackorders &&
                //    groupdeal.AllowBackInStockSubscriptions &&
                //    groupdeal.GetTotalStockQuantity() > 0 &&
                //    prevStockQuantity <= 0 &&
                //    groupdeal.Published &&
                //    !groupdeal.Deleted)
                //{
                //    _backInStockSubscriptionService.SendNotificationsToSubscribers(groupdeal);
                //}

                ////activity log
                //_customerActivityService.InsertActivity("EditProduct", _localizationService.GetResource("ActivityLog.EditProduct"), groupdeal.Name);

                //SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    //SaveSelectedTabIndex();

                    return RedirectToAction("Edit", new { id = groupDeal.Id, area = "Admin" });
                }
                return RedirectToAction("Index", new { area = "Admin" });
            }

            //If we got this far, something failed, redisplay form
            //PrepareProductModel(model, groupdeal, false, true);
            //PrepareAclModel(model, groupdeal, true);
            //PrepareStoresMappingModel(model, groupdeal, true);
            return View("EditGroupDeal", model);
        }

        [HttpPost]
        public ActionResult Delete(DataSourceRequest command, int id)
        {
            _groupdealService.DeleteGroupdeal(_groupdealService.GetGroupDealById(id));
            return new NullJsonResult();
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create()
        {
            var model = new GroupDealViewModel();
            /*
            var vendors = _vendorService.GetAllVendors();
            model.AvailableVendors = new List<SelectListItem>();
            foreach (var vendor in vendors)
            {
                model.AvailableVendors.Add(new SelectListItem {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }
            */
            PrepareGroupdealViewModel(model, null, true, true);

            return View("CreateGroupdeal", model);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupDealViewModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (_workContext.CurrentVendor != null)
                {
                    model.VendorId = _workContext.CurrentVendor.Id;
                }
                //vendors cannot edit "Show on home page" property
                if (_workContext.CurrentVendor != null && model.ShowOnHomePage)
                {
                    model.ShowOnHomePage = false;
                }

                //var groupDeal = new ModelsMapper().CreateMap<GroupDealViewModel, GroupDeal>(model);
                var groupDealProduct = new ModelsMapper().CreateMap<GroupDealViewModel, Product>(model);
                groupDealProduct.DisplayOrder = 1;
                groupDealProduct.ProductType = ProductType.SimpleProduct;
                groupDealProduct.OrderMaximumQuantity = 10;
                groupDealProduct.OrderMinimumQuantity = 1;
                groupDealProduct.Published = true;

                // datetime fields
                groupDealProduct.CreatedOnUtc = DateTime.UtcNow;
                groupDealProduct.UpdatedOnUtc = DateTime.UtcNow;
                groupDealProduct.AvailableEndDateTimeUtc = DateTime.Parse("01/01/2016");
                groupDealProduct.AvailableStartDateTimeUtc = DateTime.UtcNow;
                groupDealProduct.PreOrderAvailabilityStartDateTimeUtc = DateTime.UtcNow;
                groupDealProduct.SpecialPriceStartDateTimeUtc = DateTime.UtcNow;
                groupDealProduct.SpecialPriceEndDateTimeUtc = DateTime.Parse("01/01/2016");
                //_groupdealService.InsertGroupDeal(groupDeal);
                _productService.InsertProduct(groupDealProduct);

                //search engine name
                model.SeName = groupDealProduct.ValidateSeName(model.SeName, groupDealProduct.Name, true);
                _urlRecordService.SaveSlug(groupDealProduct, model.SeName, 0);

                groupDealProduct.SetIsGroupDeal(true);
                _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.Active, true);
                _genericAttributeService.SaveAttribute(groupDealProduct, GroupDealAttributes.FinePrint, model.FinePrint);
                
                return RedirectToAction("Index", new { area = "Admin" });
            }

            return View("CreateGroupdeal", model);
        }

        [ValidateInput(false)]
        public ActionResult GroupdealPictureAdd(int pictureId, int displayOrder,
            string overrideAltAttribute, string overrideTitleAttribute,
            int groupdealId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            if (pictureId == 0)
                throw new ArgumentException();

            var groupdeal = _groupdealService.GetGroupDealById(groupdealId);
            if (groupdeal == null)
                throw new ArgumentException("No groupdeal found with the specified id");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
                return RedirectToAction("Index");

            var picture = _pictureService.GetPictureById(pictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");

            _groupdealService.InsertGroupdealPicture(new GroupdealPicture
            {
                PictureId = pictureId,
                GroupdealId = groupdealId,
                DisplayOrder = displayOrder,
            });

            _pictureService.UpdatePicture(picture.Id,
                _pictureService.LoadPictureBinary(picture),
                picture.MimeType,
                picture.SeoFilename,
                overrideAltAttribute,
                overrideTitleAttribute);

            _pictureService.SetSeoFilename(pictureId, _pictureService.GetPictureSeName("groupdeal.Name"));

            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GroupdealPictureList(DataSourceRequest command, int groupdealId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var product = _groupdealService.GetGroupDealById(groupdealId);
                if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }

            var groupdealPictures = _groupdealService.GetGroupdealPicturesByGroupdealId(groupdealId);
            var groupdealPicturesViewModel = groupdealPictures
                .Select(x =>
                {
                    var picture = _pictureService.GetPictureById(x.PictureId);
                    if (picture == null)
                        throw new Exception("Picture cannot be loaded");
                    var m = new GroupdealPictureViewModel
                    {
                        Id = x.Id,
                        GroupdealId = x.GroupdealId,
                        PictureId = x.PictureId,
                        PictureUrl = _pictureService.GetPictureUrl(picture),
                        OverrideAltAttribute = picture.AltAttribute,
                        OverrideTitleAttribute = picture.TitleAttribute,
                        DisplayOrder = x.DisplayOrder
                    };
                    return m;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = groupdealPicturesViewModel,
                Total = groupdealPicturesViewModel.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GroupdealPictureUpdate(GroupdealPictureViewModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var groupdealPicture = _groupdealService.GetGroupdealPictureById(model.Id);
            if (groupdealPicture == null)
                throw new ArgumentException("No product picture found with the specified id");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var product = _productService.GetProductById(groupdealPicture.GroupdealId);
                if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }

            groupdealPicture.DisplayOrder = model.DisplayOrder;
            _groupdealService.UpdateGroupdealPicture(groupdealPicture);

            var picture = _pictureService.GetPictureById(groupdealPicture.PictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");

            _pictureService.UpdatePicture(picture.Id,
                _pictureService.LoadPictureBinary(picture),
                picture.MimeType,
                picture.SeoFilename,
                model.OverrideAltAttribute,
                model.OverrideTitleAttribute);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult GroupdealPictureDelete(int id)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var groupdealPicture = _groupdealService.GetGroupdealPictureById(id);
            if (groupdealPicture == null)
                throw new ArgumentException("No product picture found with the specified id");

            var groupdealId = groupdealPicture.GroupdealId;

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var groupdeal = _groupdealService.GetGroupDealById(groupdealId);
                if (groupdeal != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }
            var pictureId = groupdealPicture.PictureId;
            _groupdealService.DeleteGroupdealPicture(groupdealPicture);

            var picture = _pictureService.GetPictureById(pictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");
            _pictureService.DeletePicture(picture);

            return new NullJsonResult();
        }

        [NonAction]
        protected virtual void PrepareGroupdealViewModel(GroupDealViewModel model, Product product,
            bool setPredefinedValues, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (product != null)
            {
                var parentGroupedProduct = _productService.GetProductById(product.ParentGroupedProductId);
                if (parentGroupedProduct != null)
                {
                    model.AssociatedToProductId = product.ParentGroupedProductId;
                    model.AssociatedToProductName = parentGroupedProduct.Name;
                }
            }

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;
            if (product != null)
            {
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);
            }

            //little performance hack here
            //there's no need to load attributes, categories, manufacturers when creating a new product
            //anyway they're not used (you need to save a product before you map add them)
            if (product != null)
            {
                foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
                {
                    model.AvailableProductAttributes.Add(new SelectListItem
                    {
                        Text = productAttribute.Name,
                        Value = productAttribute.Id.ToString()
                    });
                }
                foreach (var manufacturer in _manufacturerService.GetAllManufacturers(showHidden: true))
                {
                    model.AvailableManufacturers.Add(new SelectListItem
                    {
                        Text = manufacturer.Name,
                        Value = manufacturer.Id.ToString()
                    });
                }
                var allCategories = _categoryService.GetAllCategories(showHidden: true);
                foreach (var category in allCategories)
                {
                    model.AvailableCategories.Add(new SelectListItem
                    {
                        Text = category.GetFormattedBreadCrumb(allCategories),
                        Value = category.Id.ToString()
                    });
                }
            }

            //copy product
            if (product != null)
            {
                model.CopyProductModel.Id = product.Id;
                model.CopyProductModel.Name = "Copy of " + product.Name;
                model.CopyProductModel.Published = true;
                model.CopyProductModel.CopyImages = true;
            }

            //templates
            var templates = _productTemplateService.GetAllProductTemplates();
            foreach (var template in templates)
            {
                model.AvailableProductTemplates.Add(new SelectListItem
                {
                    Text = template.Name,
                    Value = template.Id.ToString()
                });
            }

            //vendors
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            model.AvailableVendors.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"),
                Value = "0"
            });
            var vendors = _vendorService.GetAllVendors(showHidden: true);
            foreach (var vendor in vendors)
            {
                model.AvailableVendors.Add(new SelectListItem
                {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }

            //delivery dates
            model.AvailableDeliveryDates.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.DeliveryDate.None"),
                Value = "0"
            });
            var deliveryDates = _shippingService.GetAllDeliveryDates();
            foreach (var deliveryDate in deliveryDates)
            {
                model.AvailableDeliveryDates.Add(new SelectListItem
                {
                    Text = deliveryDate.Name,
                    Value = deliveryDate.Id.ToString()
                });
            }

            //warehouses
            var warehouses = _shippingService.GetAllWarehouses();
            model.AvailableWarehouses.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"),
                Value = "0"
            });
            foreach (var warehouse in warehouses)
            {
                model.AvailableWarehouses.Add(new SelectListItem
                {
                    Text = warehouse.Name,
                    Value = warehouse.Id.ToString()
                });
            }

            //multiple warehouses
            foreach (var warehouse in warehouses)
            {
                var pwiModel = new GroupDealViewModel.ProductWarehouseInventoryModel
                {
                    WarehouseId = warehouse.Id,
                    WarehouseName = warehouse.Name
                };
                if (product != null)
                {
                    var pwi = product.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
                    if (pwi != null)
                    {
                        pwiModel.WarehouseUsed = true;
                        pwiModel.StockQuantity = pwi.StockQuantity;
                        pwiModel.ReservedQuantity = pwi.ReservedQuantity;
                        pwiModel.PlannedQuantity = _shipmentService.GetQuantityInShipments(product, pwi.WarehouseId, true, true);
                    }
                }
                model.ProductWarehouseInventoryModels.Add(pwiModel);
            }

            //product tags
            if (product != null)
            {
                var result = new StringBuilder();
                for (int i = 0; i < product.ProductTags.Count; i++)
                {
                    var pt = product.ProductTags.ToList()[i];
                    result.Append(pt.Name);
                    if (i != product.ProductTags.Count - 1)
                        result.Append(", ");
                }
                model.ProductTags = result.ToString();
            }

            //tax categories
            var taxCategories = _taxCategoryService.GetAllTaxCategories();
            model.AvailableTaxCategories.Add(new SelectListItem { Text = "---", Value = "0" });
            foreach (var tc in taxCategories)
                model.AvailableTaxCategories.Add(new SelectListItem { Text = tc.Name, Value = tc.Id.ToString(), Selected = product != null && !setPredefinedValues && tc.Id == product.TaxCategoryId });

            //baseprice units
            var measureWeights = _measureService.GetAllMeasureWeights();
            foreach (var mw in measureWeights)
                model.AvailableBasepriceUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceUnitId });
            foreach (var mw in measureWeights)
                model.AvailableBasepriceBaseUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceBaseUnitId });

            //specification attributes
            var specificationAttributes = _specificationAttributeService.GetSpecificationAttributes();
            for (int i = 0; i < specificationAttributes.Count; i++)
            {
                var sa = specificationAttributes[i];
                model.AddSpecificationAttributeModel.AvailableAttributes.Add(new SelectListItem { Text = sa.Name, Value = sa.Id.ToString() });
                if (i == 0)
                {
                    //attribute options
                    foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(sa.Id))
                        model.AddSpecificationAttributeModel.AvailableOptions.Add(new SelectListItem { Text = sao.Name, Value = sao.Id.ToString() });
                }
            }
            //default specs values
            model.AddSpecificationAttributeModel.ShowOnProductPage = true;

            //discounts
            model.AvailableDiscounts = _discountService
                .GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true)
                .Select(d => d.ToModel())
                .ToList();
            if (!excludeProperties && product != null)
            {
                model.SelectedDiscountIds = product.AppliedDiscounts.Select(d => d.Id).ToArray();
            }

            //default values
            if (setPredefinedValues)
            {
                model.MaximumCustomerEnteredPrice = 1000;
                model.MaxNumberOfDownloads = 10;
                model.RecurringCycleLength = 100;
                model.RecurringTotalCycles = 10;
                model.RentalPriceLength = 1;
                model.StockQuantity = 10000;
                model.NotifyAdminForQuantityBelow = 1;
                model.OrderMinimumQuantity = 1;
                model.OrderMaximumQuantity = 10000;

                model.UnlimitedDownloads = true;
                model.IsShipEnabled = true;
                model.AllowCustomerReviews = true;
                model.Published = true;
                model.VisibleIndividually = true;
            }
        }
    }
}