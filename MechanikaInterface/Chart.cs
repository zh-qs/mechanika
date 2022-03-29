using System;
using System.Collections.Generic;
using System.Drawing;
using Mechanika;

namespace MechanikaInterface
{
    public class Chart
    {
        const double defaultOffCoef = 50;
        double offCoef = defaultOffCoef;
        List<ChartElement> elements = new List<ChartElement>();

        public Chart(List<ChartElement> lc) { elements = lc; }
        public void AddElement(ChartElement el) => elements.Add(el);
        public void Draw(Graphics g, bool N = false)
        {
            if (!N)
            {
                SetMaxWart();
                g.DrawString("M[kNm]", Util.BigFont, Brushes.Black, new Point(10, 410));
                foreach (ChartElement el in elements) el.Draw(g);
            }
            foreach (ChartElement el in elements)
                if (!el.Differentiated) el.Differentiate();
            g.DrawString(N ? "N[kN]" : "V[kN]", Util.BigFont, Brushes.Black, new Point(10, N ? 10 : 210));
            SetMaxWart();
            foreach (ChartElement el in elements) el.Draw(g, true, N);
        }
        public void ReduceOffCoef(double val)
        {
            offCoef -= val;
            ChartLine.coef -= val;
        }
        public void IncreaseOffCoef(double val)
        {
            offCoef += val;
            ChartLine.coef += val;
        }
        public void ResetOffCoef()
        {
            offCoef = defaultOffCoef;
            ChartLine.coef = ChartLine.defaultCoef;
        }
        void SetMaxWart()
        {
            double M = 0.0;
            foreach (ChartElement element in elements)
            {
                double wart = element.GetMaxWart();
                if (wart > M) M = wart;
            }
            ChartLine.offcoef = M < Util.eps ? offCoef : offCoef / M;
        }
    }
}
