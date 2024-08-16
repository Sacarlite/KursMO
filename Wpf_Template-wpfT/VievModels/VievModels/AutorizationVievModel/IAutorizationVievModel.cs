using Autofac.Util;
using Domain.UserBd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModels.Windows;

namespace VievModel.VievModels.AutorizationVievModel
{
    public interface IAutorizationVievModel: IWindowViewModel, IDisposable
    {
        event Action<User> AutorizationAccess;
    }
}
