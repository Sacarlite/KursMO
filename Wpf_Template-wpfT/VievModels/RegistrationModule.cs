using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.VievModels.AboutWindowVievModel;
using VievModels.VievModels.MainWindow;
namespace VievModel
{
    public class RegistrationModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AdminMainVievModel>().As<IAdminMainVievModel>()
               .InstancePerDependency();
            builder.RegisterType<ResearcherMainVievModel>().As<IResearcherMainVievModel>()
               .InstancePerDependency();
            builder.RegisterType<AboutVievModel>().As<IAboutWindowVievModel>()
                .InstancePerDependency();
            builder.RegisterType<AutorizationVievModel>().As<IAutorizationVievModel>()
                .InstancePerDependency();
            builder.RegisterType<MenuMainWindowVievModel>().As<IMenuMainWindowVievModel>()
                .InstancePerDependency();
            base.Load(builder);
        }
    }
}
