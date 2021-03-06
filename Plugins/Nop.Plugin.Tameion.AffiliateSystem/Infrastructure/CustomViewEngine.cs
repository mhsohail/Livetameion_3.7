﻿using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Plugins/Tameion.AffiliateProgram/Views/{0}.cshtml",
                "~/Plugins/Tameion.AffiliateProgram/Views/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = new[] {
                "~/Plugins/Tameion.AffiliateProgram/Views/{0}.cshtml",
                "~/Plugins/Tameion.AffiliateProgram/Views/{1}/{0}.cshtml"
            };
        }
    }
}
