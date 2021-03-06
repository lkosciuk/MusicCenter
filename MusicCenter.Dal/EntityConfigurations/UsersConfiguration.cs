﻿using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    class UsersConfiguration : BaseEntityMap<Users>
    {
        public UsersConfiguration() : base()
        {
            this.ToTable("Users");
            this.Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(a => a.password).IsRequired();
            this.Property(a => a.email).IsRequired();

            HasOptional(u => u.favourites).WithRequired();
        }
    }
}
