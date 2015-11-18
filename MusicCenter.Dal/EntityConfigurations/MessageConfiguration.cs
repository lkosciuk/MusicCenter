using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityConfigurations
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            //this.HasKey(a => a.Id);

            this.Property(a => a.title).HasMaxLength(50).IsRequired();
            this.Property(a => a.content).HasMaxLength(1000).IsRequired();
            this.Property(a => a.sentDate).IsOptional();
            this.Property(a => a.isReaded).IsRequired();
            
            //relationships
            this.HasOptional(a => a.UserAuthor).WithMany(a => a.sentMessages);
            this.HasOptional(a => a.BandAuthor).WithMany(a => a.sentMessages);
            this.HasMany(a => a.UserReceivers).WithMany(a => a.receivedMessages);
            this.HasMany(a => a.BandReceivers).WithMany(a => a.receivedMessages);

            //configure table map
            this.ToTable("Message");
        }
    }
}
