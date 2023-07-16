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

            // Only unique daily plans can be present in a exercise
            modelBuilder.Entity<ExercisePlanExerciseDailyPlanModel>()
                .HasIndex(e => new { e.ExercisePlanId, e.ExerciseDailyPlanId });

            

            // An application user cannot have multiple same exercise plans. He can switch to the plan if he
            // has used it in the past
            modelBuilder.Entity<ApplicationUserExercisePlanModel>()
                .HasIndex(e => new { e.ApplicationUserId, e.ExercisePlanId }).IsUnique()
                ;

            // Default time for intensity is set to current time
            modelBuilder.Entity<IntensityModel>()
                .Property(e => e.ExerciseDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<BodyWeightModel>()
                .Property(bw => bw.CreatedAt)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<BodyWeightModel>()
                .Property(bw => bw.RecordedDate)
                .HasDefaultValueSql("getdate()");


            modelBuilder.Entity<SleepModel>()
                .Property( s => s.SleepDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<SleepModel>()
                .Property( s => s.CreatedAt)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<CalorieModel>()
                .Property(c => c.CalorieConsumptionDate)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<CalorieModel>()
                .Property(c=> c.CreatedAt)
                .HasDefaultValueSql("getdate()");


        }




        

        public DbSet<ExerciseTypeModel> ExerciseType { get; set; }
        public DbSet<ExercisePlanModel> ExercisePlan { get; set; }
        public DbSet<fitt.Models.ExerciseDailyPlanModel> ExerciseDailyPlan { get; set; } = default!;

        public DbSet<ExerciseModel> Exercise { get; set; }
        public DbSet<fitt.Models.ExerciseDailyPlanModelExerciseModel> ExerciseDailyPlanExercise { get; set; } = default!;
        public DbSet<fitt.Models.ExercisePlanExerciseDailyPlanModel> ExercisePlanExerciseDailyPlanModel { get; set; } = default!;
        public DbSet<fitt.Models.ApplicationUserExercisePlanModel> ApplicationUserExercisePlanModel { get; set; } = default!;
        public DbSet<fitt.Models.IntensityModel> IntensityModel { get; set; } = default!;

        public DbSet<BodyWeightModel> BodyWeight { get; set; } = default!;

        public DbSet<SleepModel> Sleep { get; set; } = default!;

        public DbSet<CalorieModel> Calorie { get; set; } = default!;

    }
}