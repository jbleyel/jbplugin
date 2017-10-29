using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace Exporter
{
    public partial class Form1 : Form
    {
        private string DBPath;
        private string GDPath;
        private int SyncMode;
        private int closecd;
        private Profiles _profiles;
        private Boolean finished;
        
        private int maxFiles;
        private int maxRetry;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            closecd = 0;
            finished = false;
            progressBar1.Visible = false;
            lblStatus.Text = "";
            lblModus.Text = "unknown";
            _profiles = new Profiles();

            maxFiles = 10;
            maxRetry = 0;

            string _ld = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\jbplugin\\Logs";
            if (!Directory.Exists(_ld))
                Directory.CreateDirectory(_ld);


            LogWriter.SetDir(_ld);
            LogWriter.Instance.WriteToLog(String.Format("Start Exporter {0}", typeof(Form1).Assembly.GetName().Version));

            this.Text = "Exporter " + typeof(Form1).Assembly.GetName().Version;

        }

        void StartWorkerWifi()
        {
            backgroundWorkerWiFi.RunWorkerAsync();
        }

        void Find(string ip, string port)
        {

            int RC = maxRetry+1;

            try
            {
                while(RC>0)
                {

                    SetStatus(Properties.Resources.CheckConnection);

                    LogWriter.Instance.WriteToLog("CC");

                    int x = _profiles.SetMode(0, WebFunc.BaseUrl(ip, port));
                    if (x != 0)
                        LogWriter.Instance.WriteToLog(string.Format("Find {0}", x));

                    if (x == 0)
                    {
                        _profiles.StatusLabel = lblStatus;
                        this.BeginInvoke(new Action(StartWorkerWifi));
                        break;
                    }
                    else if (x == 2)
                    {
                        RC--;
                        if(RC<1)
                        {
                            SetStatus(Properties.Resources.ConnectionError);
                            SetTimerLabelVis(true);
                            break;
                        }
                    }

                }

            }
            catch
            {
                //isDeviceConn = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                return;
            }

            LogWriter.Instance.WriteToLog("Form Closing");

            backgroundWorkerDB.CancelAsync();
            backgroundWorkerGD.CancelAsync();
            backgroundWorkerWiFi.CancelAsync();

        }

        private void EnableTimer()
        {
            this.timer1.Enabled = true;
        }

        private void SetTimerLabelVis(bool vis)
        {
            if (lblclosetimer.InvokeRequired)
            {
                lblclosetimer.BeginInvoke(new Action<bool>(_SetTimerLabelVis), vis);
            }
            else
            {
                _SetTimerLabelVis(vis);
            }

            if (vis)
            {
                this.BeginInvoke(new Action(EnableTimer));
            }

        }

        private void _SetTimerLabelVis(bool vis)
        {
            lblclosetimer.Visible = vis;
        }


        private void SetStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.BeginInvoke(new Action<string>(SetStausInternal), message);
            }
            else
            {
                SetStausInternal(message);
            }


        }

        private void SetStausInternal(string txt)
        {
            lblStatus.Text = txt;
        }
        
        private void backgroundWorkerGD_DoWork(object sender, DoWorkEventArgs e)
        {
            _profiles.BGW = backgroundWorkerGD;
            _profiles.SetMode(2, GDPath);
            SetStatus(Properties.Resources.CompareProfiles);
            _profiles.Compare(true);
            if (backgroundWorkerGD.CancellationPending)
                return;
            SetStatus(Properties.Resources.CreateExportData);
            _profiles.CreateZip(true,maxFiles);
            if (backgroundWorkerGD.CancellationPending)
                return;
            SetStatus("Export to Google Drive");
            _profiles.WriteDropBox();
            SetStatus(Properties.Resources.Finished);
            finished = true;
        }


        private void backgroundWorkerDB_DoWork(object sender, DoWorkEventArgs e)
        {
            _profiles.BGW = backgroundWorkerDB;
            _profiles.SetMode(1, DBPath);
            SetStatus(Properties.Resources.CompareProfiles);
            _profiles.Compare(true);
            if (backgroundWorkerGD.CancellationPending)
                return;
            SetStatus(Properties.Resources.CreateExportData);
            _profiles.CreateZip(true,maxFiles);
            if (backgroundWorkerGD.CancellationPending)
                return;
            SetStatus("Export to DropBox");
            _profiles.WriteDropBox();
            SetStatus(Properties.Resources.Finished);
            finished = true;

        }


        private void backgroundWorkerWiFi_DoWork(object sender, DoWorkEventArgs e)
        {
            LogWriter.Instance.WriteToLog("WiFi Worker");
            _profiles.BGW = backgroundWorkerWiFi;

            SetStatus(Properties.Resources.CompareProfiles);
            bool rr = _profiles.Compare(false);
            if (!rr)
            {
                e.Result = "1"; 
                return;
            }

            if (backgroundWorkerWiFi.CancellationPending)
                return;
            SetStatus(Properties.Resources.CreateExportData);
            _profiles.CreateZip(false,maxFiles);
            if (backgroundWorkerWiFi.CancellationPending)
                return;
            SetStatus(Properties.Resources.SendData);
            LogWriter.Instance.WriteToLog("Send");
            _profiles.SendWLAN();
            SetStatus(Properties.Resources.Finished);
            finished = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            backgroundWorkerDB.CancelAsync();
            backgroundWorkerGD.CancelAsync();
            backgroundWorkerWiFi.CancelAsync();

            Application.ExitThread();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            closecd++;
            if (closecd > 20)
            {
                this.btnClose_Click(null, null);
            }
        }

        private void backgroundWorkerWiFi_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e != null && e.Result != null)
            {
                if(e.Result.ToString()  == "1")
                {

                    this.WindowState = FormWindowState.Normal;
                    BringToFront();

                    return;
                }

            }
            if (!finished)
                return;
            SetTimerLabelVis(true);

        }

        private void backgroundWorkerDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!finished)
                return;
            SetTimerLabelVis(true);

        }

        private void backgroundWorkerGD_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!finished)
                return;
            SetTimerLabelVis(true);

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin");
            if (rk == null)
                rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jbplugin");

            object s = rk.GetValue("DBPath");

            if (s != null)
            {
                DBPath = s.ToString();
            }

            s = rk.GetValue("GDPath");

            if (s != null)
            {
                GDPath = s.ToString();
            }

            s = rk.GetValue("SyncMode");
            if (s != null)
            {
                if (!Int32.TryParse(s.ToString(), out SyncMode))
                    SyncMode = 0;
            }

            if (SyncMode == 1)
            {
                if (!Directory.Exists(DBPath))
                    return;
            }

            if (SyncMode == 2)
            {
                if (!Directory.Exists(GDPath))
                    return;
            }

            if (SyncMode == 1)
            {
                lblModus.Text = "DropBox";
                _profiles.StatusLabel = lblStatus;
                LogWriter.Instance.WriteToLog("Start DB Sync");
                backgroundWorkerDB.RunWorkerAsync();
            }

            if (SyncMode == 2)
            {
                lblModus.Text = "Google Drive";
                _profiles.StatusLabel = lblStatus;
                LogWriter.Instance.WriteToLog("Start GD Sync");
                backgroundWorkerGD.RunWorkerAsync();
            }

            if (SyncMode == 0)
            {
                lblModus.Text = "WiFi";

                object oip = rk.GetValue("IP");
                object oport = rk.GetValue("Port");

                object _o = rk.GetValue("MAXRC");
                if (_o != null)
                {
                    int m = 0;
                    Int32.TryParse(_o.ToString(), out m);
                    maxRetry= m;
                }

                _o = rk.GetValue("MAXFILES");
                if (_o != null)
                {
                    int m = 10;
                    Int32.TryParse(_o.ToString(), out m);
                    maxFiles = m;
                }

                if (oip != null && oport != null)
                {
                    if (oip.ToString() != "" && oport.ToString() != "")
                    {
                        LogWriter.Instance.WriteToLog("Start WiFi Sync");
                        string[] arg = new string[] { oip.ToString(),oport.ToString() };
                        FindWorker.RunWorkerAsync(arg);
                        return;
                    }
                }

                LogWriter.Instance.WriteToLog("IP/Port not defnined");

            }

        }

        private void FindWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] parts = (string[])e.Argument;
            Find(parts[0], parts[1]);

        }
    }
}
