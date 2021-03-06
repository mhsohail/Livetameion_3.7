﻿using Nop.Plugin.Misc.TaxGroups.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Infrastructure
{
    public class TaxClassMap : EntityTypeConfiguration<TaxClass>
    {
        public TaxClassMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("TaxClasses");
            HasKey(g => g.Id);

            Property(g => g.Name);
        }
    }
}
