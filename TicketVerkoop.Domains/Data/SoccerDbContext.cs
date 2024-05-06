using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TicketVerkoop.Domains.Entities;

namespace TicketVerkoop.Domains.Data;

public partial class SoccerDbContext : DbContext
{
    public SoccerDbContext()
    {
    }

    public SoccerDbContext(DbContextOptions<SoccerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abonnement> Abonnements { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Bestelling> Bestellings { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Ploeg> Ploegs { get; set; }

    public virtual DbSet<Ring> Rings { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Stadium> Stadia { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Zitplaat> Zitplaats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL19_VIVES; Database=SoccersDB; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Abonnement>(entity =>
        {
            entity.ToTable("Abonnement");

            entity.Property(e => e.AbonnementId).HasColumnName("AbonnementID");
            entity.Property(e => e.BestellingId).HasColumnName("BestellingID");
            entity.Property(e => e.PloegId).HasColumnName("PloegID");
            entity.Property(e => e.Prijs).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Bestelling).WithMany(p => p.Abonnements)
                .HasForeignKey(d => d.BestellingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonnement_Bestelling");

            entity.HasOne(d => d.Ploeg).WithMany(p => p.Abonnements)
                .HasForeignKey(d => d.PloegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Abonnement_Ploeg");
        });

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

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
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
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetUserRoles).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.HasKey(e => e.BestellingId).HasName("PK_Table_1");

            entity.ToTable("Bestelling");

            entity.Property(e => e.BestellingId).HasColumnName("BestellingID");
            entity.Property(e => e.BestelDatum).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Bestellings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bestelling_AspNetUsers");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.ToTable("Match");

            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.Datum).HasColumnType("datetime");
            entity.Property(e => e.PloegThuisId).HasColumnName("PloegThuisID");
            entity.Property(e => e.PloegUitId).HasColumnName("PloegUitID");
            entity.Property(e => e.StadiumId).HasColumnName("StadiumID");

            entity.HasOne(d => d.PloegThuis).WithMany(p => p.MatchPloegThuis)
                .HasForeignKey(d => d.PloegThuisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Ploeg");

            entity.HasOne(d => d.PloegUit).WithMany(p => p.MatchPloegUits)
                .HasForeignKey(d => d.PloegUitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Ploeg1");

            entity.HasOne(d => d.Stadium).WithMany(p => p.Matches)
                .HasForeignKey(d => d.StadiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Match_Stadium");
        });

        modelBuilder.Entity<Ploeg>(entity =>
        {
            entity.ToTable("Ploeg");

            entity.Property(e => e.PloegId).HasColumnName("PloegID");
            entity.Property(e => e.Naam)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ThuisStadiumId).HasColumnName("ThuisStadiumID");

            entity.HasOne(d => d.ThuisStadium).WithMany(p => p.Ploegs)
                .HasForeignKey(d => d.ThuisStadiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ploeg_Stadium");
        });

        modelBuilder.Entity<Ring>(entity =>
        {
            entity.ToTable("Ring");

            entity.Property(e => e.RingId).HasColumnName("RingID");
            entity.Property(e => e.StadiumId).HasColumnName("StadiumID");
            entity.Property(e => e.ZoneLocatie)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Stadium).WithMany(p => p.Rings)
                .HasForeignKey(d => d.StadiumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ring_Stadium");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.ToTable("Section");

            entity.Property(e => e.SectionId).HasColumnName("SectionID");
            entity.Property(e => e.RingId).HasColumnName("RingID");

            entity.HasOne(d => d.Ring).WithMany(p => p.Sections)
                .HasForeignKey(d => d.RingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Section_Ring");
        });

        modelBuilder.Entity<Stadium>(entity =>
        {
            entity.ToTable("Stadium");

            entity.Property(e => e.StadiumId).HasColumnName("StadiumID");
            entity.Property(e => e.Naam)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Stad)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.BestellingId).HasColumnName("BestellingID");
            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.Prijs).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Bestelling).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.BestellingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Bestelling");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Match");
        });

        modelBuilder.Entity<Zitplaat>(entity =>
        {
            entity.HasKey(e => e.ZitplaatsId);

            entity.Property(e => e.ZitplaatsId).HasColumnName("ZitplaatsID");
            entity.Property(e => e.AbonnementId).HasColumnName("AbonnementID");
            entity.Property(e => e.SectionId).HasColumnName("SectionID");
            entity.Property(e => e.TicketId).HasColumnName("TicketID");

            entity.HasOne(d => d.Abonnement).WithMany(p => p.Zitplaats)
                .HasForeignKey(d => d.AbonnementId)
                .HasConstraintName("FK_Zitplaats_AbonnementID");

            entity.HasOne(d => d.Section).WithMany(p => p.Zitplaats)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zitplaats_Section");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Zitplaats)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_Zitplaats_Ticket");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
