using ChartDirector;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationMathMethods.ExhaustiveSearch.VievModels;

public partial class Chart3DVievModel : ObservableObject
{
    private readonly List<List<Point>> points;
    [ObservableProperty]
    private SurfaceChart _chart;
    public Chart3DVievModel(List<List<Point>> points)
    {
        this.points = points;
        Draw();

    }
    private void Draw()
    {
        _chart = new SurfaceChart(720, 600);
        _chart.setPlotRegion(330, 290, 360, 360, 270);
        List<double> Xs = new List<double>();
        List<double> Ys = new List<double>();
        List<double> Zs = new List<double>();
        bool flag = true;
       for (int i = 0; i < points.Count; i++)
        {
            Xs.Add(points[i][0].T1);
            for (int j = 0; j < points[i].Count; j++)
            {
                if (flag)
                {
                    Ys.Add(points[i][j].T2);
                }
                Zs.Add(points[i][j].Cf);
            }
            flag = false;
        }
        _chart.setData(Xs.ToArray(), Ys.ToArray(), Zs.ToArray());
        // Set the view angles
        _chart.setViewAngle(30, 45);


        // Add a color axis (the legend) in which the left center is anchored at (660, 270). Set
        // the length to 200 pixels and the labels on the right side.
        _chart.setColorAxis(650, 270, ChartDirector.Chart.Left, 200, ChartDirector.Chart.Right);

        // Set the x, y and z axis titles using 10 points Arial Bold font
        _chart.xAxis().setTitle("T1", "Arial Bold", 15);
        _chart.yAxis().setTitle("T2", "Arial Bold", 15);

        // Set axis label font
        _chart.xAxis().setLabelStyle("Arial", 10);
        _chart.yAxis().setLabelStyle("Arial", 10);
        _chart.zAxis().setLabelStyle("Arial", 10);
        _chart.colorAxis().setLabelStyle("Arial", 10);

    }
}
