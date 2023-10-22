using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(e => e.Id);
        builder.Property(f => f.Id)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(f => f.Nombre)
            .IsRequired()
            .HasColumnName("Nombre")
            .HasComment("Nombre del usuario")
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(f => f.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasComment("Password del usuario")
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(f => f.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasComment("Correo del usuario")
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.HasMany(p => p.Rols)
            .WithMany(p => p.Users)
            .UsingEntity<UserRol>(

                j => j
                    .HasOne(pt => pt.Rol)
                    .WithMany(t => t.UserRols)
                    .HasForeignKey(pt => pt.IdRolFK),

                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(t => t.UserRols)
                    .HasForeignKey(pt => pt.IdUserFK),

                j =>
                {
                    j.ToTable("UserRol");
                    j.HasKey(t => new { t.IdUserFK, t.IdRolFK });
                });

        builder.HasMany(p => p.RefreshTokens)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.IdUserFK);
    }
}