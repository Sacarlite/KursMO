using Application.Factory;
using Autofac;
using Bootstrapper.Factory;
using Bootstrapper.UserBd;
using Domain.Factories;
using Domain.PasswordService;
using Domain.UserBd;
using PasswordService;
using Vievs;

namespace Application;

public class RegistrationModule:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //Регистрация фабрики окон
        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();
        //Регистрация фабрики страниц
        builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();

        builder.RegisterType<UserDatabaseLocator>().As<IUserDatabaseLocator>().SingleInstance();
        builder.RegisterType<UserDbContext>().SingleInstance();
        builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().SingleInstance();
        builder.RegisterGeneric(typeof(WindowVievModelsFactory<>)).As(typeof(IWindowVievModelsFactory<>)).SingleInstance();
        builder.RegisterGeneric(typeof(PageVievModelsFactory<>)).As(typeof(IPageVievModelsFactory<>)).SingleInstance();
        base.Load(builder);
    }
}