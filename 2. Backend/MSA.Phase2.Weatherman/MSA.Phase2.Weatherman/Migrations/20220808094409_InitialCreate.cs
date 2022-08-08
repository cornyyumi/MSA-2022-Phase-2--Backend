using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Phase2.Weatherman.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherInfo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    visibility = table.Column<int>(type: "INTEGER", nullable: true),
                    dt = table.Column<int>(type: "INTEGER", nullable: true),
                    cod = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherInfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Main",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    temp = table.Column<float>(type: "REAL", nullable: false),
                    feels_like = table.Column<float>(type: "REAL", nullable: false),
                    temp_min = table.Column<float>(type: "REAL", nullable: false),
                    temp_max = table.Column<float>(type: "REAL", nullable: false),
                    WeatherInfoForeignKey = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Main", x => x.id);
                    table.ForeignKey(
                        name: "FK_Main_WeatherInfo_WeatherInfoForeignKey",
                        column: x => x.WeatherInfoForeignKey,
                        principalTable: "WeatherInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeatherInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.id);
                    table.ForeignKey(
                        name: "FK_Weather_WeatherInfo_WeatherInfoId",
                        column: x => x.WeatherInfoId,
                        principalTable: "WeatherInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Main_WeatherInfoForeignKey",
                table: "Main",
                column: "WeatherInfoForeignKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weather_WeatherInfoId",
                table: "Weather",
                column: "WeatherInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Main");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "WeatherInfo");
        }
    }
}
