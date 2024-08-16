using CommunityToolkit.Mvvm.Input;
using Domain.MethodsBD;
using System.Windows.Input;

namespace VievModel.VievModels.MainWindow.ControlsVievModel;

public interface IMenuMainWindowVievModel:IDisposable
{
    event Action<Method>? MethodChanged;
    void CloseAboutWindow();
}