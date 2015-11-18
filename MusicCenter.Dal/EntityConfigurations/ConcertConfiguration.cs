using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class ConcertConfiguration : EntityTypeConfiguration<Concert>
    {
        public ConcertConfiguration()
        {
            //this.HasKey(a => a.Id);
            this.Property(a => a.date).IsRequired();
            this.Property(a => a.ticketPrice).HasMaxLength(15).IsOptional();
            this.Property(a => a.ticketUrl).HasMaxLength(200).IsOptional();
            this.Property(a => a.address).HasMaxLength(30).IsRequired();
            this.Property(a => a.coordinatesX).IsOptional();
            this.Property(a => a.coordinatesY).IsOptional();

            //relationships
            this.HasMany(a => a.images).WithOptional(a => a.concert);
            //this.HasMany(a => a.users).WithMany(a => a.concerts);
            this.HasOptional(a => a.tour).WithMany(a => a.concerts);
            this.HasRequired(a => a.band).WithMany(a => a.concerts);
            this.HasMany(a => a.favouritess).WithMany(a => a.concerts);
            //configure table map
            this.ToTable("Concert");
        }
    }
}
