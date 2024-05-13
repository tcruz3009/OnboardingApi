using Microsoft.EntityFrameworkCore;
using OnboardingApi.Domain.Models;

namespace OnboardingApi.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public virtual DbSet<Totver> Totvers { get; set; }
        public virtual DbSet<Atividade> Atividades { get; set; }
        public virtual DbSet<Onboarding> Onboardings { get; set; }
        public virtual DbSet<AtividadeOnboarding> AtividadesOnboardings { get; set; }

    }
}
