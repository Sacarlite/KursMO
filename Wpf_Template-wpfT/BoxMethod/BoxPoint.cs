using MetaInfo;

namespace BoxMethod
{
    class BoxPoint : Point
    {
        public bool Condition { get; set; }

        public BoxPoint(double t1, double t2, bool condition)
            : base(t1, t2)
        {
            this.Condition = condition;
        }

        public BoxPoint()
            : base() { }

        public BoxPoint(double t1, double t2)
            : base(t1, t2) { }
    }
}
