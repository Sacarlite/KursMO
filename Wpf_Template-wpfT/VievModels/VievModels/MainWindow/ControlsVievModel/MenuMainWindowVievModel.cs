

using DevExpress.Mvvm;
using Domain.Factories;
using System.Windows.Input;
using VievModels.VievModels.AboutWindowVievModel;
using VievModels.Windows;
using Vievs.Windows;

namespace VievModel.VievModels.MainWindow.ControlsVievModel;

public class MenuMainWindowVievModel : IMenuMainWindowVievModel
{
    private readonly IWindowManager _windowManager;
    private IWindowVievModelsFactory<IAboutWindowVievModel> _aboutWindowViewModelFactory;
    private IAboutWindowVievModel? _aboutWindowVievModel;
    public MenuMainWindowVievModel(IWindowManager windowManager,
        IWindowVievModelsFactory<IAboutWindowVievModel> aboutWindowViewModelFactory)
    {
        _windowManager = windowManager;
        _aboutWindowViewModelFactory = aboutWindowViewModelFactory;
    }

    public void Dispose()
    {
      //TODO EVENTS
    }

    public ICommand OpenAboutWindowCommand
    {
        get
        {
            return new DelegateCommand(OpenAboutWindow);
        }
    }

    private void OpenAboutWindow()
    {
        if (_aboutWindowVievModel == null)
        {
            _aboutWindowVievModel = _aboutWindowViewModelFactory.Create();

            var aboutWindow = _windowManager.Show(_aboutWindowVievModel);

            aboutWindow.Closed += OnAboutWindowClosed;
        }
        else
        {
            _windowManager.Show(_aboutWindowVievModel);
        }
    }

    private void OnAboutWindowClosed(object? sender, EventArgs e)
    {
        if (sender is IWindow window)
        {
            window.Closed -= OnAboutWindowClosed;

            _aboutWindowVievModel = null;
        }
    }

    public void CloseAboutWindow()
    {
        if (_aboutWindowVievModel != null)
            _windowManager.Close(_aboutWindowVievModel);
    }
}