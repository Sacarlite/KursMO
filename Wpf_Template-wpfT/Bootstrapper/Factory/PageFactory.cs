using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.AutorizationVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModels.VievModels.AboutWindowVievModel;
using VievModels.Windows;
using Vievs.Windows.AboutWindow;
using Vievs.Windows.AdminWindow;
using Vievs.Windows.AutorizationWindow;
using Vievs.Windows.MainWindow;
using Vievs.Windows;
using Vievs;
using Vievs.Pages;
using VievModel.PageVievModels;
using Vievs.Pages.AdminsListPage;
using VievModel.PageVievModels.UserPageVievModel;
using Vievs.Pages.UsersListPage;

namespace Bootstrapper.Factory
{
    public class PageFactory : IPageFactory
    {
        private IComponentContext _componentContext;
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>()
        {
           { typeof(IAdminPageVievModel), typeof(IAdminsListPage)},
             { typeof(IUserPageVievModel), typeof(IUsersListPage)},
    };
        public PageFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public IPage Create<TPageViewModel>(TPageViewModel viewModel) where TPageViewModel : IPageVievModel
        {
            if (!_map.TryGetValue(typeof(TPageViewModel), out var windowType))
                throw new InvalidOperationException($"There is no window registered for {typeof(TPageViewModel)}");
            var instance = _componentContext.Resolve(windowType, TypedParameter.From(viewModel));


            return (IPage)instance;
        }

       
    }
}
