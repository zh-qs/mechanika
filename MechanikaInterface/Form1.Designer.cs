namespace MechanikaInterface
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.precisePointer = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.belkaButton = new System.Windows.Forms.ToolStripButton();
            this.pretButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ppButton = new System.Windows.Forms.ToolStripButton();
            this.pnpButton = new System.Windows.Forms.ToolStripButton();
            this.utwButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.obcpunButton = new System.Windows.Forms.ToolStripButton();
            this.obcciaButton = new System.Windows.Forms.ToolStripButton();
            this.momskupButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.przButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.calButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cliButton = new System.Windows.Forms.ToolStripButton();
            this.undoSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ZoomBelComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.ZoomForceComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.precisePointer)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 452);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(900, 200);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.precisePointer);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panel1.Location = new System.Drawing.Point(33, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 434);
            this.panel1.TabIndex = 1;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // precisePointer
            // 
            this.precisePointer.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.precisePointer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.precisePointer.Location = new System.Drawing.Point(0, 0);
            this.precisePointer.Name = "precisePointer";
            this.precisePointer.Size = new System.Drawing.Size(8, 8);
            this.precisePointer.TabIndex = 0;
            this.precisePointer.TabStop = false;
            this.precisePointer.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.belkaButton,
            this.pretButton,
            this.toolStripSeparator1,
            this.ppButton,
            this.pnpButton,
            this.utwButton,
            this.toolStripSeparator2,
            this.obcpunButton,
            this.obcciaButton,
            this.momskupButton,
            this.toolStripSeparator3,
            this.przButton,
            this.toolStripSeparator5,
            this.calButton,
            this.toolStripSeparator4,
            this.cliButton,
            this.undoSplitButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(33, 689);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // belkaButton
            // 
            this.belkaButton.AccessibleDescription = "2";
            this.belkaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.belkaButton.Image = ((System.Drawing.Image)(resources.GetObject("belkaButton.Image")));
            this.belkaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.belkaButton.Name = "belkaButton";
            this.belkaButton.Size = new System.Drawing.Size(30, 20);
            this.belkaButton.Tag = "bel";
            this.belkaButton.Text = "Belka";
            // 
            // pretButton
            // 
            this.pretButton.AccessibleDescription = "2";
            this.pretButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pretButton.Image = ((System.Drawing.Image)(resources.GetObject("pretButton.Image")));
            this.pretButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pretButton.Name = "pretButton";
            this.pretButton.Size = new System.Drawing.Size(30, 20);
            this.pretButton.Tag = "kra";
            this.pretButton.Text = "Pręt";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(30, 6);
            // 
            // ppButton
            // 
            this.ppButton.AccessibleDescription = "1";
            this.ppButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ppButton.Image = ((System.Drawing.Image)(resources.GetObject("ppButton.Image")));
            this.ppButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ppButton.Name = "ppButton";
            this.ppButton.Size = new System.Drawing.Size(30, 20);
            this.ppButton.Tag = "pod pp";
            this.ppButton.Text = "Podpora przegubowa przesuwna";
            // 
            // pnpButton
            // 
            this.pnpButton.AccessibleDescription = "1";
            this.pnpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pnpButton.Image = ((System.Drawing.Image)(resources.GetObject("pnpButton.Image")));
            this.pnpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pnpButton.Name = "pnpButton";
            this.pnpButton.Size = new System.Drawing.Size(30, 20);
            this.pnpButton.Tag = "pod pnp";
            this.pnpButton.Text = "Podpora przegubowa nieprzesuwna";
            // 
            // utwButton
            // 
            this.utwButton.AccessibleDescription = "1";
            this.utwButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.utwButton.Image = ((System.Drawing.Image)(resources.GetObject("utwButton.Image")));
            this.utwButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.utwButton.Name = "utwButton";
            this.utwButton.Size = new System.Drawing.Size(30, 20);
            this.utwButton.Tag = "pod utw";
            this.utwButton.Text = "Utwierdzenie";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(30, 6);
            // 
            // obcpunButton
            // 
            this.obcpunButton.AccessibleDescription = "2";
            this.obcpunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.obcpunButton.Image = ((System.Drawing.Image)(resources.GetObject("obcpunButton.Image")));
            this.obcpunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.obcpunButton.Name = "obcpunButton";
            this.obcpunButton.Size = new System.Drawing.Size(30, 20);
            this.obcpunButton.Tag = "obc pun";
            this.obcpunButton.Text = "Obciążenie punktowe";
            // 
            // obcciaButton
            // 
            this.obcciaButton.AccessibleDescription = "3";
            this.obcciaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.obcciaButton.Image = ((System.Drawing.Image)(resources.GetObject("obcciaButton.Image")));
            this.obcciaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.obcciaButton.Name = "obcciaButton";
            this.obcciaButton.Size = new System.Drawing.Size(30, 20);
            this.obcciaButton.Tag = "obc cia";
            this.obcciaButton.Text = "Obciążenie ciągłe";
            // 
            // momskupButton
            // 
            this.momskupButton.AccessibleDescription = "2";
            this.momskupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.momskupButton.Image = ((System.Drawing.Image)(resources.GetObject("momskupButton.Image")));
            this.momskupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.momskupButton.Name = "momskupButton";
            this.momskupButton.Size = new System.Drawing.Size(30, 20);
            this.momskupButton.Tag = "obc mom";
            this.momskupButton.Text = "Moment skupiony";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(30, 6);
            // 
            // przButton
            // 
            this.przButton.AccessibleDescription = "1";
            this.przButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.przButton.Image = ((System.Drawing.Image)(resources.GetObject("przButton.Image")));
            this.przButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.przButton.Name = "przButton";
            this.przButton.Size = new System.Drawing.Size(30, 20);
            this.przButton.Tag = "prz";
            this.przButton.Text = "Przegub";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(30, 6);
            // 
            // calButton
            // 
            this.calButton.AccessibleDescription = "0";
            this.calButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.calButton.Image = ((System.Drawing.Image)(resources.GetObject("calButton.Image")));
            this.calButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.calButton.Name = "calButton";
            this.calButton.Size = new System.Drawing.Size(30, 20);
            this.calButton.Tag = "cal";
            this.calButton.Text = "Oblicz";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(30, 6);
            // 
            // cliButton
            // 
            this.cliButton.AccessibleDescription = "0";
            this.cliButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cliButton.Image = ((System.Drawing.Image)(resources.GetObject("cliButton.Image")));
            this.cliButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cliButton.Name = "cliButton";
            this.cliButton.Size = new System.Drawing.Size(30, 20);
            this.cliButton.Tag = "cli";
            this.cliButton.Text = "Wyczyść";
            // 
            // undoSplitButton
            // 
            this.undoSplitButton.AccessibleDescription = "0";
            this.undoSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.undoSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("undoSplitButton.Image")));
            this.undoSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.undoSplitButton.Name = "undoSplitButton";
            this.undoSplitButton.Size = new System.Drawing.Size(30, 20);
            this.undoSplitButton.Tag = "cof";
            this.undoSplitButton.Text = "Cofnij";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator6,
            this.toolStripLabel2,
            this.ZoomBelComboBox,
            this.toolStripLabel3,
            this.ZoomForceComboBox});
            this.toolStrip2.Location = new System.Drawing.Point(33, 664);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(908, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(121, 22);
            this.toolStripLabel1.Text = "Położenie kursora: 0 0";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(132, 22);
            this.toolStripLabel2.Text = "Skala na rysunku: belek:";
            // 
            // ZoomBelComboBox
            // 
            this.ZoomBelComboBox.Items.AddRange(new object[] {
            "1%",
            "5%",
            "10%",
            "25%",
            "50%",
            "100%",
            "150%",
            "200%",
            "300%",
            "400%",
            "500%",
            "1000%"});
            this.ZoomBelComboBox.Name = "ZoomBelComboBox";
            this.ZoomBelComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel3.Text = ", wektorów:";
            // 
            // ZoomForceComboBox
            // 
            this.ZoomForceComboBox.Items.AddRange(new object[] {
            "1%",
            "5%",
            "10%",
            "25%",
            "50%",
            "100%",
            "150%",
            "200%",
            "300%",
            "400%",
            "500%",
            "1000%"});
            this.ZoomForceComboBox.Name = "ZoomForceComboBox";
            this.ZoomForceComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(941, 689);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Mechanika";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.precisePointer)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton belkaButton;
        private System.Windows.Forms.ToolStripButton pretButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ppButton;
        private System.Windows.Forms.ToolStripButton pnpButton;
        private System.Windows.Forms.ToolStripButton utwButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton calButton;
        private System.Windows.Forms.ToolStripButton obcpunButton;
        private System.Windows.Forms.ToolStripButton obcciaButton;
        private System.Windows.Forms.ToolStripButton momskupButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton przButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton cliButton;
        private System.Windows.Forms.PictureBox precisePointer;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox ZoomBelComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox ZoomForceComboBox;
        private System.Windows.Forms.ToolStripSplitButton undoSplitButton;
    }
}

