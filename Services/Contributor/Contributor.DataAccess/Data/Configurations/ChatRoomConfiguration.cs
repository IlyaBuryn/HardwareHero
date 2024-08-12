namespace Contributor.DataAccess.Data.Configurations
{
    internal class ChatRoomConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Subject).IsRequired().HasMaxLength(ValidationValues.SubjectMaxLength);

            builder.Property(c => c.TimeStamp).IsRequired().HasDefaultValue(DateTime.Now);

            builder.HasMany(cr => cr.ChatMessages)
                .WithOne(cm => cm.ChatRoom)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(x => x
                .HasCheckConstraint("MaxParticipantsCount",
                $"COUNT(Participants) <= {ValidationValues.ChatParticipantsMaxCount}"));
        }
    }
}
