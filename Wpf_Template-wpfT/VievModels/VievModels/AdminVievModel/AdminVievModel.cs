using Bootstrapper.Factory;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Settings;
using VievModel.PageVievModels;
using VievModel.PageVievModels.MethodsPageVievModel;
using VievModel.Windows;
using Vievs.Page;

namespace VievModel.VievModels.AdminMainVievModel
{
    public partial class AdminVievModel
        : WindowVievModel<IAdminWindowMementoWrapper>,
            IAdminVievModel
    {
        private IAdminWindowMementoWrapper _windowMementoWrapper;
        private readonly IPageManager pageManager;

        [ObservableProperty]
        System.Windows.Controls.Page adminsListPage;

        [ObservableProperty]
        System.Windows.Controls.Page methodsListPage;

        public AdminVievModel(
            IAdminWindowMementoWrapper adminWindowMementoWrapper,
            IPageVievModelsFactory<IAdminPageVievModel> adminsPageVievModelsFactory,
            IPageVievModelsFactory<IMethodsPageVievModel> methodsListPageVievModelFactory,
            Vievs.Page.IPageManager pageManager
        )
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

        public void Dispose() { }
    }
}
