using Domain.MethodsBD;

namespace VievModel.VievModels.MainWindow.ControlsVievModel;

public interface IMenuMainWindowVievModel : IDisposable
{
    event Action<Method>? MethodChanged;
    event Action<MetaInfo.Task>? TaskChanged;
    void CloseAboutWindow();
}
