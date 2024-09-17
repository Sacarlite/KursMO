using Domain.MethodsBD;
using VievModels.Windows;

namespace VievModel.VievModels.AddMethodVievModel
{
    public interface IAddMethodVievModel : IWindowViewModel, IDisposable
    {
        event Action<Method?> AddNewMethod;
        event Action WindowClosingAct;
    }
}
