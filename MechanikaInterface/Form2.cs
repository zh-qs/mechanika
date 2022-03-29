using System;
using System.Drawing;
using System.Windows.Forms;
using Mechanika;

namespace MechanikaInterface
{
    public partial class Form2 : Form
    {
        bool moving = false;
        int xMouse, yMouse;
        Chart chMV, chN;
        Graphics g;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Chart chMV, Chart chN)
        {
            this.chMV = chMV;
            this.chN = chN;
            InitializeComponent();
        }

        private void DrawCharts()
        {
            g.Clear(Color.White);
            chMV.Draw(g);
            chN.Draw(g, true);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e) //=
        {
            chMV.ResetOffCoef();
            chN.ResetOffCoef();
            DrawCharts();
        }

        private void button2_Click(object sender, EventArgs e) //-
        {
            chMV.ReduceOffCoef(10);
            chN.ReduceOffCoef(10);
            DrawCharts();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = panel1.CreateGraphics();
            DrawCharts();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                ModifyChartLineParameters(e.X - xMouse, e.Y - yMouse);
                DrawCharts();
            }
            xMouse = e.X;
            yMouse = e.Y;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            xMouse = e.X;
            yMouse = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            ModifyChartLineParameters(e.X - xMouse, e.Y - yMouse);
            DrawCharts();
        }

        private void button3_Click(object sender, EventArgs e) //+
        {
            chMV.IncreaseOffCoef(10);
            chN.IncreaseOffCoef(10);
            DrawCharts();
        }

        void ModifyChartLineParameters(int x, int y)
        {
            ChartLine.x0 += x;
            ChartLine.y0 += y;
        }
    }
}
