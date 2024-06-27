using GestaoDeResiduos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeResiduos.Infra
{
    public class DatabaseContext : DbContext
    { 
          
        public virtual DbSet<DistrictModel> Districts { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<StateModel> States { get; set; }
        public virtual DbSet<RegionModel> Regions { get; set; }
        public virtual DbSet<GarbageCollectionTypeModel> GarbageCollectionTypes { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
              
            //usuarios
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("USERS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("ID")
                      .HasDefaultValueSql("USERS_SEQ.NEXTVAL")
                      .ValueGeneratedOnAdd();

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
            
            //estados
            modelBuilder.Entity<StateModel>(entity =>
            {
                  entity.ToTable("STATES");
                  entity.HasKey(e => e.Id);
                  
                  entity.Property(e => e.Id).HasColumnName("ID").HasDefaultValueSql("STATES_SEQ.NEXTVAL").ValueGeneratedOnAdd();
                  entity.Property(e => e.Name).IsRequired().HasColumnName("NAME");
                  entity.Property(e => e.UF).IsRequired().HasMaxLength(2).HasColumnName("UF");;
                  entity.HasIndex(e => e.UF).IsUnique().HasDatabaseName("IDX_UF");
            });
            
            //regioes
            modelBuilder.Entity<RegionModel>(entity =>
            {
                  entity.ToTable("REGIONS");

                  entity.HasKey(e => e.Id);

                  entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasDefaultValueSql("REGIONS_SEQ.NEXTVAL")
                        .ValueGeneratedOnAdd();

                  entity.Property(e => e.Name)
                        .IsRequired()
                        .HasColumnName("NAME");

                  entity.Property(e => e.StateId)
                        .IsRequired()
                        .HasColumnName("STATE_ID");

                  entity.HasOne(e => e.State)
                        .WithMany()
                        .HasForeignKey(e => e.StateId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
            
            
            //bairros
            modelBuilder.Entity<DistrictModel>(entity =>
            {
                  entity.ToTable("DISTRICTS");

                  entity.HasKey(e => e.Id);

                  entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasDefaultValueSql("DISTRICTS_SEQ.NEXTVAL")
                        .ValueGeneratedOnAdd();

                  entity.Property(e => e.Name)
                        .IsRequired()
                        .HasColumnName("NAME");

                  entity.Property(e => e.RegionId)
                        .IsRequired()
                        .HasColumnName("REGION_ID");

                  entity.HasOne(e => e.Region)
                        .WithMany()
                        .HasForeignKey(e => e.RegionId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
            
            //ruas
            modelBuilder.Entity<StreetModel>(entity =>
            {
                  entity.ToTable("STREETS");

                  entity.HasKey(e => e.Id);

                  entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasDefaultValueSql("STREETS_SEQ.NEXTVAL")
                        .ValueGeneratedOnAdd();

                  entity.Property(e => e.Name)
                        .IsRequired()
                        .HasColumnName("NAME");

                  entity.Property(e => e.DistrictId)
                        .IsRequired()
                        .HasColumnName("DISTRICT_ID");

                  entity.HasOne(e => e.District)
                        .WithMany()
                        .HasForeignKey(e => e.DistrictId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
            
            //tipo de coleta
            modelBuilder.Entity<GarbageCollectionTypeModel>(entity =>
            {
                  entity.ToTable("GARBAGE_COLLECTION_TYPES");

                  entity.HasKey(e => e.Id);

                  entity.Property(e => e.Id)
                        .HasColumnName("ID")
                        .HasDefaultValueSql("GARBAGE_COLLECTION_SEQ.NEXTVAL")
                        .ValueGeneratedOnAdd();

                  entity.Property(e => e.Name)
                        .IsRequired()
                        .HasColumnName("NAME");
                  
            });


            base.OnModelCreating(modelBuilder);
        }

    }
}
