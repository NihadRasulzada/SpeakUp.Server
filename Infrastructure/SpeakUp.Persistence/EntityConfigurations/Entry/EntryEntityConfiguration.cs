using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence.EntityConfigurations.Entry;

public class EntryEntityConfiguration : BaseEntityConfiguration<Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Domain.Models.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("entry", SpeakUpContext.DEFAULT_SCHEMA);


        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.Entries)
            .HasForeignKey(i => i.CreatedById);
    }
}