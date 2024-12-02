using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Factories;
using Domain.Settings;
using Domain.UserBd;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.ResearcherMainVievModel;
using VievModel.Windows;
using VievModels.Windows;

namespace VievModel.VievModels.AutorizationVievModel
{
    public partial class AutorizationVievModel
        : WindowVievModel<IAutorizationWindowMementoWrapper>,
            IAutorizationVievModel
    {
        private IAutorizationWindowMementoWrapper windowMementoWrapper;
        private readonly IWindowManager windowManager;
        private IUserDatabaseLocator userDatabaseLocator;
        private IWindowVievModelsFactory<IAdminVievModel> adminVievModelsFactory;
        private IWindowVievModelsFactory<IResearcherMainVievModel> researcherVievModelsFactory;

        public AutorizationVievModel(
            IAutorizationWindowMementoWrapper windowMementoWrapper,
            IWindowManager windowManager,
            IUserDatabaseLocator userDatabaseLocator,
            IWindowVievModelsFactory<IAdminVievModel> adminVievModelsFactory,
            IWindowVievModelsFactory<IResearcherMainVievModel> researcherVievModelsFactory
        )
            : base(windowMementoWrapper)
        {
            this.windowMementoWrapper = windowMementoWrapper;
            this.windowManager = windowManager;
            this.userDatabaseLocator = userDatabaseLocator;
            this.adminVievModelsFactory = adminVievModelsFactory;
            this.researcherVievModelsFactory = researcherVievModelsFactory;
        }

        [ObservableProperty]
        private string title = "Авторизация";

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool rememberMe;

        public event Action<Role> AutorizationAccess;

        [RelayCommand]
        private void LoginUser()
        {
            try
            {
                var users = userDatabaseLocator
                    .Context.Users.ToList();
                var user = userDatabaseLocator
                    .Context.Users.Include(x => x.Role)
                    .Where(i => i.Login == login)
                    .First();
                if (user.Password == Password)
                {
                    AutorizationAccess.Invoke(user.Role);
                }
                else
                {
                    MessageBox.Show(
                        "Неверный логин или пароль",
                        "Ошибка авторизации",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Неверный логин или пароль",
                    "Ошибка авторизации",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
