using System;
using System.Collections.Generic;

namespace Mechanika
{
    public enum TypObciazenia : int
    {
        R=0, H=1, M=2
    }
    public abstract class Podpora
    {
        protected Belka b;
        protected Punkt pol;
        public int ObcIndeks { get; set; }
        public List<Obciazenie> obciazenia = new List<Obciazenie>();
        public Punkt Miejsce { get { return pol; } }
        public Belka Belka { get => b; }
        public Podpora(Belka b, Punkt polozenie)
        {
            this.b = b;
            if (b.JestNaBelce(polozenie)) pol = polozenie;
            else throw new ArgumentException("Podpora nie znajduje się przy belce");
        }
        public abstract Obciazenie DodajObciazenie(TypObciazenia typ, Wektor v);
        public abstract int LiczbaReakcjiPodporowych();
        public void ZmienBelke(Belka bel)
        {
            if (b != bel)
            {
                b = bel;
                foreach (Obciazenie o in obciazenia)
                    o.Belka = bel;
            }
        }

        public abstract Podpora Clone();
    }

    public class PodporaPrzegubowaPrzesuwna : Podpora 
    {

        public PodporaPrzegubowaPrzesuwna(Belka b, Punkt polozenie) : base(b,polozenie) { obciazenia.Add(new ObciazeniePunktowe(b, pol, new Wektor(0, 1)));}

        public override Obciazenie DodajObciazenie(TypObciazenia typ, Wektor v)
        {
            if (typ != TypObciazenia.R) throw new ArgumentException("Dla podpory przegubowej przesuwnej dopuszczany tylko typ obciazenia R");
            obciazenia[(int)TypObciazenia.R] = new ObciazeniePunktowe(b, pol, v);
            return obciazenia[(int)TypObciazenia.R];
        }

        public override int LiczbaReakcjiPodporowych() => 1;

        public override Podpora Clone()
        {
            Podpora p = new PodporaPrzegubowaPrzesuwna(b, pol);
            p.DodajObciazenie(TypObciazenia.R, obciazenia[(int)TypObciazenia.R].Wartosc);
            return p;
        }
    }

    public class PodporaPrzegubowaNieprzesuwna : Podpora
    {
        public PodporaPrzegubowaNieprzesuwna(Belka b, Punkt polozenie) : base(b, polozenie) 
        { 
            obciazenia.Add(new ObciazeniePunktowe(b, pol, new Wektor(0, 1))); 
            obciazenia.Add(new ObciazeniePunktowe(b, pol, new Wektor(1, 0))); 
        }

        public override Obciazenie DodajObciazenie(TypObciazenia typ, Wektor v)
        {
            if (typ == TypObciazenia.M) throw new ArgumentException("Dla podpory przegubowej nieprzesuwnej dopuszczane tylko typy obciazenia R i H");
            return obciazenia[(int)typ] = new ObciazeniePunktowe(b, pol, v);
        }

        public override int LiczbaReakcjiPodporowych() => 2;

        public override Podpora Clone()
        {
            Podpora p = new PodporaPrzegubowaNieprzesuwna(b, pol);
            p.DodajObciazenie(TypObciazenia.R, obciazenia[(int)TypObciazenia.R].Wartosc);
            p.DodajObciazenie(TypObciazenia.H, obciazenia[(int)TypObciazenia.H].Wartosc);
            return p;
        }
    }

    public class PodporaUtwierdzenie : Podpora
    {
        public PodporaUtwierdzenie(Belka b, Punkt polozenie) : base(b, polozenie) 
        {
            obciazenia.Add(new ObciazeniePunktowe(b, pol, new Wektor(0, 1)));
            obciazenia.Add(new ObciazeniePunktowe(b, pol, new Wektor(1, 0)));
            obciazenia.Add(new ObciazenieMomentSkupiony(b, pol, 1.0)); 
        }

        public override Obciazenie DodajObciazenie(TypObciazenia typ, Wektor v)
        {
            if (typ == TypObciazenia.M) return obciazenia[(int)typ] = new ObciazenieMomentSkupiony(b, pol, v.X);
            else return obciazenia[(int)typ] = new ObciazeniePunktowe(b, pol, v);
        }

        public override int LiczbaReakcjiPodporowych() => 3;

        public override Podpora Clone()
        {
            Podpora p = new PodporaUtwierdzenie(b, pol);
            p.DodajObciazenie(TypObciazenia.R, obciazenia[(int)TypObciazenia.R].Wartosc);
            p.DodajObciazenie(TypObciazenia.H, obciazenia[(int)TypObciazenia.H].Wartosc);
            p.DodajObciazenie(TypObciazenia.M, obciazenia[(int)TypObciazenia.M].Wartosc);
            return p;
        }
    }
    
}
