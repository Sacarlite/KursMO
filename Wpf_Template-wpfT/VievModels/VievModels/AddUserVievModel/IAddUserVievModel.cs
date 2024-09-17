using Domain.UserBd;
using VievModels.Windows;

namespace VievModel.VievModels.AddUserVievModel
{
    public interface IAddUserVievModel : IWindowViewModel, IDisposable
    {
        event Action<User?> AddNewUser;
        event Action WindowClosingAct;
    }
}
