using extrakeys.Services;
using extrakeys.Services.Interfaces;
using extrakeys.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace extrakeys;
public static class ServiceCollectionExtensions {
    public static void AddApplicationServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IBoardRepository, BoardRepositoryImpl>();
        collection.AddSingleton<IBoardProgrammerService, BoardProgrammingServiceImpl>();
        // ViewModels:
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<ProgrammerWindowViewModel>();
        collection.AddTransient<NewKeybindWindowViewModel>();
    }
}