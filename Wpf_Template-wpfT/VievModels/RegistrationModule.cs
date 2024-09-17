using Autofac;
using VievModel.PageVievModels;
using VievModel.PageVievModels.MethodsPageVievModel;
using VievModel.VievModels.AddMethodVievModel;
using VievModel.VievModels.AddUserVievModel;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.MainVievModel;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.VievModels.AboutWindowVievModel;

namespace VievModel
{
    public class RegistrationModule : Module
    {
        //Регистрация VievModels
        protected override void Load(ContainerBuilder builder)
        {
            //Регистрация главной VievModel
            builder.RegisterType<MainVievModel>().As<IMainVievModel>().SingleInstance();
            //Регистрация VievModels окон
            builder.RegisterType<AdminVievModel>().As<IAdminVievModel>().InstancePerDependency();
            builder
                .RegisterType<ResearcherMainVievModel>()
                .As<IResearcherMainVievModel>()
                .InstancePerDependency();
            builder
                .RegisterType<AboutVievModel>()
                .As<IAboutWindowVievModel>()
                .InstancePerDependency();
            builder
                .RegisterType<AutorizationVievModel>()
                .As<IAutorizationVievModel>()
                .InstancePerDependency();
            builder
                .RegisterType<MenuMainWindowVievModel>()
                .As<IMenuMainWindowVievModel>()
                .InstancePerDependency();
            builder
                .RegisterType<AddUserVievModel>()
                .As<IAddUserVievModel>()
                .InstancePerDependency();
            builder
                .RegisterType<AddMethodVievModel>()
                .As<IAddMethodVievModel>()
                .InstancePerDependency();
            //Регистрация VievModels страниц
            builder
                .RegisterType<AdminPageVievModel>()
                .As<IAdminPageVievModel>()
                .InstancePerDependency();
            builder
                .RegisterType<MethodsPageVievModel>()
                .As<IMethodsPageVievModel>()
                .InstancePerDependency();
            base.Load(builder);
        }
    }
}
