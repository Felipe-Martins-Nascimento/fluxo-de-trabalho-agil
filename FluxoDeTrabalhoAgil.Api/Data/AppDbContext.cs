using FluxoDeTrabalhoAgil.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FluxoDeTrabalhoAgil.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }

        public DbSet<Reuniao> Reunioes { get; set; }
        public DbSet<MensagemDaily> MensagensDaily { get; set; }
        public DbSet<SprintPlanning> SprintPlannings { get; set; }
        public DbSet<SprintTask> SprintTasks { get; set; }
        public DbSet<SprintReview> SprintReviews { get; set; }
        public DbSet<EntregaReview> EntregasReview { get; set; }
        public DbSet<SprintRetro> SprintRetros { get; set; }
        public DbSet<RetroCard> RetroCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MensagemDaily>()
                .Property(x => x.Audios)
                .HasConversion(

                    v => JsonSerializer.Serialize(
                        v ?? new List<string>(),
                        new JsonSerializerOptions()
                    ),

                    v => string.IsNullOrWhiteSpace(v)

                        ? new List<string>()

                        : JsonSerializer.Deserialize<List<string>>(
                            v,
                            new JsonSerializerOptions()
                        ) ?? new List<string>()
                );

            modelBuilder.Entity<MensagemDaily>()
                .Property(x => x.Respostas)
                .HasConversion(

                    v => JsonSerializer.Serialize(
                        v ?? new List<RespostaDaily>(),
                        new JsonSerializerOptions()
                    ),

                    v => string.IsNullOrWhiteSpace(v)

                        ? new List<RespostaDaily>()

                        : JsonSerializer.Deserialize<List<RespostaDaily>>(
                            v,
                            new JsonSerializerOptions()
                        ) ?? new List<RespostaDaily>()
                );
            modelBuilder.Entity<RetroCard>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<SprintRetro>()
                .HasMany(x => x.Cards)
                .WithOne(x => x.SprintRetro)
                .HasForeignKey(x => x.SprintRetroId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}