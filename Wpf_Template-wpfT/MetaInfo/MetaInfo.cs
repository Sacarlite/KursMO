namespace MetaInfo
{
    public class Point
    {
        public Point() { }

        public Point(double T1, double T2)
        {
            this.T1 = T1;
            this.T2 = T2;
        }

        public Point(double T1, double T2, double Cf)
        {
            this.T1 = T1;
            this.T2 = T2;
            this.Cf = Cf;
        }

        public double T1 { get; set; }
        public double T2 { get; set; }
        public double Cf { get; set; }
    }

    public class CorrectionFactors
    {
        public CorrectionFactors(
            double alpfa,
            double betta,
            double mu,
            double delta,
            double g,
            double a,
            double n
        )
        {
            Alpfa = alpfa;
            Betta = betta;
            Mu = mu;
            Delta = delta;
            G = g;
            A = a;
            N = n;
        }

        public CorrectionFactors() { }

        public double Alpfa { get; set; }
        public double Betta { get; set; }
        public double Mu { get; set; }
        public double Delta { get; set; }
        public double G { get; set; }
        public double A { get; set; }
        public double N { get; set; }
    }

    public class Limitations
    {
        public Limitations(double minT1, double maxT1, double minT2, double maxT2, double minDiff)
        {
            MinT1 = minT1;
            MaxT1 = maxT1;
            MinT2 = minT2;
            MaxT2 = maxT2;
            MinDiff = minDiff;
        }

        public Limitations() { }

        public double MinT1 { get; set; }
        public double MaxT1 { get; set; }
        public double MinT2 { get; set; }
        public double MaxT2 { get; set; }
        public double MinDiff { get; set; }
    }

    public class ExhaustiveSearchFactors
    {
        public ExhaustiveSearchFactors(double eps, double step)
        {
            Eps = eps;
            Step = step;
        }

        public ExhaustiveSearchFactors() { }

        public double Eps { get; set; }
        public double Step { get; set; }
    }
}
