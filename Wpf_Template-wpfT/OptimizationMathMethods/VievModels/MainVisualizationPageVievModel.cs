using CommunityToolkit.Mvvm.ComponentModel;
using MetaInfo;
using OptimizationMathMethods.VisualzationPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace OptimizationMathMethods.VievModels;

public partial class MainVisualizationPageVievModel :ObservableObject,IMainVisualizationPageVievModel
{

    [ObservableProperty]
    Page chart2DPage;
    [ObservableProperty]
    Page chart3DPage;
    [ObservableProperty]
    Page pointTablePage;



    public MainVisualizationPageVievModel()
    {
        Chart2DPage = new DefaultPage();
        Chart3DPage = new DefaultPage();
        PointTablePage = new DefaultPage();
    }
  
    public void  ReloadPages(List<List<Point>> points)
    {
        Chart2DPage = new Chart2D(points);
        Chart3DPage = new Chart3D(points);
        PointTablePage = new PointTable(points);
    }
}
