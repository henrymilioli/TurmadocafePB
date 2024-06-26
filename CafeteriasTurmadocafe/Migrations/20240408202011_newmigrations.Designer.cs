﻿// <auto-generated />
using System;
using Cafeteria.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cafeteria.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240408202011_newmigrations")]
    partial class newmigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cafeteria.Models.Avaliacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CafeteriasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuariosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CafeteriasId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("Cafeteria.Models.CafeteriaC", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("usuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("usuarioId");

                    b.ToTable("CafeteriaC");
                });

            modelBuilder.Entity("Cafeteria.Models.Evento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CafeteriasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuariosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CafeteriasId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("Cafeteria.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Cafeteria.Models.Avaliacao", b =>
                {
                    b.HasOne("Cafeteria.Models.CafeteriaC", "Cafeterias")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("CafeteriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Models.Usuario", "Usuarios")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cafeterias");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Cafeteria.Models.CafeteriaC", b =>
                {
                    b.HasOne("Cafeteria.Models.Usuario", "usuario")
                        .WithMany("Cafeterias")
                        .HasForeignKey("usuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("Cafeteria.Models.Evento", b =>
                {
                    b.HasOne("Cafeteria.Models.CafeteriaC", "Cafeterias")
                        .WithMany("Eventos")
                        .HasForeignKey("CafeteriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cafeteria.Models.Usuario", "Usuarios")
                        .WithMany("Eventos")
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cafeterias");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Cafeteria.Models.CafeteriaC", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("Cafeteria.Models.Usuario", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("Cafeterias");

                    b.Navigation("Eventos");
                });
#pragma warning restore 612, 618
        }
    }
}
