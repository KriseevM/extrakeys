using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace extrakeys.Utility;

public static class ResourceHostExtensions
{
    public static IServiceProvider GetServiceProvider(this IResourceHost control)
    {
        return (IServiceProvider)(control.FindResource(typeof(IServiceProvider)) ??
                                  throw new ApplicationException("Service provider is not present"));
    }

    public static T CreateInstance<T>(this IResourceHost control)
    {
        return ActivatorUtilities.CreateInstance<T>(control.GetServiceProvider());
    }
}