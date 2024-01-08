﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DogMatch_web_api.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Afgewezen", b =>
                {
                    b.Property<long>("Hond1Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Hond2Id")
                        .HasColumnType("bigint");

                    b.HasKey("Hond1Id", "Hond2Id");

                    b.HasIndex("Hond2Id");

                    b.ToTable("afgewezen");
                });

            modelBuilder.Entity("Models.Hond", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Foto")
                        .HasColumnType("character varying (200)");

                    b.Property<DateTime>("Geboortedatum")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Geslacht")
                        .HasColumnType("character varying (4)");

                    b.Property<string>("Naam")
                        .HasColumnType("character varying (200)");

                    b.HasKey("Id");

                    b.ToTable("hond");
                });

            modelBuilder.Entity("Models.Hond_Profiel", b =>
                {
                    b.Property<long>("HondId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProfielId")
                        .HasColumnType("bigint");

                    b.HasKey("HondId", "ProfielId");

                    b.HasIndex("HondId")
                        .IsUnique();

                    b.HasIndex("ProfielId")
                        .IsUnique();

                    b.ToTable("hondprofiel");
                });

            modelBuilder.Entity("Models.Match", b =>
                {
                    b.Property<long>("Hond1Id")
                        .HasColumnType("bigint");

                    b.Property<long>("Hond2Id")
                        .HasColumnType("bigint");

                    b.HasKey("Hond1Id", "Hond2Id");

                    b.HasIndex("Hond2Id");

                    b.ToTable("match");
                });

            modelBuilder.Entity("Models.Profiel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Beschrijving")
                        .HasColumnType("character varying (200)");

                    b.Property<string>("Voorkeur")
                        .HasColumnType("character varying (5)");

                    b.HasKey("Id");

                    b.ToTable("profiel");
                });

            modelBuilder.Entity("Models.Afgewezen", b =>
                {
                    b.HasOne("Models.Hond", "Hond1")
                        .WithMany("AfgewezenHond1")
                        .HasForeignKey("Hond1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Hond", "Hond2")
                        .WithMany("AfgewezenHond2")
                        .HasForeignKey("Hond2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hond1");

                    b.Navigation("Hond2");
                });

            modelBuilder.Entity("Models.Hond_Profiel", b =>
                {
                    b.HasOne("Models.Hond", "Hond")
                        .WithOne("Profiel")
                        .HasForeignKey("Models.Hond_Profiel", "HondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Profiel", "Profiel")
                        .WithOne("Hond")
                        .HasForeignKey("Models.Hond_Profiel", "ProfielId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hond");

                    b.Navigation("Profiel");
                });

            modelBuilder.Entity("Models.Match", b =>
                {
                    b.HasOne("Models.Hond", "Hond1")
                        .WithMany("MatchedHond1")
                        .HasForeignKey("Hond1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Hond", "Hond2")
                        .WithMany("MatchedHond2")
                        .HasForeignKey("Hond2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hond1");

                    b.Navigation("Hond2");
                });

            modelBuilder.Entity("Models.Hond", b =>
                {
                    b.Navigation("AfgewezenHond1");

                    b.Navigation("AfgewezenHond2");

                    b.Navigation("MatchedHond1");

                    b.Navigation("MatchedHond2");

                    b.Navigation("Profiel");
                });

            modelBuilder.Entity("Models.Profiel", b =>
                {
                    b.Navigation("Hond");
                });
#pragma warning restore 612, 618
        }
    }
}
