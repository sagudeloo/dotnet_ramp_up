using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace background_service.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] {"Id", "Name", "TimeZone"},
                values: new object[,]
                {
                    {
                        1,
                        "Bogota",
                        "America/Bogota"
                    },
                    {
                        2,
                        "Chicago",
                        "America/Chicago"
                    },
                    {
                        3,
                        "Argentina",
                        "America/Argentina/Buenos_Aires"
                    },
                    {
                        4,
                        "Detroit",
                        "America/Detroit"
                    },
                    {
                        5,
                        "London",
                        "Europe/London"
                    },
                    {
                        6,
                        "Japan",
                        "Japan"
                    },
                    {
                        7,
                        "Madrid",
                        "Europe/Madrid"
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 }
            );
        }
    }
}
