using Microsoft.Extensions.DependencyInjection;
using ImageGrabber.Services;
using ImageGrabber.Stores;
using ImageGrabber.ViewModels;
using System;
using System.Windows;

namespace ImageGrabber
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();


            services.AddSingleton<ImageService>();
            services.AddSingleton<ImageStore>();
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));

            services.AddTransient<HomeViewModel>(s => new HomeViewModel(CreateAccountNavigationService(s)));

            services.AddTransient<ImageViewModel>(s => new ImageViewModel(
                s.GetRequiredService<ImageStore>(),
                CreateHomeNavigationService(s)));

            services.AddSingleton<MainViewModel>();

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeViewModel>());
        }

        private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ImageViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ImageViewModel>());
        }

       

    }
}
