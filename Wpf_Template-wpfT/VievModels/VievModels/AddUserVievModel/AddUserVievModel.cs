using System.Windows;
using Bootstrapper.UserBd;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Settings;
using Domain.UserBd;
using VievModel.Windows;

namespace VievModel.VievModels.AddUserVievModel
{
    public partial class AddUserVievModel
        : WindowVievModel<IAddUserWindowMementoWrapper>,
            IAddUserVievModel
    {
        private readonly IAddUserWindowMementoWrapper addUserWindowMementoWrapper;
        private IUserDatabaseLocator userDatabaseLocator;

        [ObservableProperty]
        List<Role> roles;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        Role? selectedRole;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        private string login = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddUserCommand))]
        private string password = "";

        public event Action<User?> AddNewUser;
        public event Action WindowClosingAct;

        public AddUserVievModel(
            IAddUserWindowMementoWrapper addUserWindowMementoWrapper,
            IUserDatabaseLocator userDatabaseLocator
        )
            : base(addUserWindowMementoWrapper)
        {
            this.addUserWindowMementoWrapper = addUserWindowMementoWrapper;
            this.userDatabaseLocator = userDatabaseLocator;
            Roles = userDatabaseLocator.Context.Roles.ToList();
        }

        private bool CanOk()
        {
            if (SelectedRole is null)
            {
                return false;
            }
            return SelectedRole.Name != ""
                && !string.IsNullOrEmpty(Login)
                && !string.IsNullOrEmpty(Password);
        }

        [RelayCommand(CanExecute = nameof(CanOk))]
        void AddUser(Window window)
        {
            var user = new User(Login, Password);
            user.Role = SelectedRole!;
            userDatabaseLocator.Context.Users.Add(user);
            userDatabaseLocator.Context.SaveChanges();
            Login = "";
            Password = "";
            SelectedRole = null;
            AddNewUser?.Invoke(user);
            WindowClosingAct?.Invoke();
        }

        public void Dispose()
        {
            WindowClosingAct?.Invoke();
        }
    }
}
