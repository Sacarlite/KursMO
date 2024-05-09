using Application.Factory;
using Autofac;
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
        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();
        builder.RegisterType<UserDatabaseLocator>().As<IUserDatabaseLocator>().SingleInstance();
        builder.RegisterType<UserDbContext>().SingleInstance();
        builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().SingleInstance();
        //builder.RegisterType<MainVievModelFactory>().As<IMainVievModelFactory>().SingleInstance();
        builder.RegisterGeneric(typeof(Factory<>)).As(typeof(IFactory<>)).SingleInstance();
        base.Load(builder);
    }
}