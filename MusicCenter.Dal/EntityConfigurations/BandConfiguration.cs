﻿using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class BandConfiguration : BaseEntityMap<Band>
    {
        public BandConfiguration()
            : base()
        {
            this.ToTable("Band");

            this.Property(a => a.email).IsRequired();
            this.Property(a => a.name).IsRequired();
            this.Property(a => a.description);
            this.Property(a => a.phoneNumber);
            this.Property(a => a.bandCreationDate).HasColumnType("datetime2");
            this.Property(a => a.bandResolveDate).HasColumnType("datetime2");
            //relationships
            this.HasRequired(t => t.user)
                 .WithMany(t => t.bands)
                 .HasForeignKey(d => d.UserID);

            this.HasMany(t => t.members)
                .WithMany(t => t.bands)
                .Map(m =>
                {
                    m.ToTable("BandToBandMember");
                    m.MapLeftKey("BandID");
                    m.MapRightKey("MemberID");
                });
        }
    }
}
