using System;

namespace Mechanika
{
    public struct Wektor
    {
        private double x, y;
        public Wektor(double x, double y) { this.x = x; this.y = y; }
        public Wektor(Punkt x, Punkt y) { this.x = y.X - x.X; this.y = y.Y - x.Y; }
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
        public double Length() { return Math.Sqrt(x * x + y * y); }
        public double LenSq() { return x * x + y * y; }
        public static Wektor operator +(Wektor w, Wektor v)
        {
            return new Wektor(v.X + w.X, v.Y + w.Y);
        }
        public static Wektor operator *(double x, Wektor v)
        {
            return new Wektor(v.X * x, v.Y * x);
        }
        public static Wektor operator *(Wektor v, double x)
        {
            return new Wektor(v.X * x, v.Y * x);
        }
    }

   
}
