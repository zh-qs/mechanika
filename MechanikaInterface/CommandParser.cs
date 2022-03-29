using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mechanika;
using MechanikaInterface.Collections;

namespace MechanikaInterface
{
    class CommandParser
    {
        const int stackCapacity = 5;
        const double arrowHeight = 3;
        static readonly double sqrt3 = Math.Sqrt(3);
        const double ciaHeight = 20;
        const double wysPodUtw = 10;
        const double def_coef = 50, def_fcoef = 10;
        double x0 = 200, y0 = 200;

        double coef = def_coef, fcoef = def_fcoef;

        //Panel panel;
        Graphics g;
        TextBox textBox;
        public int CommandCounter { get; private set; } = 0;
        public CommandParser(Graphics g, TextBox textBox) 
        {
            //this.panel = panel;
            this.g = g;
            this.g.Clear(Color.White);
            this.textBox = textBox;
        }
        
        Mechanika.Environment env = new Mechanika.Environment();
        LimitedStack<IMemento> mementos = new LimitedStack<IMemento>(stackCapacity);
        
        public bool AdditionalInfoNeeded { get; private set; } = false;
        public string AdditionalMessage { get; private set; } = "";
        object[] AdditionalParameters;
        public void Parse(string command)
        {
            ++CommandCounter;
            if (AdditionalInfoNeeded)
            {
                ParseSpecial(command);
                return;
            }
            if (command == "") return;
            string[] commArgs = command.Split(' ');

            if (commArgs[0] == Strings.quitCommand)
            {
                Application.Exit();
            }
            else if (commArgs[0] == Strings.clearCommand)
            {
                mementos.Push(env.GetMemento(Strings.cliOper));
                string arg = "";
                try
                {
                    arg = commArgs[1];
                }
                catch
                {
                    env.Clear();
                }
                if (arg == Strings.obcCommand) env.ClearObc();
                else if (arg == Strings.podporaCommand) env.ClearPod();
                else if (arg == Strings.belkaCommand) env.Clear();
                else if (arg == Strings.przegubCommand) env.ClearPrz();
                else if (arg != "") MessageBox.Show(Strings.WrongOpt);
            }
            else if (commArgs[0] == Strings.belkaCommand)
            {
                List<Belka> toAdd = new List<Belka>();
                for (int i = 1; i + 1 < commArgs.Length; i += 2)
                {
                    Punkt p1, p2;
                    Belka b;
                    if (i + 3 < commArgs.Length)
                    {
                        try
                        {
                            p1 = new Punkt(double.Parse(commArgs[i]), double.Parse(commArgs[i + 1]));
                            p2 = new Punkt(double.Parse(commArgs[i + 2]), double.Parse(commArgs[i + 3]));
                            b = new Belka(p1, p2);
                            env.SprawdzBelke(b);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            return;
                        }
                        toAdd.Add(b);
                    }
                }
                mementos.Push(env.GetMemento(Strings.belkaOper));
                foreach (Belka b in toAdd)
                    env.DodajBelke(b);
                //Punkt p1, p2;
                //try
                //{
                //    p1 = new Punkt(double.Parse(commArgs[1]), double.Parse(commArgs[2]));
                //    p2 = new Punkt(double.Parse(commArgs[3]), double.Parse(commArgs[4]));
                //}
                //catch (Exception e)
                //{
                //    MessageBox.Show(e.Message);
                //    return;
                //}
                //env.DodajBelke(new Belka(p1, p2));
            }
            else if (commArgs[0] == Strings.podporaCommand)
            {
                Punkt p;
                Belka bel;
                try
                {
                    p = new Punkt(double.Parse(commArgs[2]), double.Parse(commArgs[3]));
                    bel = env.ZnajdzBelke(p);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                if (commArgs[1] == Strings.podPPArg)
                {
                    mementos.Push(env.GetMemento(Strings.podPPOper));
                    env.DodajPodpore(new PodporaPrzegubowaPrzesuwna(bel, p));
                }
                else if (commArgs[1] == Strings.podPNPArg)
                {
                    mementos.Push(env.GetMemento(Strings.podPNPOper));
                    env.DodajPodpore(new PodporaPrzegubowaNieprzesuwna(bel, p));
                }
                else if (commArgs[1] == Strings.podUTWArg)
                {
                    mementos.Push(env.GetMemento(Strings.podUTWOper));
                    env.DodajPodpore(new PodporaUtwierdzenie(bel, p));
                }
                else MessageBox.Show(Strings.WrongArg);
                //if (commArgs[1] == podPPArg) env.DodajPodpore(new PodporaPrzegubowaPrzesuwna(env.belki[int.Parse(commArgs[2])], new Punkt(double.Parse(commArgs[3]), double.Parse(commArgs[4]))));
                //else if (commArgs[1] == podPNPArg) env.DodajPodpore(new PodporaPrzegubowaNieprzesuwna(env.belki[int.Parse(commArgs[2])], new Punkt(double.Parse(commArgs[3]), double.Parse(commArgs[4]))));
                //else if (commArgs[1] == podUTWArg) env.DodajPodpore(new PodporaUtwierdzenie(env.belki[int.Parse(commArgs[2])], new Punkt(double.Parse(commArgs[3]), double.Parse(commArgs[4]))));
            }
            else if (commArgs[0] == Strings.przegubCommand)
            {
                Punkt p;
                List<Belka> lb;
                //Belka bel;
                try
                {
                    p = new Punkt(double.Parse(commArgs[1]), double.Parse(commArgs[2]));
                    env.SprawdzPrzegub(p);
                    lb = env.belki.FindAll(b => b.JestNaBelce(p));
                    if (lb.Count == 0) throw new ArgumentException(Strings.BrakBelek);

                    for (int i = 0; i < lb.Count; ++i)
                        g.DrawString(i.ToString(), Util.MediumFont, Brushes.Black, PointOnPlane((lb[i].Start + lb[i].End) / 2));
                    RequireAdditionalInfo(Strings.PSPBelNos, lb, p);
                    return;
                    //bel = env.ZnajdzBelke(p);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                //env.DodajPrzegub(new Przegub(bel, p));
            }
            else if (commArgs[0] == Strings.obcCommand)
            {
                Punkt p1,p2;
                Wektor w;
                Belka b;
                try
                {
                    p1 = new Punkt(double.Parse(commArgs[2]), double.Parse(commArgs[3]));
                    b = env.ZnajdzBelke(p1);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                if (commArgs[1] == Strings.obcPktArg)
                {
                    try { w = new Wektor(-double.Parse(commArgs[4]), -double.Parse(commArgs[5])); }
                    catch (Exception e) { MessageBox.Show(e.Message); return; }
                    mementos.Push(env.GetMemento(Strings.obcPktOper));
                    env.DodajObciazenie(new ObciazeniePunktowe(b, p1, w));
                }
                else if (commArgs[1] == Strings.obcCiagArg)
                {
                    try
                    {
                        p2 = new Punkt(double.Parse(commArgs[4]), double.Parse(commArgs[5]));
                        w = new Wektor(-double.Parse(commArgs[6]), -double.Parse(commArgs[7]));
                        b = env.ZnajdzBelke(p1, p2);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                    mementos.Push(env.GetMemento(Strings.obcCiaOper));
                    env.DodajObciazenie(new ObciazenieCiagle(b, p1, p2, w));
                }
                else if (commArgs[1] == Strings.obcMomArg)
                {
                    double m;
                    try { m = -double.Parse(commArgs[4]); }
                    catch (Exception e) { MessageBox.Show(e.Message); return; }
                    mementos.Push(env.GetMemento(Strings.obcMomSOper));
                    env.DodajObciazenie(new ObciazenieMomentSkupiony(b, p1, m));
                }
                else MessageBox.Show(Strings.WrongArg);
            }
            else if (commArgs[0] == Strings.calCommand)
            {
                List<Obciazenie> obcPoObl = new List<Obciazenie>(env.obciazenia);
                double[] x = env.ObliczReakcje();
                foreach (double f in x) 
                    if (double.IsNaN(f) || double.IsInfinity(f))
                    {
                        MessageBox.Show(Strings.WrongPar);
                        return;
                    }
                int m = 0;
                foreach (Podpora p in env.podpory)
                {
                    obcPoObl.Add(new ObciazeniePunktowe(p.obciazenia[0].Belka, p.obciazenia[0].Miejsce, new Wektor(0.0, -x[m++])));
                    DrawObc(obcPoObl[obcPoObl.Count-1],"R");
                    if (p.obciazenia.Count > 1)
                    {
                        obcPoObl.Add(new ObciazeniePunktowe(p.obciazenia[1].Belka, p.obciazenia[1].Miejsce, new Wektor(-x[m++], 0.0)));
                        DrawObc(obcPoObl[obcPoObl.Count - 1], "H");
                    }
                    if (p.obciazenia.Count > 2)
                    {
                        obcPoObl.Add(new ObciazenieMomentSkupiony(p.obciazenia[2].Belka, p.obciazenia[2].Miejsce, -x[m++]));
                        DrawObc(obcPoObl[obcPoObl.Count - 1], "M");
                    }
                    // tu można zresetować obciążenia!
                }

                //wykresy
                Chart chMV = new Chart(env.GetChartElements()), chN = new Chart(env.GetChartElements(true));
                Form2 form = new Form2(chMV, chN);
                form.Show();
                //Util.ZamienXYObciazenia(env.obciazenia); // obciazenia tez sa referencyjne
                return;
            }
            else if (commArgs[0] == Strings.kratCommand)
            {
                List<Pret> toAdd = new List<Pret>(); 
                for (int i = 1;i+1<commArgs.Length;i+=2)
                {
                    Punkt p1, p2;
                    Pret pret;
                    if (i+3<commArgs.Length)
                    {
                        try
                        {
                            p1 = new Punkt(double.Parse(commArgs[i]), double.Parse(commArgs[i + 1]));
                            p2 = new Punkt(double.Parse(commArgs[i + 2]), double.Parse(commArgs[i + 3]));
                            pret = new Pret(p1, p2);
                            env.SprawdzPret(pret);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                            return;
                        }
                        toAdd.Add(pret);
                        
                    }                      
                }
                mementos.Push(env.GetMemento(Strings.kratOper));
                foreach (Pret p in toAdd)
                    env.DodajPret(p);
            }
            else if (commArgs[0] == Strings.zoomCommand)
            {
                double zf;
                try
                {
                    zf = double.Parse(commArgs[2]);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
                if (zf > 0)
                {
                    if (commArgs[1] == "b") coef *= zf;
                    else if (commArgs[1] == "f") fcoef *= zf;
                    else if (commArgs[1] == "a")
                    {
                        coef *= zf;
                        fcoef *= zf;
                    }
                    else MessageBox.Show(Strings.WrongArg);
                }
                else MessageBox.Show(Strings.WrongArg);
            }
            else if (commArgs[0] == Strings.helpCommand)
            {
                MessageBox.Show(Strings.Help);
            }
            else if (commArgs[0] == Strings.aboutCommand)
            {
                MessageBox.Show(Strings.About);
            }
            else if (commArgs[0] == Strings.undoCommand)
            {
                int opNumber;
                if (commArgs.Length == 1) opNumber = 0;
                else
                {
                    try
                    {
                        opNumber = int.Parse(commArgs[1]);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
                if (mementos.Count > opNumber)
                {
                    for (int i = opNumber; i > 0; --i)
                        mementos.Pop();
                    env.RestoreFrom(mementos.Pop());
                }
                    
            }
            else MessageBox.Show(Strings.WrongCom);
            Render();
        }

        void RequireAdditionalInfo(string message, params object[] par)
        {
            AdditionalInfoNeeded = true;
            AdditionalMessage = message;
            AdditionalParameters = par;
            textBox.AppendText(System.Environment.NewLine + message);
            ++CommandCounter;
        }
        void ParseSpecial(string command)
        {
            AdditionalInfoNeeded = false;
            //if (command == "") return;
            string[] commArgs = command.Split(' ');
            if (AdditionalMessage == Strings.PSPBelNos)
            {
                List<Belka> zew = new List<Belka>(), lb = (List<Belka>)AdditionalParameters[0];
                Punkt pol = (Punkt)AdditionalParameters[1];
                foreach (string nb in commArgs)
                {
                    if (nb == "") continue;
                    zew.Add(lb[int.Parse(nb)]);
                }
                foreach (Belka b in zew)
                {
                    lb.Remove(b);
                }
                mementos.Push(env.GetMemento(Strings.przegubOper));
                env.DodajPrzegub(new Przegub(lb, zew, pol));
                
            }
            Render();
        }

        public Point PointOnPlane(Punkt p, double xOff = 0, double yOff = 0)
        {
            return new Point((int)(p.X * coef + x0 + xOff), (int)(p.Y * coef + y0 + yOff));
        }
        public void Render()
        {
            g.Clear(Color.White);
            foreach (Belka b in env.belki)
            {
                if (b.GetType() == typeof(Belka))
                    g.DrawLine(Util.ThickPen, PointOnPlane(b.Start), PointOnPlane(b.End));
                else // Pret
                    g.DrawLine(Util.LightPen, PointOnPlane(b.Start), PointOnPlane(b.End));
            }
            foreach (Obciazenie obc in env.obciazenia)
                DrawObc(obc);
            foreach (Podpora p in env.podpory)
                DrawPodp(p);
            foreach (Przegub p in env.przeguby)
            {
                Rectangle rect = new Rectangle(PointOnPlane(p.Miejsce, -4, -4), new Size(new Point(8, 8)));
                g.DrawEllipse(Util.LightPen, rect);
                g.FillEllipse(Brushes.White, rect);
            }
        }

      
        void DrawPodp(Podpora p)
        {
            if (p.GetType().Name.Contains("PodporaPrzegubowa"))
            {
                g.DrawLines(Util.LightPen, new Point[] { PointOnPlane(p.Miejsce), PointOnPlane(p.Miejsce, -10, 20), PointOnPlane(p.Miejsce, 10, 20), PointOnPlane(p.Miejsce) });
                if (!p.GetType().Name.Contains("Nie"))
                {
                    g.DrawLine(Util.LightPen, PointOnPlane(p.Miejsce, -10, 25), PointOnPlane(p.Miejsce, 10, 25));
                }
            }
            else
            {
                Wektor w = new Wektor(p.Belka.Start, p.Belka.End);
                g.DrawLine(Util.LightPen, PointOnPlane(p.Miejsce, -wysPodUtw * w.Y / w.Length(), wysPodUtw * w.X / w.Length()), PointOnPlane(p.Miejsce, wysPodUtw * w.Y / w.Length(), -wysPodUtw * w.X / w.Length()));
            }
        }
        
        void DrawObc(Obciazenie obc, string ozn = "")
        {
            if (obc.GetType().Name.Contains("Punkt"))
            {
                DrawArrow(obc.Miejsce, obc.Wartosc.X * fcoef, obc.Wartosc.Y * fcoef);
                g.DrawString(ozn + (ozn == "" ? "" : "=") + Math.Round(obc.Wartosc.Length(), 3, MidpointRounding.ToEven).ToString(), Util.SmallFont, Brushes.Black, PointOnPlane(obc.Miejsce, obc.Wartosc.X * fcoef + 4, obc.Wartosc.Y * fcoef / 2));
            }
            //g.DrawLine(Pens.Black, PointOnPlane(obc.Miejsce), PointOnPlane(obc.Miejsce, obc.Wartosc.X * fcoef, obc.Wartosc.Y * fcoef));
            else if (obc.GetType().Name.Contains("Ciagl"))
            {
                Wektor w = new Wektor(obc.Miejsce, ((ObciazenieCiagle)obc).End);
                g.DrawPolygon(Pens.Black, new Point[] { PointOnPlane(obc.Miejsce), PointOnPlane(((ObciazenieCiagle)obc).End), PointOnPlane(((ObciazenieCiagle)obc).End, ciaHeight * w.Y / w.Length(), -ciaHeight * w.X / w.Length()), PointOnPlane(obc.Miejsce, ciaHeight * w.Y / w.Length(), -ciaHeight * w.X / w.Length()) });
                //g.DrawRectangle(Pens.Black, new Rectangle(PointOnPlane(obc.Miejsce), new Size(new Point((int)(w.X * coef), (int)(obc.Sila(Os.Y) < 0 ? -ciaHeight : ciaHeight)))));
                for (int i = 1; i < w.Length() * 2; ++i)
                    DrawArrow(new Punkt(obc.Miejsce.X + i * w.X / w.Length() / 2, obc.Miejsce.Y + i * w.Y / w.Length() / 2), ciaHeight * obc.Wartosc.X / obc.Wartosc.Length(), ciaHeight * obc.Wartosc.Y / obc.Wartosc.Length());
                g.DrawString(ozn + (ozn == "" ? "" : "=") + Math.Round(obc.Wartosc.Length(), 3, MidpointRounding.ToEven).ToString(), Util.SmallFont, Brushes.Black, PointOnPlane(obc.Miejsce, w.X*coef / 2 + (ciaHeight + 5) * w.Y / w.Length(), w.Y * coef - (ciaHeight + 5) * w.X / w.Length()));
            }
            else
            {
                Rectangle rect = new Rectangle(PointOnPlane(obc.Miejsce, -8, -8), new Size(new Point(16, 16)));
                if (obc.Wartosc.X > 0)
                {
                    g.DrawArc(Util.LightPen, rect, 240, 240);
                    g.DrawLine(Util.LightPen, PointOnPlane(obc.Miejsce, -4, -4 * sqrt3), PointOnPlane(obc.Miejsce, -4 + arrowHeight * sqrt3, -4 * sqrt3 + arrowHeight));
                    g.DrawLine(Util.LightPen, PointOnPlane(obc.Miejsce, -4, -4 * sqrt3), PointOnPlane(obc.Miejsce, -4 + arrowHeight * sqrt3, -4 * sqrt3 - arrowHeight));
                }
                else
                {
                    g.DrawArc(Util.LightPen, rect, 60, 240);
                    g.DrawLine(Util.LightPen, PointOnPlane(obc.Miejsce, 4, -4 * sqrt3), PointOnPlane(obc.Miejsce, 4 - arrowHeight * sqrt3, -4 * sqrt3 + arrowHeight));
                    g.DrawLine(Util.LightPen, PointOnPlane(obc.Miejsce, 4, -4 * sqrt3), PointOnPlane(obc.Miejsce, 4 - arrowHeight * sqrt3, -4 * sqrt3 - arrowHeight));
                }
                g.DrawString(ozn + (ozn == "" ? "" : "=") + Math.Round(obc.Wartosc.Length(), 3, MidpointRounding.ToEven).ToString(), Util.SmallFont, Brushes.Black, PointOnPlane(obc.Miejsce, 7, -20));
            }
        }


        void DrawArrow(Punkt p1,double xOff, double yOff)
        {
            double len = Math.Sqrt(xOff * xOff + yOff * yOff);
            if (len < Util.eps) return;
            g.DrawLine(Pens.Black, PointOnPlane(p1), PointOnPlane(p1,xOff,yOff));
            Punkt pArr1, pArr2;
            pArr1 = new Punkt(arrowHeight / len * (yOff + xOff * sqrt3), -arrowHeight / len * (xOff - yOff * sqrt3));
            pArr2 = new Punkt(-arrowHeight / len * (yOff - xOff * sqrt3), arrowHeight / len * (xOff + yOff * sqrt3));
            g.DrawLine(Util.LightPen, PointOnPlane(p1), PointOnPlane(p1, pArr1.X, pArr1.Y));
            g.DrawLine(Util.LightPen, PointOnPlane(p1), PointOnPlane(p1, pArr2.X, pArr2.Y));
        }

        public void MoveScr(int x, int y)
        {
            g.TranslateTransform(x, y);
            x0 += x;
            y0 += y;
            //Render();
        }
        public double GetCoef() => coef;
        public double GetX0() => x0;
        public double GetY0() => y0;

        public void SetBZoom(double absoluteFactor)
        {
            coef = def_coef * absoluteFactor;
            Render();
        }
        public void SetFZoom(double absoluteFactor)
        {
            fcoef = def_fcoef * absoluteFactor;
            Render();
        }

        public string[] GetMementoHistory()
        {
            IMemento[] memTbl = mementos.GetAllItems();
            string[] history = new string[mementos.Count];
            for (int i = 0; i < mementos.Count; i++)
                history[i] = memTbl[i].GetOperationName();
            return history;
        }

    }
}
