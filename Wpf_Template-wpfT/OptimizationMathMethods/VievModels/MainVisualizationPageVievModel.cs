using CommunityToolkit.Mvvm.ComponentModel;
using MetaInfo;
using OptimizationMathMethods.VisualzationPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OptimizationMathMethods.VievModels;

public partial class MainVisualizationPageVievModel : ObservableObject
{
    private List<List<Point>> points;
    [ObservableProperty]
    private List<Visualisation> visualisations;

    public MainVisualizationPageVievModel(List<List<Point>> points, List<Visualisation> visualisations)
    {
        this.points = points;
        this.visualisations = visualisations;
    }

    private void Loded()
    {
        visualisations.Add(new Visualisation(new Chart2D(points),"2D график"));
        visualisations.Add(new Visualisation(new Chart3D(points), "3D график"));
    }
}
