﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.Windows;
using Vievs.Pages.AdminsListPage;
using Vievs.Pages.UsersListPage;
using Vievs.Window;
using Vievs.Windows.AboutWindow;
using Vievs.Windows.AdminWindow;
using Vievs.Windows.MainWindow;

namespace Vievs
{
    public class RegistrationModule:Module
    {
        //Регистрация vievs в контейнере
        protected override void Load(ContainerBuilder builder)
        {
            //Регистрация окон
            builder.RegisterType<AdminWindow>().As<IAdminWindow>().InstancePerDependency();
            builder.RegisterType<Vievs.MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
            builder.RegisterType<Windows.AboutWindow.AboutWindow>().As<IAboutWindow>().InstancePerDependency();
            builder.RegisterType<Windows.AutorizationWindow.AutorizationWindow>().As<Windows.AutorizationWindow.IAutorizationWindow>().InstancePerDependency();
            //Регистрация менеджеров
            builder.RegisterType<WindowManager>().As<IWindowManager>().InstancePerDependency();
            //Регистрация страниц
            builder.RegisterType<UsersListPage>().As<IUsersListPage>().InstancePerDependency();
            builder.RegisterType<AdminsListPage>().As<IAdminsListPage>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
