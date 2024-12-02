using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using Domain.Factories;
using Domain.MethodsBD;
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

    [ObservableProperty]
    ObservableCollection<MetaInfo.Task> forms;

    [ObservableProperty]
    MetaInfo.Task selectedForm;

    public event Action<Method>? MethodChanged;
    public event Action<MetaInfo.Task>? TaskChanged;

    public MenuMainWindowVievModel(
        IWindowManager windowManager,
        IWindowVievModelsFactory<IAboutWindowVievModel> aboutWindowViewModelFactory,
        IMethodsDatabaseLocator methodsDatabaseLocator
    )
    {
        Forms = new ObservableCollection<MetaInfo.Task>();
        Forms.Add(new MetaInfo.DefaultTask());
        Forms.Add(new MetaInfo.Task1());
        Forms.Add(new MetaInfo.Task2());
        SelectedForm = Forms.First();
        _windowManager = windowManager;
        _aboutWindowViewModelFactory = aboutWindowViewModelFactory;
        this.methodsDatabaseLocator = methodsDatabaseLocator;
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

    partial void OnSelectedFormChanged(MetaInfo.Task value)
    {
        TaskChanged?.Invoke(value);
    }

    public ICommand OpenAboutWindowCommand
    {
        get { return new DelegateCommand(OpenAboutWindow); }
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
