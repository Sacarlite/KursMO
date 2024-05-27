using Bootstrapper.UserBd;
using Domain.Factories;
using Domain.PasswordService;
using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModel.Windows;
using VievModels.Windows;

namespace VievModel.VievModels.RegistrationWindowVievModel
{
    public class RegistrationWindowVievModel : WindowVievModel<IRegistrationWindowMementoWrapper>, IRegistrationWindowVievModel
    {
        private  IRegistrationWindowMementoWrapper windowMementoWrapper;
        private  IWindowManager windowManager;
        private  IUserDatabaseLocator userDatabaseLocator;
        private  IPasswordHasher passwordHasher;
        private  IFactory<IResearcherMainVievModel> mainWindowVievModelFactory;

        public RegistrationWindowVievModel(IRegistrationWindowMementoWrapper windowMementoWrapper, 
            IWindowManager windowManager, IUserDatabaseLocator userDatabaseLocator, IPasswordHasher passwordHasher,
            IFactory<IResearcherMainVievModel> MainWindowVievModelFactory)
            : base(windowMementoWrapper)
        {
            this.windowMementoWrapper = windowMementoWrapper;
            this.windowManager = windowManager;
            this.userDatabaseLocator = userDatabaseLocator;
            this.passwordHasher = passwordHasher;
            mainWindowVievModelFactory = MainWindowVievModelFactory;
        }

        public void Dispose()
        {

        }

        public void WindowClosing()
        {

        }
    }
}
