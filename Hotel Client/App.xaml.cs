using Hotel_Client.Repositories.Interfaces;
using Hotel_Client.Repositories;
using Hotel_Client.Services.Interfaces;
using Hotel_Client.Services;
using Hotel_Client.View;
using Hotel_Client.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IHotelRepository, HotelRepository>();
            services.AddSingleton<IAlertService, AlertService>();
            services.AddSingleton<IShareService, ShareService>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<LoginPage>();

            ServiceProvider = services.BuildServiceProvider();

            var loginPage = ServiceProvider.GetRequiredService<LoginPage>();
            loginPage.Show();
        }
    }
}
