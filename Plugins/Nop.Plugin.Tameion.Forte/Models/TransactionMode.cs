using System.ComponentModel.DataAnnotations;
namespace Nop.Plugin.Tameion.Forte.Models
{
    public enum TransactionMode
    {
        [Display(Name = "Authorize")]
        Authorize = 1,
        [Display(Name = "Authorize and Capture")]
        AuthorizeAndCapture = 2
    }
}
