using Microsoft.EntityFrameworkCore.Migrations;

namespace UdemyIdentityServer.AuthServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "customUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[] { 1, "istanbul", "fcakiroglu@outlook.com", "password", "facakiroglu16" });

            migrationBuilder.InsertData(
                table: "customUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[] { 2, "Ankara", "ahmet@outlook.com", "password", "ahmet16" });

            migrationBuilder.InsertData(
                table: "customUsers",
                columns: new[] { "Id", "City", "Email", "Password", "UserName" },
                values: new object[] { 3, "Konya", "mehmet@outlook.com", "password", "mehmet16" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customUsers");
        }
    }
}
