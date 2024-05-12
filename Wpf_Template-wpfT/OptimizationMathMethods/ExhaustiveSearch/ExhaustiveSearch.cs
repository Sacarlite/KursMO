﻿using OptimizationMathMethods.ExhaustiveSearch.VievModels;
using OptimizationMathMethods.ExhaustiveSearch.VisualzationPages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationMathMethods.ExhaustiveSearch;

class ExhaustiveSearch
{
    private readonly CorrectionFactors correctionFactors;
    private readonly Limitations limitations;
    private readonly ExhaustiveSearchFactors exhaustiveSearchFactors;
    private int roundCount;
    public MainPage VisualPage { get; set; }
    public List<List<Point>>? points { get; private set; }
    public Point minNum { get; private set; }
    public Point maxNum { get; private set; }
    public ExhaustiveSearch(CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors)
    {
        this.correctionFactors = correctionFactors;
        this.limitations = limitations;
        this.exhaustiveSearchFactors = exhaustiveSearchFactors;
        RoundCalc();
        GetPoints();
        GetExtr();
        MainVisualizationPageVievModel visualizationPageVievModel = new MainVisualizationPageVievModel(points);
        VisualPage =new MainPage(visualizationPageVievModel);
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

        double max = 0;
        foreach (var elem in points)
        {
            foreach (var point in elem)
            {
                if (point.Cf > max)
                {
                    max = point.Cf;
                }
            }
        }
        double min = max;
        foreach (var elem in points)
        {
            foreach (var point in elem)
            {
                if (point.Cf < min)
                {
                    min = point.Cf;
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

