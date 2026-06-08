using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeTrabalhoAgil.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAudiosDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Audios",
                table: "MensagensDaily",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audios",
                table: "MensagensDaily");
        }
    }
}
