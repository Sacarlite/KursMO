namespace OptimizationMathMethods;

public class Point
{
    public Point(double T1, double T2,double Cf)
    {
        this.T1 = T1;
        this.T2 = T2;
        this.Cf = Cf;
    }
    public double T1 { get; set; }
    public double T2 { get; set; }
    public double Cf { get; set; }
}
class CorrectionFactors
{
    public CorrectionFactors(double alpfa,double betta,double mu,double delta,double g,double a,double n)
    {
        Alpfa = alpfa;
        Betta = betta;
        Mu = mu;
        Delta = delta;
        G = g;
        A = a;
        N = n;
    }

    public double Alpfa { get; }
    public double Betta { get; }
    public double Mu { get; }
    public double Delta { get; }
    public double G { get; }
    public double A { get; }
    public double N { get; }
}

class Limitations
{
    public Limitations(int minT1,int maxT1, int minT2, int maxT2, int minSum)
    {
        MinT1 = minT1;
        MaxT1 = maxT1;
        MinT2 = minT2;
        MaxT2 = maxT2;
        MinSum = minSum;
    }

    public int MinT1 { get; }
    public int MaxT1 { get; }
    public int MinT2 { get; }
    public int MaxT2 { get; }
    public int MinSum { get; }
}
