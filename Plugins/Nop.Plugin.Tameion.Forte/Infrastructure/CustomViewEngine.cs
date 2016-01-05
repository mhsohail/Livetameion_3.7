using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Tameion.Forte.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] 
            {
                "~/Plugins/Tameion.Forte/Views/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = new[] 
            {
                "~/Plugins/Tameion.Forte/Views/{1}/{0}.cshtml"
            };
        }
    }
}
