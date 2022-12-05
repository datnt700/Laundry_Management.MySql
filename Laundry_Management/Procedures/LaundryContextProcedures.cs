using Microsoft.EntityFrameworkCore;

namespace Laundry_Management.Procedures
{
    public partial class LaundryContextProcedures : DbContext
    {
        public LaundryContextProcedures() { }

        public LaundryContextProcedures(DbContextOptions<LaundryContextProcedures> options) : base(options) 
        { }

        public virtual DbSet<sp_GetMachineById> sp_GetMachineById { get; set; } = null!;
        public virtual DbSet<sp_InsertMachine> sp_InsertMachine { get; set; } = null!;
        public virtual DbSet<sp_UpdateMachine> sp_UpdateMachine { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sp_GetMachineById>(entity =>
            {
                entity.ToTable("machine");
                entity.HasNoKey();

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.Branch)
                    .HasMaxLength(250)
                    .HasColumnName("branch");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_active");

                entity.Property(e => e.MachineName)
                    .HasMaxLength(250)
                    .HasColumnName("machine_name");

                entity.Property(e => e.MachineType).HasColumnName("machine_type");

                entity.Property(e => e.Size)
                    .HasMaxLength(45)
                    .HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<sp_InsertMachine>(entity =>
            {
                entity.ToTable("machine");
                entity.HasNoKey();

                entity.HasIndex(e => e.LocationId, "fk_machine_location1_idx");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.Branch)
                    .HasMaxLength(250)
                    .HasColumnName("branch");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_active");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MachineName)
                    .HasMaxLength(250)
                    .HasColumnName("machine_name");

                entity.Property(e => e.MachineType).HasColumnName("machine_type");

                entity.Property(e => e.Size)
                    .HasMaxLength(45)
                    .HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

            });

            modelBuilder.Entity<sp_UpdateMachine>(entity =>
            {
                entity.ToTable("machine");
                entity.HasNoKey();

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.MachineName)
                    .HasMaxLength(250)
                    .HasColumnName("machine_name");

            });
        }
    }
}
