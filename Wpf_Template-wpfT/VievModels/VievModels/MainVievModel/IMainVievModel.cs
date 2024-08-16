using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModels.Windows;
using Vievs.Windows;

namespace VievModel.VievModels.MainVievModel
{
    public interface IMainVievModel: IWindowViewModel, IDisposable
    {
        IWindow Run();
    }
}
