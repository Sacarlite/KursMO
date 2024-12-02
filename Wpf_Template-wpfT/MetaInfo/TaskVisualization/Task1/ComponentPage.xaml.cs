using System.Windows.Controls;

namespace MetaInfo.TaskVisualization.Task1
{
    /// <summary>
    /// Логика взаимодействия для ComponentPage.xaml
    /// </summary>
    public partial class ComponentPage : Page
    {
        public ComponentPage(Task1ViewModel task1ViewModel)
        {
            InitializeComponent();
            DataContext = task1ViewModel;
        }
    }
}
