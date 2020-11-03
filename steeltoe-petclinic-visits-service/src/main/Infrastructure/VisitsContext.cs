using System;
using Microsoft.EntityFrameworkCore;
using steeltoe_petclinic_visits_api.Domain;

namespace steeltoe_petclinic_visits_api.Infrastructure
{
  public partial class VisitsContext : DbContext
  {
    public VisitsContext() {
    }

    public VisitsContext(DbContextOptions<VisitsContext> options)
        : base(options) {
    }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
     
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Visit>(entity =>
      {
        entity.ToTable("visits");

        entity.HasIndex(e => e.PetId)
            .HasName("visits_pet_id");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(8000)
            .IsUnicode(false);

        entity.Property(e => e.PetId).HasColumnName("pet_id");

        entity.Property(e => e.Date)
            .HasColumnName("visit_date")
            .HasColumnType("date");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
