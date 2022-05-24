using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebCbt_Backend.Models;

namespace WebCbt_Backend.Data
{
    public partial class WebCbtDbContext : DbContext
    {
        public WebCbtDbContext()
        {
        }

        public WebCbtDbContext(DbContextOptions<WebCbtDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Evaluation> Evaluations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=ConnectionStrings:WebCbt");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("gender_t", new[] { "male", "female", "other", "prefer not to say" });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.Property(e => e.EvaluationId)
                    .HasColumnName("EvaluationID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Category).HasColumnType("character varying");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("Users_UserId");

                entity.Property(e => e.UserId).UseIdentityAlwaysColumn();

                entity.Property(e => e.Gender).HasColumnType("character varying");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_Users_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
