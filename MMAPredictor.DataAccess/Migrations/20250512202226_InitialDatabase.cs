using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAPredictor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fighters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NbWins = table.Column<int>(type: "INTEGER", nullable: false),
                    NbLoss = table.Column<int>(type: "INTEGER", nullable: false),
                    NbDraws = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    Weight = table.Column<int>(type: "INTEGER", nullable: true),
                    Reach = table.Column<int>(type: "INTEGER", nullable: true),
                    StrikesLandedByMinute = table.Column<float>(type: "REAL", nullable: true),
                    StrikesAccuracy = table.Column<float>(type: "REAL", nullable: true),
                    StrikesAbsorbedByMinute = table.Column<float>(type: "REAL", nullable: true),
                    StrikingDefenseAccuracy = table.Column<float>(type: "REAL", nullable: true),
                    TakedownAverage = table.Column<float>(type: "REAL", nullable: true),
                    TakedownAccuracy = table.Column<float>(type: "REAL", nullable: true),
                    TakedownDefenseAccuracy = table.Column<float>(type: "REAL", nullable: true),
                    SubmissionsAverage = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MmaEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Town = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MmaEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FightResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Result = table.Column<int>(type: "INTEGER", nullable: false),
                    WinnerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FightResults_Fighters_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Fighters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fighter1Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Fighter2Id = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_Fighter1Id",
                        column: x => x.Fighter1Id,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fights_Fighters_Fighter2Id",
                        column: x => x.Fighter2Id,
                        principalTable: "Fighters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fights_MmaEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MmaEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FightRounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    FightId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FightRounds_Fights_FightId",
                        column: x => x.FightId,
                        principalTable: "Fights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FightResults_WinnerId",
                table: "FightResults",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FightRounds_FightId",
                table: "FightRounds",
                column: "FightId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_EventId",
                table: "Fights",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_Fighter1Id",
                table: "Fights",
                column: "Fighter1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_Fighter2Id",
                table: "Fights",
                column: "Fighter2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FightResults");

            migrationBuilder.DropTable(
                name: "FightRounds");

            migrationBuilder.DropTable(
                name: "Fights");

            migrationBuilder.DropTable(
                name: "Fighters");

            migrationBuilder.DropTable(
                name: "MmaEvents");
        }
    }
}
