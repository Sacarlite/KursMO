using System.Data;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using MetaInfo;

namespace OptimizationMathMethods
{
    public abstract class VisualisationOptimization : ObservableObject
    {
        protected CorrectionFactors correctionFactors { get; set; }
        protected Limitations limitations { get; set; }
        protected ExhaustiveSearchFactors exhaustiveSearchFactors { get; set; }
        protected List<List<Point>>? points;

        protected VisualisationOptimization(
            CorrectionFactors correctionFactors,
            Limitations limitations,
            ExhaustiveSearchFactors exhaustiveSearchFactors
        )
        {
            this.correctionFactors = correctionFactors;
            this.limitations = limitations;
            this.exhaustiveSearchFactors = exhaustiveSearchFactors;
            points = new List<List<Point>>();
        }

        protected VisualisationOptimization() { }

        public Tuple<List<List<Point>>, DataTable> GetPoints()
        {
            var eps = RoundCalc(exhaustiveSearchFactors);
            var dt = new DataTable();
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(column);
            int counter = 0;
            double i;
            for (i = limitations.MinT1; i < limitations.MaxT1; i = i + exhaustiveSearchFactors.Step)
            {
                DataColumn new_column = new DataColumn();
                new_column.DataType = System.Type.GetType("System.Decimal");
                dt.Columns.Add(new_column);
                counter++;
            }
            int j;
            var r = dt.NewRow();
            r[0] = "";
            for (
                i = limitations.MinT1, j = 1;
                j < counter;
                i = i + exhaustiveSearchFactors.Step, j++
            )
            {
                r[j] = i;
            }
            dt.Rows.Add(r);
            for (
                double k = limitations.MinT2;
                k < limitations.MaxT2;
                k = k + exhaustiveSearchFactors.Step
            )
            {
                List<Point> tmp_points = new List<Point>();
                var row = dt.NewRow();
                row[0] = k.ToString();
                for (
                    i = limitations.MinT1, j = 1;
                    j < counter;
                    i = i + exhaustiveSearchFactors.Step, j++
                )
                {
                    var a = (
                        Math.Pow(
                            k - correctionFactors.Betta * correctionFactors.A,
                            correctionFactors.N
                        )
                    );
                    var result = Math.Round(
                        correctionFactors.Alpfa
                            * correctionFactors.G
                            * (
                                Math.Pow(
                                    k - correctionFactors.Betta * correctionFactors.A,
                                    correctionFactors.N
                                )
                                + correctionFactors.Mu * Math.Exp(k + j)
                                + correctionFactors.Delta * (k - i)
                            ),
                        eps
                    );
                    row[j] = result;
                }
                points.Add(tmp_points);
                dt.Rows.Add(row);
            }

            return new Tuple<List<List<Point>>, DataTable>(points!, dt);
        }

        private int RoundCalc(ExhaustiveSearchFactors exhaustiveSearchFactors)
        {
            string[] tokens = exhaustiveSearchFactors
                .Eps.ToString("G", CultureInfo.InvariantCulture)
                .Split(".");
            return tokens.Length > 1 ? tokens[1].Length : 0;
        }
    }
}
