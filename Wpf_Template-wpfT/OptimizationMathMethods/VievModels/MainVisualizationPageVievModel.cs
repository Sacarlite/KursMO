using System.Data;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using MetaInfo;
using OptimizationMathMethods.VisualzationPages;

namespace OptimizationMathMethods.VievModels;

public partial class MainVisualizationPageVievModel
    : ObservableObject,
        IMainVisualizationPageVievModel
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

    public void ReloadPages(Tuple<List<List<Point>>, DataTable> tuple)
    {
        Chart2DPage = new Chart2D(tuple.Item1);
        Chart3DPage = new Chart3D(tuple.Item1);
        PointTablePage = new PointTable(tuple.Item2);
    }
}
