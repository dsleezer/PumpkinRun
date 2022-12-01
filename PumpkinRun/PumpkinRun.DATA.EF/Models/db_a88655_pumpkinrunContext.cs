using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PumpkinRun.DATA.EF.Models
{
    public partial class db_a88655_pumpkinrunContext : DbContext
    {
        public db_a88655_pumpkinrunContext()
        {
        }

        public db_a88655_pumpkinrunContext(DbContextOptions<db_a88655_pumpkinrunContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Benefit> Benefits { get; set; } = null!;
        public virtual DbSet<CircuitLink> CircuitLinks { get; set; } = null!;
        public virtual DbSet<HomeRaceInfo> HomeRaceInfos { get; set; } = null!;
        public virtual DbSet<MessageFromTom> MessageFromToms { get; set; } = null!;
        public virtual DbSet<RaceDayInformation> RaceDayInformations { get; set; } = null!;
        public virtual DbSet<RaceResult> RaceResults { get; set; } = null!;
        public virtual DbSet<RaceVideo> RaceVideos { get; set; } = null!;
        public virtual DbSet<SilentAuction> SilentAuctions { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<SponsorType> SponsorTypes { get; set; } = null!;
        public virtual DbSet<SponsorTypeBenefit> SponsorTypeBenefits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=sql8002.site4now.net;Database=db_a88655_pumpkinrun;User Id=db_a88655_pumpkinrun_admin;Password=C3ntriq@;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

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

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
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

            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.Property(e => e.BenefitDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CircuitLink>(entity =>
            {
                entity.Property(e => e.CircuitName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CircuitRaceDate).HasColumnType("date");

                entity.Property(e => e.CircuitUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HomeRaceInfo>(entity =>
            {
                entity.ToTable("HomeRaceInfo");

                entity.Property(e => e.PumpkinRunRaceDate).HasColumnType("date");

                entity.Property(e => e.RegistrationLink)
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MessageFromTom>(entity =>
            {
                entity.HasKey(e => e.MessageId);

                entity.ToTable("MessageFromTom");

                entity.Property(e => e.MessageContent).IsUnicode(false);

                entity.Property(e => e.Photo1)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Photo2)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Photo3)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Photo4)
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RaceDayInformation>(entity =>
            {
                entity.HasKey(e => e.RaceDayId);

                entity.ToTable("RaceDayInformation");

                entity.Property(e => e.InstructionsFile)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.PacketPickupOption1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PacketPickupOption2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PacketPickupOption3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduledDate).HasColumnType("date");

                entity.Property(e => e.SchoolPickupDate).HasColumnType("date");
            });

            modelBuilder.Entity<RaceResult>(entity =>
            {
                entity.Property(e => e.ExternalResultsUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FemalWinner2Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.FemaleWinner1Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.FemaleWinner1Time).HasColumnType("time(4)");

                entity.Property(e => e.FemaleWinner2Time).HasColumnType("time(4)");

                entity.Property(e => e.FemaleWinner3Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.FemaleWinner3Time).HasColumnType("time(4)");

                entity.Property(e => e.MaleWinner1Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.MaleWinner1Time).HasColumnType("time(4)");

                entity.Property(e => e.MaleWinner2Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.MaleWinner2Time).HasColumnType("time(4)");

                entity.Property(e => e.MaleWinner3Name)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.MaleWinner3Time).HasColumnType("time(4)");

                entity.Property(e => e.RacePhotosUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RaceYear).HasColumnType("date");
            });

            modelBuilder.Entity<RaceVideo>(entity =>
            {
                entity.Property(e => e.RaceYear).HasColumnType("date");

                entity.Property(e => e.VideoTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SilentAuction>(entity =>
            {
                entity.HasKey(e => e.Said);

                entity.Property(e => e.Said).HasColumnName("SAID");

                entity.Property(e => e.AuctionMessage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AuctionUrl)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.Property(e => e.SponsorLogo)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.SponsorName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SponsorUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SponsorVideoUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.SponsorType)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.SponsorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sponsors_SponsorTypes");
            });

            modelBuilder.Entity<SponsorType>(entity =>
            {
                entity.Property(e => e.SponsorTypeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SponsorTypeBenefit>(entity =>
            {
                entity.HasKey(e => e.Stbid);

                entity.Property(e => e.Stbid).HasColumnName("STBID");

                entity.HasOne(d => d.Benefit)
                    .WithMany(p => p.SponsorTypeBenefits)
                    .HasForeignKey(d => d.BenefitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SponsorTypeBenefits_Benefits");

                entity.HasOne(d => d.SponsorType)
                    .WithMany(p => p.SponsorTypeBenefits)
                    .HasForeignKey(d => d.SponsorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SponsorTypeBenefits_SponsorTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
