//
// Form1.cs
//
// jbplugin / DoImport
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
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace DoImport
{
    public partial class Form1 : Form
    {

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

            Debug.Print(cn);

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
                    if (text.Contains("Add Multiple") || text.Contains("Mehrere EAN"))
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            int max = 0;
            while (!CheckWindow() && max < 50)
            {
                Thread.Sleep(1000);
                max++;
            }

            Thread.Sleep(2000);
            if (bAddDVDsWindowisopen == 1)
            {
                WindowEnumDelegate del = new WindowEnumDelegate(WindowEnumProc);
                EnumChildWindows(AddDVDsWindow, del, 1);
                if (bAddDVDsWindowisopen == 2)
                {

                    // button is visible
                    SendMessage(AddDVDsWindow, WM_LBUTTONDOWN, 0, 0);
                    PostMessage(AddDVDsWindow, WM_LBUTTONUP, 0, 0);

                    bAddDVDsWindowisopen = 3;
                }
            }

            if (bAddDVDsWindowisopen == 3)
            {

                SetForegroundWindow(AddDVDsWindow);
                foreach (string ee in Environment.GetCommandLineArgs())
                {
                    if (ee.ToLower().Contains("exe"))
                        continue;

                    if (ee.Trim() != "")
                        SendKeys.SendWait(ee.Trim() + "{ENTER}");
                }

            }
            this.Close();
 
        }
    }
}
