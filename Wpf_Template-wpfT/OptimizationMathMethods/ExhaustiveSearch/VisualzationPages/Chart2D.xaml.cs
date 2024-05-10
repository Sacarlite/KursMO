using OptimizationMathMethods.ExhaustiveSearch.VievModels;
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

namespace OptimizationMathMethods.ExhaustiveSearch.VisualzationPages
{
    /// <summary>
    /// Логика взаимодействия для Chart2D.xaml
    /// </summary>
    public partial class Chart2D : Page
    {
        public Chart2D(Chart2DVievModel chart2DVievModel)
        {
            InitializeComponent();
            DataContext=chart2DVievModel;
        }
    }
}
