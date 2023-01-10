//
// Profiles.cs
//
// jbplugin / Exporter
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
using System.Security.Cryptography;
using System.Xml;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Resources;

namespace Exporter
{
    [ComVisible(false)]
    public class Profiles
    {
         private int Mode;
         private List<Device> _Devices;
         private List<string> _DataBases;
        private List<string> _USEDDB;
         public Dictionary<string, Dictionary<string, string>> profilesHashes;
         public Dictionary<string, Dictionary<string, string>> profilesImages;
        public Dictionary<string, Dictionary<string, string>> profilesIHashes;
        private string _DropBoxPath;
        public const string DVDP = "DVD Profiler";
        public const string SOFTWARE = "Software";
        public const string DB = "DataBase";

        public BackgroundWorker BGW { get; set; }

         public Label StatusLabel;

         internal Profiles() {
             _Devices = null;
             BGW = null;
             this.profilesHashes = new Dictionary<string, Dictionary<string, string>>();
             this.profilesImages = new Dictionary<string, Dictionary<string, string>>();
            this.profilesIHashes = new Dictionary<string, Dictionary<string, string>>();
            this._DataBases = new List<string>();
            this._USEDDB = new List<string>();

            LogWriter.Instance.WriteToLog(String.Format("Profiles init Dir: {0}", this.DBDir));

            foreach (string f in Directory.GetDirectories(this.DBDir))
             {
                 if (Directory.Exists(f + "\\Export"))
                     this._DataBases.Add(Path.GetFileName(f));
             }
            
           // this._DataBases.Clear();
           // this._DataBases.Add("JB TEST");
            
             Read();
         }

         public void GetUsedDBS()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin");
            if (rk == null)
                rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\jbplugin");

            object s = rk.GetValue("UsedDBS");

//            List<string> dbs = _profiles.DVDProfilerDBs();

            if (s != null)
            {
                string l = s.ToString();
                string[] _l = l.Split("|".ToCharArray());
                _USEDDB.AddRange(_l);
            }
            /*
            else if (dbs.Count > 0)
            {
                string[] _l = dbs.ToArray();
                _USEDDB.AddRange(_l);
                string l = string.Join("|", _l);
                rk.Close();
                rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin", true);
                rk.SetValue("UsedDBS", l);

            }
            */
            rk.Close();

        }

         private void SetStatus(string txt)
         {
             StatusLabel.Invoke((MethodInvoker)(() => StatusLabel.Text = txt));
         }

         private string DBDir
         {
             get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\" + DVDP + "\\" + DB + "s\\"; }
         }
         public void Read()
         {

               foreach (string DB in this._DataBases)
               {
                    LogWriter.Instance.WriteToLog(String.Format("Profiles Read DB {0}", DB));

                    this.profilesHashes[DB] = null;

                   string f = DBDir + DB + "\\Export\\hashes.info";
                   if (File.Exists(f))
                   {
                       this.profilesHashes[DB] = new Dictionary<string, string>();
                       string[] ll = File.ReadAllLines(f);
                       foreach (string line in ll)
                       {
                           string[] _line = line.Split(";".ToCharArray());
                           this.profilesHashes[DB].Add(_line[0], _line[1]);
                       }

                   }
                   else
                      LogWriter.Instance.WriteToLog(String.Format("Profiles ERR NO Hash :{0}", f));


                   f = DBDir + DB + "\\Export\\images.info";
                   this.profilesImages[DB] = null;
                   if (File.Exists(f))
                   {
                       this.profilesImages[DB] = new Dictionary<string, string>();
                       string[] ll = File.ReadAllLines(f);
                       foreach (string line in ll)
                       {
                           string[] _line = line.Split(";".ToCharArray());
                           this.profilesImages[DB].Add(_line[0], _line[1]);
                       }

                   }

                f = DBDir + DB + "\\Export\\ihashes.info";
                this.profilesIHashes[DB] = null;
                if (File.Exists(f))
                {
                    this.profilesIHashes[DB] = new Dictionary<string, string>();
                    string[] ll = File.ReadAllLines(f);
                    foreach (string line in ll)
                    {
                        string[] _line = line.Split(";".ToCharArray());
                        this.profilesIHashes[DB].Add(_line[0], _line[1]);
                    }

                }

            }
         }

         internal void RemoveOldFiles()
         {
             if (!Directory.Exists(this._DropBoxPath))
                 return;

             List<string> infofiles = new List<string>();
             List<string> delist = new List<string>();

             foreach (string fn in Directory.GetFiles(this._DropBoxPath, "*.info"))
             {
                 infofiles.Add(Path.GetFileNameWithoutExtension(fn));
             }
             
             foreach (string fn in Directory.GetFiles(this._DropBoxPath, "*.zip"))
             {
                 string ff = Path.GetFileNameWithoutExtension(fn);
                 foreach (string _fn in infofiles)
                 {
                     if (ff.Substring(0, _fn.Length) == _fn)
                         continue;
                 }
                 delist.Add(fn);
             }

             foreach (string fn in delist)
             {
                 File.Delete(fn);
             }

         }

         internal void SendWLAN()
         {

           //  SetStatus("Send Profiles over WiFi");

             if (_Devices == null)
                 return;

             if (this.profilesHashes == null)
                 return;

             foreach (Device d in _Devices)
             {

                 if (this.profilesHashes[d.DBName] == null)
                     break;

                 string exportDir = DBDir + d.DBName + "\\Export\\";
                 string delinfofile = exportDir + d.Name + "\\del.info";
                 string zinfofile = exportDir + d.Name + "\\zip.info";

                 List<string> zinfo = new List<string>();
                 foreach (string fn in Directory.GetFiles(exportDir + d.Name, "*.zip"))
                 {
                     string dst = d.Name + "_" + Path.GetFileName(fn);
                     zinfo.Add(dst);
                 }
                 File.WriteAllLines(zinfofile, zinfo.ToArray());

                 if (!d.SendFileList(zinfofile))
                     break;

                 zinfo = new List<string>();

                 string[] fns = Directory.GetFiles(exportDir + d.Name, "*.zip");

                 int pos=1;
                 foreach (string fn in fns)
                 {
                    LogWriter.Instance.WriteToLog(string.Format("Send Profile {0}", fn));
                    if (!d.SendFile(fn))
                     {
                        LogWriter.Instance.WriteToLog(string.Format("Error Send Profile {0}", fn));
                        break;
                     }
                     else
                         zinfo.Add(fn);
                     pos++;
                 }

                foreach (string fn in zinfo)
                 {
                     File.Delete(fn);
                 }

                 d.SetNewLastWrite();

             }
         }

         internal void WriteDropBox()
         {
             if (_Devices == null)
                 return;

             if (this.profilesHashes == null)
                 return;
             if (!Directory.Exists(this._DropBoxPath))
                 return;

             foreach (Device d in _Devices)
             {
                 if (BGW != null && BGW.CancellationPending)
                     return;

                 if (this.profilesHashes[d.DBName] == null)
                     break;

                 string exportDir = DBDir + d.DBName + "\\Export\\";
                 string delinfofile = exportDir + d.Name + "\\del.info";
                // string zinfofile = exportDir + d.Name + "\\zip.info";

                 foreach (string fn in Directory.GetFiles(exportDir + d.Name, "*.zip"))
                 {
                     if (BGW != null && BGW.CancellationPending)
                         return;

                     string src = fn;
                     string dst = this._DropBoxPath + "\\" + d.Name + "_" + Path.GetFileName(fn);
                     if (File.Exists(dst))
                     { 
                         FileInfo _src = new FileInfo(src);
                         FileInfo _dst = new FileInfo(dst);
                         if (_src.Length == _dst.Length)
                         {
                             string _smd5="1";
                             string _dmd5 = "2";
                             using (var md5 = MD5.Create())
                             {
                                 using (var stream = File.OpenRead(fn))
                                 {
                                     _smd5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-","").ToLowerInvariant();
                                 }
                                 using (var stream = File.OpenRead(dst))
                                 {
                                     _dmd5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
                                 }
                             }

                             if (_smd5 == _dmd5)
                                 continue;
                            
                         }
                     }
                     File.Copy(src, dst, true);
                 }

             }
         
         }

         internal void CreateZip(bool dp,int Maxfiles)
         {


            bool _nodev = (_Devices == null);
            bool _noph = (this.profilesHashes == null);

            LogWriter.Instance.WriteToLog(String.Format("Create Zip {0} {1}", _nodev, _noph));

            if (_nodev || _noph)
                return;


            foreach (Device d in _Devices)
             {
                 if (BGW != null && BGW.CancellationPending)
                     return;

                if (this.profilesHashes[d.DBName] == null)
                {
                    LogWriter.Instance.WriteToLog(String.Format("Create Zip ERR: {0}","Break PH"));
                    break;
                }

                List<string> imgs = new List<string>();
                 List<string> fl = new List<string>();

                 string exportDir = DBDir + d.DBName + "\\Export\\";
                LogWriter.Instance.WriteToLog(String.Format("Create Zip Export Dir: {0}", exportDir));

                if (!Directory.Exists(exportDir + d.Name))
                     Directory.CreateDirectory(exportDir + d.Name);

                string delinfofile = exportDir + d.Name + "\\del.info";
                if (d.DelList != null && d.DelList.Count>0)
                {
                    File.WriteAllLines(delinfofile, d.DelList.ToArray());
                }
                else
                    delinfofile = "";

                 if (d.SendList != null)
                 {
                    int max = Maxfiles;
                    if (max < 10)
                        max = 10;
                    if (max > 100)
                        max = 100;

                    int pos = 0;
                    int zfc = 0;

                     // get zip list

                     string _fn = exportDir + "Profiles";
                     
                    if (delinfofile !="")
                    {
                        if (d.SendList.Count == 0)
                        {

                            string zfn = exportDir + d.Name + "\\" + zfc.ToString() + ".zip";
                            fl.Add(delinfofile);
                            string _e = PackClass.PackFileso(fl, zfn, imgs);
                        }
                    }
                    
                    LogWriter.Instance.WriteToLog(String.Format("Create Zip File Dir: {0}", exportDir + d.Name));

                    foreach (string _id in d.SendList)
                     {
                         if (BGW != null && BGW.CancellationPending)
                             return;

                         if (!this.profilesHashes[d.DBName].ContainsKey(_id))
                            continue;

                         if(delinfofile!="")
                            fl.Add(delinfofile);

                        delinfofile = "";

                        string f = _fn + "\\" + _id + "_" + this.profilesHashes[d.DBName][_id] + ".xml";
 
                         fl.Add(f);

                        if(d.SendiList.Contains(_id))
                        {
                            if (this.profilesImages[d.DBName].ContainsKey(_id))
                                imgs.Add(this.profilesImages[d.DBName][_id]);
                        }

                        pos++;
                         if (pos > max)
                         {
                             List<string> fll = new List<string>();
                             foreach (string s in fl)
                                 fll.Add(Path.GetFileNameWithoutExtension(s));

                             string zfn = exportDir + d.Name + "\\" + zfc.ToString() + ".zip";
                             zfc++;
                            LogWriter.Instance.WriteToLog(String.Format("Create Zip File: {0}", zfn));

                            string _e = PackClass.PackFileso(fl, zfn, imgs);
                             pos = 0;
                             fl.Clear();
                             imgs.Clear();
                         }
                     }

                     if (fl.Count > 0)
                     {
                         string _zfn = exportDir + d.Name + "\\" + zfc.ToString() + ".zip";
                         string __e = PackClass.PackFileso(fl, _zfn, imgs);
                         List<string> _fll = new List<string>();
                         foreach (string s in fl)
                             _fll.Add(Path.GetFileNameWithoutExtension(s));

                     }
                     else
                        LogWriter.Instance.WriteToLog(String.Format("Create Zip {0}", "no Files"));

                }
            }
         }

         internal bool Compare(bool dp)
         {

            bool _nodev = (_Devices == null);
            bool _noph = (this.profilesHashes == null);

            LogWriter.Instance.WriteToLog(String.Format("Compare {0} {1}", _nodev, _noph));

            if (_nodev || _noph)
                return false;

            bool rt = false;

            foreach (Device d in _Devices)
             {
                 if (BGW!=null && BGW.CancellationPending)
                     return false;

                 if(this.profilesHashes.ContainsKey(d.DBName))
                     d.Compare(this.profilesHashes[d.DBName],this.profilesIHashes[d.DBName]);

                 // write old device info
                 int olw = d.GetOldLastWrite();
                 if (olw != d.getLastwrite)
                 {
                     if (!dp)
                     {

                        string exportDir = DBDir + d.DBName + "\\Export\\";
                        LogWriter.Instance.WriteToLog(String.Format("Compare Dir : {0}", exportDir));

                        List<string> zinfo = new List<string>();

                        if (Directory.Exists(exportDir + d.Name))
                        {
                            foreach (string fn in Directory.GetFiles(exportDir + d.Name, "*.zip"))
                            {
                                if (BGW != null && BGW.CancellationPending)
                                    return false;

                                zinfo.Add(Path.GetFileName(fn));
                            }
                            rt = true;
                        }
                        else
                        {
                            LogWriter.Instance.WriteToLog(String.Format("NO old ZIP Files in : {0}", exportDir+ d.Name));
                        }

                        if (!Directory.Exists(exportDir))
                        {

                            LogWriter.Instance.WriteToLog(String.Format("Err export dir not exist: {0}", exportDir));

                            try
                            {
                                SetStatus(String.Format(Properties.Resources.WrongDBName, d.DBName));
                            }
                            catch
                            {
                                SetStatus(String.Format("DVD Profiler Database: '{0}' not found. You need to create a new Database inside the App and define the correct Database Name.", d.DBName));
                            }
                            return false;
                        }
                        else
                            rt = true;
                     }
                 }

             }
             return rt;
        }


        public int SetMode(int M, string path)
         {
             Mode = M;
             // DB
             if (Mode == 1)
             {
                 _Devices = null;

                 this._DropBoxPath = path;

                 string[] files = Directory.GetFiles(path, "*.info");
                 if (files.Length == 0)
                     return 1;

                 _Devices = new List<Device>();
                 foreach (string f in files)
                 {
                     string s = "";
                     Device d = new Device(f);
                     if (d.DBName != "")
                     {
                         d.ReadOldProfilesDB(out s);
                         _Devices.Add(d);
                     }
                 }
                 return 0;
             }

             if (Mode == 2)
             {
                 _Devices = null;

                 this._DropBoxPath = path;

                 string[] files = Directory.GetFiles(path, "*.info");
                 if (files.Length == 0)
                     return 1;

                 _Devices = new List<Device>();
                 foreach (string f in files)
                 {
                     string s = "";
                     Device d = new Device(f);
                     if (d.DBName != "")
                     {
                         d.ReadOldProfilesDB(out s);
                         _Devices.Add(d);
                     }
                 }
                 return 0;
             }
             // WIFI
             if (Mode == 0)
             {
                 string s = "";
                 _Devices = new List<Device>();
                 Device d = new Device("");
                 int r = d.ReadOldProfilesWF(path, out s);
                LogWriter.Instance.WriteToLog(String.Format("ReadOldProfilesWF {0} {1} {2}", r,d.DBName,d.Name));

                if(_DataBases.Count == 1)
                {
                    if (d.DBName != _DataBases[0])
                    {
                        LogWriter.Instance.WriteToLog(String.Format("AUTO FIX DB Name {0} <> {1}", d.DBName, _DataBases[0]));
                        d.setDBName(_DataBases[0]);
                    }
                }

                if (d.DBName!="")
                     _Devices.Add(d);

                return r;
             }
             return 2;
         }
        
    }

    public class Device
    {
        private string _Name;
        private string _FName;
        private Dictionary<string, string> _oldlist;
        private Dictionary<string, string> _oldilist;
        private List<string> _sendlist;
        private List<string> _sendilist;
        private List<string> _dellist;
//        private DateTime _LastInfo;
        private string _BaseU;
        private int _lastwrite;
        private int _dbc;


        public List<string> SendiList { get { return _sendilist; } }
        public List<string> SendList { get { return _sendlist; } }
        public List<string> DelList { get { return _dellist; } }

        public int getLastwrite {
            get {
                return _lastwrite;
            }
        }

        public int getDBC
        {
            get
            {
                return _dbc;
            }
        }

        public void setDBName(string nn)
        {
            string[] sp = Path.GetFileNameWithoutExtension(_FName).Split("_".ToCharArray());
            if (sp.Length == 2)
            {
                _FName = sp[0] + "_" + nn;
                _Name = Path.GetFileNameWithoutExtension(_FName);
            }
        }

        public string DBName {
            get {
                string[] sp = Path.GetFileNameWithoutExtension(_FName).Split("_".ToCharArray());
                if (sp.Length == 2)
                {
                    return sp[1];
                }
                return "";
            }
        }

        public string Name { get { return _Name; } }

        public Device(string Name)
        {
            _FName = Name;
            _Name = Path.GetFileNameWithoutExtension(Name);
            _oldlist = null;
            _oldilist = null;
            _sendlist = null;
            _dellist = null;
            _sendilist = null;

        }

        internal void SetNewLastWrite()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin",true);
            rk.SetValue(_Name + "_LW",_lastwrite.ToString());
            rk.Close();
        }

        internal int GetOldLastWrite()
        {
            int olw = -1;
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\jbplugin");
            object s = rk.GetValue(_Name + "_LW");
            if (s != null)
            {
                if (!Int32.TryParse(s.ToString(), out olw))
                    olw = -1;
            }
            rk.Close();
            return olw;
        }

        internal bool SendFileList(string fn)
        {
            return WebFunc.UploadFile(_BaseU + "/idpl", fn);
        }

        internal bool SendFile(string fn)
        {
            string dst = _Name + "_" + Path.GetFileName(fn);
            return WebFunc.UploadFile(_BaseU + "/idp/" + dst, fn);
        }

        internal int ReadOldProfilesWF(string BaseUrl, out string Res)
        {

            Res = "";
            _oldlist = new Dictionary<string, string>();
            _oldilist = new Dictionary<string, string>();
            _BaseU = BaseUrl;
            try
            {
                string _res;
                string get = WebFunc.HttpGet(_BaseU + "/gdp", out _res);
                Res = _res;
                if (get != null)
                {

                    int count = 0;
                    string devname = "";
                    string dbname = "";
                    _dbc = 0;


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

                        if (n.InnerText == "dbcount")
                        {
                            if (!Int32.TryParse(nn.InnerText, out _dbc))
                                _dbc = 0;
                        }

                        if (n.InnerText == "lastwrite")
                        {
                            if (!Int32.TryParse(nn.InnerText, out _lastwrite))
                                _lastwrite = -1;
                        }

                        if (n.InnerText == "list")
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
                        
                        if (n.InnerText == "images")
                        {
                            foreach (XmlNode nl in nn.ChildNodes)
                            {
                                if (nl.Name == "string")
                                {
                                    string[] ele = nl.InnerText.Split(';');
                                    string[] fn = ele[0].Split('.');
                                    string fnn = fn[0];
                                    if(fn.Length==3)
                                    {
                                        fnn += "." + fn[1].Replace("F", "");
                                    }
                                    else if (fn.Length == 2)
                                    {
                                        fnn = fn[0];
                                        fnn = fnn.Substring(0,fnn.Length-1);
                                    }
                                    _oldilist.Add(fnn, ele[1]);
                                }
                            }
                        }
                        
                    }
                    if (dbname == "")
                        dbname = "Default";

                    if (count > -1 && _oldlist.Count == count)
                    {
                        //OK

                        _Name = _FName = devname + "_" + dbname;

                    }
                    else
                    {
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

        internal int ReadOldProfilesDB(out string Res)
        {
            Res = "";
            _oldlist = new Dictionary<string, string>();

            try
            {
                string get = File.ReadAllText(_FName);
                if (get != null)
                {

                    int count = 0;
                    int lastwrite = 0;
 
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

                        if (n.InnerText == "lastwrite")
                        {
                            if (!Int32.TryParse(nn.InnerText, out lastwrite))
                                lastwrite = -1;
                        }

                        if (n.InnerText == "list")
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

                    }

                    if (count > -1 && _oldlist.Count == count)
                    {
                        //OK
                    }
                    else
                    {
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

        internal void Compare(Dictionary<string, string> mHashes, Dictionary<string, string> mIHashes)
        {
            _sendilist = null;
            _sendlist = null;
            _dellist = null;
            if (mHashes != null)
            {
                _sendlist = new List<string>();
                _sendilist = new List<string>();
                foreach (string k in mHashes.Keys)
                {
                    if (_oldlist.ContainsKey(k))
                    {
                        if (_oldlist[k] == mHashes[k])
                            continue;
                    }
                    _sendlist.Add(k);
                }


                foreach (string k in mIHashes.Keys)
                {
                    if (_oldilist.ContainsKey(k))
                    {
                        if (_oldilist[k] == mIHashes[k])
                            continue;
                    }
                    _sendilist.Add(k);
                }


                // ADD profile if image changed
                foreach (string _id in _sendilist)
                {
                    if (_sendlist.Contains(_id))
                        continue;
                    _sendlist.Add(_id);
                }

                _dellist = new List<string>();

                foreach (string k in _oldlist.Keys)
                {
                    if (!mHashes.ContainsKey(k))
                        _dellist.Add(k);

                }
              
                // dellist
            }

        }
    }

}
