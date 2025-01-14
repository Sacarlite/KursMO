﻿using System.Windows;
using Autofac;
using Domain.Factories;
using Infrastructure.Settings;
using NLog;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.MainVievModel;
using VievModels.Windows;

namespace Application;

internal class Application : IApplication, IDisposable
{
    public static readonly ILogger Logger = LogManager.GetLogger(nameof(Application));
    private readonly ILifetimeScope _applicationlifetimeScope;

    public Application(ILifetimeScope lifetimeScope)
    {
        Logger.Info("Create lifetimeScope");
        _applicationlifetimeScope = lifetimeScope.BeginLifetimeScope(RegisterDependencies);
        
    }


    public Window Run()
    {
        InitializeDependencies();
        _applicationlifetimeScope.Resolve<IWindowMementoWrapperInitializer>();
        var mainWindowVievModelFactory = _applicationlifetimeScope.Resolve<IWindowVievModelsFactory<IMainVievModel>>();        
        var mainWindowVievModel = mainWindowVievModelFactory.Create();
        var autorizationWindow =mainWindowVievModel.Run();
        if (autorizationWindow is not Window window) throw new NotImplementedException();
        return window;
    }


    public void Dispose()
    {
        Logger.Info("Dispose Application");
        _applicationlifetimeScope.Dispose();
    }

    private static void RegisterDependencies(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterModule<Vievs.RegistrationModule>()
            .RegisterModule<VievModel.RegistrationModule>()
            .RegisterModule<Infrastructure.RegistrationModule>()
            .RegisterModule<RegistrationModule>();
    }

    private void InitializeDependencies()
    {
        var windowWrapperInitializer =
            _applicationlifetimeScope.Resolve<IEnumerable<IWindowMementoWrapperInitializer>>();
        foreach (var elem in windowWrapperInitializer) elem.initialize();
    }
}

public interface IApplication
{
    Window Run();
}