using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Settings;
using Domain.Settings;
using Infrastructure.Settings.WindowWrappers;
using Domain.Version;
using Infrastructure.ExelExplorer;
using Domain.ExelExplorer;

namespace Infrastructure
{
    public class RegistrationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowMementoWrapper>().As<IMainWindowMementoWrapper>().As<IWindowMementoWrapperInitializer>().SingleInstance();

            builder.RegisterType<AboutWindowMementoWrapper>().As<IAboutWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>().SingleInstance();

            builder.RegisterType<AutorizationWindowMementoWrapper>().As<IAutorizationWindowMementoWrapper>()
               .As<IWindowMementoWrapperInitializer>().SingleInstance();

            builder.RegisterType<AdminWindowMementoWrapper>().As<IAdminWindowMementoWrapper>()
              .As<IWindowMementoWrapperInitializer>().SingleInstance();
            builder.RegisterType<AddUserWindowMementoWrapper>().As<IAddUserWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer >().SingleInstance();
            builder.RegisterType<ExelExplorer.ExelExplorer>().As<IExelExplorer>().SingleInstance();
            builder.RegisterType<AplicationVersionProvider>().As<IAplicationVersionProvider>().SingleInstance();

            base.Load(builder);
        }
    }
}
