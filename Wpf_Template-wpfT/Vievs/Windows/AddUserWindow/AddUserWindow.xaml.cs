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
using System.Windows.Shapes;
using VievModel.VievModels.AddUserVievModel;
using Vievs.Windows.AboutWindow;

namespace Vievs.Windows.AddUserWindow
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : System.Windows.Window, IAddUserWindow
    {
        public AddUserWindow(IAddUserVievModel addUserVievModel)
        {
            InitializeComponent();
            DataContext=addUserVievModel;
        }
    }
}
