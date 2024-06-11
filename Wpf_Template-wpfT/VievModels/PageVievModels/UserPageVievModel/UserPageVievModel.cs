using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.UserBd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VievModel.PageVievModels.UserPageVievModel
{
    public partial class UserPageVievModel: ObservableObject,IUserPageVievModel
    {
        private IUserDatabaseLocator userDatabaseLocator;
        [ObservableProperty]
        List<User> users;
        [ObservableProperty]
        User user;
        public UserPageVievModel(IUserDatabaseLocator userDatabaseLocator)
        {
            this.userDatabaseLocator = userDatabaseLocator;
        }
        [RelayCommand]
        private void DeleteAdminCommand()
        {
            userDatabaseLocator.Context.Remove(user);
        }
        [RelayCommand]
        private void AddNewAdminCommand()
        {
            User = new User();
        }
        [RelayCommand]
        private void SaveAdminCommand()
        {
            if (userDatabaseLocator.Context.Users.Where(u => u.Id == User.Id).FirstOrDefault() == null)
            {
                User newAdmin = new User();
                newAdmin.Login = User.Login;
                newAdmin.Password = User.Password;
                newAdmin.Role = Domain.Roles.Roles.Researcher;
                userDatabaseLocator.Context.Add(newAdmin);
                userDatabaseLocator.Context.SaveChanges();
            }
            else
            {
                userDatabaseLocator.Context.SaveChanges();
            }
            OnPageLoading();
        }
        public void OnPageLoading()
        {
            if (userDatabaseLocator != null)
            {
                Users = userDatabaseLocator.Context.Users.Where(u => u.Role == Domain.Roles.Roles.Researcher).ToList();
                User = Users.FirstOrDefault();
            }

        }

        public void Dispose()
        {
            userDatabaseLocator.Context.SaveChanges();
            Users.Clear();


        }

    }
}
