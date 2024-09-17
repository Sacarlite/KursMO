using MetaInfo;

namespace BoxMethod
{
    public class Cpoint : Point
    {
        public Cpoint(double t1, double t2, double cf, Point GPoint, Point DPoint)
            : base(t1, t2, cf)
        {
            this.GPoint = GPoint;
            this.DPoint = DPoint;
        }

        public Point GPoint { get; set; }
        public Point DPoint { get; set; }
    }
}
