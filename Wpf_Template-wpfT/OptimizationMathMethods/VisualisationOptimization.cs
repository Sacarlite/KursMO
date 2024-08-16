using CommunityToolkit.Mvvm.ComponentModel;
using MetaInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationMathMethods
{
    public abstract class VisualisationOptimization:ObservableObject
    {
        protected  CorrectionFactors correctionFactors { get; set; }
        protected Limitations limitations { get; set; }
        protected ExhaustiveSearchFactors exhaustiveSearchFactors { get; set; }
        protected List<List<Point>>? points;

        protected VisualisationOptimization(CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors)
        {
            this.correctionFactors = correctionFactors;
            this.limitations = limitations;
            this.exhaustiveSearchFactors = exhaustiveSearchFactors;
            points= new List<List<Point>>();
        }
        protected VisualisationOptimization()
        {
        }
        public List<List<Point>> GetPoints()
        {
            var eps = RoundCalc(exhaustiveSearchFactors);
            for (double i = limitations.MinT1; i < limitations.MaxT1; i = i + exhaustiveSearchFactors.Step)
            {
                List<Point> tmp_points = new List<Point>();
                for (double j = limitations.MinT2; j < limitations.MaxT2; j = j + exhaustiveSearchFactors.Step)
                {
                    var a = (Math.Pow(j - correctionFactors.Betta * correctionFactors.A, correctionFactors.N));
                    var result = Math.Round(correctionFactors.Alpfa * correctionFactors.G * (Math.Pow(j - correctionFactors.Betta * correctionFactors.A, correctionFactors.N) + correctionFactors.Mu * Math.Exp(i + j)
                        + correctionFactors.Delta * (j - i)), eps);
                    tmp_points.Add(new Point(i, j, result));
                }
                points.Add(tmp_points);
            }
            return points;
        }
        private  int RoundCalc(ExhaustiveSearchFactors exhaustiveSearchFactors)
        {
            string[] tokens = exhaustiveSearchFactors.Eps.ToString("G", CultureInfo.InvariantCulture).Split(".");
            return tokens.Length > 1 ? tokens[1].Length : 0;
        }
    }
}
