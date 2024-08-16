

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using Domain.Factories;
using Domain.MethodsBD;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VievModels.VievModels.AboutWindowVievModel;
using VievModels.Windows;
using Vievs.Windows;

namespace VievModel.VievModels.MainWindow.ControlsVievModel;

public partial class MenuMainWindowVievModel : ObservableObject, IMenuMainWindowVievModel
{
    private readonly IWindowManager _windowManager;
    private IWindowVievModelsFactory<IAboutWindowVievModel> _aboutWindowViewModelFactory;
    private IMethodsDatabaseLocator methodsDatabaseLocator;
    private IAboutWindowVievModel? _aboutWindowVievModel;
    [ObservableProperty]
    ObservableCollection<Method> methods;
    [ObservableProperty]
    Method selectedMethod;

    public event Action<Method>? MethodChanged;

    public MenuMainWindowVievModel(IWindowManager windowManager,
        IWindowVievModelsFactory<IAboutWindowVievModel> aboutWindowViewModelFactory, IMethodsDatabaseLocator methodsDatabaseLocator)
    {
        _windowManager = windowManager;
        _aboutWindowViewModelFactory = aboutWindowViewModelFactory;
        this.methodsDatabaseLocator = methodsDatabaseLocator;
        //Заглушка
        Method bruteForce = new Method("Метод полного перебора", @"C:\Users\vlade\source\repos\KursMO\Wpf_Template-wpfT\BruteForceMethod\obj\Debug\net8.0-windows\BruteForceMethod.dll", "Описание описание");
        methodsDatabaseLocator.Context.Methods.Add(bruteForce);
        methodsDatabaseLocator.Context.SaveChanges();
        Methods = methodsDatabaseLocator.Context.Methods.ToObservableCollection();
    }

    public void Dispose()
    {
        //TODO EVENTS
    }
 
    partial void OnSelectedMethodChanged(Method value)
    {
        MethodChanged?.Invoke(value);
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

