using Autofac;
using VievModels.Windows;
using Vievs.Page;
using Vievs.Pages.AdminsListPage;
using Vievs.Pages.MethodsListPage;
using Vievs.Window;
using Vievs.Windows.AboutWindow;
using Vievs.Windows.AddMethod;
using Vievs.Windows.AddUserWindow;
using Vievs.Windows.AdminWindow;
using Vievs.Windows.MainWindow;

namespace Vievs
{
    public class RegistrationModule : Module
    {
        //Регистрация vievs в контейнере
        protected override void Load(ContainerBuilder builder)
        {
            //Регистрация окон
            builder.RegisterType<AdminWindow>().As<IAdminWindow>().InstancePerDependency();
            builder
                .RegisterType<Vievs.MainWindow.MainWindow>()
                .As<IMainWindow>()
                .InstancePerDependency();
            builder
                .RegisterType<Windows.AboutWindow.AboutWindow>()
                .As<IAboutWindow>()
                .InstancePerDependency();
            builder
                .RegisterType<Windows.AutorizationWindow.AutorizationWindow>()
                .As<Windows.AutorizationWindow.IAutorizationWindow>()
                .InstancePerDependency();
            builder.RegisterType<AddUserWindow>().As<IAddUserWindow>().InstancePerDependency();
            builder.RegisterType<AddMethodWindow>().As<IAddMethodWindow>().InstancePerDependency();
            //Регистрация менеджеров
            builder.RegisterType<WindowManager>().As<IWindowManager>().InstancePerDependency();
            builder.RegisterType<PageManager>().As<IPageManager>().InstancePerDependency();
            //Регистрация страниц
            builder.RegisterType<AdminsListPage>().As<IAdminsListPage>().InstancePerDependency();
            builder.RegisterType<MethodsListPage>().As<IMethodsListPage>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
