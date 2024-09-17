using System.Windows;
using Domain.Factories;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.Windows;
using Vievs.Windows;

namespace VievModel.VievModels.MainVievModel
{
    public class MainVievModel : IMainVievModel
    {
        private IAdminVievModel adminVievModel;
        private IResearcherMainVievModel researcherVievModel;
        private IAutorizationVievModel autorizationVievModel;
        private IWindowManager windowManager;

        public MainVievModel(
            IWindowManager windowManager,
            IWindowVievModelsFactory<IAdminVievModel> adminVievModelsFactory,
            IWindowVievModelsFactory<IResearcherMainVievModel> researcherVievModelsFactory,
            IWindowVievModelsFactory<IAutorizationVievModel> autorizationVievModelFactory
        )
        {
            adminVievModel = adminVievModelsFactory.Create();
            researcherVievModel = researcherVievModelsFactory.Create();
            autorizationVievModel = autorizationVievModelFactory.Create();
            autorizationVievModel.AutorizationAccess += AutorizationVievModel_AutorizationAccess;
            this.windowManager = windowManager;
        }

        private void AutorizationVievModel_AutorizationAccess(Domain.UserBd.User obj)
        {
            switch (obj.Role.Id)
            {
                case 1:
                    windowManager.Show(adminVievModel);
                    break;
                case 2:
                    windowManager.Show(researcherVievModel);
                    break;
                default:
                    MessageBox.Show("Coming Soon");
                    break;
            }
            windowManager.Close(autorizationVievModel);
        }

        public IWindow Run()
        {
            return windowManager.Show(researcherVievModel);
        }

        public void Dispose() { }

        public void WindowClosing() { }
    }
}
