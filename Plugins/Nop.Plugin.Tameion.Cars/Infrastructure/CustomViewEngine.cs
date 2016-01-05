using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Tameion.Cars.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Plugins/Tameion.Cars/Views/{0}.cshtml",
                "~/Plugins/Tameion.Cars/Views/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = new[] {
                "~/Plugins/Tameion.Cars/Views/{0}.cshtml",
                "~/Plugins/Tameion.Cars/Views/{1}/{0}.cshtml"
            };
        }
    }
}
