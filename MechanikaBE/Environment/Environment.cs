using System;
using System.Collections.Generic;

namespace Mechanika
{
    public partial class Environment : IMemorable
    {
        public List<Belka> belki = new List<Belka>();
        public List<Obciazenie> obciazenia = new List<Obciazenie>();
        public List<Podpora> podpory = new List<Podpora>();
        public List<Przegub> przeguby = new List<Przegub>();

        List<Tarcza> tarcze = new List<Tarcza>();
        List<Belka> odwiedzone = new List<Belka>();

        int liczbaObcPodp = 0;

        public void SprawdzBelke(Belka b)
        {
            if (b.GetType() != typeof(Belka))
                throw new ArgumentException("Metoda DodajBelke nie obsluguje pretow");
            foreach (Belka bel in belki)
            {
                if (bel.JestNaBelce(b.Start) && bel.JestNaBelce(b.End))
                    throw new ArgumentException("Belka nie moze nachodzic na inna");
            }
        }

        public void DodajBelke(Belka b)
        {
            if (b.GetType() != typeof(Belka))
                throw new ArgumentException("Metoda DodajBelke nie obsluguje pretow");
            List<Belka> _tBel = new List<Belka>();
            foreach (Belka bel in belki)
            {
                if (bel.JestNaBelce(b.Start) && bel.JestNaBelce(b.End))
                    throw new ArgumentException("Belka nie moze nachodzic na inna");
                if (b.Start == bel.Start || b.Start == bel.End || b.End == bel.Start || b.End == bel.End) continue;
                else if (bel.JestNaBelce(b.Start))
                {
                    _tBel.Add(new Belka(b.Start, bel.End));
                    bel.End = b.Start;
                }
                else if (bel.JestNaBelce(b.End))
                {
                    _tBel.Add(new Belka(b.End, bel.End));
                    bel.End = b.End;
                }
                else if (b.JestNaBelce(bel.Start))
                {
                    _tBel.Add(new Belka(b.Start, bel.Start));
                    b.Start = bel.Start;
                }
                else if (b.JestNaBelce(bel.End))
                {
                    _tBel.Add(new Belka(b.Start, bel.End));
                    b.Start = bel.End;
                }
                // jeszcze krzyżowanie się belek!
            }
            belki.AddRange(_tBel);
            belki.Add(b);

            //adoptuj osierocone obc. i podpory!
            AdoptujSieroty();
        }

        public void SprawdzPret(Pret p)
        {
            foreach (Belka bel in belki)
            {
                if (bel.JestNaBelce(p.Start) && bel.JestNaBelce(p.End))
                {
                    if (bel.GetType() == typeof(Belka))
                        throw new ArgumentException("Pret nie moze nachodzic na belke");
                    else return;
                }
            }
            
        }
        public void DodajPret(Pret p)
        {
            foreach (Belka bel in belki)
            {
                if (bel.JestNaBelce(p.Start) && bel.JestNaBelce(p.End))
                {
                    if (bel.GetType() == typeof(Belka))
                        throw new ArgumentException("Pret nie moze nachodzic na belke");
                    else return;
                }
            }
            belki.Add(p);
            if (!przeguby.Exists(pr => Punkt.AlmostEqual(p.Start, pr.Miejsce)))
                DodajPrzegub(new Przegub(p.Start, belki));//przeguby.Add(new Przegub(p.Start, belki));
            else
            {
                Przegub prz = przeguby.Find(pr => Punkt.AlmostEqual(p.Start, pr.Miejsce));
                if (!prz.wewBelki.Contains(p))
                    prz.wewBelki.Add(p);
            }
            if (!przeguby.Exists(pr => Punkt.AlmostEqual(p.End, pr.Miejsce)))
                DodajPrzegub(new Przegub(p.End, belki));//przeguby.Add(new Przegub(p.End,belki));
            else
            {
                Przegub prz = przeguby.Find(pr => Punkt.AlmostEqual(p.End, pr.Miejsce));
                if (!prz.wewBelki.Contains(p))
                    prz.wewBelki.Add(p);
            }
            AdoptujSieroty();
        }
        public void DodajObciazenie(Obciazenie o)
        {
            obciazenia.Add(o);
        }
        public void DodajPodpore(Podpora p)
        {
            p.ObcIndeks = liczbaObcPodp;
            podpory.Add(p);
            liczbaObcPodp += p.LiczbaReakcjiPodporowych();
        }

        public void SprawdzPrzegub(Punkt p1, bool ctch = false)
        {
            if (przeguby.Exists(pr => Punkt.AlmostEqual(p1, pr.Miejsce)))
            {
                if (ctch) return;
                else throw new ArgumentException("Przegub już istnieje");
            }
        }
        public void DodajPrzegub(Przegub p, bool ctch = false)
        {
            if (przeguby.Exists(pr => Punkt.AlmostEqual(p.Miejsce, pr.Miejsce)))
            { 
                if (ctch) return;
                else throw new ArgumentException("Przegub już istnieje"); 
            }
            przeguby.Add(p);
            List<Belka> _tZew = new List<Belka>(), _tWew = new List<Belka>();
            foreach (Belka b in p.zewBelki)
            {
                if (b.JestNaBelce(p.Miejsce) && !b.JestKoncem(p.Miejsce))
                {
                    _tZew.Add(new Belka(p.Miejsce, b.End));
                    b.End = p.Miejsce;
                }
            }
            foreach (Belka b in p.wewBelki)
            {
                if (b.JestNaBelce(p.Miejsce) && !b.JestKoncem(p.Miejsce))
                {
                    _tWew.Add(new Belka(p.Miejsce, b.End));
                    b.End = p.Miejsce;
                }
            }
            p.zewBelki.AddRange(_tZew);
            p.wewBelki.AddRange(_tWew);
            belki.AddRange(_tZew);
            belki.AddRange(_tWew);

            //adoptuj osierocone obc. i podpory! I przeguby!
            AdoptujSieroty();
        }

        int LiczbaNiewiadomychWPrzegubach()
        {
            int s = 0;
            foreach (Przegub p in przeguby)
                s += 2 * p.wewBelki.Count + (p.zewBelki.Count > 0 ? 2 : 0);
            return s;
        }

        void NadajIndeksyPrzegubom()
        {
            int i = 0;
            foreach (Przegub p in przeguby)
            {
                p.PrzIndex = i;
                i += 2 * p.wewBelki.Count + (p.zewBelki.Count > 0 ? 2 : 0);
            }
        }

        public double[] ObliczReakcje()
        {
            Tarcza.CalkLiczbaNiewiadomych = 0;
            NadajIndeksyPrzegubom();
            PodzielNaTarcze();
            LinearEquationSystem system = new LinearEquationSystem(tarcze.Count * Tarcza.LiczbaRownan + 2 * przeguby.Count, liczbaObcPodp + LiczbaNiewiadomychWPrzegubach()); // dwa*przeguby dodajemy na każde 2 równania równowagi dodanych sił w przegubach
            int i = 1;
            foreach (Tarcza t in tarcze)
            {
                system.SetAB(i, t.UkladRownan(liczbaObcPodp + LiczbaNiewiadomychWPrzegubach()));
                i += Tarcza.LiczbaRownan;
            }
            if (przeguby.Count > 0)
            {
                for (int ii = 0; ii < przeguby.Count - 1; ++ii)
                {
                    for (int j = liczbaObcPodp + przeguby[ii].PrzIndex + 1; j < liczbaObcPodp + przeguby[ii + 1].PrzIndex + 1; j += 2)
                    {
                        system.SetA(2 * ii + tarcze.Count * Tarcza.LiczbaRownan + 1, j, 1);
                        system.SetA(2 * ii + tarcze.Count * Tarcza.LiczbaRownan + 2, j + 1, 1);
                    }
                }
                for (int j = liczbaObcPodp + przeguby[przeguby.Count - 1].PrzIndex + 1; j < liczbaObcPodp + LiczbaNiewiadomychWPrzegubach() + 1; j += 2)
                {
                    system.SetA(2 * (przeguby.Count - 1) + tarcze.Count * Tarcza.LiczbaRownan + 1, j, 1);
                    system.SetA(2 * (przeguby.Count - 1) + tarcze.Count * Tarcza.LiczbaRownan + 2, j + 1, 1);
                }
            }
            double[] rozw = system.Solve();
            foreach (Tarcza t in tarcze)
                t.UzupelnijNiewiadome(rozw);
            double[] x = rozw[..liczbaObcPodp];

            return x;
        }

        public Belka ZnajdzBelke(Punkt p)
        {
            foreach (Belka b in belki)
            {
                if (b.JestNaBelce(p)) return b;
            }
            throw new ArgumentException("Punkt nie znajduje sie na zadnej z belek");
        }
        public Belka ZnajdzBelke(Punkt p1, Punkt p2)
        {
            foreach (Belka b in belki)
            {
                if (b.JestNaBelce(p1) && b.JestNaBelce(p2)) return b;
            }
            throw new ArgumentException("Punkty nie znajduje sie na jednej belce");
        }

        public void Clear()
        {
            przeguby.Clear();
            obciazenia.Clear();
            podpory.Clear();
            belki.Clear();
            liczbaObcPodp = 0;
        }
        public void ClearObc() => obciazenia.Clear();
        public void ClearPrz() => przeguby.Clear();
        public void ClearPod()
        {
            podpory.Clear();
            liczbaObcPodp = 0;
        }

        public void PodzielNaTarcze()
        {
            tarcze.Clear();
            odwiedzone.Clear();
            foreach (Belka b in belki)
                PodzielNaTarcze2(b, null);
        }
        public void PodzielNaTarcze2(Belka b, Tarcza t) // zał. przeguby mogą być tylko na końcach belek!!!
        {
            if (!odwiedzone.Contains(b))
            {
                if (t == null)
                {
                    t = new Tarcza();
                    tarcze.Add(t);
                }
                odwiedzone.Add(b);
                t.DodajBelke(b, obciazenia, podpory);
                RozejrzyjSie(b, b.Start, t);
                RozejrzyjSie(b, b.End, t);
            }
        }

        void RozejrzyjSie(Belka b, Punkt punkt, Tarcza t)
        {
            if (!przeguby.Exists(delegate (Przegub p) { return Punkt.AlmostEqual(punkt, p.Miejsce); }))
                foreach (Belka bel in belki.FindAll(bel2 => bel2.JestKoncem(punkt)))
                    PodzielNaTarcze2(bel, t);
            else
            {
                Przegub pr = przeguby.Find(p => Punkt.AlmostEqual(punkt, p.Miejsce));
                if (pr.wewBelki.Contains(b))
                {
                    t.DodajNiewiadoma(new ObciazeniePunktowe(b, punkt, new Wektor(0, 1)), liczbaObcPodp + pr.PrzIndex + 2 * pr.wewBelki.IndexOf(b));
                    t.DodajNiewiadoma(new ObciazeniePunktowe(b, punkt, new Wektor(1 ,0)), liczbaObcPodp + pr.PrzIndex + 2 * pr.wewBelki.IndexOf(b) + 1);
                }
                else if (pr.zewBelki.Contains(b))
                {
                    t.DodajNiewiadoma(new ObciazeniePunktowe(b, punkt, new Wektor(0, 1)), liczbaObcPodp + pr.PrzIndex + 2 * pr.wewBelki.Count);
                    t.DodajNiewiadoma(new ObciazeniePunktowe(b, punkt, new Wektor(1, 0)), liczbaObcPodp + pr.PrzIndex + 2 * pr.wewBelki.Count + 1);
                    foreach (Belka bel in belki.FindAll(bel2 => bel2.JestKoncem(punkt)))
                        if (pr.zewBelki.Contains(bel))
                            PodzielNaTarcze2(bel, t);
                }
                /*t.DodajNiewiadoma(new ObciazeniePunktowe(b, punkt, new Wektor(0, 1)), liczbaObcPodp + 2 * przeguby.IndexOf(pr));
                t.DodajNiewiadoma(new ObciazeniePunktowe(b, punkt, new Wektor(1, 0)), liczbaObcPodp + 2 * przeguby.IndexOf(pr) + 1);
                foreach (Belka bel in belki.FindAll(delegate (Belka bel2) { return bel2.JestKoncem(punkt); }))
                {
                    if (pr.zewBelki.Contains(bel) && pr.zewBelki.Contains(b))
                        PodzielNaTarcze2(bel, t);
                }*/
            }
        }

        public List<ChartElement> GetChartElements(bool N = false)
        {
            List<ChartElement> chartElements = new List<ChartElement>();
            foreach (Tarcza t in tarcze)
                chartElements.AddRange(t.GetChartElements(N));
            return chartElements;
        }

        void AdoptujSieroty()
        {
            List<Obciazenie> _tObcCiag = new List<Obciazenie>();
            foreach (Obciazenie obc in obciazenia)
            {
                if (obc is ObciazenieCiagle)//obc.GetType() == typeof(ObciazenieCiagle)) 
                {
                    ObciazenieCiagle obcc = obc as ObciazenieCiagle;
                    if (!obcc.Belka.JestNaBelce(obcc.Miejsce))
                        obcc.Belka = belki.Find(b => b.JestNaBelce(obcc.Miejsce) && Util.Rownoleg(new Wektor(b.Start,b.End),new Wektor(obcc.Miejsce,obcc.End)) != 0);
                    int zgodnaRownolegZBelka = Util.Rownoleg(new Wektor(obcc.Belka.Start, obcc.Belka.End), new Wektor(obcc.Miejsce, obcc.End));
                    if (!obcc.Belka.JestNaBelce(obcc.End)) //podziel obciazenie
                    {
                        Belka bel = belki.Find(b => b.JestNaBelce(obcc.End) && Util.Rownoleg(new Wektor(b.Start, b.End), new Wektor(obc.Miejsce, obcc.End)) != 0);
                        if (new Wektor(bel.Start, obcc.Miejsce).Length() < new Wektor(bel.End, obcc.Miejsce).Length()) // co jest "w środku" obciazenia - między jego pocz. a końcem
                        {
                            _tObcCiag.Add(obcc.NoweParametry(bel, bel.Start, obcc.End));//new ObciazenieCiagle(bel, bel.Start, obcc.End, obcc.Wartosc));
                            obcc.End = bel.Start;
                        }
                        else
                        {
                            _tObcCiag.Add(obcc.NoweParametry(bel, obcc.End, bel.End));       //new ObciazenieCiagle(bel, ((ObciazenieCiagle)obc).End, bel.End, obc.Wartosc)); // tu punkty odwrotnie, bo od startu do endu (w sumie konstruktor i tak zamieni, zastanowić się nad skróceniem)
                            obcc.End = bel.End;
                        }
                    }
                    if (Util.Rownoleg(new Wektor(obc.Belka.Start, obc.Belka.End), new Wektor(obc.Miejsce, ((ObciazenieCiagle)obc).End)) == -1) // odwracamy pierwsze obc., jeśli trzeba
                        obcc.OdwrocObc();

                }
                else if (!obc.Belka.JestNaBelce(obc.Miejsce))
                    obc.Belka = belki.Find(b => b.JestNaBelce(obc.Miejsce));
            }
            obciazenia.AddRange(_tObcCiag);
            foreach (Podpora p in podpory)
                if (!p.Belka.JestNaBelce(p.Miejsce))
                    p.ZmienBelke(belki.Find(b => b.JestNaBelce(p.Miejsce)));

            foreach (Przegub p in przeguby)
            {
                for (int i = 0; i < p.zewBelki.Count; ++i)
                    if (!p.zewBelki[i].JestKoncem(p.Miejsce))
                        p.zewBelki[i] = belki.Find(b => b.JestKoncem(p.Miejsce) 
                                                    && (Util.Rownoleg(new Wektor(p.Miejsce, p.zewBelki[i].Start), new Wektor(p.Miejsce, b.Start)) == 1 
                                                    && Util.Rownoleg(new Wektor(p.Miejsce, p.zewBelki[i].Start), new Wektor(p.Miejsce, b.End)) == 1));
                for (int i = 0; i < p.wewBelki.Count; ++i)
                    if (!p.wewBelki[i].JestKoncem(p.Miejsce))
                        p.wewBelki[i] = belki.Find(b => b.JestKoncem(p.Miejsce) 
                                                     && (Util.Rownoleg(new Wektor(p.Miejsce, p.wewBelki[i].Start), new Wektor(p.Miejsce, b.Start)) == 1 
                                                     && Util.Rownoleg(new Wektor(p.Miejsce, p.wewBelki[i].Start), new Wektor(p.Miejsce, b.End)) == 1));

            }

        }
    }
}
