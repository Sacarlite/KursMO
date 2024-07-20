using MetaInfo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationMathMethods
{
    public abstract class VisualisationOptimization
    {
        protected CorrectionFactors correctionFactors { get; set; }
        protected Limitations limitations { get; set; }
        protected ExhaustiveSearchFactors exhaustiveSearchFactors { get; set; }
        private int roundCount;
        protected List<List<Point>>? points;

        protected VisualisationOptimization(CorrectionFactors correctionFactors, Limitations limitations, ExhaustiveSearchFactors exhaustiveSearchFactors)
        {
            this.correctionFactors = correctionFactors;
            this.limitations = limitations;
            this.exhaustiveSearchFactors = exhaustiveSearchFactors;
        }

        protected void GetPoints()
        {
            RoundCalc();
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

        public abstract void ConstructVisualisation(List<Visualisation> visualisation);
        
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
}
