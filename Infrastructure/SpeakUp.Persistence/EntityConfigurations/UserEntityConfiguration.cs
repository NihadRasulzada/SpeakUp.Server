using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpeakUp.Domain.Models;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence.EntityConfigurations;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user", SpeakUpContext.DEFAULT_SCHEMA);
    }
}