using MetaInfo;

namespace BoxMethod
{
    public static class BoxMethod
    {
        const int n = 2;
        const int N = 4;

        public static Point GetInfo(MetaInfo.Task task)
        {
            List<BoxPoint> points;
            var limitations = task.GetFirstLimitations();
            Point result;
            int counter;
            while (true)
            {
                counter = 0;
                points = GetRandPoints(limitations);
                foreach (BoxPoint point in points)
                {
                    if (task.GetSecondLimitations(point))
                    {
                        counter++;
                        point.Condition = true;
                        point.Cf = task.GetCalc(point);
                    }
                }
                if (counter != 0)
                {
                    Point CenterPoint = new Point();
                    foreach (BoxPoint point in points)
                    {
                        if (point.Condition)
                        {
                            CenterPoint.T1 += point.T1;
                            CenterPoint.T2 += point.T2;
                        }
                    }
                    CenterPoint.T1 = Math.Round(CenterPoint.T1 / counter, 5);
                    CenterPoint.T2 = Math.Round(CenterPoint.T2 / counter, 5);
                    CenterPoint.Cf = task.GetCalc(CenterPoint);

                    foreach (BoxPoint point in points)
                    {
                        while (true)
                        {
                            if (!point.Condition)
                            {
                                point.T1 = Math.Round(0.5 * (point.T1 + CenterPoint.T1), 5);
                                point.T2 = Math.Round(0.5 * (point.T2 + CenterPoint.T2), 5);
                            }
                            if (task.GetSecondLimitations(point))
                            {
                                point.Condition = true;
                                point.Cf = task.GetCalc(point);
                                break;
                            }
                        }
                    }

                    break;
                }
            }
            while (true)
            {
                var MinPoint = points.MinBy(p => p.Cf);
                var MaxPoint = points.MaxBy(p => p.Cf);
                Point CenterPoint = new Point();
                Point GPoint = task.GetExtrType() ? MaxPoint : MinPoint;
                Point DPoint = task.GetExtrType() ? MinPoint : MaxPoint;
                counter = 0;
                foreach (BoxPoint point in points)
                {
                    //Могут быть 2 одинаковых точки
                    if (point.Cf == DPoint.Cf && counter == 0)
                    {
                        counter++;
                        continue;
                    }
                    CenterPoint.T1 += point.T1;
                    CenterPoint.T2 += point.T2;
                }
                CenterPoint.T1 = Math.Round(CenterPoint.T1 / (N - 1), 5);
                CenterPoint.T2 = Math.Round(CenterPoint.T2 / (N - 1), 5);
                CenterPoint.Cf = task.GetCalc(CenterPoint);

                double B = 0.0;

                B += (
                    Math.Abs(CenterPoint.T1 - MinPoint.T1) + Math.Abs(CenterPoint.T1 - MaxPoint.T1)
                );
                B += (
                    Math.Abs(CenterPoint.T2 - MinPoint.T2) + Math.Abs(CenterPoint.T2 - MaxPoint.T2)
                );
                B /= N;

                if (B < task.GetEps() * 0.1)
                {
                    result = CenterPoint;
                    break;
                }

                BoxPoint newPoint = GetNewPoint(CenterPoint, limitations, DPoint, task.GetEps());
                if (!task.GetSecondLimitations(newPoint))
                {
                    while (true)
                    {
                        newPoint.T1 = Math.Round(0.5 * (newPoint.T1 + CenterPoint.T1), 5);
                        newPoint.T2 = Math.Round(0.5 * (newPoint.T2 + CenterPoint.T2), 5);
                        if (task.GetSecondLimitations(newPoint))
                        {
                            newPoint.Condition = true;
                            newPoint.Cf = task.GetCalc(newPoint);
                            break;
                        }
                    }
                }
                else
                {
                    newPoint.Condition = true;
                    newPoint.Cf = task.GetCalc(newPoint);
                }
                int counterr = 0;
                while (true)
                {
                    if (task.GetExtrType())
                    {
                        if (newPoint.Cf > DPoint.Cf)
                        {
                            counterr = 0;
                            break;
                        }
                    }
                    else
                    {
                        if (newPoint.Cf < DPoint.Cf)
                        {
                            counterr = 0;
                            break;
                        }
                    }

                    newPoint.T1 = Math.Round((GPoint.T1 + newPoint.T1) / 2, 5);
                    newPoint.T2 = Math.Round((GPoint.T2 + newPoint.T2) / 2, 5);
                    newPoint.Cf = task.GetCalc(newPoint);
                    counterr++;

                    if (counterr > 1000)
                    {
                        return CenterPoint;
                    }
                }
                points.Sort(
                    (e1, e2) =>
                    {
                        return e1.Cf.CompareTo(e2.Cf);
                    }
                );
                if (task.GetExtrType())
                {
                    points[0] = newPoint;
                }
                else
                {
                    points[3] = newPoint;
                }
            }
            return result;
        }

        private static BoxPoint GetNewPoint(
            Point CenterPoint,
            Tuple<double, double, double, double, double> limitations,
            Point DPoint,
            double eps
        )
        {
            BoxPoint newPoint = new BoxPoint();
            newPoint.Condition = false;
            newPoint.T1 = Math.Round(2.3 * CenterPoint.T1 - 1.3 * DPoint.T1, 5);

            if (newPoint.T1 < limitations.Item1)
            {
                newPoint.T1 = limitations.Item1 + eps;
            }
            else if (newPoint.T1 > limitations.Item2)
            {
                newPoint.T1 = limitations.Item2 - eps;
            }

            newPoint.T2 = Math.Round(2.3 * CenterPoint.T2 - 1.3 * DPoint.T2, 5);

            if (newPoint.T2 < limitations.Item3)
            {
                newPoint.T2 = limitations.Item3 + eps;
            }
            else if (newPoint.T2 > limitations.Item4)
            {
                newPoint.T2 = limitations.Item4 - eps;
            }

            return newPoint;
        }

        private static List<BoxPoint> GetRandPoints(
            Tuple<double, double, double, double, double> task
        )
        {
            List<BoxPoint> points = new List<BoxPoint>();
            for (int j = 0; j < N; j++)
            {
                points.Add(
                    new BoxPoint(
                        GetRand(task.Item1, task.Item2),
                        GetRand(task.Item3, task.Item4),
                        false
                    )
                );
            }
            return points;
        }

        private static bool Predicate(Point p, Limitations limitations)
        {
            return p.T2 - p.T1 >= limitations.MinDiff;
        }

        private static double GetRand(double g, double h)
        {
            Random rnd = new Random();
            var result = Math.Round(g + rnd.NextDouble() * (h - g), 5);
            return result;
        }
    }
}
