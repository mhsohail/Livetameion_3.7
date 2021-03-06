﻿using Nop.Plugin.Misc.GroupDeals.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Maps
{
    public class GroupDealMap : EntityTypeConfiguration<GroupDeal>
    {
        public GroupDealMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("Groupdeals");
            HasKey(gd => gd.Id);

            Property(gd => gd.Active);
            Property(gd => gd.SeName);
            Property(gd => gd.CouponCode);
        }
    }
}
