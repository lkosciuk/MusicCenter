using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    class UsersConfiguration 
        : BaseEntityMap<Users>
    {
        public UsersConfiguration()
        {
            this.ToTable("Users");

            this.Property(a => a.password).HasMaxLength(10).IsRequired();
            this.Property(a => a.email).HasMaxLength(20).IsRequired();

            //relationships
            HasOptional(u => u.profilePhoto).WithRequired();
            HasOptional(u => u.favourites).WithRequired();
            HasOptional(u => u.bandMember).WithRequired();
        }
    }
}
