using System.Data;
using System.Windows.Controls;

namespace OptimizationMathMethods.VisualzationPages
{
    /// <summary>
    /// Логика взаимодействия для PointTable.xaml
    /// </summary>
    public partial class PointTable : Page
    {
        public DataTable table;

        public PointTable(DataTable table)
        {
            InitializeComponent();
            this.table = table;
        }

        private void tableGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tableGrid.ItemsSource = table.DefaultView;
        }
    }
}
