using System.Globalization;
using MetaInfo;

namespace BruteForceMethod
{
    public static class BruteForceMethod
    {
        public static Point GetInfo(MetaInfo.Task task)
        {
            var limitations = task.GetFirstLimitations();
            try
            {
                List<List<Point>> points = new List<List<Point>>();
                for (
                    double i = limitations.Item1;
                    i < limitations.Item2;
                    i = Math.Round(i + limitations.Item5, RoundCalc(task.GetEps()))
                )
                {
                    List<Point> tmp_points = new List<Point>();
                    for (
                        double j = limitations.Item3;
                        j < limitations.Item4;
                        j = Math.Round(j + limitations.Item5, RoundCalc(task.GetEps()))
                    )
                    {
                        if (j == 7.8 && i == 6.9)
                        {
                            var a = 0;
                        }
                        var p = new Point(i, j);
                        if (task.GetSecondLimitations(p))
                        {
                            p.Cf = task.GetCalc(p);
                        }
                        tmp_points.Add(p);
                    }
                    points.Add(tmp_points);
                }
                Point min = points[0][0];
                Point max = points[0][0];
                foreach (var point in points)
                {
                    if (point.MinBy(p => p.Cf).Cf < min.Cf)
                    {
                        min = point.MinBy(p => p.Cf);
                    }
                    if (point.MaxBy(p => p.Cf).Cf > max.Cf)
                    {
                        max = point.MaxBy(p => p.Cf);
                    }
                }

                return task.GetExtrType() ? max : min;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int RoundCalc(double eps)
        {
            string[] tokens = eps.ToString("G", CultureInfo.InvariantCulture).Split(".");
            return tokens.Length > 1 ? tokens[1].Length : 0;
        }
    }
}
