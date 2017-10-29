//
// LogWriter.cs
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
using System.IO;
using System.Runtime.InteropServices;

namespace Exporter
{
    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    /// </summary>
    [ComVisible(false)]
    public class LogWriter
    {
        protected static volatile LogWriter instance;
        private static string logDir = "";//<Path to your Log Dir or Config Setting>;
        private static string logFile = "Exporter.log";// <Your Log File Name or Config Setting>;
        private static int m_iMaxSize = 5;

        /// <summary>
        /// Private constructor to prevent instance creation
        /// </summary>
        private LogWriter() { }

        ~LogWriter()
        {
        }

        public static bool SetDir(string ld)
        {
            logDir = ld;

            string logPath = logDir + "\\" + logFile;

            if (!Directory.Exists(ld))
                Directory.CreateDirectory(ld);

            long lFileSize = 0;
            if (File.Exists(logPath))
            {
                FileInfo fi = new FileInfo(logPath);
                lFileSize = fi.Length;
                lFileSize = lFileSize / (1024 * 1024);
            }

            if (lFileSize > m_iMaxSize)
            {
                string sSuccessorFileName = logPath.Substring(0, logPath.Length - 4) + "_1" + ".log";
                if (File.Exists(sSuccessorFileName))
                    File.Delete(sSuccessorFileName);
                File.Move(logPath, sSuccessorFileName);
            }

            return true;
        }

        /// <summary>
        /// An LogWriter instance that exposes a single instance
        /// </summary>
        public static LogWriter Instance
        {
            get
            {
                // If the instance is null then create one and init the Queue
                if (instance == null)
                {
                    instance = new LogWriter();
                }
                return instance;
            }
        }

        /// <summary>
        /// The single instance method that writes to the log file
        /// </summary>
        /// <param name="message">The message to write to the log</param>
        public void WriteToLog(string message)
        {
            if (logDir == "")
                return;

            lock (this)
            {

                string logPath = logDir + "\\" + logFile;

                Log _logEntry = new Log(message);

                try
                {
                    // This could be optimised to prevent opening and closing the file for each write
                    //   using (FileStream fs = File.Open(logPath, FileMode.Append, FileAccess.Write))
                    {
                        //                        using (StreamWriter log = new StreamWriter(fs))
                        using (StreamWriter log = new StreamWriter(logPath, true))
                        {
                            log.WriteLine(string.Format("{0} {1}\t{2}", _logEntry.LogDate, _logEntry.LogTime, _logEntry.Message));
                        }
                    }

                }
                catch
                {

                }

            }

        }
    }

    /// <summary>
    /// A Log class to store the message and the Date and Time the log entry was created
    /// </summary>
    [ComVisible(false)]
    public class Log
    {
        public string Message { get; set; }
        public string LogTime { get; set; }
        public string LogDate { get; set; }

        public Log(string message)
        {
            Message = message;
            LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            LogTime = DateTime.Now.ToString("HH:mm:ss.fff tt");
        }
    }
}
