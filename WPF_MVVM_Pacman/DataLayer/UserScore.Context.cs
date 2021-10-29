namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UserScoreEntities : DbContext
    {
        public UserScoreEntities()
            //: base("metadata=res://*/UserScore.csdl|res://*/UserScore.ssdl|res://*/UserScore.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=localhost;initial catalog=UserScore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;providerName=System.Data.EntityClient")
          :  base ("name=UserScoreEntities")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Score>()
                .HasKey(t => t.ScoreId)
                .HasKey(t => t.UserName)
                .HasKey(t => t.MyScore)
                .HasKey(t => t.DateAdded)
                ;
            //modelBuilder.Entity<Score>().HasRequired(p => p.Person);

        }

        public virtual DbSet<Score> Users { get; set; }

       
    }
}
