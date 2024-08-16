
using OptimizationMathMethods.VievModels;
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
using System.Xml;

namespace OptimizationMathMethods.VisualzationPages
{
    /// <summary>
    /// Логика взаимодействия для VisualisationPage.xaml
    /// </summary>
    public partial class VisualisationPage : Page
    {
        public VisualisationPage(MainVisualizationPageVievModel? mainVisualizationPageVievModel)
        {
            InitializeComponent();
            if(mainVisualizationPageVievModel is not  null)
            {
                DataContext = mainVisualizationPageVievModel;
            }
            DataContext= mainVisualizationPageVievModel;
        }
    }
}
