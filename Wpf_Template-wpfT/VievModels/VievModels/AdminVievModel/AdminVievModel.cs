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
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.Windows;
using Vievs.Page;
using VievModel.PageVievModels.MethodsPageVievModel;

namespace VievModel.VievModels.AdminMainVievModel
{
    public partial class AdminVievModel : WindowVievModel<IAdminWindowMementoWrapper>, IAdminVievModel
    {
        private IAdminWindowMementoWrapper _windowMementoWrapper;
        private readonly IPageManager pageManager;
        [ObservableProperty]
        System.Windows.Controls.Page adminsListPage;
        [ObservableProperty]
        System.Windows.Controls.Page methodsListPage;
        public AdminVievModel(IAdminWindowMementoWrapper adminWindowMementoWrapper, IPageVievModelsFactory<IAdminPageVievModel> adminsPageVievModelsFactory, 
            IPageVievModelsFactory<IMethodsPageVievModel> methodsListPageVievModelFactory,
           Vievs.Page.IPageManager pageManager)
         : base(adminWindowMementoWrapper)
    {
        _windowMementoWrapper = adminWindowMementoWrapper;
            var methodsListPageVievModel = methodsListPageVievModelFactory.Create();
            MethodsListPage = pageManager.GetPage(methodsListPageVievModel);

            var adminsPageVievModel = adminsPageVievModelsFactory.Create();
            AdminsListPage = pageManager.GetPage(adminsPageVievModel);

            this.pageManager = pageManager;
        }
    public string Title => "TODO YorTitle";

        public void Dispose()
        {
            
        }
    }
}
