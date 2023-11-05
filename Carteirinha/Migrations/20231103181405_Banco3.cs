using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carteirinha.Migrations
{
    /// <inheritdoc />
    public partial class Banco3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data_de_nascimento",
                table: "Carteirinhas",
                newName: "Idade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Idade",
                table: "Carteirinhas",
                newName: "Data_de_nascimento");
        }
    }
}
