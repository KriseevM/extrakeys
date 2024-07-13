using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using extrakeys.Utility;
using extrakeys.ViewModels;
using extrakeys.Views;
using Microsoft.Extensions.DependencyInjection;

namespace extrakeys;

public partial class App : Application
{

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        var collection = new ServiceCollection();
        collection.AddApplicationServices();
        IServiceProvider services = collection.BuildServiceProvider();
        Resources[typeof(IServiceProvider)] = services;
    }


    public override void OnFrameworkInitializationCompleted()
    {
        // Lang.Resources.Culture = new CultureInfo("ru-RU");
        var vm = this.GetServiceProvider().GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm,
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}