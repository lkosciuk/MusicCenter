using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            //this.HasKey(a => a.Id);

            this.Property(a => a.name).HasMaxLength(20).IsRequired();

            //relationships
            this.HasMany(a => a.bands).WithMany(a => a.genres);
            this.HasMany(a => a.albums).WithMany(a => a.genres);

            //configure table map
            this.ToTable("Genre");
        }
    }
}
