using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class TourConfiguration : BaseEntityMap<Tour>
    {
        public TourConfiguration()
            : base()
        {
            this.ToTable("Tour");

            this.Property(a => a.name).IsRequired();
            this.Property(a => a.description).IsOptional();

            //relationships
            this.HasRequired(t => t.band)
                 .WithMany(t => t.tours)
                 .HasForeignKey(d => d.BandID);
        }
    }
}
