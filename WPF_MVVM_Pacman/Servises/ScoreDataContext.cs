//using Core;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
////using DbContext = Microsoft.EntityFrameworkCore.DbContext;

//namespace Pacman
//{
//    public class ScoreDataContext : Microsoft.EntityFrameworkCore.DbContext
//    {
//        //public ScoreDataContext()
//        //   : base("UserScore")
//        //{ }

//        //public DbSet<UserScore> Users { get; set; }

//        public ScoreDataContext(DbContextOptions<ScoreDataContext> options)
//        : base(options)
//        {
          
//        }
    

//        public Microsoft.EntityFrameworkCore.DbSet<UserScore> UserScores { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//        }

       
//    }
//    public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
//    {
//        public Microsoft.EntityFrameworkCore.DbSet<UserScore> Usersaa { get; set; }

//        public ApplicationContext(DbContextOptions<ApplicationContext> options)
//            : base(options)
//        {
//            //Database.EnsureCreated();
//        }

//        public ApplicationContext()
//        {
//            //Database.EnsureCreated();
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            //optionsBuilder.UseSqlServer("Data Source=UserScore.db");
//            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UserScore;Trusted_Connection=True;");
//            //if (!optionsBuilder.IsConfigured)
//            //{
//      //      DbConnection sd = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection();
//      //      sd.ConnectionString = "Data Source=.;" +
//      //"Initial Catalog=UserScore;" +
//      //"Integrated Security=True";
//      //      optionsBuilder.UseSqlServer(sd);
//            //}
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder
//                .Entity<UserScore>(eb =>
//                {
//                    eb.HasNoKey();
//                    eb.Property(v => v.Name).HasColumnName("UserName");
//                    eb.Property(v => v.Score).HasColumnName("Score");
//                });
//        }
//    }
//}
