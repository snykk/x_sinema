using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using x_sinema.Models;

namespace x_sinema.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActorModel>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<MovieActorModel>().HasOne(m => m.Movie).WithMany(am => am.MovieActor).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieActorModel>().HasOne(m => m.Actor).WithMany(am => am.MovieActor).HasForeignKey(m => m.ActorId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ActorModel> Actors { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<MovieActorModel> Actors_Movies { get; set; }
        public DbSet<CinemaModel> Cinemas { get; set; }
        public DbSet<ProducerModel> Producers { get; set; }
    }
}