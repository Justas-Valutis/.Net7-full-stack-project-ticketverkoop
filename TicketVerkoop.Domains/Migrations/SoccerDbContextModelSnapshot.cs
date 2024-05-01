﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketVerkoop.Domains.Data;

#nullable disable

namespace TicketVerkoop.Domains.Migrations
{
    [DbContext(typeof(SoccerDbContext))]
    partial class SoccerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Abonnement", b =>
                {
                    b.Property<int>("AbonnementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AbonnementID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AbonnementId"));

                    b.Property<int>("BestellingNavigationBestellingId")
                        .HasColumnType("int");

                    b.Property<int>("PloegId")
                        .HasColumnType("int")
                        .HasColumnName("PloegID");

                    b.Property<string>("PloegNaam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("RingNaam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int")
                        .HasColumnName("SectionID");

                    b.Property<string>("StadiaNaam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ZitplaatsId")
                        .HasColumnType("int")
                        .HasColumnName("ZitplaatsID");

                    b.HasKey("AbonnementId");

                    b.HasIndex("BestellingNavigationBestellingId");

                    b.ToTable("Abonnement", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Bestelling", b =>
                {
                    b.Property<int>("BestellingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BestellingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BestellingId"));

                    b.Property<int?>("AbonnementId")
                        .HasColumnType("int")
                        .HasColumnName("AbonnementID");

                    b.Property<DateTime>("BestelDatum")
                        .HasColumnType("datetime");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int")
                        .HasColumnName("TicketID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("BestellingId")
                        .HasName("PK_Table_1");

                    b.ToTable("Bestelling", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MatchID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MatchId"));

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime");

                    b.Property<int>("PloegThuisId")
                        .HasColumnType("int")
                        .HasColumnName("PloegThuisID");

                    b.Property<int>("PloegUitId")
                        .HasColumnType("int")
                        .HasColumnName("PloegUitID");

                    b.Property<int>("StadiumId")
                        .HasColumnType("int")
                        .HasColumnName("StadiumID");

                    b.HasKey("MatchId");

                    b.HasIndex("PloegThuisId");

                    b.HasIndex("PloegUitId");

                    b.HasIndex("StadiumId");

                    b.ToTable("Match", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ploeg", b =>
                {
                    b.Property<int>("PloegId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PloegID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PloegId"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ThuisStadiumId")
                        .HasColumnType("int")
                        .HasColumnName("ThuisStadiumID");

                    b.HasKey("PloegId");

                    b.HasIndex("ThuisStadiumId");

                    b.ToTable("Ploeg", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ring", b =>
                {
                    b.Property<int>("RingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RingID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RingId"));

                    b.Property<int>("StadiumId")
                        .HasColumnType("int")
                        .HasColumnName("StadiumID");

                    b.Property<string>("ZoneLocatie")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("RingId");

                    b.HasIndex("StadiumId");

                    b.ToTable("Ring", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SectionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectionId"));

                    b.Property<int>("AantalZitplaatsen")
                        .HasColumnType("int");

                    b.Property<double>("Prijs")
                        .HasColumnType("float");

                    b.Property<int>("RingId")
                        .HasColumnType("int")
                        .HasColumnName("RingID");

                    b.HasKey("SectionId");

                    b.HasIndex("RingId");

                    b.ToTable("Section", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Stadium", b =>
                {
                    b.Property<int>("StadiumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StadiumID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StadiumId"));

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Stad")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("StadiumId");

                    b.ToTable("Stadium", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TicketID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<int>("BestellingNavigationBestellingId")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int")
                        .HasColumnName("MatchID");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("RingNaam")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int")
                        .HasColumnName("SectionID");

                    b.Property<int>("StoelenBesteld")
                        .HasColumnType("int");

                    b.Property<int>("ZitplaatsId")
                        .HasColumnType("int")
                        .HasColumnName("ZitplaatsID");

                    b.HasKey("TicketId");

                    b.HasIndex("BestellingNavigationBestellingId");

                    b.HasIndex("MatchId");

                    b.HasIndex("ZitplaatsId");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Zitplaat", b =>
                {
                    b.Property<int>("ZitplaatsId")
                        .HasColumnType("int")
                        .HasColumnName("ZitplaatsID");

                    b.Property<int>("SectionId")
                        .HasColumnType("int")
                        .HasColumnName("SectionID");

                    b.HasKey("ZitplaatsId");

                    b.HasIndex("SectionId");

                    b.ToTable("Zitplaats");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Abonnement", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Bestelling", "BestellingNavigation")
                        .WithMany("Abonnementen")
                        .HasForeignKey("BestellingNavigationBestellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BestellingNavigation");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Match", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Ploeg", "PloegThuis")
                        .WithMany("MatchPloegThuis")
                        .HasForeignKey("PloegThuisId")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Ploeg");

                    b.HasOne("TicketVerkoop.Domains.Entities.Ploeg", "PloegUit")
                        .WithMany("MatchPloegUits")
                        .HasForeignKey("PloegUitId")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Ploeg1");

                    b.HasOne("TicketVerkoop.Domains.Entities.Stadium", "Stadium")
                        .WithMany("Matches")
                        .HasForeignKey("StadiumId")
                        .IsRequired()
                        .HasConstraintName("FK_Match_Stadium");

                    b.Navigation("PloegThuis");

                    b.Navigation("PloegUit");

                    b.Navigation("Stadium");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ploeg", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Stadium", "ThuisStadium")
                        .WithMany("Ploegs")
                        .HasForeignKey("ThuisStadiumId")
                        .IsRequired()
                        .HasConstraintName("FK_Ploeg_Stadium");

                    b.Navigation("ThuisStadium");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ring", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Stadium", "Stadium")
                        .WithMany("Rings")
                        .HasForeignKey("StadiumId")
                        .IsRequired()
                        .HasConstraintName("FK_Ring_Stadium");

                    b.Navigation("Stadium");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Section", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Ring", "Ring")
                        .WithMany("Sections")
                        .HasForeignKey("RingId")
                        .IsRequired()
                        .HasConstraintName("FK_Section_Ring");

                    b.Navigation("Ring");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ticket", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Bestelling", "BestellingNavigation")
                        .WithMany("Tickets")
                        .HasForeignKey("BestellingNavigationBestellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketVerkoop.Domains.Entities.Match", "Match")
                        .WithMany("Tickets")
                        .HasForeignKey("MatchId")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_Match");

                    b.HasOne("TicketVerkoop.Domains.Entities.Zitplaat", "Zitplaats")
                        .WithMany("Tickets")
                        .HasForeignKey("ZitplaatsId")
                        .IsRequired()
                        .HasConstraintName("FK_Ticket_Zitplaats");

                    b.Navigation("BestellingNavigation");

                    b.Navigation("Match");

                    b.Navigation("Zitplaats");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Zitplaat", b =>
                {
                    b.HasOne("TicketVerkoop.Domains.Entities.Section", "Section")
                        .WithMany("Zitplaats")
                        .HasForeignKey("SectionId")
                        .IsRequired()
                        .HasConstraintName("FK_Zitplaats_Section");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Bestelling", b =>
                {
                    b.Navigation("Abonnementen");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Match", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ploeg", b =>
                {
                    b.Navigation("MatchPloegThuis");

                    b.Navigation("MatchPloegUits");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Ring", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Section", b =>
                {
                    b.Navigation("Zitplaats");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Stadium", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Ploegs");

                    b.Navigation("Rings");
                });

            modelBuilder.Entity("TicketVerkoop.Domains.Entities.Zitplaat", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}