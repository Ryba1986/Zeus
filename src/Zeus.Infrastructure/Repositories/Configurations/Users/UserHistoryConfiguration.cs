using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Domain.Users;

namespace Zeus.Infrastructure.Repositories.Configurations.Users
{
   internal sealed class UserHistoryConfiguration : IEntityTypeConfiguration<UserHistory>
   {
      public void Configure(EntityTypeBuilder<UserHistory> builder)
      {
         builder.Property(x => x.Id)
            .IsRequired();

         builder.Property(x => x.UserId)
            .IsRequired();

         builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

         builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

         builder.Property(x => x.Role)
            .HasMaxLength(30)
            .IsRequired();

         builder.Property(x => x.IsActive)
           .IsRequired();

         builder.Property(x => x.CreatedById)
            .IsRequired();

         builder.Property(x => x.CreateDate)
            .IsRequired();

         builder.HasKey(x => x.Id)
            .IsClustered();

         builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId);

         builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedById);
      }
   }
}
