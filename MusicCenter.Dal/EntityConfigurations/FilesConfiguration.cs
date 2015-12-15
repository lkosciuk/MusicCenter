using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class FilesConfiguration : BaseEntityMap<Files>
    {
        public FilesConfiguration()
            : base()
        {
            this.ToTable("Files");
            //this.Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(a => a.name).HasMaxLength(100).IsRequired();
            this.Property(a => a.path).HasMaxLength(100).IsRequired();

            //relationships

            this.HasOptional(t => t.band)
                 .WithMany(t => t.images)
                 .HasForeignKey(d => d.BandID);

            this.HasOptional(t => t.concert)
                 .WithMany(t => t.images)
                 .HasForeignKey(d => d.ConcertID);

            this.HasOptional(t => t.tour)
                 .WithMany(t => t.images)
                 .HasForeignKey(d => d.TourID);

            this.HasOptional(t => t.user)
                 .WithMany(t => t.images)
                 .HasForeignKey(d => d.UserID);
        }
    }
}
