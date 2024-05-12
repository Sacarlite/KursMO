using CommunityToolkit.Mvvm.ComponentModel;
using OptimizationMathMethods.ExhaustiveSearch.VisualzationPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OptimizationMathMethods.ExhaustiveSearch.VievModels;

public partial class MainVisualizationPageVievModel : ObservableObject
{
    private List<List<Point>> points;
    [ObservableProperty]
    private TabelPage tablePage;
    [ObservableProperty]
    private Chart2D chart2DPage;
    [ObservableProperty]
    private Chart3D chart3DPage;

    public MainVisualizationPageVievModel(List<List<Point>> points)
    {
        this.points = points;
        Loded();
    }

    private void Loded()
    {
        TableVievModel tableVievModel = new TableVievModel(points);
        tablePage=new TabelPage(tableVievModel);
        chart2DPage = new Chart2D(points);
        chart3DPage=new Chart3D(points);
    }
}
