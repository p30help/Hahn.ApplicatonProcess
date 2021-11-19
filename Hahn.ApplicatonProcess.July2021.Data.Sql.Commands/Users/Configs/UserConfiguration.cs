using Hahn.ApplicationProcess.July2021.Domain.Users;
using Hahn.ApplicationProcess.July2021.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Commands.Users.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).HasConversion(x => x.Value,
                x => new HumanName(x));

            builder.Property(x => x.LastName).HasConversion(x => x.Value,
                x => new HumanName(x));

            builder.Property(x => x.Age).HasConversion(x => x.Value,
                x => new HumanAge(x));

            builder.Property(x => x.Email).HasConversion(x => x.Value,
                x => new EmailField(x));
        }
    }
}