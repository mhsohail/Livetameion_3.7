using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using Nop.Core.Data;
using Nop.Services.Events;
using Nop.Core.Domain.Orders;
using Nop.Core;
using Nop.Services.Orders;
using Nop.Core.Domain.Tax;
using Nop.Services.Directory;
using Nop.Services.Catalog;
using Nop.Core.Domain.Discounts;
using System.Text.RegularExpressions;
using Nop.Admin.Models.Catalog;

namespace Nop.Plugin.Tameion.AffiliateSystem.Services
{
    public class AffiliateProgramService : IAffiliateProgramService
    {
        #region Fields
        private readonly IRepository<AffiliateProgram> _affiliateProgramRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<ProgramCondition> _programConditionRepository;
        private readonly ICurrencyService _currencyService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStoreContext _storeContext;
        private readonly TaxSettings _taxSettings;
        private readonly IWorkContext _workContext;
        #endregion

        #region AffiliateProgram service methods
        public AffiliateProgramService(IRepository<AffiliateProgram> affiliateProgramRepository,
            IRepository<ProgramCondition> programConditionRepository,
            IEventPublisher eventPublisher,
            IWorkContext workContext,
            IStoreContext storeContext,
            IShoppingCartService shoppingCartService,
            TaxSettings taxSettings,
            IOrderTotalCalculationService orderTotalCalculationService,
            ICurrencyService currencyService,
            IPriceFormatter priceFormatter)
        {
            _affiliateProgramRepository = affiliateProgramRepository;
            _programConditionRepository = programConditionRepository;
            _eventPublisher = eventPublisher;
            _workContext = workContext;
            _storeContext = storeContext;
            _shoppingCartService = shoppingCartService;
            _taxSettings = taxSettings;
            _orderTotalCalculationService = orderTotalCalculationService;
            _currencyService = currencyService;
            _priceFormatter = priceFormatter;
        }

        public void DeleteAffiliateProgram(AffiliateProgram affiliateProgram)
        {
            if (affiliateProgram == null)
                throw new ArgumentNullException("affiliateProgram");

            _affiliateProgramRepository.Delete(affiliateProgram);

            _eventPublisher.EntityDeleted(affiliateProgram);
        }

        public AffiliateProgram GetAffiliateProgramById(int affiliateProgramId)
        {
            return _affiliateProgramRepository.GetById(affiliateProgramId);
        }

        public IList<AffiliateProgram> GetAllAffiliatePrograms()
        {
            return _affiliateProgramRepository.Table.ToList();
        }
        
        public void InsertAffiliateProgram(AffiliateProgram affiliateProgram)
        {
            if (affiliateProgram == null)
                throw new ArgumentNullException("affiliateProgram");

            _affiliateProgramRepository.Insert(affiliateProgram);

            _eventPublisher.EntityInserted(affiliateProgram);
        }

        public void UpdateAffiliateProgram(AffiliateProgram affiliateProgram)
        {
            if (affiliateProgram == null)
                throw new ArgumentNullException("affiliateProgram");

            _affiliateProgramRepository.Update(affiliateProgram);

            _eventPublisher.EntityUpdated(affiliateProgram);
        }

        public bool VerifyQualificaion(AffiliateProgram AffiliateProgram, Order order)
        {
            foreach (var condition in AffiliateProgram.Conditions.ToList())
            {
                if (condition.Operation == LogicalOperation.Is)
                {
                    var valueType = order.GetType().IsValueType;
                    // comparing cart total price, if value is value type
                    if (condition.Value != null)
                    {
                        if (condition.ConditionAttributeName == "CartTotalPrice")
                        {
                            var ans = CompareReferenceTypes<string, string>(condition.OperationSymbol, GetCartTotalPrice().ToString(), condition.Value);
                        }

                        if (condition.ConditionAttributeName == "CartItemsCount")
                        {
                            var ans = CompareReferenceTypes<string, string>(condition.OperationSymbol, GetCartItemsCount().ToString(), condition.Value);
                        }
                        
                        // if value is numeric (including fractions)
                        var match = Regex.Match(condition.Value, @"^\d+(?:\.?\d*|\s\d+\/\d+)$");
                        if (match.Success)
                        {

                        }
                    }
                    else // else reference type
                    {
                        
                    }
                }
            }
            
            return true;
        }

        private IEnumerable<ShoppingCartItem> GetCartItems()
        {
            var cart = _workContext.CurrentCustomer.ShoppingCartItems
                    .Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart)
                    .LimitPerStore(_storeContext.CurrentStore.Id)
                    .ToList();

            return cart;
        }

        private decimal GetCartTotalPrice()
        {
            var cart = GetCartItems().ToList();
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
            var subTotalFormated = _priceFormatter.FormatPrice(subtotal, true, _workContext.WorkingCurrency, _workContext.WorkingLanguage, subTotalIncludingTax);
            var totalProducts = cart.GetTotalProducts();

            return subtotal;
        }

        private int GetCartItemsCount()
        {
            return GetCartItems().ToList().GetTotalProducts();
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
        
        private bool CompareValueTypes<T>(string op, T x, T y) where T : IComparable
        {
            switch (op)
            {
                case "==": return x.CompareTo(y) == 0;
                case "!=": return x.CompareTo(y) != 0;
                case ">": return x.CompareTo(y) > 0;
                case ">=": return x.CompareTo(y) >= 0;
                case "<": return x.CompareTo(y) < 0;
                case "<=": return x.CompareTo(y) <= 0;
                default: return false;
            }
        }
        #endregion

        #region ProgramCondition service methods
        public void UpdateProgramCondition(ProgramCondition condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            _programConditionRepository.Update(condition);

            _eventPublisher.EntityUpdated(condition);
        }

        public IList<ProgramCondition> GetAllProgramConditions()
        {
            throw new NotImplementedException();
        }

        public ProgramCondition GetProgramConditionById(int condition)
        {
            throw new NotImplementedException();
        }
        
        public void InsertProgramCondition(ProgramCondition condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            _programConditionRepository.Insert(condition);

            _eventPublisher.EntityInserted(condition);
        }
        
        public void DeleteProgramCondition(ProgramCondition condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            _programConditionRepository.Delete(condition);

            _eventPublisher.EntityDeleted(condition);
        }
        #endregion
    }
}
