//
// PackClass.cs
//
// jbplugin / Exporter
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
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Runtime.InteropServices;
using System.IO.Compression;

using ICSharpCode.SharpZipLib.Core;

namespace Exporter
{
    [ComVisible(false)]
    public static class PackClass
    {

        /*

        public static MemoryStream PackFiles(List<string> FNs, string ZFN)
        {

            MemoryStream outputMemStream = new MemoryStream();
            try
            {

                using (ZipOutputStream s = new ZipOutputStream(outputMemStream))
                {
                    s.SetLevel(9); // 0 - store only to 9 - means best compression
                    foreach (string FN in FNs)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(FN));
                        FileInfo fi = new FileInfo(FN);
                        entry.Size = fi.Length;
                        entry.DateTime = DateTime.Now;

                        byte[] bbuffera = new byte[4096];
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(FN))
                        {
                            StreamUtils.Copy(fs, s, bbuffera);
                        }

                    }
                    // the created file would be invalid.
                    s.Finish();

                    // Close is important to wrap things up and unlock the file.
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteToLog("Error pack file");
                return null;
            }

            return outputMemStream;

        }
        */
        public static string PackFileso(List<string> FNs, string ZFN, List<string> img)
        {

            try
            {

                using (ZipOutputStream s = new ZipOutputStream(File.Create(ZFN)))
                {
                    s.SetLevel(9); // 0 - store only to 9 - means best compression
                  
                    foreach (string FN in FNs)
                    {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(FN));
                    FileInfo fi = new FileInfo(FN);
                    entry.Size = fi.Length;
                    entry.DateTime = DateTime.Now;

                    byte[] bbuffera = new byte[4096];
                    s.PutNextEntry(entry);
                    using (FileStream fs = File.OpenRead(FN))
                    {
                        StreamUtils.Copy(fs, s, bbuffera);
                    }

                    }

                    s.SetLevel(0);

                    foreach (string imgf in img)
                    {

                        if (imgf == null)
                            continue;

                        ZipEntry entry = new ZipEntry(Path.GetFileName(imgf));
                        FileInfo fi = new FileInfo(imgf);
                        entry.Size = fi.Length;
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);

                        byte[] bbuffer = new byte[4096];
                        using (FileStream fs = File.OpenRead(imgf))
                        {
                            StreamUtils.Copy(fs, s, bbuffer);
                        }

                    }
                    s.Finish();
                    s.Close();

                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteToLog("Error pack file");
                return "a";
            }

            return "";
        }

        /*

        public static string ReadStringFromGZFile(string fn)
        {

            byte[] text = File.ReadAllBytes(fn);
            byte[] decompressed = Decompress(text);
            return Encoding.GetEncoding(1252).GetString(decompressed);
        }

        public static void WriteStringToGZFile(string fn, string Text)
        {
            // Convert 10000 character string to byte array.
            byte[] text = Encoding.GetEncoding(1252).GetBytes(Text);

            // Use compress method.
            byte[] compress = Compress(text);

            // Write compressed data.
            File.WriteAllBytes(fn, compress);
        }

        /// <summary>
        /// Compresses byte array to new byte array.
        /// </summary>
        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }


        static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
        */
      
    }
}
