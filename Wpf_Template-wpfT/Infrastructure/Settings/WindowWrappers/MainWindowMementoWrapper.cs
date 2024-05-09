using Bootstrapper.Common;
using Domain.Settings;
using Infrastructure.Settings.DefaultMementas;

namespace Infrastructure.Settings.WindowWrappers;

internal class MainWindowMementoWrapper : WindowMementoWrapper<MainWindowMemento>, IMainWindowMementoWrapper
{
    public MainWindowMementoWrapper(IPathService pathService) :
        base(pathService)
    {

    }
    protected override string MementoName => "MainWindowMemento";
}