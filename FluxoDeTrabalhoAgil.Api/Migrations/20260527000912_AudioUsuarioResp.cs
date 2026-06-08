using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeTrabalhoAgil.Api.Migrations
{
    /// <inheritdoc />
    public partial class AudioUsuarioResp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioUrl",
                table: "MensagensDaily",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioResposta",
                table: "MensagensDaily",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioUrl",
                table: "MensagensDaily");

            migrationBuilder.DropColumn(
                name: "UsuarioResposta",
                table: "MensagensDaily");
        }
    }
}
