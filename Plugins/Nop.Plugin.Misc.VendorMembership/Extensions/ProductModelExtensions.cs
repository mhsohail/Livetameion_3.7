using Nop.Admin.Models.Catalog;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Extensions
{
    public static class ProductModelExtensions
    {
        public static List<ViewModels.ProductSpecificationAttributeModel> GetProductSpecifications(this ProductModel productModel, string productType)
        {
            productModel.Id = 62;
            var _specificationAttributeService = EngineContext.Current.Resolve<ISpecificationAttributeService>();
            var specs = _specificationAttributeService.GetSpecificationAttributes().Where(sa => sa.Name.StartsWith(productType)).ToList();

            var productSpecs = _specificationAttributeService.GetProductSpecificationAttributes(productModel.Id);
            var specIds = productSpecs.Select(psas => psas.SpecificationAttributeOption.SpecificationAttributeId).ToList();

            foreach (var spec in specs)
            {
                //_specificationAttributeService.DeleteProductSpecificationAttribute(productSpecs[specs.IndexOf(spec)]);

                if (!specIds.Contains(spec.Id))
                {
                    ProductSpecificationAttribute psa = new ProductSpecificationAttribute
                    {
                        SpecificationAttributeOptionId = spec.SpecificationAttributeOptions.ToList()[0].Id,
                        //SpecificationAttributeOption = spec.SpecificationAttributeOptions.ToList()[0],
                        ProductId = productModel.Id,
                        ShowOnProductPage = true,
                        AttributeTypeId = (spec.SpecificationAttributeOptions.Count > 1) ? (int)SpecificationAttributeType.Option : (int)SpecificationAttributeType.CustomText
                    };
                    _specificationAttributeService.InsertProductSpecificationAttribute(psa);
                    productSpecs.Add(psa);
                }
            }
            ///////////////////
            var modelList = new List<ViewModels.ProductSpecificationAttributeModel>();
            PrepareProductSpecsModel(productSpecs, modelList);
            return modelList;
        }

        private static void PrepareProductSpecsModel(IList<ProductSpecificationAttribute> productSpecs, List<ViewModels.ProductSpecificationAttributeModel> modelList)
        {
            if (modelList == null)
                throw new ArgumentNullException("model");

            if (productSpecs == null)
                throw new ArgumentNullException("productSpecs");

            var _specificationAttributeService = EngineContext.Current.Resolve<ISpecificationAttributeService>();
            var _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            var _workContext = EngineContext.Current.Resolve<IWorkContext>();

            IEnumerable<ViewModels.ProductSpecificationAttributeModel> productrSpecsModel = null;

            if (modelList.Count > 0)
            {
                productrSpecsModel = modelList
                .Select(x =>
                {
                    var psa = _specificationAttributeService.GetProductSpecificationAttributeById(x.Id);
                    psa.AttributeType.GetLocalizedEnum(_localizationService, _workContext);

                    var psaModel = new ViewModels.ProductSpecificationAttributeModel
                    {
                        ProductId = 62,
                        Id = x.Id,
                        AttributeTypeName = psa.AttributeType.GetLocalizedEnum(_localizationService, _workContext),
                        AttributeName = psa.SpecificationAttributeOption.SpecificationAttribute.Name,
                        AllowFiltering = x.AllowFiltering,
                        ShowOnProductPage = x.ShowOnProductPage,
                        DisplayOrder = x.DisplayOrder,
                        SpecificationAttributeOptionId = x.SpecificationAttributeOptionId
                    };

                    // custom code
                    foreach (var option in psa.SpecificationAttributeOption.SpecificationAttribute.SpecificationAttributeOptions)
                    {
                        psaModel.AvailableOptions.Add(new SelectListItem
                        {
                            Text = option.Name,
                            Value = option.Id.ToString()
                        });
                    }

                    switch (psa.AttributeType)
                    {
                        case SpecificationAttributeType.Option:
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(psa.SpecificationAttributeOption.Name);
                            break;
                        case SpecificationAttributeType.CustomText:
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                            break;
                        case SpecificationAttributeType.CustomHtmlText:
                            //do not encode?
                            //psaModel.ValueRaw = x.CustomValue;
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                            break;
                        case SpecificationAttributeType.Hyperlink:
                            psaModel.ValueRaw = x.CustomValue;
                            break;
                        default:
                            break;
                    }
                    return psaModel;
                })
                .ToList();

                modelList.Clear();
                modelList.AddRange(productrSpecsModel);
            }
            else
            {
                productrSpecsModel = productSpecs
                .Select(x =>
                {
                    var psa = _specificationAttributeService.GetProductSpecificationAttributeById(x.Id);
                    psa.AttributeType.GetLocalizedEnum(_localizationService, _workContext);

                    var psaModel = new ViewModels.ProductSpecificationAttributeModel
                    {
                        ProductId = 62,
                        Id = x.Id,
                        AttributeTypeName = psa.AttributeType.GetLocalizedEnum(_localizationService, _workContext),
                        AttributeName = psa.SpecificationAttributeOption.SpecificationAttribute.Name,
                        AllowFiltering = x.AllowFiltering,
                        ShowOnProductPage = x.ShowOnProductPage,
                        DisplayOrder = x.DisplayOrder,
                        SpecificationAttributeOptionId = x.SpecificationAttributeOptionId,
                        SpecificationAttributeOption = x.SpecificationAttributeOption
                    };

                    // custom code
                    foreach (var option in psa.SpecificationAttributeOption.SpecificationAttribute.SpecificationAttributeOptions)
                    {
                        psaModel.AvailableOptions.Add(new SelectListItem
                        {
                            Text = option.Name,
                            Value = option.Id.ToString()
                        });
                    }

                    switch (psa.AttributeType)
                    {
                        case SpecificationAttributeType.Option:
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(psa.SpecificationAttributeOption.Name);
                            break;
                        case SpecificationAttributeType.CustomText:
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                            break;
                        case SpecificationAttributeType.CustomHtmlText:
                            //do not encode?
                            //psaModel.ValueRaw = x.CustomValue;
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                            break;
                        case SpecificationAttributeType.Hyperlink:
                            psaModel.ValueRaw = x.CustomValue;
                            break;
                        default:
                            break;
                    }
                    return psaModel;
                })
                .ToList();

                modelList.AddRange(productrSpecsModel);
            }
        }
    }
}
