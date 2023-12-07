using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PruebMyper.Models
{
    public partial class TrabajadoresPruebaContext : DbContext
    {
        public TrabajadoresPruebaContext()
        {
        }

        public TrabajadoresPruebaContext(DbContextOptions<TrabajadoresPruebaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Distrito> Distritos { get; set; }
        public virtual DbSet<Provincium> Provincia { get; set; }
        public virtual DbSet<Trabajadore> Trabajadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.ToTable("Distrito");

                entity.Property(e => e.NombreDistrito)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProvinciaNavigation)
                    .WithMany(p => p.Distritos)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK__Distrito__IdProv__3C69FB99");
            });

            modelBuilder.Entity<Provincium>(entity =>
            {
                entity.Property(e => e.NombreProvincia)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Provincia__IdDep__3D5E1FD2");
            });

            modelBuilder.Entity<Trabajadore>(entity =>
            {
                entity.Property(e => e.Nombres)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.HasOne(d => d.oDepartamento)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__Trabajado__IdDep__3E52440B");

                entity.HasOne(d => d.oDistrito)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdDistrito)
                    .HasConstraintName("FK__Trabajado__IdDis__3F466844");

                entity.HasOne(d => d.oProvincia)
                    .WithMany(p => p.Trabajadores)
                    .HasForeignKey(d => d.IdProvincia)
                    .HasConstraintName("FK__Trabajado__IdPro__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
