using Domain.UserBd;
using VievModels.Windows;

namespace VievModel.VievModels.AutorizationVievModel
{
    public interface IAutorizationVievModel : IWindowViewModel, IDisposable
    {
        event Action<Role> AutorizationAccess;
    }
}
