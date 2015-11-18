using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class BandMemberConfiguration : EntityTypeConfiguration<BandMember>
    {
        public BandMemberConfiguration()
            : base()
        {
            this.ToTable("BandMember");

            this.Property(a => a.fullName).HasMaxLength(50).IsRequired();

            //relationships
            
        }
    }
}
