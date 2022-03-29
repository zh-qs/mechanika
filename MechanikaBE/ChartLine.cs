using System;
using System.Drawing;

namespace Mechanika
{
    public class ChartLine
    {
        public const double defaultCoef = 20;
        const double txtOff = 5;
        public static double y0 = 100, x0 = 30;
        double y0internal;
        public static double offcoef = 5, coef = defaultCoef;
        public Punkt pocz, kon;
        public double wart_pocz, wart_kon, wart_sr;
        public double wart_pocz_d = 0, wart_kon_d = 0, wart_sr_d = 0;

        public double xekstr = -1, Mekstr = 0;

        public ChartLine(Punkt pocz, Punkt kon, double wp, double sr, double wk)
        {
            this.pocz = pocz;
            this.kon = kon;
            wart_pocz = wp;
            wart_kon = wk;
            wart_sr = sr;
        }

        public void Draw(Graphics g, bool differentiated, bool N, bool pret = false)
        {
            double wart_pocz_rys = wart_pocz_d, wart_sr_rys = wart_sr_d, wart_kon_rys = wart_kon_d;
            if (N) y0internal = y0;
            else if (differentiated) y0internal = y0 + 10 * coef;
            else
            {
                y0internal = y0 + 20 * coef;
                wart_pocz_rys = wart_pocz; wart_sr_rys = wart_sr; wart_kon_rys = wart_kon;
            }
            //if (N) x0 = 430;
            //else x0 = 30;
            Wektor w = new Wektor(pocz, kon);
            if (w.Length() == 0) return;
            if (pret)
            {
                if (!N) return;
                g.DrawLine(Util.MediumPen, PointOnPlane(pocz), PointOnPlane(kon));
                if (Math.Abs(wart_pocz_rys) >= Util.eps) g.DrawString(Math.Round(differentiated ? wart_pocz_rys : -wart_pocz_rys, 3, MidpointRounding.ToEven).ToString(), Util.SmallFont, Brushes.Black, PointOnPlane((pocz + kon) / 2));
                return;
            }
            // odtąd dla normalnych belek
            g.DrawLine(Util.MediumPen, PointOnPlane(pocz), PointOnPlane(kon));
            g.DrawLine(Util.LightPen, PointOnPlane(pocz), PointOnPlane(pocz, w.Y * wart_pocz_rys / w.Length(), -w.X * wart_pocz_rys / w.Length()));

            Point p1 = PointOnPlane(pocz, w.Y * wart_pocz_rys / w.Length(), -w.X * wart_pocz_rys / w.Length());
            Point p2 = PointOnPlane((pocz + kon) / 2, w.Y * wart_sr_rys / w.Length(), -w.X * wart_sr_rys / w.Length());
            Point p3 = PointOnPlane(kon, w.Y * wart_kon_rys / w.Length(), -w.X * wart_kon_rys / w.Length());

            g.DrawCurve(Util.LightPen, new Point[] { p1, p2, p3 });
            g.DrawLine(Util.LightPen, PointOnPlane(kon), PointOnPlane(kon, w.Y * wart_kon_rys / w.Length(), -w.X * wart_kon_rys / w.Length()));

            if (differentiated && !N && xekstr > 0)
            {
                double q = xekstr / w.Length();
                Punkt p = new Punkt(pocz.X * (1 - q) + kon.X * q, pocz.Y * (1 - q) + kon.Y * q);
                y0internal = y0 + 20 * coef;
                double mTxtOff = Mekstr > 0 ? -txtOff / offcoef : txtOff / offcoef;
                g.DrawString("x = " + Math.Round(xekstr, 3, MidpointRounding.ToEven).ToString() + ", Meks = " + Math.Round(-Mekstr, 3, MidpointRounding.ToEven).ToString(), Util.VerySmallFont, Brushes.Black, PointOnPlane(p, w.Y * (Mekstr / (w.Length()) + mTxtOff), -w.X * (Mekstr / (w.Length()) + mTxtOff)));
                y0internal = y0 + 10 * coef;
            }
            if (Math.Abs(wart_pocz_rys) >= Util.eps) g.DrawString(Math.Round(differentiated ? wart_pocz_rys : -wart_pocz_rys, 3, MidpointRounding.ToEven).ToString(), Util.SmallFont, Brushes.Black, PointOnPlane(pocz, w.Y * wart_pocz_rys / (w.Length()), -w.X * wart_pocz_rys / (w.Length())));
            if (Math.Abs(wart_kon_rys) >= Util.eps) g.DrawString(Math.Round(differentiated ? wart_kon_rys : -wart_kon_rys, 3, MidpointRounding.ToEven).ToString(), Util.SmallFont, Brushes.Black, PointOnPlane(kon, w.Y * wart_kon_rys / (w.Length()), -w.X * wart_kon_rys / (w.Length())));
        }

        Point PointOnPlane(Punkt p, double xOff = 0, double yOff = 0)
        {
            return new Point((int)(p.X * coef + x0 + xOff * offcoef), (int)(p.Y * coef + y0internal + yOff * offcoef));
        }

        //public ChartLine Split(double off)
        //{
        //    double L = (new Wektor(pocz, kon)).Length();
        //    if (L < off) return null;

        //    double off0 = 2 * off / L;
        //    double w = ((wart_kon + wart_pocz) / 2 - wart_sr) * off0 * off0 + (2 * wart_sr - (wart_kon + 3 * wart_pocz) / 2) * off0 + wart_pocz;
        //    double wsr1 = ((wart_kon + wart_pocz) / 8 - wart_sr / 4) * off0 * off0 + (wart_sr - (wart_kon + 3 * wart_pocz) / 4) * off0 + wart_pocz;
        //    double wsr2 = ((wart_kon + wart_pocz) / 8 - wart_sr / 4) * off0 * off0 + (wart_kon - wart_pocz) / 4 * off0 + wart_sr;

        //    Punkt p = new Punkt(pocz.X + (kon.X - pocz.X) * off0 / 2, pocz.Y + (kon.Y - pocz.Y) * off0 / 2);
        //    ChartLine c = new ChartLine(p, kon, w, wart_kon, wsr2);
        //    wart_kon = w;
        //    wart_sr = wsr1;
        //    kon = p;
        //    return c;
        //}
    }
}
