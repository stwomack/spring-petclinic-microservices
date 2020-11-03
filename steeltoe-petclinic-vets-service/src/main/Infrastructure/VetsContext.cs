using System;
using Microsoft.EntityFrameworkCore;
using steeltoe_petclinic_vets_api.Domain;

namespace steeltoe_petclinic_vets_api.Infrastructure
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

    public virtual DbSet<VetSpecialty> VetSpecialties { get; set; }
    public virtual DbSet<Vet> Vets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
     
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<VetSpecialty>(entity =>
      {
        entity.ToTable("vet_Specialties");

        entity.HasKey(sc => new { sc.VetId, sc.SpecialtyId });

        entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

        entity.Property(e => e.VetId).HasColumnName("vet_id");
      });

      modelBuilder.Entity<Vet>(entity =>
      {
        entity.ToTable("vets");

        entity.HasIndex(e => e.LastName)
            .HasName("vets_last_name");

        entity.Property(e => e.Id)
               .UseHiLo("vet_hilo")
               .HasColumnName("id");

        entity.Property(e => e.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(30)
            .IsUnicode(false);

        entity.Property(e => e.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(30)
            .IsUnicode(false);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
