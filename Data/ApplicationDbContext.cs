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


            //modelBuilder.Entity<ExerciseDailyPlanModel>()
            //    .HasMany(e => e.Exercises).WithMany(e => e.ExerciseDailyPlans)
            //    .UsingEntity<ExerciseDailyPlanAndExercisesModel>(
            //        l=> l.HasOne<ExerciseModel>().WithMany().HasForeignKey(e=>e.ExerciseId),
            //        r => r.HasOne<ExerciseDailyPlanModel>().WithMany().HasForeignKey(l => l.ExerciseDailyPlanId)
            //    );

            modelBuilder.Entity<ExerciseModel>()
                .HasMany(e => e.ExerciseDailyPlans)
                .WithMany(e => e.Exercises)
                ;

            modelBuilder.Entity<ExerciseDailyPlanModel>()
                .HasMany(e => e.Exercises)
                .WithMany(p => p.ExerciseDailyPlans);


            //modelBuilder.Entity<ExerciseDailyPlanExerciseModel>()
            //    .HasKey(x => new { x.ExerciseDailyPlanId, x.ExerciseId });





        }




        public DbSet<ExerciseModel> Exercise { get; set; }

        public DbSet<ExerciseTypeModel> ExerciseType { get; set; }
        public DbSet<ExercisePlanModel> ExercisePlan { get; set; }
        public DbSet<fitt.Models.ExerciseDailyPlanModel> ExerciseDailyPlan { get; set; } = default!;
        public DbSet<fitt.Models.ExerciseDailyPlanModelExerciseModel> ExerciseDailyPlanExerciseModel { get; set; } = default!;
        
        //public DbSet<ExerciseDailyPlanExerciseModel> ExerciseDailyPlanExercise { get; set; }
        //}
    }
}