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
using VievModel.PageVievModels.MethodsPageVievModel;

namespace Vievs.Pages.MethodsListPage
{
    /// <summary>
    /// Логика взаимодействия для MethodsListPage.xaml
    /// </summary>
    public partial class MethodsListPage : System.Windows.Controls.Page, IMethodsListPage
    {
        public MethodsListPage(IMethodsPageVievModel methodsPageVievModel)
        {
            DataContext = methodsPageVievModel;
            InitializeComponent();
        }
    }
}
