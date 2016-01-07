using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;

namespace Nop.Plugin.Misc.VendorMembership.Areas.Vendor.Controllers
{
    public class ReturnRequestController : Nop.Admin.Controllers.ReturnRequestController
    {
        public ReturnRequestController(IReturnRequestService returnRequestService, 
            IOrderService orderService,
            ICustomerService customerService, IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService, IWorkContext workContext,
            IWorkflowMessageService workflowMessageService, LocalizationSettings localizationSettings,
            ICustomerActivityService customerActivityService, IPermissionService permissionService)
            : base(returnRequestService, orderService, customerService, 
                  dateTimeHelper, localizationService, 
                  workContext, workflowMessageService, 
                  localizationSettings, customerActivityService, permissionService)
        {  }
    }
}
