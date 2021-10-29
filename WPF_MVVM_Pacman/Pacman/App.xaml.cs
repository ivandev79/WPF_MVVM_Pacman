using System.Windows;
//using DataLayer;
using Microsoft.Extensions.DependencyInjection;

namespace Pacman
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);
          //var _serviceProvider = serviceCollection.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            //        services
            //               //.AddDbContext<BloggingContext>(opt => opt.UseInMemoryDatabase("UnitOfWork"))
            //               .AddUnitOfWork<ScoreDataContext>()
            //               .AddCustomRepository<UserScore, CustomBlogRepository>();
            //        optionsBuilder
            //.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //        var optionsBuilder = new DbContextOptionsBuilder<ScoreDataContext>();
            //        optionsBuilder.UseSqlServer("Data Source=blog.db");
            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Data Source=UserScore.db"));


        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            //var mainWindow = _serviceProvider.GetService<MainWindow>();
            //mainWindow.Show();
        }

    }

   
}
