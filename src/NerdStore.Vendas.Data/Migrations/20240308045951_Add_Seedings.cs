using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NerdStore.Vendas.Data.Migrations
{
    public partial class Add_Seedings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__SeedingHistory",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK___SeedingHistory", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__SeedingHistory");
        }
    }
}
