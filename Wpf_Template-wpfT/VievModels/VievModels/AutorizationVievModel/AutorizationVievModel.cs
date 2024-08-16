
using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Factories;
using Domain.Settings;
using VievModel.Windows;
using VievModels.Windows;
using CommunityToolkit.Mvvm.Input;
using Domain.UserBd;
using System.Windows;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.VievModels.ResearcherMainVievModel;

namespace VievModel.VievModels.AutorizationVievModel
{
    public partial class AutorizationVievModel : WindowVievModel<IAutorizationWindowMementoWrapper>, IAutorizationVievModel
    {
        private  IAutorizationWindowMementoWrapper windowMementoWrapper;
        private readonly IWindowManager windowManager;
        private  IUserDatabaseLocator userDatabaseLocator;
        private  IWindowVievModelsFactory<IAdminVievModel> adminVievModelsFactory;
        private  IWindowVievModelsFactory<IResearcherMainVievModel> researcherVievModelsFactory;

        public AutorizationVievModel(IAutorizationWindowMementoWrapper windowMementoWrapper,
             IWindowManager windowManager, IUserDatabaseLocator userDatabaseLocator,
             IWindowVievModelsFactory<IAdminVievModel>adminVievModelsFactory,
             IWindowVievModelsFactory<IResearcherMainVievModel> researcherVievModelsFactory) : base(windowMementoWrapper)
        {
            this.windowMementoWrapper = windowMementoWrapper;
            this.windowManager = windowManager;
            this.userDatabaseLocator = userDatabaseLocator;
            this.adminVievModelsFactory = adminVievModelsFactory;
            this.researcherVievModelsFactory = researcherVievModelsFactory;
            Role admin = new Role("Aдминистратор");
            Role user = new Role("Пользователь");
            userDatabaseLocator.Context.Roles.AddRange(admin, user);
            User user1 = new User("user1", "123") { Role = user };
            User user2 = new User("user2", "123") { Role = user };
            User user3 = new User("user3", "123") { Role = user };
            User user4 = new User("user4", "123") { Role = user };
            User user5 = new User("user5", "123") { Role = user };
            userDatabaseLocator.Context.Users.AddRange(user1, user2, user3, user4, user5);
            User admin1 = new User("admin1", "321") { Role = admin };
            User admin2 = new User("admin2", "321") { Role = admin };
            userDatabaseLocator.Context.Users.AddRange(admin1, admin2);
            userDatabaseLocator.Context.SaveChanges();
        }
        [ObservableProperty]
        private string title = "Авторизация";
        [ObservableProperty]
        private string login;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private bool rememberMe;

        public event Action<User> AutorizationAccess;

        [RelayCommand]
        private void LoginUser()
        {
            try
            {
                var user = userDatabaseLocator.Context.Users.Where(i => i.Login == login).First();
                if (user.Password==Password)
                {
                    AutorizationAccess.Invoke(user);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации",
                      MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
