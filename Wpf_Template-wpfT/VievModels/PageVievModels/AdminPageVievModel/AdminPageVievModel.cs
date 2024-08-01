using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using Domain.Factories;
using Domain.UserBd;
using Domain.UserEventArgs;
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
            
            Role admin = new Role("Aдминистратор");
            Role user = new Role("Пользователь");
            userDatabaseLocator.Context.Roles.AddRange(admin,user);
            User user1 = new User("user1", "123") {Role= user };
            User user2 = new User("user2", "123") { Role = user };
            User user3 = new User("user3", "123") { Role = user };
            User user4 = new User("user4", "123") { Role = user };
            User user5 = new User("user5", "123") { Role = user };
            userDatabaseLocator.Context.Users.AddRange(user1, user2, user3, user4, user5);
            User admin1=new User("admin1","321") { Role = admin };
            User admin2 = new User("admin2", "321") { Role = admin };
            userDatabaseLocator.Context.Users.AddRange(admin1, admin2);
            userDatabaseLocator.Context.SaveChanges();
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
