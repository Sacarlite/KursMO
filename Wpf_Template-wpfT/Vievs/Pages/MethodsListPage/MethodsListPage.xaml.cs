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
