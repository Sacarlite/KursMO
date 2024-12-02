using Autofac;
using Domain.Settings;
using Domain.Version;
using Infrastructure.Settings;
using Infrastructure.Settings.WindowWrappers;

namespace Infrastructure
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MainWindowMementoWrapper>()
                .As<IMainWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();

            builder
                .RegisterType<AboutWindowMementoWrapper>()
                .As<IAboutWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();

            builder
                .RegisterType<AutorizationWindowMementoWrapper>()
                .As<IAutorizationWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();

            builder
                .RegisterType<AdminWindowMementoWrapper>()
                .As<IAdminWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();
            builder
                .RegisterType<AddUserWindowMementoWrapper>()
                .As<IAddUserWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();
            builder
                .RegisterType<AddMethodWindowMementoWrapper>()
                .As<IAddMethodWindowMementoWrapper>()
                .As<IWindowMementoWrapperInitializer>()
                .SingleInstance();
            builder
                .RegisterType<AplicationVersionProvider>()
                .As<IAplicationVersionProvider>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
