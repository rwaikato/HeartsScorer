using ScoreKeeper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ScoreKeeper.Dal
{
    public class ScoreKeeperContext : DbContext
    {
        public ScoreKeeperContext()
            : base("ScoreKeeperContext")
        {
        }
        
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayersGameMap> PlayersGameMaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}