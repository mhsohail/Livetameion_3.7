using Nop.Core.Configuration;
using Nop.Plugin.Tameion.Forte.Models;

namespace Nop.Plugin.Tameion.BridgePay.Models
{
    public class ForteSettings : ISettings
    {
        public bool UseSandbox { get; set; }
        public TransactionMode TransactMode { get; set; }
        public string TransactionKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MerchantKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to "additional fee" is specified as percentage. true - percentage, false - fixed value.
        /// </summary>
        public bool AdditionalFeePercentage { get; set; }
        /// <summary>
        /// Additional fee
        /// </summary>
        public decimal AdditionalFee { get; set; }
        public string GatewayUrl { get; set; }
    }
}
