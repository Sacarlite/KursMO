using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using VievModel.PageVievModels;
using VievModel.PageVievModels.UserPageVievModel;
using VievModel.VievModels.AddUserVievModel;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.VievModels.AboutWindowVievModel;
namespace VievModel
{
    public class RegistrationModule:Module
    {
        //Регистрация VievModels
        protected override void Load(ContainerBuilder builder)
        {
            //Регистрация VievModels окон
            builder.RegisterType<AdminVievModel>().As<IAdminVievModel>()
               .InstancePerDependency();
            builder.RegisterType<ResearcherMainVievModel>().As<IResearcherMainVievModel>()
               .InstancePerDependency();
            builder.RegisterType<AboutVievModel>().As<IAboutWindowVievModel>()
                .InstancePerDependency();
            builder.RegisterType<AutorizationVievModel>().As<IAutorizationVievModel>()
                .InstancePerDependency();
            builder.RegisterType<MenuMainWindowVievModel>().As<IMenuMainWindowVievModel>()
                .InstancePerDependency();
            builder.RegisterType<AddUserVievModel>().As<IAddUserVievModel>()
             .InstancePerDependency();
            //Регистрация VievModels страниц
            builder.RegisterType<UserPageVievModel>().As<IUserPageVievModel>()
               .InstancePerDependency();
            builder.RegisterType<AdminPageVievModel>().As<IAdminPageVievModel>()
               .InstancePerDependency();
            base.Load(builder);
        }
    }
}
