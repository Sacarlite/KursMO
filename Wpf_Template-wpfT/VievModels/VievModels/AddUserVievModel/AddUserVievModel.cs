using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Settings;
using Domain.UserBd;
using Domain.UserEventArgs;
using Infrastructure.UserEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VievModel.VievModels.AdminMainVievModel;
using VievModel.Windows;

namespace VievModel.VievModels.AddUserVievModel
{
    public partial class AddUserVievModel : WindowVievModel<IAddUserWindowMementoWrapper>, IAddUserVievModel
    {
        private readonly IAddUserWindowMementoWrapper addUserWindowMementoWrapper;
        private  IUserDatabaseLocator userDatabaseLocator;
        [ObservableProperty]
        List<Role> roles;
        [ObservableProperty]
        Role selectedRole;
        [ObservableProperty]
        private string login = "";
        [ObservableProperty]
        private string password = "";

        public event Action<IEventArgs> AddNewUser;
        public event Action WindowClosingAct;

        public AddUserVievModel(IAddUserWindowMementoWrapper addUserWindowMementoWrapper, IUserDatabaseLocator userDatabaseLocator) :base(addUserWindowMementoWrapper)
        {
            this.addUserWindowMementoWrapper = addUserWindowMementoWrapper;
            this.userDatabaseLocator = userDatabaseLocator;
        }
        private bool CanOk()
        {
            return selectedRole.Name != "" && !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }
        [RelayCommand(CanExecute = nameof(CanOk))]
        void AddUser(Window window)
        {
            AddNewUser?.Invoke(new UserEventArgs(new User(login,password,selectedRole)));
            window.Close();
        }
        public void Dispose()
        {
            WindowClosingAct?.Invoke();
        }
    }
}
