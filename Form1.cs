using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chronomater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // form
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.AcceptButton = button1;
            // listview
            this.listView1.Columns.Add("Time List", listView1.Width);
            listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            // timer1
            this.timer1.Interval = 3_600_000;
            this.timer2.Interval = 60_000;
            this.timer3.Interval = 1_000;
            this.timer4.Interval = 10;
            this.button2.Enabled = false;
            // tooltip
            this.toolTip1.InitialDelay = 200; this.toolTip1.AutoPopDelay = 3000; this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true; this.toolTip1.StripAmpersands = true; this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = ToolTipIcon.Info; this.toolTip1.UseAnimation = true;
            this.toolTip1.SetToolTip(this.button1, "Start button or Continue button.");
            this.toolTip1.SetToolTip(this.button2, "When the stop button is pressed, the time is recorded.");
            this.toolTip1.SetToolTip(this.button3, "Pressing the restart button resets the stopwatch and stops");
            this.toolTip1.SetToolTip(this.label1, "Hour");
            this.toolTip1.SetToolTip(this.label2, "Minute");
            this.toolTip1.SetToolTip(this.label3, "Second");
            this.toolTip1.SetToolTip(this.label4, "Milisecond");
            this.toolTip1.SetToolTip(this.listView1, "Recorded times list");
            // notifyıcon
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.ShowBalloonTip(2000, "Info", "Chronomater keeps running in the background", ToolTipIcon.Info);

        }

        int hour = 0, minute = 0, seconds = 0, miliseconds = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Start(); this.timer2.Start(); this.timer3.Start(); this.timer4.Start();
            this.button2.Enabled = true; this.button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop(); this.timer2.Stop(); this.timer3.Stop(); this.timer4.Stop();
            this.button2.Enabled = false; this.button1.Enabled = true;
            this.button1.Text = "Continue";

            string result = "->" + hour + ":" + minute + ":" + seconds + ":" + miliseconds;

            ListViewItem lst = new ListViewItem(result);
            listView1.Items.Add(lst);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hour == 1024) { MessageBox.Show("hour is reached the maximum value ! Application is closed"); Application.Exit(); }
            hour++;
            if (hour.ToString().Length == 1) { label1.Text = "0" + hour; }
            else if (hour.ToString().Length == 2) { label1.Text = hour.ToString(); }
            else { label1.Location = new Point(label1.Location.X - 10, label1.Location.Y); }
            label1.Text = hour.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Stop(); this.timer2.Stop(); this.timer3.Stop(); this.timer4.Stop();
            hour = 0; minute = 0; seconds = 0; miliseconds = 0;
            this.label1.Text = "00"; this.label2.Text = "00"; this.label3.Text = "00"; this.label4.Text = "00";
            this.button1.Text = "Start"; this.button2.Enabled = false; this.button1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (minute == 59)
            {
                minute = 0;
                label2.Text = "0" + minute;
            }
            else
            {
                minute++;
                if (minute.ToString().Length == 1) { label2.Text = "0" + minute; }
                else if (minute.ToString().Length == 2) { label2.Text = minute.ToString(); }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (seconds == 59)
            {
                seconds = 0;
                label3.Text = "0" + seconds;
            }
            else
            {
                seconds++;
                if (seconds.ToString().Length == 1) { label3.Text = "0" + seconds; }
                else if (seconds.ToString().Length == 2) { label3.Text = seconds.ToString(); }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (miliseconds == 100)
            {
                miliseconds = 0;
                label4.Text = "0" + miliseconds;
            }
            else
            {
                miliseconds++;
                if (miliseconds.ToString().Length == 1) { label4.Text = "0" + miliseconds; }
                else if (miliseconds.ToString().Length == 2) { label4.Text = miliseconds.ToString(); }
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.WindowState = FormWindowState.Normal;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
