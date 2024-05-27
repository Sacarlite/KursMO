using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.Windows;
using Vievs.Window;
using Vievs.Windows.AboutWindow;
using Vievs.Windows.MainWindow;

namespace Vievs
{
    public class RegistrationModule:Module
    {
        //Регистрация vievs в контейнере
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Vievs.MainWindow.MainWindow>().As<IMainWindow>().UsingConstructor(typeof(IAdminMainVievModel)).InstancePerDependency();
            builder.RegisterType<Vievs.MainWindow.MainWindow>().As<IMainWindow>().UsingConstructor(typeof(IResearcherMainVievModel)).InstancePerDependency();
            builder.RegisterType<Windows.AboutWindow.AboutWindow>().As<IAboutWindow>().InstancePerDependency();
            builder.RegisterType<Windows.AutorizationWindow.AutorizationWindow>().As<Windows.AutorizationWindow.IAutorizationWindow>().InstancePerDependency();
            builder.RegisterType<WindowManager>().As<IWindowManager>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
