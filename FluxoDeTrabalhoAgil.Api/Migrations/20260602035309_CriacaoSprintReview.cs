using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeTrabalhoAgil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoSprintReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SprintReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeSprint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TasksTotais = table.Column<int>(type: "int", nullable: false),
                    TasksConcluidas = table.Column<int>(type: "int", nullable: false),
                    TasksNaoConcluidas = table.Column<int>(type: "int", nullable: false),
                    VelocityEntregue = table.Column<int>(type: "int", nullable: false),
                    VelocityNaoEntregue = table.Column<int>(type: "int", nullable: false),
                    Participantes = table.Column<int>(type: "int", nullable: false),
                    BugsCriticos = table.Column<int>(type: "int", nullable: false),
                    BugsMedios = table.Column<int>(type: "int", nullable: false),
                    BugsBaixos = table.Column<int>(type: "int", nullable: false),
                    BugsCorrigidos = table.Column<int>(type: "int", nullable: false),
                    BugsPendentes = table.Column<int>(type: "int", nullable: false),
                    BugsEncontrados = table.Column<int>(type: "int", nullable: false),
                    StoryPointsPlanejados = table.Column<int>(type: "int", nullable: false),
                    StoryPointsEntregues = table.Column<int>(type: "int", nullable: false),
                    EficienciaSprint = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TaxaSucesso = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempoMedioTask = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParticipacaoTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PontosPositivos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PontosMelhoria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FeedbackScrumMaster = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FeedbackProductOwner = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProximosPassos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DecisoesTomadas = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintReviews", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EntregasReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SprintReviewId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregasReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregasReview_SprintReviews_SprintReviewId",
                        column: x => x.SprintReviewId,
                        principalTable: "SprintReviews",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EntregasReview_SprintReviewId",
                table: "EntregasReview",
                column: "SprintReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntregasReview");

            migrationBuilder.DropTable(
                name: "SprintReviews");
        }
    }
}
