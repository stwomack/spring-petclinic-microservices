using Microsoft.EntityFrameworkCore;
using Petclinic.Vets.Domain;

namespace Petclinic.Vets.Infrastructure
{
    public partial class VetsContext : DbContext
    {
        public VetsContext()
        {
        }

        public VetsContext(DbContextOptions<VetsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Specialty> Specialties { get; set; }

        public virtual DbSet<VetSpecialty> VetSpecialties { get; set; }

        public virtual DbSet<Vet> Vets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("specialties");

                entity.HasIndex(e => e.Name)
                    .HasName("name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<VetSpecialty>(entity =>
            {
                entity.HasKey("VetId", "SpecialtyId").HasName("vet_id");

                entity.ToTable("vet_specialties");

                entity.HasIndex(e => e.SpecialtyId)
                    .HasName("specialty_id");

                entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

                entity.Property(e => e.VetId).HasColumnName("vet_id");

                entity.HasOne(d => d.Specialty)
                    .WithMany(d => d.VetSpecialties)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vet_specialties_ibfk_2");

                entity.HasOne(d => d.Vet)
                    .WithMany(d => d.VetSpecialties)
                    .HasForeignKey(d => d.VetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("vet_specialties_ibfk_1");
            });

            modelBuilder.Entity<Vet>(entity =>
            {
                entity.ToTable("vets");

                entity.HasIndex(e => e.LastName)
                    .HasName("last_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
