//
// WebFunc.cs
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
using System.Text;
using System.Net;
using System.IO;

namespace jbplugin
{
    class WebFunc
    {
        public static string BaseUrl(string IP, string Port)
        {
            return "http://" + IP + ":" + Port;
        }

        static public string HttpGet(string uri,out string result)
        {
            string content = null;
            result = "";
            try
            {

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "GET";
                webRequest.KeepAlive = false;
                webRequest.ContentType = "application/xhtml+xml";

                webRequest.ProtocolVersion = HttpVersion.Version11;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
                StringBuilder contentBuilder = new StringBuilder();
                contentBuilder.Append(sr.ReadToEnd());
                content = contentBuilder.ToString();
            }
            catch (WebException /*ex*/)
            {
                result = "Connection Error 1";
                return null;
            }
            catch (Exception /*ee*/)
            {
                // problem
                result = "Connection Error 2";
                return null;
            }
            result = "";
            return content.ToString();
        }


    }
}
