using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeResiduos.Infra
{
    public class DatabaseContext : DbContext
    {

        public virtual DbSet<UserModel> Users { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("USERS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("ID")
                      .HasDefaultValueSql("USERS_SEQ.NEXTVAL");

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasColumnName("NAME");

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasColumnName("EMAIL");

                entity.HasIndex(e => e.Email)
                      .IsUnique()
                      .HasDatabaseName("IDX_EMAIL");

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasColumnName("PASSWORD");

                entity.Property(e => e.BirthDate)
                      .IsRequired()
                      .HasColumnName("BIRTH_DATE");

                entity.Property(e => e.Role)
                      .IsRequired()
                      .HasColumnName("ROLE");

            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
