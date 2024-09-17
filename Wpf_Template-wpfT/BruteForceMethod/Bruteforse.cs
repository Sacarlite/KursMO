using System.Globalization;
using MetaInfo;

namespace BruteForceMethod
{
    public static class BruteForceMethod
    {
        public static Point GetInfo(
            CorrectionFactors correctionFactors,
            Limitations limitations,
            ExhaustiveSearchFactors exhaustiveSearchFactors
        )
        {
            try
            {
                List<List<Point>> points = new List<List<Point>>();
                var eps = RoundCalc(exhaustiveSearchFactors);
                for (
                    double i = limitations.MinT1;
                    i < limitations.MaxT1;
                    i = i + exhaustiveSearchFactors.Step
                )
                {
                    List<Point> tmp_points = new List<Point>();
                    for (
                        double j = limitations.MinT2;
                        j < limitations.MaxT2;
                        j = j + exhaustiveSearchFactors.Step
                    )
                    {
                        var result = Math.Round(
                            correctionFactors.Alpfa
                                * correctionFactors.G
                                * (
                                    Math.Pow(
                                        j - correctionFactors.Betta * correctionFactors.A,
                                        correctionFactors.N
                                    )
                                    + correctionFactors.Mu * Math.Exp(i + j)
                                    + correctionFactors.Delta * (j - i)
                                ),
                            eps
                        );
                        tmp_points.Add(new Point(i, j, result));
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

                return max;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static int RoundCalc(ExhaustiveSearchFactors exhaustiveSearchFactors)
        {
            string[] tokens = exhaustiveSearchFactors
                .Eps.ToString("G", CultureInfo.InvariantCulture)
                .Split(".");
            return tokens.Length > 1 ? tokens[1].Length : 0;
        }
    }
}
