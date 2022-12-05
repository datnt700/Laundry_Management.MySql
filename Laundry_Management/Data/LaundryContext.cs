using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Laundry_Management.Models;

namespace Laundry_Management.Data
{
    public partial class LaundryContext : DbContext
    {
        public LaundryContext()
        {
        }

        public LaundryContext(DbContextOptions<LaundryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Machine> Machines { get; set; } = null!;
        public virtual DbSet<MachineHistory> MachineHistories { get; set; } = null!;
        public virtual DbSet<MachineMode> MachineModes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
        public virtual DbSet<ServiceMode> ServiceModes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasIndex(e => e.UserIdHost, "fk_location_users1_idx");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Coordinates)
                    .HasMaxLength(20)
                    .HasColumnName("coordinates");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_active");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(45)
                    .HasColumnName("location_name");

                entity.Property(e => e.UserIdHost).HasColumnName("user_id_host");

                entity.HasOne(d => d.UserIdHostNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.UserIdHost)
                    .HasConstraintName("fk_location_users1");
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.ToTable("machine");

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

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fk_machine_location1");
            });

            modelBuilder.Entity<MachineHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PRIMARY");

                entity.ToTable("machine_history");

                entity.HasIndex(e => e.LocationId, "fk_machine_history_location1_idx");

                entity.HasIndex(e => e.MachineId, "fk_machine_history_machine1_idx");

                entity.HasIndex(e => e.UserId, "fk_machine_history_users1_idx");

                entity.Property(e => e.HistoryId).HasColumnName("history_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TimeUse)
                    .HasColumnType("time")
                    .HasColumnName("time_use");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.MachineHistories)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fk_machine_history_location");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineHistories)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("fk_machine_history_machine");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MachineHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_machine_history_users");
            });

            modelBuilder.Entity<MachineMode>(entity =>
            {
                entity.ToTable("machine_mode");

                entity.HasIndex(e => e.MachineId, "fk_machine_mode_machine1_idx");

                entity.HasIndex(e => e.ServiceModeId, "fk_machine_mode_service_mode1_idx");

                entity.Property(e => e.MachineModeId)
                    .ValueGeneratedNever()
                    .HasColumnName("machine_mode_id");

                entity.Property(e => e.MachineId).HasColumnName("machine_id");

                entity.Property(e => e.ServiceModeId).HasColumnName("service_mode_id");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.MachineModes)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("fk_machine_mode_machine1");

                entity.HasOne(d => d.ServiceMode)
                    .WithMany(p => p.MachineModes)
                    .HasForeignKey(d => d.ServiceModeId)
                    .HasConstraintName("fk_machine_mode_service_mode1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_active");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.ToTable("role_user");

                entity.HasIndex(e => e.RoleId, "fk_role_user_role1_idx");

                entity.HasIndex(e => e.UserId, "fk_role_user_users1_idx");

                entity.Property(e => e.RoleUserId)
                    .ValueGeneratedNever()
                    .HasColumnName("role_user_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_role_user_role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoleUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_role_user_users");
            });

            modelBuilder.Entity<ServiceMode>(entity =>
            {
                entity.HasKey(e => e.ModeId)
                    .HasName("PRIMARY");

                entity.ToTable("service_mode");

                entity.Property(e => e.ModeId).HasColumnName("mode_id");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_active");

                entity.Property(e => e.ModeName)
                    .HasMaxLength(250)
                    .HasColumnName("mode_name");

                entity.Property(e => e.PricePerMinute).HasColumnName("price_per_minute");

                entity.Property(e => e.PricePerSize).HasColumnName("price_per_size");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Salt, "email_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AccountType).HasColumnName("account_type");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_active");

                entity.Property(e => e.IsLock)
                    .HasColumnType("bit(10)")
                    .HasColumnName("is_lock");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.PassHash)
                    .HasMaxLength(250)
                    .HasColumnName("pass_hash");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Salt)
                    .HasMaxLength(100)
                    .HasColumnName("salt");

                entity.Property(e => e.UserName)
                    .HasMaxLength(250)
                    .HasColumnName("user_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
