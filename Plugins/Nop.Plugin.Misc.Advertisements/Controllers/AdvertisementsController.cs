using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.Advertisements.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Plugin.Misc.Advertisements.Services;
using Nop.Plugin.Misc.Advertisements.DomainModels;
using Nop.Plugin.Misc.Advertisements.ViewModels;
using Nop.Services.Helpers;
using Nop.Admin.Extensions;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Routing;

namespace Nop.Plugin.Misc.Advertisements.Controllers
{

    [AdminAuthorize]
    public class AdvertisementsController : BasePluginController
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IWorkContext _workContext;
        private readonly IVendorService _vendorService;
        private readonly IStoreService _storeService;

        public AdvertisementsController(IAdvertisementService advertisementService,
            IDateTimeHelper dateTimeHelper,
            IWorkContext workContext, 
            IVendorService vendorService,
            IStoreService storeService)
        {
            _advertisementService = advertisementService;
            _dateTimeHelper = dateTimeHelper;
            _workContext = workContext;
            _vendorService = vendorService;
            _storeService = storeService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new AdvertisementListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AdvertisementList(DataSourceRequest command, AdvertisementListModel model)
        {
            var ads = _advertisementService.SearchAdvertisements(
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                keywords: model.SearchAdvertisementName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult();
            gridModel.Data = ads.Select(x =>
            {
                var adModel = new ModelsMapper().CreateMap<Advertisement, AdvertisementModel>(x);
                //little hack here:
                //ensure that product full descriptions are not returned
                //otherwise, we can get the following error if products have too long descriptions:
                //"Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property. "
                //also it improves performance
                adModel.Description = string.Empty;

                //picture
                //var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                //adModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                ////product type
                //adModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                ////friendly stock qantity
                ////if a simple product AND "manage inventory" is "Track inventory", then display
                //if (x.ProductType == ProductType.SimpleProduct && x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                //    adModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();
                return adModel;
            });
            gridModel.Total = ads.TotalCount;

            return Json(gridModel);
        }
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var model = new AdvertisementModel();
            PrepareAdvertisementModel(model, null, true, true);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            PrepareStoresMappingModel(model, null, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdvertisementModel model, HttpPostedFileBase Banner)
        {
            if (model == null)
                throw new ArgumentNullException("advertisement");

            string requestUrl = string.Format(Request.Url.Scheme + "://" + Request.Url.Authority + "/Admin/Picture/AsyncUpload");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json; charset=UTF-8";

            var email = "sohailx2x@yahoo.com";
            var password = "123456";
            string credentials = String.Format("{0}:{1}", email, password);
            byte[] bytes = Encoding.ASCII.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            string authorization = String.Concat("Basic ", base64);
            request.Headers.Add("Authorization", authorization);

            //byte[] fileBytes = System.IO.File.ReadAllBytes(Banner.FileName);
            //StringBuilder serializedBytes = new StringBuilder();

            ////Let's serialize the bytes of your file
            //fileBytes.ToList<byte>().ForEach(x => serializedBytes.AppendFormat("{0}.", Convert.ToUInt32(x)));
            //string postParameters = String.Format("{0}", serializedBytes.ToString());
            //byte[] postData = Encoding.UTF8.GetBytes(postParameters);
            //////////////

            byte[] data;
            using (Stream inputStream = Banner.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                //whether the stream returned is already a MemoryStream
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            byte[] buffer = new byte[0x1000];
            Banner.InputStream.Read(buffer, 0, buffer.Length);
            //////////////////////////////////
            using (var streamWriter = request.GetRequestStream())
            {
                streamWriter.Write(data, 0, data.Length);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            //Receipt Receipt = null;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responstText = streamReader.ReadToEnd();
                //Receipt = serializer.Deserialize<Receipt>(responstText);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.VendorId = _workContext.CurrentVendor.Id;
                    }

                    var advertise = new ModelsMapper().CreateMap<AdvertisementModel, Advertisement>(model);
                    advertise.CreatedOnUtc = DateTime.UtcNow;
                    advertise.UpdatedOnUtc = DateTime.UtcNow;

                    _advertisementService.InsertAdvertisement(advertise);

                    return RedirectToAction("Edit", new { Id = model.Id });
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message + "<br />" + exc.InnerException.InnerException.Message);
            }

            PrepareAdvertisementModel(model, null, true, true);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            PrepareStoresMappingModel(model, null, false);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var advertisement = _advertisementService.GetAdvertisementById(id);
            if (advertisement == null || advertisement.Deleted)
                //No advertisement found with the specified id
                return RedirectToAction("List");

            var model = new ModelsMapper().CreateMap<Advertisement, AdvertisementModel>(advertisement);

            PrepareAdvertisementModel(model, null, false, true);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            PrepareStoresMappingModel(model, null, false);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdvertisementModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            try
            {
                //Update
                if (ModelState.IsValid)
                {
                    var advertisement = _advertisementService.GetAdvertisementById(model.Id);
                    advertisement = new ModelsMapper().CreateMap<AdvertisementModel, Advertisement>(model, advertisement);
                    advertisement.UpdatedOnUtc = DateTime.UtcNow;
                    _advertisementService.UpdateAdvertisement(advertisement);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message + "<br />" + exc.InnerException.InnerException.Message);
            }

            PrepareAdvertisementModel(model, null, false, true);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            PrepareStoresMappingModel(model, null, false);
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var advertise = _advertisementService.GetAdvertisementById(id);
            if (advertise == null)
                return RedirectToAction("List");

            advertise.UpdatedOnUtc = DateTime.UtcNow;
            advertise.Deleted = true;
            _advertisementService.DeleteAdvertisement(advertise);

            return RedirectToAction("List");
        }

        [NonAction]
        protected virtual void PrepareAdvertisementModel(AdvertisementModel model, Advertisement advertisement,
            bool setPredefinedValues, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (advertisement != null)
            {
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(advertisement.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(advertisement.UpdatedOnUtc, DateTimeKind.Utc);
            }
            
            //vendors
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            model.AvailableVendors.Add(new SelectListItem
            {
                Text = "None",
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
            
            //default values
            if (setPredefinedValues)
            {
                model.Published = true;
            }
        }

        [NonAction]
        protected virtual void PrepareStoresMappingModel(AdvertisementModel model, Advertisement advertisement, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableStores = _storeService
                .GetAllStores()
                .Select(s => s.ToModel())
                .ToList();
            if (!excludeProperties)
            {
                if (advertisement != null)
                {
                    //model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(advertisement);
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            var advertisements = new List<Advertisement>();
            if (selectedIds != null)
            {
                advertisements.AddRange(_advertisementService.GetAdvertisementsByIds(selectedIds.ToArray()));

                for (int i = 0; i < advertisements.Count; i++)
                {
                    var product = advertisements[i];

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                        continue;

                    _advertisementService.DeleteAdvertisement(product);
                }
            }

            return Json(new { Result = true });
        }
    }
}