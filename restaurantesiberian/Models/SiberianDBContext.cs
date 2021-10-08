using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace restaurantesiberian.Models
{
    public partial class SiberianDBContext : DbContext
    {
        public SiberianDBContext()
        {
        }

        public SiberianDBContext(DbContextOptions<SiberianDBContext> options)
            : base(options)
        {
        }

        public  DbSet<Ciudad> Ciudads { get; set; }
        public  DbSet<Restaurante> Restaurantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=WALTER-PC;Database=SiberianDB;User=GEOSHOP;Password=GEOSHOP20;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.Idciudad);

                entity.ToTable("Ciudad");

                entity.Property(e => e.Idciudad).HasColumnName("IDCiudad");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha Creacion");

                entity.Property(e => e.NombreCiudad)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Nombre Ciudad");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.HasKey(e => e.Idrestaurante);

                entity.Property(e => e.Idrestaurante)
                    .ValueGeneratedNever()
                    .HasColumnName("IDRestaurante");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha Creacion");

                entity.Property(e => e.Idciudad).HasColumnName("IDCiudad");

                entity.Property(e => e.NombreRestaurante)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdciudadNavigation)
                    .WithMany(p => p.Restaurantes)
                    .HasForeignKey(d => d.Idciudad)
                    .HasConstraintName("FK_Restaurantes_Ciudad");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
