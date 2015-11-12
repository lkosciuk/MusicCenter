using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class FilesConfiguration : EntityTypeConfiguration<Files>
    {
        public FilesConfiguration()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.name).HasMaxLength(100).IsRequired();
            this.Property(a => a.path).HasMaxLength(100).IsRequired();

            //relationships
            //this.HasOptional(a => a.user).WithOptionalDependent(a => a.profilePhoto);
            this.HasOptional(a => a.band).WithMany(a => a.images);
            this.HasOptional(a => a.concert).WithMany(a => a.images);
            this.HasOptional(a => a.tour).WithMany(a => a.images);

            //configure table map
            this.ToTable("Files");
        }
    }
}
