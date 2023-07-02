using Duende.IdentityServer.EntityFramework.Options;
using fitt.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol;

namespace fitt.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base model first and then make necessary changes
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExerciseTypeModel>()
                .HasMany(e => e.ExercisePlan);

            // causes scaffold generator error
            //modelBuilder.Entity<ExercisePlanModel>()
            //    .HasMany(e => e.ExerciseDailyPlans);

            modelBuilder.Entity<ExerciseDailyPlanModel>()
                .HasMany<ExercisePlanModel>()
                .WithMany(e=>e.ExerciseDailyPlans);


            // A daily plan can contain many exercises 
            modelBuilder.Entity<ExerciseDailyPlanModel>()
                .HasMany(e => e.Exercises)
                .WithMany(p => p.ExerciseDailyPlans);


            // An exercise can be present many plans
            modelBuilder.Entity<ExerciseModel>()
                .HasMany(e => e.ExerciseDailyPlans)
                .WithMany(e => e.Exercises);

            

            // Only unique exercises can be present in a daily plan
            modelBuilder.Entity<ExerciseDailyPlanModelExerciseModel>()
                .HasIndex(e=>new { e.ExerciseId , e.ExerciseDailyPlanId }).IsUnique();

            


        }




        

        public DbSet<ExerciseTypeModel> ExerciseType { get; set; }
        public DbSet<ExercisePlanModel> ExercisePlan { get; set; }
        public DbSet<fitt.Models.ExerciseDailyPlanModel> ExerciseDailyPlan { get; set; } = default!;

        public DbSet<ExerciseModel> Exercise { get; set; }
        public DbSet<fitt.Models.ExerciseDailyPlanModelExerciseModel> ExerciseDailyPlanExercise { get; set; } = default!;
        
    }
}