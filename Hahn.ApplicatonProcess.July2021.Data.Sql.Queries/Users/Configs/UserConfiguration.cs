using Hahn.ApplicationProcess.July2021.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicationProcess.July2021.Data.Sql.Queries.Users.Configs
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDto>
    {
        public void Configure(EntityTypeBuilder<UserDto> builder)
        {
            
        }
    }
}