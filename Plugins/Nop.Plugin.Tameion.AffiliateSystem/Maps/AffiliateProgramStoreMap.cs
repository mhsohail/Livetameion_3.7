﻿using Nop.Plugin.Tameion.AffiliateSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.AffiliateSystem.Maps
{
    public class AffiliateProgramStoreMap : EntityTypeConfiguration<AffiliateProgramStore>
    {
        public AffiliateProgramStoreMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("AffiliateProgramStores");
            HasKey(aff => aff.Id);

            Property(aff => aff.AffiliateProgramId);
            Property(aff => aff.StoreId);
        }
    }
}
