using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shpora
{
    public partial class Form1 : Form
    {
        int OffsetX = 20;
        int OffsetY = 50;

        double MaxOpacity = 1;
        float Interval = 2;
        float timer = 0;
        string lastClip = "";

        public Form1()
        {
            InitializeComponent();
        }
      
        protected override void SetVisibleCore(bool value)
        {
            if (value)
            {
                Opacity = MaxOpacity;
                timer = Interval * 1000;
            }
            else
            {
                Opacity = 0;
                timer = 0;
            }
            base.SetVisibleCore(value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hide();
            FindDuplicateProc();
            Settings();
            label1.Text = Base.FileImport();
            Scale();
            timer1.Enabled = true;
            Show();
            lastClip = Clipboard.GetText(TextDataFormat.UnicodeText);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Scale();
            if (Visible)
            {
                timer -= timer1.Interval;
                if (timer <= 0)
                    Hide();
            }
            else
            {
                if (!lastClip.Equals(Clipboard.GetText(TextDataFormat.UnicodeText)))
                {
                    lastClip = Clipboard.GetText(TextDataFormat.UnicodeText);
                    label1.Text = Base.FindAnsver(lastClip);
                    Show();
                }
            }
        }

        private void SetLocation(int x, int y)
        {
            Location = new Point(x, y);
        }

        private void Scale()
        {
            Height = label1.Height;
            Width = label1.Width;
            SetLocation(OffsetX, SystemInformation.PrimaryMonitorSize.Height - Height - OffsetY);
        }

        private void Settings()
        {
            Config.GetConfig();
            OffsetX = Config.OffsetX;
            OffsetY = Config.OffsetY;
            Interval = Config.Interval;
            MaxOpacity = Config.Opacity;
            label1.Font = Config.Font;
            Base.SetSplitter(Config.Splitter);
        }

        private void FindDuplicateProc()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Equals(Process.GetCurrentProcess().ProcessName)
                    && !proc.Id.Equals(Process.GetCurrentProcess().Id)
                    )
                {
                    DialogResult dr = MessageBox.Show("Приложение уже запущено, закрыть?", "", MessageBoxButtons.YesNo);
                    if (dr.Equals(DialogResult.Yes))
                        proc.Kill();
                    Process.GetCurrentProcess().Kill();
                }
            }

        }
    }
}
