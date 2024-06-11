using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.UserBd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VievModel.PageVievModels;

namespace VievModel.PageVievModels
{
    public partial class AdminPageVievModel :ObservableObject, IAdminPageVievModel
    {
        private IUserDatabaseLocator userDatabaseLocator;
        [ObservableProperty]
        List<User> admins;
        [ObservableProperty]
        User admin;
        public AdminPageVievModel(IUserDatabaseLocator userDatabaseLocator)
        {
            this.userDatabaseLocator = userDatabaseLocator;
        }
        [RelayCommand]
        private void DeleteAdminCommand()
        {
            userDatabaseLocator.Context.Remove(admin);
        }
        [RelayCommand]
        private void AddNewAdminCommand()
        {
            Admin = new User();
        }
        [RelayCommand]
        private void SaveAdminCommand()
        {
            if(userDatabaseLocator.Context.Users.Where(u => u.Id == Admin.Id).FirstOrDefault()==null) {
                User newAdmin = new User();
                newAdmin.Login = Admin.Login;
                newAdmin.Password = Admin.Password;
                newAdmin.Role=Domain.Roles.Roles.Admin;
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
            if (userDatabaseLocator != null) {
                Admins = userDatabaseLocator.Context.Users.Where(u=>u.Role==Domain.Roles.Roles.Admin).ToList();
                Admin = Admins.FirstOrDefault();
            }

        }

        public void Dispose()
        {
            userDatabaseLocator.Context.SaveChanges();
            Admins.Clear();


        }
    }
}
