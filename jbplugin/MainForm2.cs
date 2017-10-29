//
// MainForm2.cs
//
// jbplugin 
// Copyright 2017 Jörg Bleyel
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// Linking this library statically or dynamically with other modules is
// making a combined work based on this library.  Thus, the terms and
// conditions of the GNU General Public License cover the whole
// combination.
// 
// As a special exception, the copyright holders of this library give you
// permission to link this library with independent modules to produce an
// executable, regardless of the license terms of these independent
// modules, and to copy and distribute the resulting executable under
// terms of your choice, provided that you also meet, for each linked
// independent module, the terms and conditions of the license of that
// module.  An independent module is a module which is not derived from
// or based on this library.  If you modify this library, you may extend
// this exception to your version of the library, but you are not
// obligated to do so.  If you do not wish to do so, delete this
// exception statement from your version.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Diagnostics;
using Invelos.DVDProfilerPlugin;
using System.Xml;
using Microsoft.Win32;
using System.Threading;

namespace jbplugin
{
    [ComVisible(false)]
    public partial class MainForm2 : Form
    {
        public IDVDProfilerAPI DVDProfilerAPI;
        private static BonjourBrowser bbb = null;

        private static int lastWrite = -1;
        public AutoResetEvent ewh;

        public jbplugin plug;


        public MainForm2()
        {
            InitializeComponent();

            this.Text = "JBPlugin " + typeof(MainForm2).Assembly.GetName().Version;

            radioButtonDB.Checked = false;
            radioButtonWIFI.Checked = false;
            radioButtonGD.Checked = false;
            lblDBFolder.Text = "";
            lblGDFolder.Text = "";

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin");
            if (rk == null)
                rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jbplugin");

            object s = rk.GetValue("DBPath");

            if(s!=null)
                lblDBFolder.Text = s.ToString();

            s = rk.GetValue("SyncMode");
            int SyncMode = 0;

            if (s != null)
            {
                if (!Int32.TryParse(s.ToString(), out SyncMode))
                    SyncMode = 0;
            }

           s = rk.GetValue("AUTOEXPORT");
            if (s != null)
            {
                int a = 0;
                if (!Int32.TryParse(s.ToString(), out a))
                    a = 0;

                checkBoxAutostartExport.Checked = (a == 1);

            }


            if (SyncMode == 0)
            {
                object o = rk.GetValue("IP");
                if (o != null)
                {
                    textBoxIP.Text = o.ToString();
                }
                o = rk.GetValue("Port");
                if (o != null)
                {
                    textBoxPort.Text = o.ToString();
                }

                radioButtonWIFI.Checked = true;

                o = rk.GetValue("MAXRC");
                if (o != null)
                {
                    int m = 0;
                    Int32.TryParse(o.ToString(), out m);
                    maxretryonnetworkerror.Value = m;
                }

                o = rk.GetValue("MAXFILES");
                if (o != null)
                {
                    int m = 10;
                    Int32.TryParse(o.ToString(), out m);
                    maxprofilesperzip.Value = m;
                }

            }
            else if (SyncMode == 1)
            {
                radioButtonDB.Checked = true;
            }
            else if (SyncMode == 2)
                radioButtonGD.Checked = true;

            btnDBFolder.Visible = lblDBFolder.Visible = radioButtonDB.Checked;
            btnGDFolder.Visible = lblGDFolder.Visible = radioButtonGD.Checked;

            btnautoimportean.Enabled = false;
            buttonDELEAN.Enabled = btnautoimportean.Enabled;

            bbb = new BonjourBrowser("_MMC2._tcp");
            bbb.DidServiceFind += new BonjourBrowser.ServiceFind(OnDidServiceFind);
            bbb.DidServiceRemoved += new BonjourBrowser.ServiceRemoved(OnDidServiceRemoved);
            bbb.DidServiceFailed += new BonjourBrowser.ServiceFailed(OnDidServiceFailed);

            if(SyncMode==0)
            {
                bbb.Start();
            }

            maxprofilesperzip.Enabled = (SyncMode == 0);
            maxretryonnetworkerror.Enabled = (SyncMode == 0);

            checkchanged();

        }

    
        public void SetRunning(bool running)
        {
            string t = (running) ? "Exporting ..." : "";
           // toolStripRight.Text= t;
        }

        public void checkchanged()
        {
            textBoxIP.Visible = radioButtonWIFI.Checked;
            textBoxIP.Enabled = checkBoxManualIP.Checked;
            textBoxPort.Visible = radioButtonWIFI.Checked;
            textBoxPort.Enabled = checkBoxManualIP.Checked;
            btnDBFolder.Visible = lblDBFolder.Visible = radioButtonDB.Checked;
            btnGDFolder.Visible = lblGDFolder.Visible = radioButtonGD.Checked;

            labelIPAdress.Visible = tlabelPort.Visible = radioButtonWIFI.Checked;

            if (radioButtonWIFI.Checked && textBoxPort.Enabled)
                buttonloadimport.Enabled = true;

            if(!radioButtonWIFI.Checked)
                buttonloadimport.Enabled = true;

            manualexportbtn.Enabled = false;
        }


        void OnDidServiceFind(BonjourBrowser service)
        {

            string[] a = service.resolvingService.Addresses[0].ToString().Split(":".ToCharArray());

            string ip = a[0];
            string port = a[1];

            textBoxIP.Text = ip;
            textBoxPort.Text = port;
            // ip = "192.168.2.16";
            // WebFunc.SetIpPort(ip, port);

        //    Find(ip, port);

        }

        void OnDidServiceRemoved(BonjourBrowser service)
        {
            Debug.WriteLine("REMOVE");
        }

        void OnDidServiceFailed()
        {
            toolStripStatusLabel1.Text = "No itunes found, please put IP and Port manually";
            checkBoxManualIP.Checked = true;
        }

        static void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                IDVDProfilerAPI url = (IDVDProfilerAPI)e.Argument;
                url.TriggerMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form, "DVD", "Add to Collection...");
            }
            catch
            { }
        }

        private List<string> EANS;

        
        private void ImportEAN()
        {

            string _res;
            string get = null;
            if (radioButtonWIFI.Checked)
            {
                string ip = textBoxIP.Text;
                string port = textBoxPort.Text;
                get = WebFunc.HttpGet(WebFunc.BaseUrl(ip, port) + "/eans.plist", out _res);
                toolStripStatusLabel1.Text = _res;
            }
            
            if (radioButtonDB.Checked)
            {
            }
            
            if (radioButtonGD.Checked)
            {
            }

            EANS = new List<string>();
            if (get != null)
            {
                StringReader sr = new StringReader(get);
                XmlTextReader xr = new XmlTextReader(sr);

                while (xr.Read())
                {
                    if (xr.IsStartElement("string"))
                    {
                        xr.Read();
                        EANS.Add(xr.Value.ToString());
                    }
                }
                xr.Close();
                sr.Close();

                listBoxEAN.Items.Clear();
                listBoxEAN.Items.AddRange(EANS.ToArray());

                Clipboard.Clear();

                if (listBoxEAN.Items.Count > 0)
                {
                    string cl = "";
                    foreach (string s in EANS)
                        cl += s + "\n";
                    Clipboard.SetText(cl);
                }
            }
         

        
        }
        
        static BackgroundWorker _bw;
        /*
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const uint BM_CLICK = 0x00F5;

        [DllImport("user32.dll")]
        public static extern int SendMessage(
              IntPtr hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );

        [DllImport("user32.dll")]
        public static extern int PostMessage(
              IntPtr hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );


        [DllImport("User32")]
        public static extern int SetForegroundWindow(
                IntPtr hwnd

        );

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        public delegate bool WindowEnumDelegate(IntPtr hwnd, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hwnd,
                                                  WindowEnumDelegate del,
                                                  int lParam);


        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd,
                                               StringBuilder bld, int size);



        [DllImport("user32.dll")]
        public static extern uint RealGetWindowClass(IntPtr hWnd, StringBuilder pszType, uint cchType);


        public static string GetWindowClass(IntPtr Hwnd)
        {
            if (Hwnd == IntPtr.Zero)
                return string.Empty;
            // This function gets the name of a window class from a window handle
            StringBuilder Title = new StringBuilder(512);
            RealGetWindowClass(Hwnd, Title, 512);

            return Title.ToString().Trim();
        }

        static int bAddDVDsWindowisopen = 0;
        static IntPtr AddDVDsWindow = IntPtr.Zero;
        
        public static bool WindowEnumProc(IntPtr hwnd, int lParam)
        {
            // get the text from the window
            StringBuilder bld = new StringBuilder(256);

            string cn = GetWindowClass(hwnd);


            // root add window
            if (cn == "TfrmMultipleUPCs" && lParam == 3)
            {
                bAddDVDsWindowisopen = 4;
                AddDVDsWindow = hwnd;
                return false;
            }

            // root
            if (cn == "TfrmAddDVDs" && lParam == 0)
            {
                bAddDVDsWindowisopen = 1;
                AddDVDsWindow = hwnd;
                return false;
            }

            if (cn == "TAdvGlowButton" && lParam == 1)
            {
                GetWindowText(hwnd, bld, 256);
                string text = bld.ToString();
                if (text.Length > 0)
                {
                    if (text.Contains("Add Multiple"))
                    {
                        bAddDVDsWindowisopen = 2;
                        //   Console.WriteLine(text);
                        AddDVDsWindow = hwnd;
                        return false;
                    }
                }
            }

            return true;

        }
        
        private static bool CheckWindow()
        {
            WindowEnumDelegate del = new WindowEnumDelegate(WindowEnumProc);

            Process[] processes = Process.GetProcesses(".");
            foreach (Process p in processes)
            {
                if (p.ProcessName == "dvdpro")
                {
                    // call the win32 function
                    EnumChildWindows(IntPtr.Zero, del, 0);
                }
            }
            return (bAddDVDsWindowisopen == 1);

        }
        */

        private void buttonDELEAN_Click(object sender, EventArgs e)
        {
            //    MessageBox.Show(ee.Message, "Connect Error");
            //try
            //{
            //    string dd = HttpPost(BaseUrl() + "/ean.html", true);
            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show(ee.Message, "Connect Error");
            //}
        }

        private void btnautoimportean_Click(object sender, EventArgs e)
        {

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = String.Join(" ", EANS.ToArray());

            string exe = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\jbplugin\\doimport.exe";

            psi.FileName = exe;
            if (!File.Exists(exe))
            {
                toolStripStatusLabel1.Text = "Auto Import Failed";
                return;
            }

            Process.Start(psi);

            IDVDProfilerAPI url = (IDVDProfilerAPI)DVDProfilerAPI;
            url.TriggerMenuItem(PluginConstants.FORMID_Main, PluginConstants.MENUID_Form, "DVD", "Add to Collection...");

        }

        private void MainForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //bsr.Dispose();
        }

        private void buttonloadimport_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel1.Text = "";
                ImportEAN();

                toolStripStatusLabel1.Text = "Read EANs/UPCs finished";
                btnautoimportean.Enabled = (listBoxEAN.Items.Count > 0);
                buttonDELEAN.Enabled = btnautoimportean.Enabled;
            }
            catch
            {
                toolStripStatusLabel1.Text = "Error loading EANs/UPCs from Device";
            }

        }
        
      
        private delegate void SetTextPropertyCallBack(Control c, string Value);
        public static void SetTextProperty(Control c, string Value)
        {
            if (c.InvokeRequired)
            {
                SetTextPropertyCallBack d = new SetTextPropertyCallBack(SetTextProperty);
                c.BeginInvoke(d, c, Value);
            }
            else
            {
                c.Text = Value;
            }
        }


        string dbtxtf = "DropBox Mode: You need to select the App folder 'MMCApp' in your DropBox root directory.";
        string dbtxtok = "DropBox Mode: The DVD-Profiler will update all Profiles completly unnatended";
        string gdtxtf = "Google Drive Mode: You need to select the App folder 'MMCApp' in your Google Drive root directory.";
        string gdtxtok = "Google Drive Mode: The DVD-Profiler will update all Profiles completly unnatended";
        string wltxt = "WiFi Mode: The DVD-Profiler will update all Profiles completly unnatended in your local WiFi.";
        //string wltxtf = "WiFi Mode: You need to define the Device Name of your iPad or iPhone";

        private void radioButtonDB_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDB.Checked)
            {
                if (bbb != null)
                    bbb.Stop();
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("SyncMode", "1");
                rk.Close();
                label1.Text = dbtxtf;
                if (Directory.Exists(lblDBFolder.Text))
                {
                    string result = new DirectoryInfo(lblDBFolder.Text).Name;
                    if(result=="MMCApp")
                        label1.Text = dbtxtok;
                }
                checkchanged();
            }

        }


        private void radioButtonGD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGD.Checked)
            {
                if(bbb!=null)
                    bbb.Stop();
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("SyncMode", "2");
                rk.Close();
                label1.Text = gdtxtf;
                if (Directory.Exists(lblGDFolder.Text))
                {
                    string result = new DirectoryInfo(lblGDFolder.Text).Name;
                    if (result == "MMCApp")
                        label1.Text = gdtxtok;
                }
                checkchanged();
            }

        }


        private void radioButtonWIFI_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonWIFI.Checked)
            {
                if (bbb != null)
                    bbb.Start();

                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("SyncMode", "0");
                string ip = textBoxIP.Text;
                rk.SetValue("IP", ip);
                ip = textBoxPort.Text;
                rk.SetValue("Port", ip);
                rk.Close();
                label1.Text = wltxt;
                checkchanged();
            }

        }

        private void btnDBFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                lblDBFolder.Text = fbd.SelectedPath;
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("DBPath", fbd.SelectedPath);
                rk.Close();
            }
        }

        private void btnGDFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                lblGDFolder.Text = fbd.SelectedPath;
                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("GDPath", fbd.SelectedPath);
                rk.Close();
            }

        }

        private void checkBoxManualIP_CheckedChanged(object sender, EventArgs e)
        {
            checkchanged();
        }

        private void btncheckconn_Click(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
            rk.SetValue("SyncMode", "0");
            string ip = checkBoxManualIP.Checked ? textBoxIP.Text : "";
            rk.SetValue("IP", ip);
            ip = checkBoxManualIP.Checked ? textBoxPort.Text : "";
            rk.SetValue("Port", ip);
            rk.Close();
            //  label1.Text = wltxt;
            checkchanged();

            string Res = "";
            int rc = ReadOldProfilesWF(WebFunc.BaseUrl(textBoxIP.Text,textBoxPort.Text), out Res);

            manualexportbtn.Enabled=(rc==0);


            if (rc == 1)
            {
                toolStripStatusLabel1.Text = "Error Connect to Device";
                // ex
            }
            if (rc == 2)
            {
                toolStripStatusLabel1.Text = Res;
                // no profile data
            }
            if (rc == 3)
            {
                toolStripStatusLabel1.Text = "Wrong Profile Data";
                // wrong profile data
            }
            if (rc == 0)
            {
                toolStripStatusLabel1.Text = "Success/" + Res;
                // wrong profile data
            }

        }

        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public void HttpGet2(string uri)
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            webRequest.KeepAlive = false;
            webRequest.ContentType = "application/xhtml+xml";
            webRequest.ProtocolVersion = HttpVersion.Version11;
            webRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), webRequest);

            allDone.WaitOne();


        }

        private string Content;
        private string WebResult;

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {

            Content = null;

            WebResult = "";

            try
            {

                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

                // End the operation
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                StringBuilder contentBuilder = new StringBuilder();
                contentBuilder.Append(sr.ReadToEnd());
                Content = contentBuilder.ToString();
                sr.Close();

                // Release the HttpWebResponse
                response.Close();
            }
            catch (WebException /*ex*/)
            {
                WebResult = "Connection Error 1";
            }
            catch (Exception /*ee*/)
            {
                // problem
                WebResult = "Connection Error 2";
            }


            allDone.Set();

        }


        private int ReadOldProfilesWF(string BaseUrl, out string Res)
        {
            int ol = 0;

            Res = "";
         //   _oldlist = new Dictionary<string, string>();
          //  _BaseU = BaseUrl;
            try
            {
                string _res = "";
                string get = "";// WebFunc.HttpGet(BaseUrl + "/gdp", out _res);
                Res = _res;

                HttpGet2(BaseUrl + "/gdp");
                Res = WebResult;
                get = Content;
                if (get != null)
                {

                    int count = 0;
                    //int lastwrite = 0;
                    string devname = "";
                    string dbname = "";

                    XmlDocument d = new XmlDocument();
                    d.LoadXml(get);

                    foreach (XmlNode n in d.GetElementsByTagName("key"))
                    {
                        XmlNode nn = n.NextSibling;
                        if (n.InnerText == "count")
                        {
                            if (!Int32.TryParse(nn.InnerText, out count))
                                count = -1;
                        }

                        if (n.InnerText == "name")
                        {
                            devname = nn.InnerText;
                        }

                        if (n.InnerText == "profilerdbname")
                        {
                            dbname = nn.InnerText;
                        }

                        if (n.InnerText == "lastwrite")
                        {
                            if (!Int32.TryParse(nn.InnerText, out lastWrite))
                                lastWrite = -1;
                        }

                       


                        if (n.InnerText == "list")
                        {
                            foreach (XmlNode nl in nn.ChildNodes)
                            {
                                if (nl.Name == "string")
                                {
                                    ol++;
                                }
                            }
                        }
                        /*
                        if (n.InnerText == "images")
                        {
                            foreach (XmlNode nl in nn.ChildNodes)
                            {
                                if (nl.Name == "string")
                                {
                                    string[] ele = nl.InnerText.Split(';');
                                    _oldlist.Add(ele[0], ele[1]);
                                }
                            }
                        }
                        */
                    }

                    if (count > -1 && ol == count)
                    {
                        string td = "";
                        DateTime unixEpoch = new DateTime(1970, 1, 1);
                        if (lastWrite > 0)
                        {
                            unixEpoch=unixEpoch.AddSeconds(lastWrite);
                            td = " / LastWrite:" + unixEpoch.ToShortDateString();
                        }

                        Res = devname + "/" + dbname + " / Profiles Count:" + count.ToString() + td;
                        //OK
                    }
                    else
                    {
                        Res = devname + "_" + dbname;
                        return 3;
                    }
                }
                else
                    return 2;
            }
            catch (Exception ee)
            {
                Res = ee.Message;
                return 1;
            }
            return 0;
        }

        private void manualexportbtn_Click(object sender, EventArgs e)
        {
            bool b = plug.StartExporter();
            if(!b)
            {
                toolStripStatusLabel1.Text = "Manual Export is currently not possible, please try again later";
            }
        }

        private void btnlogfiles_Click(object sender, EventArgs e)
        {
            string _ld = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\jbplugin\\Logs";

            Process.Start(_ld);
        }

        private void maxprofilesperzip_ValueChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
            rk.SetValue("MAXFILES", maxprofilesperzip.Value, RegistryValueKind.DWord);
            rk.Close();
        }

        private void maxretryonnetworkerror_ValueChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
            rk.SetValue("MAXRC", maxretryonnetworkerror.Value,RegistryValueKind.DWord);
            rk.Close();
        }

        private void checkedListBoxProfilerDBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = checkedListBoxProfilerDBs.SelectedIndex;
            if (selected != -1)
            {
                this.Text = checkedListBoxProfilerDBs.Items[selected].ToString();
            }

            List<string> nc = new List<string>();

            foreach (object itemChecked in checkedListBoxProfilerDBs.CheckedItems)
            {
                nc.Add(itemChecked.ToString());
            }

            if(nc.Count>0)
                plug.WriteUsedDB(nc);

        }

        private void checkedListBoxProfilerDBs_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void MainForm2_Shown(object sender, EventArgs e)
        {

            if (plug.UsedProfilerDBs.Count > 1)
            {
                var items = checkedListBoxProfilerDBs.Items;
                List<string> dbs = plug.Profiles.DVDProfilerDBs();
                if (dbs.Count == 0)
                    dbs.AddRange(plug.UsedProfilerDBs);

                foreach (string dd in dbs)
                {
                    items.Add(dd, (plug.UsedProfilerDBs.Contains(dd)));
                }

                checkedListBoxProfilerDBs.Visible = true;

            }
            else
            {
                checkedListBoxProfilerDBs.Visible = false;
            }
            // dbs

            labelclb1.Visible = labelclb2.Visible = checkedListBoxProfilerDBs.Visible;

        }

        private void checkBoxAutostartExport_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
            rk.SetValue("AUTOEXPORT", checkBoxAutostartExport.Checked ? 1:0, RegistryValueKind.DWord);
            rk.Close();

        }
    }
}
