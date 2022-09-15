using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SegurosChubbBack.Models
{
    public partial class SegurosChubbContext : DbContext
    {
        public SegurosChubbContext()
        {
        }

        public SegurosChubbContext(DbContextOptions<SegurosChubbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Poliza> Polizas { get; set; }
        public virtual DbSet<Seguro> Seguros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-MLPJ0UT\\SQLEXPRESS; DataBase=SegurosChubb; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.CodigoPersona)
                    .HasName("PK__Persona__C51B2076B7E01779");

                entity.ToTable("Persona");

                entity.Property(e => e.CodigoPersona)
                    .ValueGeneratedNever()
                    .HasColumnName("Codigo_Persona");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Cliente");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.HasKey(e => e.CodigoPoliza)
                    .HasName("PK__Poliza__FD19C196ABAF7303");

                entity.ToTable("Poliza");

                entity.Property(e => e.CodigoPoliza)
                    .ValueGeneratedNever()
                    .HasColumnName("Codigo_Poliza");

                entity.Property(e => e.CodigoPersona).HasColumnName("Codigo_Persona");

                entity.Property(e => e.CodigoSeguro).HasColumnName("Codigo_Seguro");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodigoPersonaNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.CodigoPersona)
                    .HasConstraintName("FK__Poliza__Estado__3A81B327");

                entity.HasOne(d => d.CodigoSeguroNavigation)
                    .WithMany(p => p.Polizas)
                    .HasForeignKey(d => d.CodigoSeguro)
                    .HasConstraintName("FK__Poliza__Codigo_S__3B75D760");
            });

            modelBuilder.Entity<Seguro>(entity =>
            {
                entity.HasKey(e => e.CodigoSeguro)
                    .HasName("PK__Seguros__BFC2F93846EFF6D6");

                entity.Property(e => e.CodigoSeguro)
                    .ValueGeneratedNever()
                    .HasColumnName("Codigo_Seguro");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ValorAsegurado).HasColumnName("Valor_Asegurado");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
