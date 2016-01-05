using Nop.Core;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using Nop.Plugin.Tameion.AffiliateSystem.Services;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.AffiliateSystem.Controllers
{
    public class AffiliatesController : BasePluginController
    {
        private readonly IAffiliateProgramService _affiliateProgramService;
        private readonly ICurrencyService _currencyService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStoreContext _storeContext;
        private readonly TaxSettings _taxSettings;
        private readonly IWorkContext _workContext;

        public AffiliatesController(IAffiliateProgramService affiliateProgramService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IShoppingCartService shoppingCartService,
            TaxSettings taxSettings,
            IOrderTotalCalculationService orderTotalCalculationService,
            ICurrencyService currencyService,
            IPriceFormatter priceFormatter)
        {
            _affiliateProgramService = affiliateProgramService;
            _workContext = workContext;
            _storeContext = storeContext;
            _shoppingCartService = shoppingCartService;
            _taxSettings = taxSettings;
            _orderTotalCalculationService = orderTotalCalculationService;
            _currencyService = currencyService;
            _priceFormatter = priceFormatter;
        }

        public ActionResult Index()
        {
            AffiliateProgram program = new AffiliateProgram();
            ProgramCondition condition = new ProgramCondition();
            condition.Name = "CartTotal";
            condition.Operation = LogicalOperation.Is;
            condition.Value = "10";
            condition.AffiliateProgram = program;

            //_affiliateProgramService.InsertAffiliateProgram(program);
            //_affiliateProgramService.InsertProgramCondition(condition);

            program = _affiliateProgramService.GetAffiliateProgramById(1);
            Order order = new Order { OrderTotal = 10 };

            var cart = _workContext.CurrentCustomer.ShoppingCartItems
                .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                .LimitPerStore(_storeContext.CurrentStore.Id)
                .ToList();

            decimal orderSubTotalDiscountAmountBase;
            Discount orderSubTotalAppliedDiscount;
            decimal subTotalWithoutDiscountBase;
            decimal subTotalWithDiscountBase;
            var subTotalIncludingTax = _workContext.TaxDisplayType == TaxDisplayType.IncludingTax && !_taxSettings.ForceTaxExclusionFromOrderSubtotal;
            _orderTotalCalculationService.GetShoppingCartSubTotal(cart, subTotalIncludingTax,
                out orderSubTotalDiscountAmountBase, out orderSubTotalAppliedDiscount,
                out subTotalWithoutDiscountBase, out subTotalWithDiscountBase);
            decimal subtotalBase = subTotalWithoutDiscountBase;
            decimal subtotal = _currencyService.ConvertFromPrimaryStoreCurrency(subtotalBase, _workContext.WorkingCurrency);
            var subTotal = _priceFormatter.FormatPrice(subtotal, true, _workContext.WorkingCurrency, _workContext.WorkingLanguage, subTotalIncludingTax);
            
            //_affiliateProgramService.VerifyQualificaion(program, order);
            var valueType = "qwer".GetType().IsValueType;

            var ans = CompareReferenceTypes<string, string> ("<=", "15.", "12.33");
            
            //_shoppingCartService.
            return View();
        }

        public bool CompareValueTypes<T>(string op, T x, T y) where T : IComparable
        {
            switch (op)
            {
                case "==":  return x.CompareTo(y) == 0;
                case "!=":  return x.CompareTo(y) != 0;
                case ">":   return x.CompareTo(y) > 0;
                case ">=":  return x.CompareTo(y) >= 0;
                case "<":   return x.CompareTo(y) < 0;
                case "<=":  return x.CompareTo(y) <= 0;
                default:    return false;
            }
        }

        private bool CompareReferenceTypes<T1, T2>(string op, T1 x, T2 y) where T1 : class where T2 : class
        {
            switch (op)
            {
                case "==":
                    if (x is string && y is string)
                    {
                        return x == y;
                    }
                    return x.GetType() == y.GetType();
                case "!=":
                    if (x is string && y is string)
                    {
                        return x != y;
                    }
                    return x.GetType() != y.GetType();
                case ">":
                    return decimal.Parse(x.ToString()) > decimal.Parse(y.ToString());
                case ">=":
                    return decimal.Parse(x.ToString()) >= decimal.Parse(y.ToString());
                case "<":
                    return decimal.Parse(x.ToString()) < decimal.Parse(y.ToString());
                case "<=":
                    return decimal.Parse(x.ToString()) <= decimal.Parse(y.ToString());
                default: return false;
            }
        }
    }
}
