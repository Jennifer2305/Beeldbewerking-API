using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DogMatch_web_api.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hond",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naam = table.Column<string>(type: "character varying (200)", nullable: true),
                    Geslacht = table.Column<string>(type: "character varying (4)", nullable: true),
                    Geboortedatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Foto = table.Column<string>(type: "character varying (200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hond", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profiel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Beschrijving = table.Column<string>(type: "character varying (200)", nullable: true),
                    Voorkeur = table.Column<string>(type: "character varying (5)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "afgewezen",
                columns: table => new
                {
                    Hond1Id = table.Column<long>(type: "bigint", nullable: false),
                    Hond2Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afgewezen", x => new { x.Hond1Id, x.Hond2Id });
                    table.ForeignKey(
                        name: "FK_afgewezen_hond_Hond1Id",
                        column: x => x.Hond1Id,
                        principalTable: "hond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_afgewezen_hond_Hond2Id",
                        column: x => x.Hond2Id,
                        principalTable: "hond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "match",
                columns: table => new
                {
                    Hond1Id = table.Column<long>(type: "bigint", nullable: false),
                    Hond2Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match", x => new { x.Hond1Id, x.Hond2Id });
                    table.ForeignKey(
                        name: "FK_match_hond_Hond1Id",
                        column: x => x.Hond1Id,
                        principalTable: "hond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_match_hond_Hond2Id",
                        column: x => x.Hond2Id,
                        principalTable: "hond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hondprofiel",
                columns: table => new
                {
                    HondId = table.Column<long>(type: "bigint", nullable: false),
                    ProfielId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hondprofiel", x => new { x.HondId, x.ProfielId });
                    table.ForeignKey(
                        name: "FK_hondprofiel_hond_HondId",
                        column: x => x.HondId,
                        principalTable: "hond",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hondprofiel_profiel_ProfielId",
                        column: x => x.ProfielId,
                        principalTable: "profiel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_afgewezen_Hond2Id",
                table: "afgewezen",
                column: "Hond2Id");

            migrationBuilder.CreateIndex(
                name: "IX_hondprofiel_HondId",
                table: "hondprofiel",
                column: "HondId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hondprofiel_ProfielId",
                table: "hondprofiel",
                column: "ProfielId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_match_Hond2Id",
                table: "match",
                column: "Hond2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "afgewezen");

            migrationBuilder.DropTable(
                name: "hondprofiel");

            migrationBuilder.DropTable(
                name: "match");

            migrationBuilder.DropTable(
                name: "profiel");

            migrationBuilder.DropTable(
                name: "hond");
        }
    }
}
