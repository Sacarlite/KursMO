using Bootstrapper.Common;
using Domain.Settings;
using Infrastructure.Settings.DefaultMementas;

namespace Infrastructure.Settings.WindowWrappers;

internal class AboutWindowMementoWrapper : WindowMementoWrapper<AboutWindowMemento>, IAboutWindowMementoWrapper
{
    public AboutWindowMementoWrapper(IPathService pathService) : base(pathService)
    {
    }

    protected override string MementoName => "AboutWindowMemento";
}