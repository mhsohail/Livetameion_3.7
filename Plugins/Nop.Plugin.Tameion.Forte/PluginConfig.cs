﻿using System;
using System.Collections.Generic;
using System.Web.Routing;
using Nop.Core.Domain.Orders;
using Nop.Core.Plugins;
using Nop.Services.Payments;
using Nop.Services.Customers;
using System.Net;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using Nop.Core;
using Nop.Services.Orders;
using Nop.Services.Security;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using System.Linq;
using Nop.Plugin.Tameion.BridgePay.Models;
using Nop.Plugin.Tameion.Forte.Models;
using Nop.Plugin.Tameion.Forte.Controllers;

namespace Nop.Plugin.Tameion.Forte
{
    public class PluginConfig : BasePlugin, IPaymentMethod
    {
        public override PluginDescriptor PluginDescriptor { get; set; }
        private readonly ForteSettings _forteSettings;
        private readonly ISettingService _settingService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerService _customerService;
        private readonly CurrencySettings _currencySettings;
        private readonly IWebHelper _webHelper;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IEncryptionService _encryptionService;

        public PluginConfig(ForteSettings forteSettings,
            ISettingService settingService,
            ICurrencyService currencyService,
            ICustomerService customerService,
            CurrencySettings currencySettings,
            IWebHelper webHelper,
            IOrderTotalCalculationService orderTotalCalculationService,
            IEncryptionService encryptionService)
        {
            this._forteSettings = forteSettings;
            this._settingService = settingService;
            this._currencyService = currencyService;
            this._customerService = customerService;
            this._currencySettings = currencySettings;
            this._webHelper = webHelper;
            this._orderTotalCalculationService = orderTotalCalculationService;
            this._encryptionService = encryptionService;
        }

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
        }
        
        public PaymentMethodType PaymentMethodType
        {
            get
            {
                return PaymentMethodType.Standard;
            }
        }
        
        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                return RecurringPaymentType.Manual;
            }
        }

        public bool SkipPaymentInfo
        {
            get
            {
                return false;
            }
        }

        public bool SupportCapture
        {
            get
            {
                return true;
            }
        }

        public bool SupportPartiallyRefund
        {
            get
            {
                return true;
            }
        }

        public bool SupportRefund
        {
            get
            {
                return true;
            }
        }

        public bool SupportVoid
        {
            get
            {
                return true;
            }
        }

        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public bool CanRePostProcessPayment(Order order)
        {
            throw new NotImplementedException();
        }

        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            throw new NotImplementedException();
        }

        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            var result = this.CalculateAdditionalFee(_orderTotalCalculationService, cart,
                _forteSettings.AdditionalFee, _forteSettings.AdditionalFeePercentage);
            return result;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "BridgePay";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Tameion.BridgePay.Controllers" }, { "area", null } };
        }

        public Type GetControllerType()
        {
            return typeof(ForteController);
        }

        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "BridgePay";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Tameion.BridgePay.Controllers" }, { "area", null } };
        }

        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            //you can put any logic here
            //for example, hide this payment method if all products in the cart are downloadable
            //or hide this payment method if current customer is from certain country
            return false;
        }

        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            // nothing
        }

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            TransactionResponse TransactionResponse = null;
            var authorizeNetPaymentSettings = _settingService.LoadSetting<ForteSettings>();
            var twoDigitMonth = processPaymentRequest.CreditCardExpireMonth.ToString("00");
            var twoDigitYear = processPaymentRequest.CreditCardExpireYear.ToString().Substring(2, 2);

            string serviceUrl = string.Format(authorizeNetPaymentSettings.GatewayUrl + "?" +
                "UserName=" + authorizeNetPaymentSettings.Username + "&" +
                "Password=" + authorizeNetPaymentSettings.Password + "&" +
                "TransType=Sale&"+
                "CardNum=" + processPaymentRequest.CreditCardNumber + "&"+
                "ExpDate=" + twoDigitMonth + twoDigitYear + "&"+
                "MagData=data&" +
                "NameOnCard=" + processPaymentRequest.CreditCardName + "&" +
                "Amount=" + processPaymentRequest.OrderTotal + "&" +
                "InvNum=1&" +
                "PNRef=1&" +
                "Zip=43600&" +
                "Street=Kamra&" +
                "CVNum=" + processPaymentRequest.CreditCardCvv2 + "&" +
                "ExtData=ext-data");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                //Receipt Receipt = null;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responstText = streamReader.ReadToEnd();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(responstText);
                    if (doc.ChildNodes[1].Name == "Response")
                    {
                        var responseNode = doc.ChildNodes[1];
                        var responseNodes = responseNode.Cast<XmlNode>().ToArray();
                        var res = responseNodes.SingleOrDefault(n => n.Name == "Result");
                        string jsonText = JsonConvert.SerializeXmlNode(node: responseNode, formatting: Newtonsoft.Json.Formatting.None, omitRootObject: true);

                        // parse json to anonymous object
                        var Response = new
                        {
                            Result = 0,
                            RespMSG = string.Empty,
                            ExtData = string.Empty
                        };
                        Response = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(jsonText, Response);

                        // parse json to class object
                        TransactionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TransactionResponse>(jsonText);
                        
                    }
                }
            }
            catch (Exception)
            { }

            var result = new ProcessPaymentResult();
            var customer = _customerService.GetCustomerById(processPaymentRequest.CustomerId);
            if (TransactionResponse != null)
            {
                switch (TransactionResponse.Result)
                {
                    case 0:
                        result.AuthorizationTransactionCode = string.Format("{0}", TransactionResponse.AuthCode);
                        result.AuthorizationTransactionResult = string.Format("Approved {0})", TransactionResponse.Message1);
                        result.AvsResult = TransactionResponse.GetAVSResult;
                        //responseFields[38];
                        //if (_authorizeNetPaymentSettings.TransactMode == TransactMode.Authorize)
                        //{
                        //    result.NewPaymentStatus = PaymentStatus.Authorized;
                        //}
                        //else
                        //{
                        //    result.NewPaymentStatus = PaymentStatus.Paid;
                        //}
                        break;
                    case 24:
                        result.AddError(string.Format("Error: {0}", TransactionResponse.Message));
                        break;
                    case 110:
                        result.AddError(string.Format("Error: {0}", TransactionResponse.RespMSG));
                        break;

                }
            }
            else
            {
                result.AddError("BridgePay unknown error");
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////
            
            //var webClient = new WebClient();
            //var form = new NameValueCollection();
            //form.Add("x_login", _authorizeNetPaymentSettings.LoginId);
            //form.Add("x_tran_key", _authorizeNetPaymentSettings.TransactionKey);
            
            ////we should not send "x_test_request" parameter. otherwise, the transaction won't be logged in the sandbox
            ////if (_authorizeNetPaymentSettings.UseSandbox)
            ////    form.Add("x_test_request", "TRUE");
            ////else
            ////    form.Add("x_test_request", "FALSE");

            //form.Add("x_delim_data", "TRUE");
            //form.Add("x_delim_char", "|");
            //form.Add("x_encap_char", "");
            //form.Add("x_version", GetApiVersion());
            //form.Add("x_relay_response", "FALSE");
            //form.Add("x_method", "CC");
            //form.Add("x_currency_code", _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode);
            //if (_authorizeNetPaymentSettings.TransactMode == TransactMode.Authorize)
            //    form.Add("x_type", "AUTH_ONLY");
            //else if (_authorizeNetPaymentSettings.TransactMode == TransactMode.AuthorizeAndCapture)
            //    form.Add("x_type", "AUTH_CAPTURE");
            //else
            //    throw new NopException("Not supported transaction mode");

            //var orderTotal = Math.Round(processPaymentRequest.OrderTotal, 2);
            //form.Add("x_amount", orderTotal.ToString("0.00", CultureInfo.InvariantCulture));
            //form.Add("x_card_num", processPaymentRequest.CreditCardNumber);
            //form.Add("x_exp_date", processPaymentRequest.CreditCardExpireMonth.ToString("D2") + processPaymentRequest.CreditCardExpireYear.ToString());
            //form.Add("x_card_code", processPaymentRequest.CreditCardCvv2);
            //form.Add("x_first_name", customer.BillingAddress.FirstName);
            //form.Add("x_last_name", customer.BillingAddress.LastName);
            //form.Add("x_email", customer.BillingAddress.Email);
            //if (!string.IsNullOrEmpty(customer.BillingAddress.Company))
            //    form.Add("x_company", customer.BillingAddress.Company);
            //form.Add("x_address", customer.BillingAddress.Address1);
            //form.Add("x_city", customer.BillingAddress.City);
            //if (customer.BillingAddress.StateProvince != null)
            //    form.Add("x_state", customer.BillingAddress.StateProvince.Abbreviation);
            //form.Add("x_zip", customer.BillingAddress.ZipPostalCode);
            //if (customer.BillingAddress.Country != null)
            //    form.Add("x_country", customer.BillingAddress.Country.TwoLetterIsoCode);
            ////x_invoice_num is 20 chars maximum. hece we also pass x_description
            //form.Add("x_invoice_num", processPaymentRequest.OrderGuid.ToString().Substring(0, 20));
            //form.Add("x_description", string.Format("Full order #{0}", processPaymentRequest.OrderGuid));
            //form.Add("x_customer_ip", _webHelper.GetCurrentIpAddress());

            //var responseData = webClient.UploadValues(GetAuthorizeNetUrl(), form);
            //var reply = Encoding.ASCII.GetString(responseData);

            //if (!String.IsNullOrEmpty(reply))
            //{
            //    string[] responseFields = reply.Split('|');
            //    switch (responseFields[0])
            //    {
            //        case "1":
            //            result.AuthorizationTransactionCode = string.Format("{0},{1}", responseFields[6], responseFields[4]);
            //            result.AuthorizationTransactionResult = string.Format("Approved ({0}: {1})", responseFields[2], responseFields[3]);
            //            result.AvsResult = responseFields[5];
            //            //responseFields[38];
            //            if (_authorizeNetPaymentSettings.TransactMode == TransactMode.Authorize)
            //            {
            //                result.NewPaymentStatus = PaymentStatus.Authorized;
            //            }
            //            else
            //            {
            //                result.NewPaymentStatus = PaymentStatus.Paid;
            //            }
            //            break;
            //        case "2":
            //            result.AddError(string.Format("Declined ({0}: {1})", responseFields[2], responseFields[3]));
            //            break;
            //        case "3":
            //            result.AddError(string.Format("Error: {0}", reply));
            //            break;

            //    }
            //}
            //else
            //{
            //    result.AddError("Authorize.NET unknown error");
            //}

            return result;
        }

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
