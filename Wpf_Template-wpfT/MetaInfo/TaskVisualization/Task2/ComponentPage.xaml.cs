using System.Windows.Controls;

namespace MetaInfo.TaskVisualization.Task2
{
    /// <summary>
    /// Логика взаимодействия для ComponentPage.xaml
    /// </summary>
    public partial class ComponentPage : Page
    {
        public ComponentPage(Task2ViewModel task2ViewModel)
        {
            InitializeComponent();
            DataContext = task2ViewModel;
        }
    }
}
