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
            : base()
        {
            this.ToTable("Message");

            this.Property(a => a.title).HasMaxLength(50).IsRequired();
            this.Property(a => a.content).HasMaxLength(1000).IsRequired();
            this.Property(a => a.sentDate).IsOptional();
            this.Property(a => a.isReaded).IsRequired();
            
            //relationships
            this.HasOptional(t => t.BandAuthor)
                .WithMany(t => t.receivedMessages)
                .HasForeignKey(d => d.BandID);

            this.HasOptional(t => t.UserAuthor)
                .WithMany(t => t.receivedMessages)
                .HasForeignKey(d => d.UserID);

            this.HasMany(t => t.UserReceivers)
                .WithMany(t => t.sentMessages)
                .Map(m =>
                {
                    m.ToTable("MessageUser");
                    m.MapLeftKey("MessageID");
                    m.MapRightKey("UserID");
                });

            this.HasMany(t => t.BandReceivers)
                .WithMany(t => t.sentMessages)
                .Map(m =>
                {
                    m.ToTable("MessageBand");
                    m.MapLeftKey("MessageID");
                    m.MapRightKey("BandID");
                });
        }
    }
}
