using System;
using System.Collections.Generic;

namespace Mechanika
{
    public class MomOblUtil
    {
        List<Belka> belki;
        List<Obciazenie> obciazenia;
        public MomOblUtil(List<Belka> lb,List<Obciazenie> lobc)
        {
            belki = lb;
            obciazenia = lobc;
        }

        void PrepareMomPo1Str() => belkiDoLiczeniaMomPo1Str.Clear();

        List<Belka> belkiDoLiczeniaMomPo1Str = new List<Belka>();

        public double OblMomentPo1Stronie(Punkt p_wyj, Belka b, Punkt p_od, Wektor kierunek, KierunekLiczenia sideDir, bool przedPunktem = true)
        {
            PrepareMomPo1Str();
            return MomentPoJednejStronie(p_wyj, b, p_od, kierunek, sideDir, przedPunktem);
        }
        double MomentPoJednejStronie(Punkt p_wyj, Belka b, Punkt p_od, Wektor kierunek, KierunekLiczenia sideDir, bool przedPunktem = true) // działa tylko dla drzew!!!
        {
            belkiDoLiczeniaMomPo1Str.Add(b);
            if (kierunek.Length() < Util.eps) return 0.0;

            int zgodnaRownolegZeStartem = Util.Rownoleg(kierunek, new Wektor(b.Start, b.End));
            if (!b.JestNaBelce(p_od) || zgodnaRownolegZeStartem == 0) return 0.0;
            double calk_mom = 0;

            foreach (Belka b2 in belki)
            {
                if (b2 != b && !belkiDoLiczeniaMomPo1Str.Contains(b2))
                {
                    if (b.Start == b2.End)
                    {
                        if ((p_od != b.Start && Util.Rownoleg(kierunek, new Wektor(p_od, b.Start)) == 1) || (p_od == b.Start && p_od == b2.End && zgodnaRownolegZeStartem == -1)) //<----E2=S-------->E
                            calk_mom += MomentPoJednejStronie(p_wyj, b2, b2.End, new Wektor(b2.End, b2.Start), KierunekLiczenia.DoStartu);
                    }
                    else if (b.Start == b2.Start)
                    {
                        if ((p_od != b.Start && Util.Rownoleg(kierunek, new Wektor(p_od, b.Start)) == 1) || (p_od == b.Start && p_od == b2.Start && zgodnaRownolegZeStartem == -1)) //<----S2=S-------->E
                            calk_mom += MomentPoJednejStronie(p_wyj, b2, b2.Start, new Wektor(b2.Start, b2.End), KierunekLiczenia.DoKonca);
                    }
                    else if (b.End == b2.End)
                    {
                        if ((p_od != b.End && Util.Rownoleg(kierunek, new Wektor(p_od, b.End)) == 1) || (p_od == b.End && p_od == b2.End && zgodnaRownolegZeStartem == 1)) //<----E2=E<--------S
                            calk_mom += MomentPoJednejStronie(p_wyj, b2, b2.End, new Wektor(b2.End, b2.Start), KierunekLiczenia.DoStartu);
                    }
                    else if (b.End == b2.Start)
                    {
                        if ((p_od != b.End && Util.Rownoleg(kierunek, new Wektor(p_od, b.End)) == 1) || (p_od == b.End && p_od == b2.Start && zgodnaRownolegZeStartem == 1)) //<----S2=E<--------S
                            calk_mom += MomentPoJednejStronie(p_wyj, b2, b2.Start, new Wektor(b2.Start, b2.End), KierunekLiczenia.DoKonca);
                    }
                }
            }
            foreach (Obciazenie obc in obciazenia)
            {
                if (obc.Belka == b)
                {
                    if (Util.Rownoleg(kierunek, new Wektor(p_od, obc.Miejsce)) == 1)
                    {
                        if (!(obc.GetType().Name.Contains("Moment") && p_wyj == obc.Miejsce && p_wyj == p_od && przedPunktem))
                            calk_mom += obc.Moment(p_wyj, sideDir);
                    }
                    else if (p_od == p_wyj && obc.GetType().Name.Contains("Ciagl") && Util.Rownoleg(kierunek, new Wektor(p_od, ((ObciazenieCiagle)obc).End)) == 1) calk_mom += obc.Moment(p_wyj, KierunekLiczenia.DoKonca);
                }
            }
            return calk_mom;
        }
    }
}
