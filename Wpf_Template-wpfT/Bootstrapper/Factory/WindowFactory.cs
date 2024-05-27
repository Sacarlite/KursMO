using Autofac;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.VievModels.AboutWindowVievModel;
using VievModels.Windows;
using Vievs;
using Vievs.Windows;
using Vievs.Windows.AboutWindow;
using Vievs.Windows.AutorizationWindow;
using Vievs.Windows.MainWindow;

namespace Application.Factory;

public class WindowFactory:IWindowFactory
{
    private IComponentContext _componentContext;
    private readonly Dictionary<Type,Type> _map=new Dictionary<Type,Type>()
    {
           { typeof(IResearcherMainVievModel), typeof(IMainWindow)},
        { typeof(IAdminMainVievModel), typeof(IMainWindow)},
        { typeof(IAboutWindowVievModel), typeof(IAboutWindow)},
        { typeof(IAutorizationVievModel), typeof(IAutorizationWindow)},
    };
    public WindowFactory(IComponentContext componentContext)
    {
        _componentContext=componentContext;
    }

    public IWindow Create<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
    {
        if (!_map.TryGetValue(typeof(TWindowViewModel), out var windowType))
            throw new InvalidOperationException($"There is no window registered for {typeof(TWindowViewModel)}");
        var instance = _componentContext.Resolve(windowType, TypedParameter.From(viewModel));


        return (IWindow)instance;
    }
}