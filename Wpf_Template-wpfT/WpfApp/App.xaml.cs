using System.Windows;
using System.Windows.Threading;
using Application;
using Bootstrapper;
using Bootstrapper.Logging;

namespace WpfApp;

public partial class App
{
    private IApplication? _application;
    private IUnhendledExeptionHandler? _unhendledExeptionHandler;
    private ApplicationBootstrapper? _bootstrapper;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _bootstrapper = new ApplicationBootstrapper();
        DispatcherUnhandledException += OnUnhandledExeptionRised;
        var _application = _bootstrapper.CreateApplication();
        MainWindow= _application.Run();
    }

    private void OnUnhandledExeptionRised(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        if (_bootstrapper is not null)
        {
            _unhendledExeptionHandler=_bootstrapper.CreateUnhendledExeptionHandler();
            _unhendledExeptionHandler.Handel(e);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _bootstrapper.Dispose();
        base.OnExit(e);
    }
}