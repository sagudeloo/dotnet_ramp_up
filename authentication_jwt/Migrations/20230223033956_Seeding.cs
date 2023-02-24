using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace authentication_jwt.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Email", "Password", "CreatedAt", "Role", "IsActiveRole" },
                values: new object[,]
                {
                    {
                        Guid.Parse("adb06bb2-f1e6-4482-852f-7cd01ca70e2e"),
                        "Terry Medhurst",
                        "atuny0@sohu.com",
                        "Password",
                        DateTime.Parse("2023/02/22 22:00:00"),
                        0,
                        true
                    },
                    {
                        Guid.Parse("9d0c0243-72b2-472c-b2e4-351ecae49d45"),
                        "Ervin Howell",
                        "Shanna@melissa.tv",
                        "Password",
                        DateTime.Parse("2023/02/21 22:00:00"),
                        0,
                        true
                    },
                    {
                        Guid.Parse("73eaf56f-d5d5-494e-8ba5-c81ee4aa64e9"),
                        "Clementine Bauch",
                        "Nathan@yesenia.net",
                        "Password",
                        DateTime.Parse("2023/02/20 22:00:00"),
                        1,
                        false
                    },
                    {
                        Guid.Parse("070af4d1-4431-479e-852b-03fcbbb21584"),
                        "Patricia Lebsack",
                        "Julianne.OConner@kory.org",
                        "Password",
                        DateTime.Parse("2023/02/19 22:00:00"),
                        2,
                        true
                    }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    Guid.Parse("adb06bb2-f1e6-4482-852f-7cd01ca70e2e"),
                    Guid.Parse("9d0c0243-72b2-472c-b2e4-351ecae49d45"),
                    Guid.Parse("73eaf56f-d5d5-494e-8ba5-c81ee4aa64e9"),
                    Guid.Parse("070af4d1-4431-479e-852b-03fcbbb21584")
                }
            );
        }
    }
}
