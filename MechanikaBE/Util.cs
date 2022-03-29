using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mechanika
{
    public static class Util
    {
        public static Font BigFont { get; } = new Font(familyName: "Arial", 15.0f);
        public static Font MediumFont { get; } = new Font(familyName: "Arial", 12.0f);
        public static Font SmallFont { get; } = new Font(familyName: "Arial", 10.0f);
        public static Font VerySmallFont { get; } = new Font(familyName: "Arial", 8.0f);

        public static Pen LightPen { get; } = Pens.Black;
        public static Pen MediumLightPen { get; } = new Pen(Color.FromArgb(255, 0, 0), 2);
        public static Pen MediumPen { get; } = new Pen(Color.FromArgb(255, 0, 0, 0), 3);
        public static Pen ThickPen { get; } = new Pen(Color.FromArgb(255, 0, 0, 0), 5);
        public const double eps = 0.00000001;
        public static double IloSk(Wektor w, Wektor v)
        {
            return w.X * v.X + w.Y * v.Y;
        }
        public static int Rownoleg(Wektor w, Wektor v)
        {
            if (Math.Abs(IloSk(w, v) - w.Length() * v.Length()) < eps) return 1;
            else if (Math.Abs(IloSk(w, v) + w.Length() * v.Length()) < eps) return -1;
            else return 0;
        }

        public static List<Obciazenie> ZamienXYObciazenia(List<Obciazenie> obciazenia)
        {
            List<Obciazenie> lObcPom = new List<Obciazenie>();
            foreach (Obciazenie obc in obciazenia)
            {
                //Wektor w = new Wektor(obc.Belka.Start, obc.Belka.End);
                //double sin = Math.Abs(w.X) / w.Length(), cos = Math.Abs(w.Y) / w.Length();
                lObcPom.Add(obc.ZamienioneXY());
                //if (obc.GetType().Name.Contains("Ciag"))
                //    lObcPom.Add(new ObciazenieCiagle(obc.Belka, obc.Miejsce, ((ObciazenieCiagle)obc).End, new Wektor(-obc.Wartosc.Y, obc.Wartosc.X)));//lObcPom.Add(new ObciazenieCiagle(obc.Belka, obc.Miejsce, ((ObciazenieCiagle)obc).End, new Wektor(obc.Wartosc.Y * cos - obc.Wartosc.X * sin, obc.Wartosc.Y * sin + obc.Wartosc.X * cos)));
                //else if (obc.GetType().Name.Contains("Punkt"))
                //    lObcPom.Add(new ObciazeniePunktowe(obc.Belka, obc.Miejsce, new Wektor(-obc.Wartosc.Y, obc.Wartosc.X)));//lObcPom.Add(new ObciazeniePunktowe(obc.Belka, obc.Miejsce, new Wektor(obc.Wartosc.Y * cos - obc.Wartosc.X * sin, obc.Wartosc.Y * sin + obc.Wartosc.X * cos)));
                //else
                //    lObcPom.Add(obc);
            }
            return lObcPom;
        }
    }
}
