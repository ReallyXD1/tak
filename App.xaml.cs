using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using FactoryManager.Desktop.Services;
using FactoryManager.Desktop.ViewModels;
using System;

namespace FactoryManager.Desktop
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Configure HttpClient
            services.AddHttpClient();

            // Register Services
            services.AddSingleton<IProductionService, ProductionService>();
            services.AddSingleton<IProductionSchedulingService, ProductionSchedulingService>();
            services.AddSingleton<IWorkstationService, WorkstationService>();
            services.AddSingleton<IOperatorService, OperatorService>();

            // Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<ProductionScheduleViewModel>();
            services.AddTransient<ProductionMonitoringViewModel>();
            services.AddTransient<WorkstationOverviewViewModel>();
            services.AddTransient<ReportsViewModel>();

            // Register Main Window
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider.Dispose();
            base.OnExit(e);
        }
    }
}
