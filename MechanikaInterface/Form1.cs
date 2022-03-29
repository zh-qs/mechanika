using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using Mechanika;

namespace MechanikaInterface
{
    public partial class Form1 : Form
    {
        bool moving = false;
        bool intMode = true;
        int xMouse, yMouse;

        double belZoomFactor = 1, forceZoomFactor = 1; 

        CommandParser cp;
        Graphics g, gbmp;
        Bitmap bmp;

        void InitializeOtherComponents()
        {
            ZoomBelComboBox.SelectedIndexChanged += ZoomBelComboBox_SelectedIndexChanged;
            ZoomForceComboBox.SelectedIndexChanged += ZoomForceComboBox_SelectedIndexChanged;
            undoSplitButton.DropDownOpening += undoSplitButton_DropdownOpening;
            undoSplitButton.ButtonClick += undoSplitButton_ButtonClick;
        }

        public Form1()
        {
            InitializeComponent();
            InitializeOtherComponents();
            g = panel1.CreateGraphics();
            bmp = new Bitmap(panel1.Width, panel1.Height);
            gbmp = Graphics.FromImage(bmp);
            cp = new CommandParser(gbmp, textBox1);
            ZoomBelComboBox.SelectedIndex = ZoomForceComboBox.SelectedIndex = Constants.DefaultZoomIndex;
        }
        void DrawPicture()
        {
            g.DrawImage(bmp, new Point(0, 0));
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Command;
            if (e.KeyChar == (char)Keys.Return)
            {
                int end = textBox1.Lines.Length;
                for (int i = cp.CommandCounter; i < end; ++i)
                {
                    Command = textBox1.Lines[i];
                    cp.Parse(Command);
                    DrawPicture();
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving) cp.MoveScr(e.X - xMouse, e.Y - yMouse);
            xMouse = e.X;
            yMouse = e.Y;
            Point ppos = cp.PointOnPlane(PlaneOnPoint(e.Location));
            precisePointer.Location = new Point(ppos.X - 4, ppos.Y - 4);
            toolStripLabel1.Text = "Położenie kursora: " + PlaneOnPoint(e.Location);
            DrawPicture();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                moving = true;
                xMouse = e.X;
                yMouse = e.Y;
            }
        }

        
        TaskCompletionSource<Punkt> tcs = null;
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!(e.ClickedItem is ToolStripButton)) return;
            Task.Factory.StartNew(() => { 
                int numOfArgsNeeded = int.Parse(e.ClickedItem.AccessibleDescription);
                string command = (string)e.ClickedItem.Tag;
                Punkt[] punkty = new Punkt[numOfArgsNeeded];
                Punkt p_toPrint;
                Action action = delegate { textBox1.AppendText(command); };
                for (int i = 0; i < numOfArgsNeeded; ++i)
                {
                    Invoke(action);
                    punkty[i] = Task.Run(async () => await GetPunktFromPanelAsync()).Result;
                    p_toPrint = punkty[i];
                    action = delegate { textBox1.AppendText(" " + p_toPrint.ToString()); };
                }
                if (command.StartsWith(Strings.obcCommand))
                {
                    punkty[numOfArgsNeeded - 1].X = (punkty[numOfArgsNeeded - 2].X - punkty[numOfArgsNeeded - 1].X) * belZoomFactor / forceZoomFactor;
                    punkty[numOfArgsNeeded - 1].Y = (punkty[numOfArgsNeeded - 2].Y - punkty[numOfArgsNeeded - 1].Y) * belZoomFactor / forceZoomFactor;
                    //if (command.Contains(Strings.obcMomArg))
                    //  punkty[numOfArgsNeeded - 1].X = Math.Sqrt(punkty[numOfArgsNeeded - 1].X * punkty[numOfArgsNeeded - 1].X + punkty[numOfArgsNeeded - 1].Y * punkty[numOfArgsNeeded - 1].Y);
                }
                if (numOfArgsNeeded > 0) action = delegate { textBox1.AppendText(" " + punkty[numOfArgsNeeded - 1].ToString()); };
                Invoke(action);
                command += PArrayToString(punkty);
                action = delegate { cp.Parse(command); DrawPicture(); textBox1.AppendText(System.Environment.NewLine); };
                Invoke(action);
            });
        }

        private void undoSplitButton_DropdownOpening(object sender, EventArgs e)
        {
            undoSplitButton.DropDownItems.Clear();
            string[] history = cp.GetMementoHistory();
            for (int i=0;i<history.Length;i++)
            {
                ToolStripButton b = new ToolStripButton(history[i]);
                b.Tag = i;
                b.Click += undoDropdownItem_Click;
                undoSplitButton.DropDownItems.Add(b);
            }
        }

        private void undoSplitButton_ButtonClick(object sender, EventArgs e)
        {
            textBox1.AppendText(Strings.undoCommand);
            cp.Parse(Strings.undoCommand);
            textBox1.AppendText(System.Environment.NewLine);
            DrawPicture();
        }

        private void undoDropdownItem_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Strings.undoCommand + $" {(int)((ToolStripItem)sender).Tag}");
            cp.Parse(Strings.undoCommand + $" {(int)((ToolStripItem)sender).Tag}");
            textBox1.AppendText(System.Environment.NewLine);
            DrawPicture();
        }

        string PArrayToString(Punkt[] punkty)
        {
            string str = "";
            for (int i=0;i<punkty.Length;++i)
                str += " " + punkty[i].ToString();
            return str;
        }
        async Task<Punkt> GetPunktFromPanelAsync()
        {
            tcs = new TaskCompletionSource<Punkt>();
            await tcs.Task;
            Punkt p = tcs.Task.Result;
            Point pp = cp.PointOnPlane(p);
            gbmp.FillEllipse(Brushes.Black, pp.X - 3, pp.Y - 3, 6, 6);
            tcs = null;
            return p;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Punkt p = PlaneOnPoint(e.Location);
            tcs?.SetResult(p);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                moving = false;
                cp.MoveScr(e.X - xMouse, e.Y - yMouse);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                intMode = false;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                intMode = true;
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            precisePointer.Visible = true;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            precisePointer.Visible = false;
        }

        Punkt PlaneOnPoint(Point point)
        {
            Punkt p = new Punkt((point.X - cp.GetX0()) / cp.GetCoef(), (point.Y - cp.GetY0()) / cp.GetCoef());
            if (intMode)
            {
                p.X = Math.Round(p.X);
                p.Y = Math.Round(p.Y);
            }
            return p;
        }

        private void ZoomForceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            forceZoomFactor = double.Parse((ZoomForceComboBox.SelectedItem as string)[..^1]) / 100.0;
            cp.SetFZoom(forceZoomFactor);
            DrawPicture();
        }

        private void ZoomBelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            belZoomFactor = double.Parse((ZoomBelComboBox.SelectedItem as string)[..^1]) / 100.0;
            cp.SetBZoom(belZoomFactor);
            DrawPicture();
        }

    }
}
