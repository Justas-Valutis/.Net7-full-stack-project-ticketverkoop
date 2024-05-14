using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketVerkoop.Domains.Migrations
{
    /// <inheritdoc />
    public partial class RelatieBestellingenTicketsAbonnementen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bestelling",
                columns: table => new
                {
                    BestellingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketID = table.Column<int>(type: "int", nullable: true),
                    AbonnementID = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BestelDatum = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.BestellingID);
                });

            migrationBuilder.CreateTable(
                name: "Stadium",
                columns: table => new
                {
                    StadiumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Stad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadium", x => x.StadiumID);
                });

            migrationBuilder.CreateTable(
                name: "Abonnement",
                columns: table => new
                {
                    AbonnementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZitplaatsID = table.Column<int>(type: "int", nullable: false),
                    PloegID = table.Column<int>(type: "int", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PloegNaam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    StadiaNaam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    RingNaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SectionID = table.Column<int>(type: "int", nullable: false),
                    BestellingNavigationBestellingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnement", x => x.AbonnementID);
                    table.ForeignKey(
                        name: "FK_Abonnement_Bestelling_BestellingNavigationBestellingId",
                        column: x => x.BestellingNavigationBestellingId,
                        principalTable: "Bestelling",
                        principalColumn: "BestellingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ploeg",
                columns: table => new
                {
                    PloegID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ThuisStadiumID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ploeg", x => x.PloegID);
                    table.ForeignKey(
                        name: "FK_Ploeg_Stadium",
                        column: x => x.ThuisStadiumID,
                        principalTable: "Stadium",
                        principalColumn: "StadiumID");
                });

            migrationBuilder.CreateTable(
                name: "Ring",
                columns: table => new
                {
                    RingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StadiumID = table.Column<int>(type: "int", nullable: false),
                    ZoneLocatie = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ring", x => x.RingID);
                    table.ForeignKey(
                        name: "FK_Ring_Stadium",
                        column: x => x.StadiumID,
                        principalTable: "Stadium",
                        principalColumn: "StadiumID");
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StadiumID = table.Column<int>(type: "int", nullable: false),
                    PloegThuisID = table.Column<int>(type: "int", nullable: false),
                    PloegUitID = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchID);
                    table.ForeignKey(
                        name: "FK_Match_Ploeg",
                        column: x => x.PloegThuisID,
                        principalTable: "Ploeg",
                        principalColumn: "PloegID");
                    table.ForeignKey(
                        name: "FK_Match_Ploeg1",
                        column: x => x.PloegUitID,
                        principalTable: "Ploeg",
                        principalColumn: "PloegID");
                    table.ForeignKey(
                        name: "FK_Match_Stadium",
                        column: x => x.StadiumID,
                        principalTable: "Stadium",
                        principalColumn: "StadiumID");
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RingID = table.Column<int>(type: "int", nullable: false),
                    Prijs = table.Column<double>(type: "float", nullable: false),
                    AantalZitplaatsen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionID);
                    table.ForeignKey(
                        name: "FK_Section_Ring",
                        column: x => x.RingID,
                        principalTable: "Ring",
                        principalColumn: "RingID");
                });

            migrationBuilder.CreateTable(
                name: "Zitplaats",
                columns: table => new
                {
                    ZitplaatsID = table.Column<int>(type: "int", nullable: false),
                    SectionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zitplaats", x => x.ZitplaatsID);
                    table.ForeignKey(
                        name: "FK_Zitplaats_Section",
                        column: x => x.SectionID,
                        principalTable: "Section",
                        principalColumn: "SectionID");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    ZitplaatsID = table.Column<int>(type: "int", nullable: false),
                    RingNaam = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SectionID = table.Column<int>(type: "int", nullable: false),
                    StoelenBesteld = table.Column<int>(type: "int", nullable: false),
                    Prijs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BestellingNavigationBestellingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Ticket_Bestelling_BestellingNavigationBestellingId",
                        column: x => x.BestellingNavigationBestellingId,
                        principalTable: "Bestelling",
                        principalColumn: "BestellingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Match",
                        column: x => x.MatchID,
                        principalTable: "Match",
                        principalColumn: "MatchID");
                    table.ForeignKey(
                        name: "FK_Ticket_Zitplaats",
                        column: x => x.ZitplaatsID,
                        principalTable: "Zitplaats",
                        principalColumn: "ZitplaatsID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_BestellingNavigationBestellingId",
                table: "Abonnement",
                column: "BestellingNavigationBestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PloegThuisID",
                table: "Match",
                column: "PloegThuisID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_PloegUitID",
                table: "Match",
                column: "PloegUitID");

            migrationBuilder.CreateIndex(
                name: "IX_Match_StadiumID",
                table: "Match",
                column: "StadiumID");

            migrationBuilder.CreateIndex(
                name: "IX_Ploeg_ThuisStadiumID",
                table: "Ploeg",
                column: "ThuisStadiumID");

            migrationBuilder.CreateIndex(
                name: "IX_Ring_StadiumID",
                table: "Ring",
                column: "StadiumID");

            migrationBuilder.CreateIndex(
                name: "IX_Section_RingID",
                table: "Section",
                column: "RingID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BestellingNavigationBestellingId",
                table: "Ticket",
                column: "BestellingNavigationBestellingId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MatchID",
                table: "Ticket",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ZitplaatsID",
                table: "Ticket",
                column: "ZitplaatsID");

            migrationBuilder.CreateIndex(
                name: "IX_Zitplaats_SectionID",
                table: "Zitplaats",
                column: "SectionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonnement");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Bestelling");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Zitplaats");

            migrationBuilder.DropTable(
                name: "Ploeg");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Ring");

            migrationBuilder.DropTable(
                name: "Stadium");
        }
    }
}
