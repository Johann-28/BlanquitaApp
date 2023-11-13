using System;
using System.Collections.Generic;
using BlanquitaAPI.Data.BlanquitaModels;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Data;

public partial class TacosBlanquitaContext : DbContext
{
    public TacosBlanquitaContext(DbContextOptions<TacosBlanquitaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combo> Combo { get; set; }

    public virtual DbSet<Orden> Orden { get; set; }

    public virtual DbSet<Perfil> Perfil { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<TipoProducto> TipoProducto { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.IdCombo).HasName("PK__Combo__D65BF2C8230EB487");

            entity.Property(e => e.Descripcion).HasMaxLength(50);

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.Combo)
                .HasForeignKey(d => d.IdOrden)
                .HasConstraintName("FK_Orden_Combo");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__Orden__C38F300D645A4506");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Orden)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orden__IdUsuario__46E78A0C");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__Perfil__C7BD5CC117BA96DC");

            entity.Property(e => e.Clave)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892104075B11C");

            entity.Property(e => e.Descripcion).HasMaxLength(50);

            entity.HasOne(d => d.IdComboNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdCombo)
                .HasConstraintName("FK_Producto_Combo");

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdTipoProducto)
                .HasConstraintName("FK__Producto__IdTipo__398D8EEE");
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto).HasName("PK__TipoProd__A974F9200A83A316");

            entity.Property(e => e.Clave).HasMaxLength(3);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F5C1BA4D");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdPerfi__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
