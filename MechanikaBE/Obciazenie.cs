using System;

namespace Mechanika
{
    public enum Os
    {
        X,Y
    }

    public enum KierunekLiczenia
    {
        Brak, DoStartu, DoKonca = -1
    }
    public abstract class Obciazenie
    {
        protected Punkt pol;
        protected Wektor wart;
        protected Belka b;
        
        public Obciazenie(Belka b, Punkt polozenie)
        {
            this.b = b;
            if (b.JestNaBelce(polozenie)) pol = polozenie;
            else throw new ArgumentException("Obciazenie nie znajduje sie na belce");
        }
        public Wektor Wartosc { get => wart; set => wart = value; }
        public Belka Belka { get => b; set => b = value; }
        public Punkt Miejsce { get => pol; }
        public abstract double Moment(Punkt p, KierunekLiczenia kier = KierunekLiczenia.Brak); // 0 - nie, 1 - do startu, -1 - do endu
        public abstract Obciazenie Clone();
        public abstract Obciazenie ZamienioneXY();
        public abstract double Sila(Os os);
    }

    public class ObciazenieCiagle : Obciazenie
    {
        protected Punkt pol_kon;
        public Punkt End { get => pol_kon; set => pol_kon = value; }
        public ObciazenieCiagle(Belka b, Punkt poczatekObc, Punkt koniecObc, Wektor wartosc) : base(b,poczatekObc)
        {
            wart = wartosc;
            if (b.JestNaBelce(koniecObc)) pol_kon = koniecObc;
            else throw new ArgumentException("Obciazenie nie znajduje sie w calosci na belce");
            if ((new Wektor(b.Start, poczatekObc)).Length() > (new Wektor(b.Start, koniecObc)).Length())
                OdwrocObc();
        }
        public void OdwrocObc()
        {
            Punkt p = pol;
            pol = pol_kon;
            pol_kon = p;
        }
        public override double Moment(Punkt p, KierunekLiczenia kier = KierunekLiczenia.Brak)
        {
            double dl = (new Wektor(pol, pol_kon)).Length();

            Punkt srodek = (pol + pol_kon) / 2;//new Punkt((pol.X + pol_kon.X) / 2, (pol.Y + pol_kon.Y) / 2);
            Wektor r = new Wektor(p, srodek);
            if (kier != KierunekLiczenia.Brak && r.Length() <= dl/2)
            {
                srodek.X = ((kier == KierunekLiczenia.DoStartu ? pol.X : pol_kon.X) + p.X) / 2;
                srodek.Y = ((kier == KierunekLiczenia.DoStartu ? pol.Y : pol_kon.Y) + p.Y) / 2;
                r = new Wektor(p, srodek);
                dl = (new Wektor(kier == KierunekLiczenia.DoStartu ? pol : pol_kon, p)).Length();
            }
            Wektor F = wart * dl;// new Wektor(wart.X * dl, wart.Y * dl);
            return r.X * F.Y - r.Y * F.X;
        }

        public override double Sila(Os os)
        {
            double dl = (new Wektor(pol, pol_kon)).Length();
            return os == Os.X ? wart.X * dl : wart.Y * dl;
        }

        public override Obciazenie Clone()
        {
            return new ObciazenieCiagle(b, pol, pol_kon, wart);
        }

        public override Obciazenie ZamienioneXY()
        {
            return new ObciazenieCiagle(Belka, Miejsce, End, new Wektor(-Wartosc.Y, Wartosc.X));
        }

        public virtual ObciazenieCiagle NoweParametry(Belka bel, Punkt pocz, Punkt kon)
        {
            return new ObciazenieCiagle(bel, pocz, kon, wart);
        }
    }

    public class ObciazeniePunktowe : Obciazenie
    {
        public ObciazeniePunktowe(Belka b, Punkt polozenie, Wektor wartosc) : base(b,polozenie)
        {
            wart = wartosc;
        }

        public override Obciazenie Clone()
        {
            return new ObciazeniePunktowe(b, pol, wart);
        }

        public override double Moment(Punkt p, KierunekLiczenia kier = KierunekLiczenia.Brak)
        {
            Wektor r = new Wektor(p, pol);
            return r.X * wart.Y - r.Y * wart.X;
        }

        public override double Sila(Os os)
        {
            return os == Os.X ? wart.X : wart.Y;
        }

        public override Obciazenie ZamienioneXY()
        {
            return new ObciazeniePunktowe(Belka, Miejsce, new Wektor(-Wartosc.Y, Wartosc.X));
        }
    }

    public class ObciazeniePomocnicze : Obciazenie
    {
        public ObciazeniePomocnicze(Belka b, Punkt polozenie, Wektor wartoscPomocnicza) : base(b,polozenie)
        {
            wart = wartoscPomocnicza;
        }

        public override Obciazenie Clone()
        {
            return new ObciazeniePomocnicze(b, pol, wart);
        }

        public override double Moment(Punkt p, KierunekLiczenia kier = KierunekLiczenia.Brak)
        {
            return 0.0;
        }

        public override double Sila(Os os)
        {
            return 0.0;
        }

        public override Obciazenie ZamienioneXY()
        {
            return new ObciazeniePomocnicze(Belka, Miejsce, new Wektor(-Wartosc.Y, Wartosc.X));
        }
    }
    public class ObciazenieMomentSkupiony : Obciazenie
    {
        public ObciazenieMomentSkupiony(Belka b, Punkt polozenie, double wartosc) : base(b,polozenie)
        {
            wart.X = wartosc;
            wart.Y = 0;
        }

        public override Obciazenie Clone()
        {
            return new ObciazenieMomentSkupiony(b, pol, wart.X);
        }

        public override double Moment(Punkt p, KierunekLiczenia kier = KierunekLiczenia.Brak)
        {
            return wart.X;
        }

        public override double Sila(Os os)
        {
            return 0.0;
        }

        public override Obciazenie ZamienioneXY()
        {
            return Clone();
        }
    }

    public class ObciazenieMomentCiagly : ObciazenieCiagle
    {
        public ObciazenieMomentCiagly(Belka b, Punkt poczatekObc, Punkt koniecObc, double wart) : base(b,poczatekObc,koniecObc,new Wektor(wart,0)) { }
        public override Obciazenie Clone()
        {
            return new ObciazenieMomentCiagly(b, pol, pol_kon, wart.X);
        }

        public override double Moment(Punkt p, KierunekLiczenia kier = KierunekLiczenia.Brak)
        {
            double dl = (new Wektor(pol, pol_kon)).Length();

            Punkt srodek = (pol + pol_kon) / 2;//new Punkt((pol.X + pol_kon.X) / 2, (pol.Y + pol_kon.Y) / 2);
            Wektor r = new Wektor(p, srodek);

            if (kier != KierunekLiczenia.Brak && r.Length() <= dl / 2)
                return new Wektor(kier == KierunekLiczenia.DoStartu ? pol : pol_kon, p).Length() * wart.X;
            return dl * wart.X;
        }

        public override double Sila(Os os)
        {
            return 0.0;
        }

        public override Obciazenie ZamienioneXY()
        {
            return Clone();
        }

        public override ObciazenieCiagle NoweParametry(Belka bel, Punkt pocz, Punkt kon)
        {
            return new ObciazenieMomentCiagly(bel, pocz, kon, wart.X);
        }
    }
}
