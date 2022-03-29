using System;

namespace Mechanika
{
    public class Belka
    {
        protected Punkt pocz, kon;
        public Belka(Punkt poczatek, Punkt koniec) { pocz = poczatek; kon = koniec; }
        public double Length() { return Math.Sqrt((pocz.X - kon.X) * (pocz.X - kon.X) + (pocz.Y - kon.Y) * (pocz.Y - kon.Y)); }
        public Punkt Start
        {
            get { return pocz; }
            set { pocz = value; }
        }
        public Punkt End
        {
            get { return kon; }
            set { kon = value; }
        }
        public virtual bool JestNaBelce(Punkt p)
        {
            Wektor x = new Wektor(pocz, p);
            Wektor y = new Wektor(p, kon);
            return Math.Abs(x.Length() + y.Length() - Length()) < Util.eps;
        }
        public bool JestKoncem(Punkt p)
        {
            return Punkt.AlmostEqual(p, pocz) || Punkt.AlmostEqual(p, kon);
        }
        public Punkt DrugiPunkt(Punkt p)
        {
            if (!JestKoncem(p)) throw new ArgumentException("Punkt nie jest koncem belki");
            return Punkt.AlmostEqual(p, pocz) ? kon : pocz;
        }

        public virtual Belka Clone() => new Belka(pocz, kon);
    }

    public class Pret : Belka
    {
        public Pret(Punkt poczatek, Punkt koniec) : base(poczatek,koniec) { }
        public override bool JestNaBelce(Punkt p)
        {
            return JestKoncem(p);
        }

        public override Belka Clone() => new Pret(pocz, kon);
    }
}
