using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace WebHostClient
{
    public partial class Form1 : Form
    {
        HttpSelfHostConfiguration config;
        HttpSelfHostServer server;
        bool started = false;

        public Form1()
        {
            Settings.PortNo = 14000;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config = new HttpSelfHostConfiguration($"http://localhost:{Settings.PortNo}");
            config.Routes.MapHttpRoute("API Default", "{controller}/{id}", new { id = RouteParameter.Optional });
            server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            started = true;
            timer1.Start();
            notifyIcon1.Text = $"Web Host | Started [{Settings.PortNo}]";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hide();
            timer1.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();
        }

        private void startStopMenuItem_Click(object sender, EventArgs e)
        {
            if (started)
            {
                server.CloseAsync().Wait();
                started = false;
                startStopMenuItem.Text = "Start";
                notifyIcon1.Text = "Web Host | Stoped";
            }
            else
            {
                config = new HttpSelfHostConfiguration($"http://localhost:{Settings.PortNo}");
                config.Routes.MapHttpRoute("API Default", "{controller}/{id}", new { id = RouteParameter.Optional });
                server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
                started = true;
                startStopMenuItem.Text = "Stop";
                notifyIcon1.Text = $"Web Host | Started [{Settings.PortNo}]";
            }
        }
    }
}
