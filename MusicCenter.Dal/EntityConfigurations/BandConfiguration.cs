using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class BandConfiguration : EntityTypeConfiguration<Band>
    {
        public BandConfiguration()
        {
            this.HasKey(a => a.Id);
            //this.Property(a => a.login).HasMaxLength(15).IsRequired();
            //this.Property(a => a.password).HasMaxLength(15).IsRequired();
            this.Property(a => a.email).HasMaxLength(15).IsRequired();
            this.Property(a => a.name).HasMaxLength(20).IsRequired();
            this.Property(a => a.description).HasMaxLength(1000);
            this.Property(a => a.phoneNumber).HasMaxLength(15);



            //relationships
            this.HasMany(a => a.members).WithMany(a => a.bands);
            this.HasMany(a => a.images).WithOptional(a => a.band);
            this.HasMany(a => a.genres).WithMany(a => a.bands);
            this.HasMany(a => a.albums).WithRequired(a => a.band);
            this.HasMany(a => a.singles).WithOptional(a => a.band);
            this.HasMany(a => a.concerts).WithRequired(a => a.band);
            this.HasMany(a => a.tours).WithRequired(a => a.band);
            this.HasMany(a => a.sentMessages).WithOptional(a => a.BandAuthor);
            this.HasMany(a => a.receivedMessages).WithMany(a => a.BandReceivers);
            this.HasMany(a => a.favourites).WithMany(a => a.bands);
            this.HasRequired(a => a.user).WithMany(a => a.bands);
            //configure table map
            this.ToTable("Band");
        }
    }
}
