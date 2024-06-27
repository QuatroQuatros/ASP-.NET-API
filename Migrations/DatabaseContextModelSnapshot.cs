﻿// <auto-generated />
using System;
using GestaoDeResiduos.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestaoDeResiduos.Models.CollectionDayModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("COLLECTION_DAY_SEQ.NEXTVAL");

                    b.Property<DateTime>("CollectionDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("COLLECTION_DATE");

                    b.Property<int>("GarbageCollectionTypeId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("GARBAGE_COLLECTION_TYPE_ID");

                    b.Property<DateTime>("ScheduleDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("SCHEDULE_DATE");

                    b.Property<int>("Status")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STATUS");

                    b.Property<int>("StreetId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STREET_ID");

                    b.HasKey("Id");

                    b.HasIndex("GarbageCollectionTypeId");

                    b.HasIndex("StreetId");

                    b.ToTable("COLLECTION_DAY", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.DistrictModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("DISTRICTS_SEQ.NEXTVAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<int>("RegionId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("REGION_ID");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("DISTRICTS", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.GarbageCollectionTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("GARBAGE_COLLECTION_SEQ.NEXTVAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.ToTable("GARBAGE_COLLECTION_TYPES", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.RegionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("REGIONS_SEQ.NEXTVAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<int>("StateId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STATE_ID");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("REGIONS", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.StateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("STATES_SEQ.NEXTVAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR2(2)")
                        .HasColumnName("UF");

                    b.HasKey("Id");

                    b.HasIndex("UF")
                        .IsUnique()
                        .HasDatabaseName("IDX_UF");

                    b.ToTable("STATES", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.StreetModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("STREETS_SEQ.NEXTVAL");

                    b.Property<int>("DistrictId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("DISTRICT_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("STREETS", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasDefaultValueSql("USERS_SEQ.NEXTVAL");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("BIRTH_DATE");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ROLE");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IDX_EMAIL");

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.CollectionDayModel", b =>
                {
                    b.HasOne("GestaoDeResiduos.Models.GarbageCollectionTypeModel", "GarbageCollectionType")
                        .WithMany()
                        .HasForeignKey("GarbageCollectionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestaoDeResiduos.Models.StreetModel", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GarbageCollectionType");

                    b.Navigation("Street");
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.DistrictModel", b =>
                {
                    b.HasOne("GestaoDeResiduos.Models.RegionModel", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.RegionModel", b =>
                {
                    b.HasOne("GestaoDeResiduos.Models.StateModel", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("GestaoDeResiduos.Models.StreetModel", b =>
                {
                    b.HasOne("GestaoDeResiduos.Models.DistrictModel", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("District");
                });
#pragma warning restore 612, 618
        }
    }
}
