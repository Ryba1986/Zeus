using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Users;

namespace Zeus.Infrastructure.Repositories.Configurations.Users
{
   internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
   {
      public void Configure(EntityTypeBuilder<User> builder)
      {
         builder.Property(x => x.Id)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

         builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

         builder.Property(x => x.Password)
            .HasMaxLength(64)
            .IsFixedLength()
            .IsRequired();

         builder.Property(x => x.Role)
            .IsRequired();

         builder.Property(x => x.IsActive)
            .IsRequired();

         builder.Property(x => x.Version)
            .IsRequired()
            .IsRowVersion();

         builder.HasKey(x => x.Id)
            .IsClustered();
      }
   }
}
