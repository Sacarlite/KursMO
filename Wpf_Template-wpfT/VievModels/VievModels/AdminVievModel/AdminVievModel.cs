using Bootstrapper.Factory;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Factories;
using Domain.Settings;
using Domain.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vievs;
using VievModel.PageVievModels;
using VievModel.PageVievModels.UserPageVievModel;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.Windows;
using Vievs.Page;

namespace VievModel.VievModels.AdminMainVievModel
{
    public partial class AdminVievModel : WindowVievModel<IAdminWindowMementoWrapper>, IAdminVievModel
    {
    private IAdminWindowMementoWrapper _windowMementoWrapper;
        
        private readonly IPageManager pageManager;
        [ObservableProperty]
        System.Windows.Controls.Page adminsListPage;
        public AdminVievModel(IAdminWindowMementoWrapper adminWindowMementoWrapper, IPageVievModelsFactory<IAdminPageVievModel> adminsPageVievModelsFactory,
            IPageVievModelsFactory<IUserPageVievModel> userPageVievModelsFactory, Vievs.Page.IPageManager pageManager)
         : base(adminWindowMementoWrapper)
    {
        _windowMementoWrapper = adminWindowMementoWrapper;
            var adminsPageVievModel = adminsPageVievModelsFactory.Create();
            AdminsListPage = pageManager.GetPage(adminsPageVievModel);

            this.pageManager = pageManager;
        }
    public string Title => "TODO YorTitle";

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
