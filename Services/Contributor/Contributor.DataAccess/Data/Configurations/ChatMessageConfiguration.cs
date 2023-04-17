using HardwareHero.Services.Shared.Models.Contributor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contributor.DataAccess.Data.Configurations
{
    internal class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Text).IsRequired().HasMaxLength(2048);

            builder.Property(c => c.Timestamp).IsRequired().HasDefaultValue(DateTime.Now);

            builder.Property(c => c.SenderId).IsRequired();

            builder.Property(c => c.ChatRoomId).IsRequired();
        }
    }
}
