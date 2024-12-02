using System.Data;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using MetaInfo;

namespace OptimizationMathMethods
{
    public abstract class VisualisationOptimization : ObservableObject
    {
        protected List<List<Point>>? points;
        protected readonly MetaInfo.Task task;

        protected VisualisationOptimization(MetaInfo.Task task)
        {
            points = new List<List<Point>>();
            this.task = task;
        }

        protected VisualisationOptimization() { }

        public Tuple<List<List<Point>>, DataTable> GetPoints()
        {
            var limitations = task.GetFirstLimitations();
            var eps = RoundCalc(task.GetEps());
            var dt = new DataTable();
            DataColumn column = new DataColumn("");
            column.DataType = System.Type.GetType("System.String");
            dt.Columns.Add(column);
            int counter = 0;
            double i;
            for (
                i = limitations.Item1;
                i < limitations.Item2;
                i = Math.Round(i + limitations.Item5, RoundCalc(task.GetEps()))
            )
            {
                DataColumn new_column = new DataColumn(Math.Round(i, eps).ToString());
                new_column.DataType = System.Type.GetType("System.String");
                dt.Columns.Add(new_column);
                counter++;
            }
            int j;
            for (
                double k = limitations.Item3;
                k < limitations.Item4;
                k = Math.Round(k + limitations.Item5, eps)
            )
            {
                List<Point> tmp_points = new List<Point>();
                var row = dt.NewRow();
                row[0] = Math.Round(k, eps).ToString();
                for (i = limitations.Item1, j = 1; j < counter; i = i + limitations.Item5, j++)
                {
                    var p = new Point(i, k);
                    if (task.GetSecondLimitations(p))
                    {
                        p.Cf = task.GetCalc(p);
                        row[j] = p.Cf.ToString();
                        tmp_points.Add(p);
                    }
                    else
                    {
                        row[j] = "~";
                    }
                }
                points.Add(tmp_points);
                dt.Rows.Add(row);
            }

            return new Tuple<List<List<Point>>, DataTable>(points!, dt);
        }

        private int RoundCalc(double eps)
        {
            string[] tokens = eps.ToString("G", CultureInfo.InvariantCulture).Split(".");
            return tokens.Length > 1 ? tokens[1].Length : 0;
        }
    }
}
