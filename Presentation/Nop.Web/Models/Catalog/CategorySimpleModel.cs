﻿using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Web.Models.Catalog
{
    public class CategorySimpleModel : BaseNopEntityModel
    {
        public CategorySimpleModel()
        {
            SubCategories = new List<CategorySimpleModel>();
        }

        public string Name { get; set; }

        public string SeName { get; set; }

        public int? NumberOfProducts { get; set; }

        public bool IncludeInTopMenu { get; set; }

        // for nopshoptheme
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public int PictureId { get; set; }

        public List<CategorySimpleModel> SubCategories { get; set; }
    }
}