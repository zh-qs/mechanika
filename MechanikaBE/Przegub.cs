using System;
using System.Collections.Generic;

namespace Mechanika
{
    public class Przegub
    {
        public List<Belka> wewBelki = new List<Belka>();
        public List<Belka> zewBelki = new List<Belka>();
        public int PrzIndex { get; set; }
        Punkt pol;
        //public bool PodBelka { get; }
        public Przegub(Punkt polozenie, List<Belka> belki)
        {
            wewBelki = belki.FindAll(b => b.JestKoncem(polozenie) && b.GetType() == typeof(Pret));
            zewBelki = belki.FindAll(b => b.JestNaBelce(polozenie) && b.GetType() == typeof(Belka));
            if (wewBelki.Count + zewBelki.Count > 0)
                pol = polozenie;
            else throw new ArgumentException("Przegub nie znajduje się przy belce");
        }
        public Przegub(List<Belka> wewBelki, List<Belka> zewBelki, Punkt polozenie)
        {
            pol = polozenie;
            foreach (Belka b in wewBelki)
                AssertPoint(b);
            foreach (Belka b in zewBelki)
                AssertPoint(b);
            this.wewBelki = new List<Belka>(wewBelki);
            this.zewBelki = new List<Belka>(zewBelki);
        }
        public Punkt Miejsce { get { return pol; } }

        void AssertPoint(Belka b)
        {
            if (!b.JestNaBelce(pol)) throw new ArgumentException("Przegub nie znajduje się przy belce");
        }

        public Przegub Clone(List<Belka> belki)
        {
            List<Belka> wew = new List<Belka>();
            foreach (Belka bel in wewBelki)
            {
                Belka b = belki.Find(b => b.Start == bel.Start && b.End == bel.End && b.GetType() == bel.GetType());
                wew.Add(b);
            }
            List<Belka> zew = new List<Belka>();
            foreach (Belka bel in zewBelki)
            {
                Belka b = belki.Find(b => b.Start == bel.Start && b.End == bel.End && b.GetType() == bel.GetType());
                zew.Add(b);
            }
            return new Przegub(wew, zew, pol);
        }
    }
}
