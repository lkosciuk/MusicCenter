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
    class UsersConfiguration 
        : BaseEntityMap<Users>
    {
        public UsersConfiguration() : base()
        {
            this.ToTable("Users");
            this.Property(a => a.password).HasMaxLength(10);
            this.Property(a => a.email).HasMaxLength(60).IsRequired();

            //relationships
            HasRequired(u => u.profilePhoto)
            .WithOptional(c => c.user);

            HasOptional(u => u.favourites).WithRequired();
        }
    }
}
