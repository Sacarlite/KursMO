using OptimizationMathMethods.ExhaustiveSearch.VievModels;
using OptimizationMathMethods.ExhaustiveSearch.VisualzationPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OptimizationMathMethods.ExhaustiveSearch;

class ExhaustiveSearch
{
    public CorrectionFactors correctionFactors { get; set; }
    public Limitations limitations { get; set; }
    public ExhaustiveSearchFactors exhaustiveSearchFactors { get; set; }
    private int roundCount;
    public MainPage VisualPage { get; set; }
    public List<List<Point>>? points { get; private set; }
    public Point minNum { get; private set; }
    public Point maxNum { get; private set; }
    public ExhaustiveSearch()
    {
    
        RoundCalc();
        GetPoints();
        GetExtr();
        DataValidation();
        MainVisualizationPageVievModel visualizationPageVievModel = new MainVisualizationPageVievModel(points);
        VisualPage =new MainPage(visualizationPageVievModel);
    }

    private void DataValidation()
    {

    }

    private void GetPoints()
    {
        points = new List<List<Point>>();
        for (double i = limitations.MinT1; i < limitations.MaxT1; i = i + exhaustiveSearchFactors.Step)
        {
            List<Point> tmp_points = new List<Point>();
            for (double j = limitations.MinT2; j < limitations.MaxT2; j = j + exhaustiveSearchFactors.Step)
            {
                if (j - i >= 1)
                {
                    tmp_points.Add(new Point(i, j, FunctionCalc(i, j)));
                }
            }
            points.Add(tmp_points);
        }
    }
    private void GetExtr()
    {

         maxNum = new Point(0,0,0);
        foreach (var elem in points)
        {
            foreach (var point in elem)
            {
                if (point.Cf > maxNum.Cf)
                {
                    maxNum = point;
                }
            }
        }
         minNum = maxNum;
        foreach (var elem in points)
        {
            foreach (var point in elem)
            {
                if (point.Cf < minNum.Cf)
                {
                    minNum = point;
                }
            }
        }
    }
    private double FunctionCalc(double T1, double T2)
    {

        return Math.Round(correctionFactors.Alpfa * correctionFactors.G * (Math.Pow(T2 - correctionFactors.Betta * correctionFactors.A, correctionFactors.N) + correctionFactors.Mu * Math.Exp(T1 + T2) + correctionFactors.Delta * (T2 - T1)), roundCount);
    }
    private void RoundCalc()
    {
        string[] tokens = exhaustiveSearchFactors.Eps.ToString("G", CultureInfo.InvariantCulture).Split(".");
        roundCount = tokens.Length > 1 ? tokens[1].Length : 0;
    }
}

