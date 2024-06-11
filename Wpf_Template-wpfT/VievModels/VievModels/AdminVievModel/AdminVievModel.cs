using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Factories;
using Domain.Settings;
using Domain.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.Windows;

namespace VievModel.VievModels.AdminMainVievModel
{
    public partial class AdminVievModel : WindowVievModel<IAdminWindowMementoWrapper>, IAdminVievModel
    {
    private IAdminWindowMementoWrapper _windowMementoWrapper;
    public AdminVievModel(IAdminWindowMementoWrapper adminWindowMementoWrapper)
         : base(adminWindowMementoWrapper)
    {
        _windowMementoWrapper = adminWindowMementoWrapper;
    }
    public string Title => "TODO YorTitle";

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
