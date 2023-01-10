//
// jbplugin.cs
//
// jbplugin 
// Copyright 2023 Jörg Bleyel
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
using System.Text;
using Invelos.DVDProfilerPlugin;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;

namespace jbplugin
{

    public delegate void AsyncMethodReadHash();

    [Guid("bd6609e4-bb47-4da2-a9ee-27b265a39cc8"), ComVisible(true)]
    public class jbplugin : IDVDProfilerPlugin, IDVDProfilerPluginInfo
    {
        IDVDProfilerAPI DVDProfilerAPI;
        const int cMenuAIDSayHello = 1;
        string MenuTokenHello = "";
        private Queue _HashQ;
        private static Queue HashQ;
        private static BackgroundWorker BW;
        private static RegistryMonitor DBMon;
        private PluginProfiles _profiles;
        private static AutoResetEvent ewh = null;
        private bool AutoexeportOnly = false;

        public List<string> UsedProfilerDBs = new List<string>();

        public jbplugin()
		{
            ewh = new AutoResetEvent(false);
            _HashQ = new Queue();
            HashQ = Queue.Synchronized(_HashQ );


// TEST
/*
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin");
            if (rk == null)
                rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jbplugin");

            object s = rk.GetValue("UsedDBS");

            _profiles = new PluginProfiles();
            _profiles.ExportDir = PluginProfiles.Getexportdir();

            List<string> dbs = _profiles.DVDProfilerDBs();

            if (s != null)
            {
                string l = s.ToString();
                string[] _l = l.Split("|".ToCharArray());
                UsedProfilerDBs.AddRange(_l);
            }
            else if (dbs.Count > 0)
            {
                string[] _l = dbs.ToArray();
                UsedProfilerDBs.AddRange(_l);
                string l = string.Join("|", _l);
                rk.Close();
                rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin",true);
                rk.SetValue("UsedDBS", l);

            }

            rk.Close();
            */
        }

        public void WriteUsedDB(List<string> nlist)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin",true);
            if (rk != null)
            {
                string[] _l = nlist.ToArray();
                UsedProfilerDBs.Clear();
                UsedProfilerDBs.AddRange(_l);
                string l = string.Join("|", _l);
                rk.SetValue("UsedDBS", l);

                rk.Close();
            }

        }

        public void Load(IDVDProfilerAPI DVDProAPI)
        {
            DVDProfilerAPI = DVDProAPI;
           // DVDProfilerAPI.RegisterForEvent(PluginConstants.EVENTID_FormCreated);
            DVDProfilerAPI.RegisterForEvent(PluginConstants.EVENTID_DVDRemoved);
            DVDProfilerAPI.RegisterForEvent(PluginConstants.EVENTID_DVDAdded);
            DVDProfilerAPI.RegisterForEvent(PluginConstants.EVENTID_DVDRefreshed);
            DVDProfilerAPI.RegisterForEvent(PluginConstants.EVENTID_DVDEditSave);

            MenuTokenHello = DVDProfilerAPI.RegisterMenuItem(PluginConstants.FORMID_Main,PluginConstants.MENUID_Form, @"Plugins", "jbplugin", cMenuAIDSayHello);

            _profiles = new PluginProfiles();
            _profiles.ExportDir = PluginProfiles.Getexportdir();

            string _ld = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\jbplugin\\Logs";
            if (!Directory.Exists(_ld))
                Directory.CreateDirectory(_ld);

            LogWriter.SetDir(_ld);
            LogWriter.Instance.WriteToLog(String.Format("Start Plugin V:{0}.{1}",GetVersionMajor(),GetVersionMinor()));
            LogWriter.Instance.WriteToLog(String.Format( "Export Dir : {0}", _profiles.ExportDir));

            string _fn = _profiles.ExportDir + "Profiles";
            if (!Directory.Exists(_fn))
                Directory.CreateDirectory(_fn);


            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin");
            if (rk == null) 
                rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jbplugin");

            object s = rk.GetValue("AUTOEXPORT");
            if(s!=null)
            {
                int a=0;
                if (!Int32.TryParse(s.ToString(), out a))
                    a = 0;

                if (a == 1)
                    AutoexeportOnly = true;

            }

            s = rk.GetValue("UsedDBS");

            List<string> dbs = _profiles.DVDProfilerDBs();

            if (s!=null)
            {
                string l = s.ToString();
                string[] _l = l.Split("||".ToCharArray());
                UsedProfilerDBs.AddRange(_l);
            }
            else if( dbs.Count>0)
            {
                string[] _l = dbs.ToArray();
                UsedProfilerDBs.AddRange(_l);
                string l = string.Join("||", _l);
                rk.Close();
                rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("UsedDBS", l);

            }

            rk.Close();

            RegistryKey mon = _profiles.GetDVDReg();
            if (mon != null)
            {
                DBMon = new RegistryMonitor(mon);
                DBMon.RegistryChanged += new EventHandler(DBMon_RegistryChanged);
                DBMon.Start();
            }

            BW = new BackgroundWorker();
            BW.WorkerSupportsCancellation = true;
            BW.WorkerReportsProgress = true;
            BW.DoWork += new DoWorkEventHandler(BW_DoWork);
            BW.ProgressChanged += new ProgressChangedEventHandler(BW_ProgressChanged);
            BW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BW_RunWorkerCompleted);

            HashQ.Enqueue("");
            ewh.Set();
         
            BW.RunWorkerAsync();

        }

        void DBMon_RegistryChanged(object sender, EventArgs e)
        {

            string _exportdir = PluginProfiles.Getexportdir();
            if (_profiles.ExportDir != _exportdir)
            {
                Debug.WriteLine("NEW DB");
                // NEW DB
                BW.CancelAsync();
                Thread.Sleep(5000);
                _profiles.ExportDir = _exportdir;
                string _fn = _exportdir + "Profiles";
                if (!Directory.Exists(_fn))
                    Directory.CreateDirectory(_fn);
                BW.RunWorkerAsync();
            }
        }

        void BW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("BW_RunWorkerCompleted");
        }

        void BW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("BW_ProgressChanged");
        }


        void BW_DoWork(object sender, DoWorkEventArgs e)
        {
            LogWriter.Instance.WriteToLog("BW_DoWork");
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            StartCheck(e);
            Thread.Sleep(1000);

            while (!BW.CancellationPending)
            {
                LogWriter.Instance.WriteToLog("Wait");
                ewh.WaitOne();

                SetRunning(true);

                LogWriter.Instance.WriteToLog("Wait Done");
                try
                {
                    if (HashQ.Count > 0)
                    {
                        string A = HashQ.Dequeue().ToString();

                        if (A.Length > 0 && A.Substring(0, 1) == "-")
                        {
                            A = A.Substring(1);
                            _profiles.ProfilesHashes.Remove(A);
                        }
                        else
                        {
                            LogWriter.Instance.WriteToLog(string.Format("Hash {0}",A));
                            LoadHashes(A,e);
                        }
                    }
                }
                catch(Exception _e)
                {
                    Debug.WriteLine(String.Format("BAD {0}", _e.Message));
                }

                if (BW.CancellationPending)
                    e.Cancel = true;

                bool doexport=false;
                try
                {
                    string _fn = _profiles.ExportDir + "Profiles";

                    string[] _files = Directory.GetFiles(_fn, "*.xml");
                    List<string> _Files = new List<string>();
                    _Files.AddRange(_files);

                    object[] aResult = (object[])DVDProfilerAPI.GetAllProfileIDs();
                    //LogWriter.Instance.WriteToLog("Load Profiles");

                    int c = aResult.Length;
                    int p = 0;

                    foreach (object d in aResult)
                    {
                        if (BW.CancellationPending)
                        {
                            LogWriter.Instance.WriteToLog("BW cancel Profiles");
                            e.Cancel = true;
                            break;
                        }

                        IDVDInfo dvd = null;
                        string id = d.ToString();

//                        LogWriter.Instance.WriteToLog(String.Format("Load Profiles {0} {1}", p++,c));
//                        Debug.WriteLine(String.Format("Write profile {0}", p++));

                        if (_profiles.ProfilesHashes.ContainsKey(id))
                        {
                            string f = _fn + "\\" + id + "_" + _profiles.ProfilesHashes[id] + ".xml";
                            if (!File.Exists(f))
                            {
                                DVDProfilerAPI.DVDByProfileID(out dvd, id, -1, -1);
                                string xf = dvd.GetXML(true);
//                                Debug.WriteLine(String.Format("Write profile {0}", id));
                                File.WriteAllText(f, xf, Encoding.GetEncoding(1252));
                                _Files.Remove(f);
                                doexport = true;
                            }
                            else
                                _Files.Remove(f);

                        }
                    }


                    foreach (string f in _Files)
                    {
                        doexport = true;
                        File.Delete(f);
                    }

                    //LogWriter.Instance.WriteToLog("Load Profiles DONE");

                }
                catch
                {
                    LogWriter.Instance.WriteToLog("Ex: Profiles");
                }

                _profiles.Write();

                LogWriter.Instance.WriteToLog("Load Profiles DONE");

                SetRunning(false);

                if (!AutoexeportOnly)
                    doexport = true;

                if (!doexport)
                    continue;

                LogWriter.Instance.WriteToLog("Start Exporter Auto");
                StartExporter();
                
            }
    
        }

        private void SetRunning (bool b)
        {
            running = b;
            if (mf != null && !mf.IsDisposed)
            {
                mf.SetRunning(running);
            }
        }

        private bool running;

        public bool StartExporter()
        {
            if (running)
                return false;

            LogWriter.Instance.WriteToLog("Start Exporter");

            ProcessStartInfo psi = new ProcessStartInfo();
            string exe = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\jbplugin\\exporter.exe";
            psi.FileName = exe;
            if (File.Exists(exe))
                Process.Start(psi);

            return true;

        }



        private void StartCheck(DoWorkEventArgs e)
        {
            LogWriter.Instance.WriteToLog("StartCheck");
            object[] aResult;
            try
            {

                aResult = (object[])DVDProfilerAPI.GetAllProfileIDs();
                if (aResult!=null)
                    LogWriter.Instance.WriteToLog(String.Format("StartCheck GetAllProfileIDs {0}", aResult.Length));

                foreach (object d in aResult)
                {
                    if (BW.CancellationPending)
                    {
                        LogWriter.Instance.WriteToLog("BW cancel StartCheck");
                        e.Cancel = true;
                        break;
                    }

                    string id = d.ToString();

                    IDVDInfo dvd = null;
                    DVDProfilerAPI.DVDByProfileID(out dvd, id, 0, 0);
                    string xvx = dvd.GetXML(true);
                    if (!xvx.Contains(id))
                    {
                        LogWriter.Instance.WriteToLog("StartCheck Sleep");
                        Thread.Sleep(2000);
                    }
                    else
                        break;
                }
            }
            catch (Exception ee)
            {
                LogWriter.Instance.WriteToLog(String.Format("StartCheck EX: {0} ", ee.Message));

            }

            LogWriter.Instance.WriteToLog("StartCheck DONE");
        }

        private void LoadHashes(string A, DoWorkEventArgs e)
        {
            if (A != "")
            {
                IDVDInfo dvd = null;
                DVDProfilerAPI.DVDByProfileID(out dvd, A, 2048 + 8192, 0);
                string xvx = dvd.GetXML(true);
                string newhash = _profiles.getMd5Hash(xvx);
                _profiles.ProfilesHashes[A] = newhash;
                string i = dvd.GetCoverImageFilename(true, true);
                if (File.Exists(i))
                   _profiles.ProfilesImages[A] = i;
 
            }
            else
            {
                Debug.WriteLine("LOAD START");
                object[] aResult;
                aResult = (object[])DVDProfilerAPI.GetAllProfileIDs();
                string oldhash="";
                string oldXX = "";
                foreach (object d in aResult)
                {
                    if (BW.CancellationPending)
                    {
                        LogWriter.Instance.WriteToLog("BW cancel Loadhash");
                        e.Cancel = true;
                        break;
                    }

                    string id = d.ToString();
                    IDVDInfo dvd = null;
                    DVDProfilerAPI.DVDByProfileID(out dvd, id, 2048 + 8192, 0);
                    string xvx = dvd.GetXML(true);
                    string newhash = _profiles.getMd5Hash(xvx);
//                    if (oldhash==newhash) 
//                       Debug.WriteLine(String.Format("LOAD ID{0}_{1}", id, newhash));
                    oldhash = newhash;
                    oldXX = xvx;
                    _profiles.ProfilesHashes[id] = newhash;
                    string i = dvd.GetCoverImageFilename(true, true);
                    if (File.Exists(i))
                        _profiles.ProfilesImages[id] = i;
                }
               
                Debug.WriteLine("LOAD FINISH");
            }

        }

        public PluginProfiles Profiles
        {
            get { return _profiles; }
        }

        public void HandleEvent(int EventType, object EventData)
        {
            switch (EventType)
            {
                case PluginConstants.EVENTID_CustomMenuClick:
                    HandleMenuClick((int)EventData);
                    break;
                case PluginConstants.EVENTID_DVDRemoved:
                    LogWriter.Instance.WriteToLog("Profile Removed"+EventData.ToString());
                    HashQ.Enqueue("-"+EventData.ToString());
                    ewh.Set();
                    break;
                case PluginConstants.EVENTID_DVDRefreshed:
                    LogWriter.Instance.WriteToLog("Profile Refreshed" + EventData.ToString());
                    HashQ.Enqueue(EventData.ToString());
                    ewh.Set();
                    break;
                case PluginConstants.EVENTID_DVDEditSave:
                    LogWriter.Instance.WriteToLog("Profile Edit Save" + EventData.ToString());
                    HashQ.Enqueue(EventData.ToString());
                    ewh.Set();
                    break;
                case PluginConstants.EVENTID_DVDAdded:
                    LogWriter.Instance.WriteToLog("Profile Added" + EventData.ToString());
                    HashQ.Enqueue(EventData.ToString());
                    ewh.Set();
                    break;
               // case PluginConstants.EVENTID_DVDImagesChanged:
               //     LogWriter.Instance.WriteToLog("Profile Image changed" + EventData.ToString());
               //     HashQ.Enqueue(EventData.ToString());
               //     break;
            }
        }

        public void WriteLog(string text)
        {
            LogWriter.Instance.WriteToLog(text);
        }

        private MainForm2 mf=null;

        private void HandleMenuClick(int MenuEventID)
        {
            if (MenuEventID == cMenuAIDSayHello)
            {
                try
                {
//                    System.Threading.Mutex appStartMutex;

//                    bool isOwnedHere = false;
//                    appStartMutex = new System.Threading.Mutex(
//                    true,
//                    "jbplugin",//Application.ProductName,
//                    out isOwnedHere
//                    );

                    //using(Mutex mutex = new Mutex(true, "Global\\jbplugin"))
                    // {
                    if (mf!=null && !mf.IsDisposed) 
                    {
                        // LogWriter.Instance.WriteToLog("MUTEX")
                        mf.BringToFront();
                        mf.SetRunning(running);
                        return;
                    }


                           // LogWriter.Instance.WriteToLog("NO MUTEX");

                            GC.Collect();
                            SetAllowUnsafeHeaderParsing20();

                             mf = new MainForm2();
                             mf.DVDProfilerAPI = DVDProfilerAPI;
                             mf.ewh = ewh;
                             mf.plug = this;
                    mf.SetRunning(running);
                    mf.Show();

                       // }

                }
                catch(Exception ee)
                {
                    MessageBox.Show(String.Format("Exeption: {0}",ee.Message),"Application Error");
                }
            }
        }

    
#region Bla

        public void Unload()
        {
            LogWriter.Instance.WriteToLog("Unload");
            BW.CancelAsync();
            DVDProfilerAPI.UnregisterMenuItem(MenuTokenHello);
        }


        public int GetPluginAPIVersion()
        {
            return PluginConstants.API_VERSION;
        }

        public string GetName()
        {
            return "jbplugin";
        }

        public string GetDescription()
        {
            return "\"JB Plugin\"";
        }

        public string GetAuthorName()
        {
            return "J.Bleyel";
        }

        public string GetAuthorWebsite()
        {
            return "http://www.myappsolut.de/";
        }

        public int GetVersionMajor()
        {
            return 2;
        }

        public int GetVersionMinor()
        {
            return 4;
        }

        public static bool SetAllowUnsafeHeaderParsing20()
        {
            //Get the assembly that contains the internal class
            Assembly aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            if (aNetAssembly != null)
            {
                //Use the assembly in order to get the internal type for the internal class
                Type aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
                if (aSettingsType != null)
                {
                    //Use the internal static property to get an instance of the internal settings class.
                    //If the static instance isn't created allready the property will create it for us.
                    object anInstance = aSettingsType.InvokeMember("Section",
                      BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });

                    if (anInstance != null)
                    {
                        //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
                        FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null)
                        {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        [DllImport("user32.dll")]
        public extern static int SetParent(int child, int parent);

        [ComImport(), Guid("0002E005-0000-0000-C000-000000000046")]
        internal class StdComponentCategoriesMgr { }

        [ComRegisterFunction()]
        public static void RegisterServer(Type t)
        {
            CategoryRegistrar.ICatRegister cr = (CategoryRegistrar.ICatRegister)new StdComponentCategoriesMgr();
            Guid clsidThis = new Guid("bd6609e4-bb47-4da2-a9ee-27b265a39cc8");
            Guid catid = new Guid("833F4274-5632-41DB-8FC5-BF3041CEA3F1");

            cr.RegisterClassImplCategories(ref clsidThis, 1,
                new Guid[] { catid });
        }

        [ComUnregisterFunction()]
        public static void UnregisterServer(Type t)
        {
            CategoryRegistrar.ICatRegister cr = (CategoryRegistrar.ICatRegister)new StdComponentCategoriesMgr();
            Guid clsidThis = new Guid("bd6609e4-bb47-4da2-a9ee-27b265a39cc8");
            Guid catid = new Guid("833F4274-5632-41DB-8FC5-BF3041CEA3F1");

            cr.UnRegisterClassImplCategories(ref clsidThis, 1,
                new Guid[] { catid });
        }

#endregion

    }

}
