using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mechanika
{
    public class ChartElement // belka na wykresie
    {
        Belka belka;
        List<Obciazenie> obciazenia;
        public List<ChartLine> chartLines = new List<ChartLine>();
        public bool Differentiated { get; private set; } = false;
        public ChartElement(Belka b,List<Obciazenie> lobc, List<Belka> belki)
        {
            belka = b;
            obciazenia = new List<Obciazenie>(lobc);
            MomOblUtil mou = new MomOblUtil(belki, lobc);

            List<Obciazenie> _tObcs = new List<Obciazenie>();
            obciazenia = obciazenia.FindAll(match => match.Belka == belka);
            foreach (Obciazenie obc in obciazenia)
            {
                if (obc.GetType().Name.Contains("Ciagl"))
                    _tObcs.Add(new ObciazeniePomocnicze(belka, ((ObciazenieCiagle)obc).End, obc.Wartosc));
            }
            obciazenia.AddRange(_tObcs);
            obciazenia.Sort(delegate (Obciazenie x, Obciazenie y)
            {
                double xd = (new Wektor(x.Miejsce, b.Start)).Length();
                double yd = (new Wektor(y.Miejsce, b.Start)).Length();
                if (xd > yd) return 1;
                else if (xd == yd) return 0;
                else return -1;
            });
            obciazenia.Insert(0, new ObciazeniePomocnicze(belka, belka.Start, new Wektor(0, 0)));
            obciazenia.Add(new ObciazeniePomocnicze(belka, belka.End, new Wektor(0, 0)));
            double _tMomps, _tMompk, _tMompsr;
            for (int i = 0; i < obciazenia.Count - 1; ++i)
            {
                Punkt psr = (obciazenia[i].Miejsce + obciazenia[i + 1].Miejsce) / 2;//new Punkt((obciazenia[i].Miejsce.X + obciazenia[i + 1].Miejsce.X) / 2, (obciazenia[i].Miejsce.Y + obciazenia[i + 1].Miejsce.Y) / 2);
                _tMompk = mou.OblMomentPo1Stronie(obciazenia[i + 1].Miejsce, belka, obciazenia[i + 1].Miejsce, new Wektor(belka.End, belka.Start), KierunekLiczenia.DoStartu);//obciazenia[i].Moment(obciazenia[i + 1].Miejsce);
                _tMomps = mou.OblMomentPo1Stronie(obciazenia[i].Miejsce, belka, obciazenia[i].Miejsce, new Wektor(belka.End, belka.Start), KierunekLiczenia.DoStartu, false);
                if (b.GetType() == typeof(Pret))
                    _tMompsr = (_tMompk + _tMomps) * 0.5;
                else
                    _tMompsr = mou.OblMomentPo1Stronie(psr, belka, psr, new Wektor(belka.End, belka.Start), KierunekLiczenia.DoStartu);
                ChartLine c = new ChartLine(obciazenia[i].Miejsce, obciazenia[i + 1].Miejsce, _tMomps, _tMompsr, _tMompk);
                chartLines.Add(c);
            }
        }
      
        public void Differentiate()
        {   
            foreach (ChartLine line in chartLines)
            {
                Wektor w = new Wektor(line.pocz, line.kon);
                if (w.Length() == 0) continue; // !
                double wp1 = -(4 * line.wart_sr - 3 * line.wart_pocz - line.wart_kon) / w.Length();
                double wsr1 = -(line.wart_kon - line.wart_pocz) / w.Length();
                double wk1 = -(3 * line.wart_kon + line.wart_pocz - 4 * line.wart_sr) / w.Length();

                double L = w.Length(), x;
                if (Math.Abs(wk1 - wp1) < Util.eps) x = -1.0;
                else x = L * wp1 / (wp1 - wk1); // miejsce zerowe prostej
                if (x > 0 && x < L)
                {
                    //Punkt p = new Punkt(pocz.X * (1 - x / L) + kon.X * x / L, pocz.Y * (1 - x / L) + kon.Y * x / L);
                    line.xekstr = x;
                    x = x / L - 0.5; // przekształcenie na (-0.5,0.5)
                    line.Mekstr = (line.wart_kon + line.wart_pocz - 2 * line.wart_sr) * 2 * x * x + (line.wart_kon - line.wart_pocz) * x + line.wart_sr;
                }

                line.wart_kon_d = wk1;
                line.wart_pocz_d = wp1;
                line.wart_sr_d = wsr1;
            }
            Differentiated = true;
        }

        public void Draw(Graphics g, bool differentiated = false, bool N = false)
        {
            bool pret = belka.GetType() == typeof(Pret);
            foreach (ChartLine line in chartLines)
                line.Draw(g, differentiated, N, pret);
        }

        public double GetMaxWart()
        {
            double M = 0.0;
            foreach (ChartLine line in chartLines)
            {
                if (Math.Abs(line.wart_kon) > M) M = Math.Abs(line.wart_kon);
                if (Math.Abs(line.wart_pocz) > M) M = Math.Abs(line.wart_pocz);
                if (Math.Abs(line.wart_sr) > M) M = Math.Abs(line.wart_sr);
            }
            return M;
        }
    }

}
