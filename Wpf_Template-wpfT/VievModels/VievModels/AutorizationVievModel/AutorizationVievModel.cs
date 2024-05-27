
using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Factories;
using Domain.Settings;
using NLog;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModel.Windows;
using VievModels.VievModels.AboutWindowVievModel;
using VievModels.Windows;
using CommunityToolkit.Mvvm.Input;
using Domain.UserBd;
using Domain.PasswordService;
using System.Windows;

namespace VievModel.VievModels.AutorizationVievModel
{
    public partial class AutorizationVievModel : WindowVievModel<IAutorizationWindowMementoWrapper>, IAutorizationVievModel
    {
        private  IAutorizationWindowMementoWrapper windowMementoWrapper;
        private readonly IWindowManager windowManager;
        private  IUserDatabaseLocator userDatabaseLocator;
        private  IPasswordHasher passwordHasher;

        public AutorizationVievModel(IAutorizationWindowMementoWrapper windowMementoWrapper,
             IWindowManager windowManager, IUserDatabaseLocator userDatabaseLocator, IPasswordHasher passwordHasher) : base(windowMementoWrapper)
        {
            this.windowMementoWrapper = windowMementoWrapper;
            this.windowManager = windowManager;
            this.userDatabaseLocator = userDatabaseLocator;
            this.passwordHasher = passwordHasher;
        }
        [ObservableProperty]
        private string title = "TODO YorTitle";
        [ObservableProperty]
        private string login;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private bool rememberMe;
        [RelayCommand]
        private void LoginUser()
        {
            try
            {
                var user = userDatabaseLocator.Context.Users.Where(i => i.Login == login).First();
                if (user.Password == passwordHasher.GetHashPassword(password))
                {
                  
                    var mainWindow = windowManager.Show(mainVievModel);
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
        }
    }
}
