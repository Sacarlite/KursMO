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

            builder.RegisterType<AplicationVersionProvider>().As<IAplicationVersionProvider>().SingleInstance();
            base.Load(builder);
        }
    }
}
