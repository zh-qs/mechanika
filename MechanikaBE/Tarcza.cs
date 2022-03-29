using System;
using System.Collections.Generic;

namespace Mechanika
{
    public class Tarcza
    {
        List<Belka> belki = new List<Belka>();
        List<Obciazenie> dane = new List<Obciazenie>();
        List<ValueTuple<Obciazenie,int>> niewiadome = new List<ValueTuple<Obciazenie, int>>();
        Punkt licz1, licz2;
        public static int CalkLiczbaNiewiadomych = 0;
        public const int LiczbaRownan = 4;
        //public static List<int> IndeksySilPodporowych = new List<int>();

        public void DodajBelke(Belka b, List<Obciazenie> lo, List<Podpora> lp)
        {
            belki.Add(b);
            dane.AddRange(lo.FindAll(delegate (Obciazenie o) { return o.Belka == b; }));
            foreach (Podpora p in lp.FindAll(delegate (Podpora pod) { return pod.Belka == b; }))
            {
                for (int i=0;i<p.obciazenia.Count;++i)
                {
                    niewiadome.Add((p.obciazenia[i].Clone(), p.ObcIndeks + i));
                }
            }
        }
        public void DodajBelke(Belka b) => belki.Add(b);
        public void DodajObciazenie(Obciazenie o) => dane.Add(o);
        //public void DodajNiewiadoma(Obciazenie o) => niewiadome.Add((o,CalkLiczbaNiewiadomych++));
        public void DodajNiewiadoma(Obciazenie o, int indeks)
        {
            if (!niewiadome.Exists(delegate((Obciazenie, int) para) { return para.Item2 == indeks; }))
                niewiadome.Add((o, indeks));
        }
        public double[,] UkladRownan(int l_niewiadom)
        {
            licz1 = belki[0].Start;
            licz2 = belki[0].End;
            double[,] ukl = new double[4,l_niewiadom + 1];
            ukl.Initialize();
            foreach ((Obciazenie obc, int indeks) in niewiadome)
            {
                ukl[0, indeks] = obc.Sila(Os.X);
                ukl[1, indeks] = obc.Sila(Os.Y);
                ukl[2, indeks] = obc.Moment(licz1);
                ukl[3, indeks] = obc.Moment(licz2);
            }
            foreach (Obciazenie obc in dane)
            {
                ukl[0, l_niewiadom] += obc.Sila(Os.X);
                ukl[1, l_niewiadom] += obc.Sila(Os.Y);
                ukl[2, l_niewiadom] += obc.Moment(licz1);
                ukl[3, l_niewiadom] += obc.Moment(licz2);
            }
            return ukl;
        }

        public void UzupelnijNiewiadome(double[] rozw)
        {
            foreach ((Obciazenie obc, int indeks) in niewiadome)
                obc.Wartosc *= (-rozw[indeks]);// new Wektor(-obc.Wartosc.X * rozw[indeks], -obc.Wartosc.Y * rozw[indeks]);
        }

        public List<ChartElement> GetChartElements(bool N = false)
        {
            List<ChartElement> chartElements = new List<ChartElement>();
            List<Obciazenie> lobc = new List<Obciazenie>(dane);
            foreach ((Obciazenie obc, _) in niewiadome)
                lobc.Add(obc);
            if (N) lobc = Util.ZamienXYObciazenia(lobc);
            foreach (Belka b in belki)
                chartElements.Add(new ChartElement(b, lobc, belki));
            return chartElements;
        }
    }
}
