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

            modelBuilder.Entity("GestaoDeResiduos.Models.RegionModel", b =>
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

                    b.Property<int>("StateId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("REGION_ID");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("DISTRICTS", (string)null);
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

            modelBuilder.Entity("GestaoDeResiduos.Models.RegionModel", b =>
                {
                    b.HasOne("GestaoDeResiduos.Models.StateModel", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("State");
                });
#pragma warning restore 612, 618
        }
    }
}
