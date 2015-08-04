using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class FavouritesConfiguration : EntityTypeConfiguration<Favourites>
    {
        public FavouritesConfiguration()
        {
            this.HasKey(a => a.Id);

            //relationships
            this.HasMany(a => a.bands).WithMany(a => a.favourites);
            this.HasMany(a => a.albums).WithMany(a => a.favourites);
            this.HasMany(a => a.tracks).WithMany(a => a.favourites);
            this.HasRequired(a => a.user).WithRequiredDependent(a => a.favourites);

            //configure table map
            this.ToTable("Favourites");
        }
    }
}
