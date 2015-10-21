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
            //this.Property(a => a.login).HasMaxLength(20).IsRequired();
            this.Property(a => a.password).HasMaxLength(10).IsRequired();
            this.Property(a => a.email).HasMaxLength(20).IsRequired();

            //relationships
            this.HasOptional(a => a.profilePhoto).WithOptionalPrincipal(a => a.user);
            //this.HasMany(a => a.concerts).WithMany(a => a.users);
            //this.HasMany(a => a.tours).WithMany(a => a.users);
            //this.HasMany(a => a.followed).WithMany(a => a.spectators);
            //this.HasMany(a => a.spectators).WithMany(a => a.followed);
            this.HasRequired(a => a.favourites).WithRequiredPrincipal(a => a.user);
            this.HasMany(a => a.receivedMessages).WithMany(a => a.UserReceivers);
            this.HasMany(a => a.sentMessages).WithOptional(a => a.UserAuthor);
            this.HasOptional(a => a.bandMember).WithOptionalDependent(a => a.user);
            this.HasMany(a => a.bands).WithOptional(a => a.user);
            this.HasMany(a => a.roles).WithMany(a => a.Users);
            //configure table map
            this.ToTable("Users");
        }
    }
}
