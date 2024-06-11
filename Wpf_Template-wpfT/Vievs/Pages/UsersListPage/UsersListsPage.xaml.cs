using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VievModel.PageVievModels.UserPageVievModel;

namespace Vievs.Pages.UsersListPage
{
    /// <summary>
    /// Логика взаимодействия для UsersListsPage.xaml
    /// </summary>
    public partial class UsersListPage : Page, IUsersListPage
    {
        public UsersListPage(IUserPageVievModel userPageVievModel)
        {
            InitializeComponent();
            DataContext = userPageVievModel;
        }
    }
}
