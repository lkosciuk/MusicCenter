using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MusicCenter.Dal.EntityModels;
using System.Data.Entity.ModelConfiguration.Conventions;
using MusicCenter.Dal.EntityConfigurations;
using Repository.Pattern.Ef6;

namespace MusicCenter.Dal
{
    public class MusicCenterDbContext : DataContext
    {
        public MusicCenterDbContext() : base("MusicCenterCs") { } //default connection string
        public MusicCenterDbContext(string connectionStringName) : base(connectionStringName) { }


		public DbSet<Album> Albums { get; set; }
		public DbSet<Band> Bands { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new AlbumConfiguration());
            modelBuilder.Configurations.Add(new BandConfiguration());
            modelBuilder.Configurations.Add(new BandMemberConfiguration());
            modelBuilder.Configurations.Add(new ConcertConfiguration());
            modelBuilder.Configurations.Add(new FavouritesConfiguration());
            modelBuilder.Configurations.Add(new FilesConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new TourConfiguration());
            modelBuilder.Configurations.Add(new TrackConfiguration());
            modelBuilder.Configurations.Add(new UsersConfiguration());


        }
    }
}
