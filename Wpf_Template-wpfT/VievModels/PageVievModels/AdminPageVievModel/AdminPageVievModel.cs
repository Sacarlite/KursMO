using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using Domain.Factories;
using Domain.UserBd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VievModel.PageVievModels;
using VievModel.VievModels.AddUserVievModel;
using VievModel.VievModels.MainWindow.ControlsVievModel;
using VievModels.Windows;
using Vievs.Windows;

namespace VievModel.PageVievModels
{
    public partial class AdminPageVievModel : ObservableObject, IAdminPageVievModel
    {
        private IUserDatabaseLocator userDatabaseLocator;
        private IWindowManager windowManager;
        private IWindow addUserWindow;
        public IAddUserVievModel addUserVievModel{get;}
        [ObservableProperty]
        ObservableCollection<User> consideredUsers;
        [ObservableProperty]
        private bool? role;
        partial void OnRoleChanging(bool? value)
        {
             if (value==true)
                {
                ConsideredUsers= users.Where(u=>u.Role.Name=="Aдминистратор").ToObservableCollection();
                }
                else
                {
                ConsideredUsers = users.Where(u => u.Role.Name == "Пользователь").ToObservableCollection();
            }
        }
       
        private ObservableCollection<User> users;
        public AdminPageVievModel(IUserDatabaseLocator userDatabaseLocator, IWindowManager windowManager, IWindowVievModelsFactory<IAddUserVievModel> addUserWindowVievModelFactory)
        {
            this.userDatabaseLocator = userDatabaseLocator;
            this.windowManager = windowManager;
            addUserVievModel = addUserWindowVievModelFactory.Create();
            addUserVievModel.AddNewUser += AddNewUser;
            addUserVievModel.WindowClosingAct += WindowClosingAct;
        }

        private void WindowClosingAct()
        {
            windowManager.Close(addUserVievModel);
        }

        private void AddNewUser(User? user)
        {
           if(user != null)
            {
                if (Role == true&& user.Role.Name=="Администратор")
                {
                    ConsideredUsers.Add(user);
                }
                else if(Role == false && user.Role.Name == "Пользователь")
                {
                    ConsideredUsers.Add(user);
                }
                users.Add(user);

            } 
        }
        

        [RelayCommand]
        private void PageLoading()
        {
            users= userDatabaseLocator.Context.Users.ToObservableCollection();
            ConsideredUsers=users.Where(u => u.Role.Name == "Пользователь").ToObservableCollection();
        }
        [RelayCommand]
        void AddUser()
        {
            addUserWindow = windowManager.Show(addUserVievModel);
        }
        [RelayCommand]
        void DeleteUser(int Id)
        {
            var user =ConsideredUsers.Where(u=>u.Id==Id).FirstOrDefault();
            if (user != null) {
                ConsideredUsers.Remove(user);
                users.Remove(user);
                userDatabaseLocator.Context.Users.Remove(user);
                userDatabaseLocator.Context.SaveChanges();
            }
            MessageBox.Show("Пользователь успешно удалён");
        }
        public void Dispose()
        {
            
        }
    }
}
