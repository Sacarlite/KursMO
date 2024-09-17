using System.Globalization;
using MetaInfo;
using NLog;

namespace VievModel
{
    public static class BoxMethod
    {
        const int n = 2;
        const int N = 4;
        public static readonly ILogger Logger = LogManager.GetLogger("Application");

        public static Point GetInfo(
            CorrectionFactors correctionFactors,
            Limitations limitations,
            ExhaustiveSearchFactors exhaustiveSearchFactors
        )
        {
            List<BoxPoint> points;
            Point result;
            Logger.Info("Начало работы метода Бокса");
            int counter;
            while (true)
            {
                counter = 0;
                points = GetRandPoints(limitations);
                foreach (BoxPoint point in points)
                {
                    if (Predicate(point, limitations))
                    {
                        counter++;
                        point.Condition = true;
                        point.Cf = FunctionCalc(
                            correctionFactors,
                            exhaustiveSearchFactors,
                            point.T1,
                            point.T2
                        );
                    }
                }
                if (counter != 0)
                {
                    Logger.Info("Генерация точек закончена");
                    Point CenterPoint = new Point();
                    Logger.Info("Расчёт cдвига точек");
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
                    CenterPoint.Cf = FunctionCalc(
                        correctionFactors,
                        exhaustiveSearchFactors,
                        CenterPoint.T1,
                        CenterPoint.T2
                    );
                    Logger.Info(
                        $"Центр сдвига точек с координатами T1={CenterPoint.T1},T2={CenterPoint.T2}. Значение Cf={CenterPoint.Cf}"
                    );
                    Logger.Info($"");
                    foreach (BoxPoint point in points)
                    {
                        if (Predicate(point, limitations))
                        {
                            continue;
                        }
                        while (true)
                        {
                            Logger.Info(
                                $"  Точка с координатами T1={point.T1},T2={point.T2}. Значение Cf={point.Cf}. Не подходит"
                            );
                            point.T1 = Math.Round(0.5 * (point.T1 + CenterPoint.T1), 5);
                            point.T2 = Math.Round(0.5 * (point.T2 + CenterPoint.T2), 5);
                            Logger.Info(
                                $"      Новое значение T1={point.T1},T2={point.T2}. Значение Cf={point.Cf}"
                            );

                            if (Predicate(point, limitations))
                            {
                                Logger.Info(
                                    $"  Точка с координатами T1={point.T1},T2={point.T2}. Значение Cf={point.Cf}. Подходит"
                                );
                                point.Condition = true;
                                point.Cf = FunctionCalc(
                                    correctionFactors,
                                    exhaustiveSearchFactors,
                                    point.T1,
                                    point.T2
                                );
                                CenterPoint = new Point();
                                foreach (BoxPoint p in points)
                                {
                                    if (p.Condition)
                                    {
                                        CenterPoint.T1 += p.T1;
                                        CenterPoint.T2 += p.T2;
                                    }
                                }
                                counter++;
                                CenterPoint.T1 = Math.Round(CenterPoint.T1 / counter, 5);
                                CenterPoint.T2 = Math.Round(CenterPoint.T2 / counter, 5);
                                CenterPoint.Cf = FunctionCalc(
                                    correctionFactors,
                                    exhaustiveSearchFactors,
                                    CenterPoint.T1,
                                    CenterPoint.T2
                                );
                                break;
                            }
                        }
                    }
                    Logger.Info($"Сдвиг точек закончен");
                    Logger.Info("");
                    break;
                }
            }

            while (true)
            {
                var MinPoint = points.MinBy(p => p.Cf);
                var MaxPoint = points.MaxBy(p => p.Cf);
                Logger.Info("Расчёт координат центра");
                Point CenterPoint = new Point();

                foreach (BoxPoint point in points)
                {
                    //Могут быть 2 одинаковых точки
                    if (point.Cf == MaxPoint.Cf)
                    {
                        continue;
                    }
                    CenterPoint.T1 += point.T1;
                    CenterPoint.T2 += point.T2;
                }
                CenterPoint.T1 = Math.Round(CenterPoint.T1 / (N - 1), 5);
                CenterPoint.T2 = Math.Round(CenterPoint.T2 / (N - 1), 5);
                CenterPoint.Cf = FunctionCalc(
                    correctionFactors,
                    exhaustiveSearchFactors,
                    CenterPoint.T1,
                    CenterPoint.T2
                );
                Logger.Info(
                    $"Центр  точек с координатами T1={CenterPoint.T1},T2={CenterPoint.T2}. Значение Cf={CenterPoint.Cf}"
                );
                double B = 0.0;
                Logger.Info($"");
                B += (
                    Math.Abs(CenterPoint.T1 - MinPoint.T1) + Math.Abs(CenterPoint.T1 - MaxPoint.T1)
                );
                B += (
                    Math.Abs(CenterPoint.T2 - MinPoint.T2) + Math.Abs(CenterPoint.T2 - MaxPoint.T2)
                );
                B /= N;
                Logger.Info($"B={B}");
                if (B < exhaustiveSearchFactors.Eps)
                {
                    Logger.Info($"");
                    Logger.Info($"");
                    Logger.Info($"");
                    result = CenterPoint;
                    break;
                }
                Logger.Info("Генерация новой точки");
                BoxPoint newPoint = GetNewPoint(
                    CenterPoint,
                    limitations,
                    MaxPoint,
                    exhaustiveSearchFactors
                );
                if (!Predicate(newPoint, limitations))
                {
                    counter = 0;
                    Logger.Info("Точка не подходит");
                    while (true)
                    {
                        if (counter > 100)
                        {
                            Logger.Info($"  Говно");
                            break;
                        }
                        newPoint.T1 = Math.Round(0.5 * (newPoint.T1 + CenterPoint.T1), 5);
                        newPoint.T2 = Math.Round(0.5 * (newPoint.T2 + CenterPoint.T2), 5);
                        Logger.Info($"  Новое значение T1={newPoint.T1},T2={newPoint.T2}.");
                        if (Predicate(newPoint, limitations))
                        {
                            newPoint.Condition = true;
                            newPoint.Cf = FunctionCalc(
                                correctionFactors,
                                exhaustiveSearchFactors,
                                newPoint.T1,
                                newPoint.T2
                            );
                            Logger.Info(
                                $"Точка с координатами T1={newPoint.T1},T2={newPoint.T2}. Значение Cf={newPoint.Cf}. Подходит"
                            );
                            break;
                        }
                        counter++;
                    }
                }
                else
                {
                    newPoint.Condition = true;
                    newPoint.Cf = FunctionCalc(
                        correctionFactors,
                        exhaustiveSearchFactors,
                        newPoint.T1,
                        newPoint.T2
                    );
                    Logger.Info(
                        $"Точка с координатами T1={newPoint.T1},T2={newPoint.T2}. Значение Cf={newPoint.Cf}. Подходит"
                    );
                }
                Logger.Info($"");
                Logger.Info($"Замена наихудшей точки новой");
                while (true)
                {
                    //if (newPoint.Cf > points.MinBy(p => p.Cf).Cf)
                    //{
                    //    break;
                    //}
                    Logger.Info(
                        $"Значение наихудшей точки {points.MaxBy(p => p.Cf).Cf}, новой точки {newPoint.Cf}"
                    );
                    if (newPoint.Cf < points.MaxBy(p => p.Cf).Cf)
                    {
                        break;
                    }
                    //newPoint.T1 = (points.MaxBy(p => p.Cf).T1 + newPoint.T1) / 2;
                    //newPoint.T2 = (points.MaxBy(p => p.Cf).T2 + newPoint.T2) / 2;
                    newPoint.T1 = Math.Round((points.MinBy(p => p.Cf).T1 + newPoint.T1) / 2, 5);
                    newPoint.T2 = Math.Round((points.MinBy(p => p.Cf).T2 + newPoint.T2) / 2, 5);
                    newPoint.Cf = FunctionCalc(
                        correctionFactors,
                        exhaustiveSearchFactors,
                        newPoint.T1,
                        newPoint.T2
                    );
                    Logger.Info(
                        $"Новое значение точки с координатами T1={newPoint.T1},T2={newPoint.T2}. Значение Cf={newPoint.Cf}."
                    );
                }
                points.Sort(
                    (e1, e2) =>
                    {
                        return e1.Cf.CompareTo(e2.Cf);
                    }
                );
                Logger.Info(
                    $"Точка с координатами T1={points[3].T1},T2={points[3].T2},Cf={points[3].Cf} заменена на T1={newPoint.T1},T2={newPoint.T2},Cf={newPoint.Cf}"
                );
                points[3] = newPoint;
                Logger.Info($"   Новые координаты точек:");
                foreach (var p in points)
                {
                    Logger.Info(
                        $"       Точка с координатами T1={p.T1},T2={p.T2}. Значение Cf={p.Cf}."
                    );
                }
            }
            Logger.Info("Выход из цикла");
            return result;
        }

        private static BoxPoint GetNewPoint(
            Point CenterPoint,
            Limitations limitations,
            Point DPoint,
            ExhaustiveSearchFactors exhaustiveSearchFactors
        )
        {
            BoxPoint newPoint = new BoxPoint();
            newPoint.Condition = false;
            newPoint.T1 = Math.Round(2.3 * CenterPoint.T1 - 1.3 * DPoint.T1, 5);
            Logger.Info($"  Сгенерировано значение T1={newPoint.T1}");
            if (newPoint.T1 < limitations.MinT1)
            {
                newPoint.T1 = limitations.MinT1 + exhaustiveSearchFactors.Eps;
                Logger.Info($"      Значение меньше минимума, новое значение T1={newPoint.T1}");
            }
            else if (newPoint.T1 > limitations.MaxT1)
            {
                newPoint.T1 = limitations.MaxT1 - exhaustiveSearchFactors.Eps;
                Logger.Info($"      Значение больше максимума, новое значение T1={newPoint.T1}");
            }

            newPoint.T2 = Math.Round(2.3 * CenterPoint.T2 - 1.3 * DPoint.T2, 5);
            Logger.Info($"  Сгенерировано значение T2={newPoint.T2}");
            if (newPoint.T2 < limitations.MinT2)
            {
                newPoint.T2 = limitations.MinT2 + exhaustiveSearchFactors.Eps;
                Logger.Info($"      Значение меньше минимума, новое значение T2={newPoint.T2}");
            }
            else if (newPoint.T2 > limitations.MaxT2)
            {
                newPoint.T2 = limitations.MaxT2 + exhaustiveSearchFactors.Eps;
                Logger.Info($"      Значение больше максимума, новое значение T2={newPoint.T2}");
            }
            Logger.Info(
                $"  Итоговое значение точка с координатами T1={newPoint.T1},T2={newPoint.T2}."
            );
            return newPoint;
        }

        private static List<BoxPoint> GetRandPoints(Limitations limitations)
        {
            Logger.Info("   Выбор случайных точек");
            List<BoxPoint> points = new List<BoxPoint>();
            for (int j = 0; j < N; j++)
            {
                points.Add(
                    new BoxPoint(
                        GetRand(limitations.MinT1, limitations.MaxT1),
                        GetRand(limitations.MinT2, limitations.MaxT2),
                        false
                    )
                );
                Logger.Info(
                    $"  Точка №{j + 1}, с координатой T1={points.Last().T1},T2={points.Last().T2}"
                );
            }
            return points;
        }

        private static bool Predicate(Point p, Limitations limitations)
        {
            // return p.T2 - p.T1 >= limitations.MinDiff;
            return 2 * Math.Pow(p.T1, 2) + Math.Pow(p.T2, 2) <= 34 && 2 * p.T1 + 3 * p.T2 <= 18;
        }

        private static double GetRand(double g, double h)
        {
            Random rnd = new Random();
            var result = Math.Round(g + rnd.NextDouble() * (h - g), 5);
            return result;
        }

        private static double FunctionCalc(
            CorrectionFactors correctionFactors,
            ExhaustiveSearchFactors exhaustiveSearchFactors,
            double T1,
            double T2
        )
        {
            //return (
            //    correctionFactors.Alpfa
            //    * correctionFactors.G
            //    * (
            //        Math.Pow((T2 - correctionFactors.Betta * correctionFactors.A), 2)
            //        + Math.Pow(correctionFactors.Mu * Math.Exp(T1 + T2), correctionFactors.N)
            //        + correctionFactors.Delta * (T2 - T1)
            //    )
            //);

            //Заглушка
            return Math.Round(Math.Pow((T1 - 3), 2) + (T2 - 4), 5);
            //Заглушка
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
