//
// PluginProfiles.cs
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
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.IO;

namespace jbplugin
{
    [ComVisible(false)]
    public class PluginProfiles
    {
        public string ExportDir;
        public Dictionary<string, string> ProfilesHashes;
        public Dictionary<string, string> ProfilesImages;

        public const string DVDP = "DVD Profiler";
        public const string SOFTWARE = "Software";
        public const string DB = "DataBase";

        internal PluginProfiles() {
             ProfilesHashes = new Dictionary<string, string>();
             ProfilesImages = new Dictionary<string, string>();
         }

        public List<string> DVDProfilerDBs()
        {
            List<string> paths = new List<string>();

            try
            {

                string fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DVD Profiler\databases.dod";

                if (File.Exists(fileName))
                {
                    byte[] fileBytes = File.ReadAllBytes(fileName);

                    string s = Encoding.UTF8.GetString(fileBytes, 0, fileBytes.Length);

                    //                    List<string> ll = new List<string>();

                    string v = "";


                    foreach (char b in s)
                    {

                        if (b == 0)
                        {
                            //                            if (v != "")
                            //                              ll.Add(v);
                            if (v != "")
                            {
                                if (Directory.Exists(v))
                                    paths.Add(v);
                                else
                                {
                                    v = v.Substring(0, v.Length - 1);
                                    if (Directory.Exists(v)) {
                                        if(File.Exists(v+@"\collection.dat"))
                                            paths.Add(v);
                                    }
                                }
                            }
                            v = "";
                        }
                        else if (b > 29)
                        {
                            v += (Char)b;
                        }

                    }


                }


            }
            catch (Exception e)
            {

            }
            return paths;
        }

        public RegistryKey GetDVDReg()
         {
             return Registry.CurrentUser.OpenSubKey(SOFTWARE + @"\Invelos Software\" + DVDP);
         }

         public static string Getexportdir()
         {
             string p = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
             p += @"\" + DVDP + "\\" + DB + "s\\";
             RegistryKey rk = Registry.CurrentUser.OpenSubKey(SOFTWARE + @"\Invelos Software\\" + DVDP);
             if (rk != null)
             {
                 p += (string)rk.GetValue("Current" + DB);
                 rk.Close();
             }

             p += "\\export\\";

             if (!Directory.Exists(p))
                 Directory.CreateDirectory(p);

             return p;

         }

         public void Write()
         { 
             if(this.ProfilesHashes!=null)
             {
                 List<string> lines = new List<string>();
                 foreach (string k in this.ProfilesHashes.Keys)
                 {
                     lines.Add(k + ";" + this.ProfilesHashes[k]);                        
                 }
                 File.WriteAllLines(this.ExportDir + "hashes.info", lines.ToArray());
                LogWriter.Instance.WriteToLog(String.Format("Write Hashes to {0}", this.ExportDir));

            }
            if (this.ProfilesImages != null)
             {
                List<string> lines = new List<string>();
                List<string> hlines = new List<string>();
                foreach (string k in this.ProfilesImages.Keys)
                {
                    lines.Add(k + ";" + this.ProfilesImages[k]);
                    FileInfo fi = new FileInfo(this.ProfilesImages[k]);
                    hlines.Add(k + ";" + fi.Length.ToString());
                }
                File.WriteAllLines(this.ExportDir + "images.info", lines.ToArray());
                File.WriteAllLines(this.ExportDir + "ihashes.info", hlines.ToArray());

                LogWriter.Instance.WriteToLog(String.Format("Write I Hashes to {0}", this.ExportDir));


            }
        }

        public string getMd5Hash(string input)
        {
             using (var md5Hasher = MD5.Create())
             {
                 byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
                 return BitConverter.ToString(data).Replace("-","").ToLowerInvariant();
             }
        }
       
    }

}
