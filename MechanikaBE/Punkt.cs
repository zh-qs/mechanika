using System;

namespace Mechanika
{    
    public struct Punkt
    {
        private double x, y;
        public Punkt(double x, double y) { this.x = x; this.y = y; }
        public double X 
        { 
            get { return x; }
            set { x = value; }    
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public static bool operator ==(Punkt x, Punkt y) => x.X == y.X && x.Y == y.Y;
        public static bool operator !=(Punkt x, Punkt y) => !(x == y);
        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Punkt) && (Punkt)obj == this;
        }
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return x.ToString() + " " + y.ToString();
        }
        public static Punkt operator +(Punkt p1, Punkt p2) => new Punkt(p1.X + p2.X, p1.Y + p2.Y);
        public static Punkt operator *(double x, Punkt p) => new Punkt(x * p.X, x * p.Y);
        public static Punkt operator *(Punkt p, double x) => new Punkt(x * p.X, x * p.Y);
        public static Punkt operator /(Punkt p, double x) => new Punkt(p.X / x, p.Y / x);
        public static bool AlmostEqual(Punkt x, Punkt y) => (new Wektor(x, y)).Length() < Util.eps;
    }
}
