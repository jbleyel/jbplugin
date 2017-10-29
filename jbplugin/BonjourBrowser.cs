//
// BonjourBrowser.cs
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
using System.Net;
using ZeroconfService;
using System.Collections;
using System.Runtime.InteropServices;

namespace jbplugin
{
    [ComVisible(false)]
    class BonjourBrowser // : IContainerControl
    {

        NetServiceBrowser nsBrowser = new NetServiceBrowser();
        public bool mBrowsing = false;

        List<NetService> ServiceList;
        List<string> txtRecordList;

        string servicelabel;
        private static string hostname;
        List<IPEndPoint> addresses;
        string m_servicename;
        public BonjourBrowser(string servicename)
        {
            m_servicename = servicename;
            //nsBrowser.InvokeableObject = this;
            nsBrowser.DidFindService += new NetServiceBrowser.ServiceFound(nsBrowser_DidFindService);
            nsBrowser.DidRemoveService += new NetServiceBrowser.ServiceRemoved(nsBrowser_DidRemoveService);
            ServiceList = new List<NetService>();
            addresses = new List<IPEndPoint>();
            txtRecordList = new List<string>();

        }
        public NetService resolvingService = null;

        public delegate void ServiceFind(BonjourBrowser service);
        public event ServiceFind DidServiceFind;

        public delegate void ServiceRemoved(BonjourBrowser service);
        public event ServiceRemoved DidServiceRemoved;

        public delegate void ServiceFailed();
        public event ServiceFailed DidServiceFailed;

        public void Stop()
        {
            GC.Collect();
            if (mBrowsing)
            {
                nsBrowser.Stop();
                if (resolvingService != null)
                {
                    resolvingService.Stop();
                    resolvingService = null;
                }
                txtRecordList.Clear();
                addresses.Clear();
                hostname = "";

                ServiceList.Clear();
                mBrowsing = false;
            }        
        }
        public void Start()
        {
            GC.Collect();

            if (!mBrowsing)
            {
                try
                {
                    nsBrowser.SearchForService(m_servicename, "");
                    mBrowsing = true;
                }
                catch (DllNotFoundException /*e*/)
                {
                    if (DidServiceFailed != null && DidServiceFailed.GetInvocationList().Length > 0)
                        DidServiceFailed.Invoke();
                }
            }
            
        }

        public System.ComponentModel.ISynchronizeInvoke InvokeableObject
        {
            get { return nsBrowser.InvokeableObject; }
            set { nsBrowser.InvokeableObject = value; }
        }
        void nsBrowser_DidRemoveService(NetServiceBrowser browser, NetService service, bool moreComing)
        {
            if (DidServiceRemoved != null && DidServiceRemoved.GetInvocationList().Length > 0)
                DidServiceRemoved.Invoke(this);

            ServiceList.Remove(service);
            service.Dispose();
        }
        ArrayList waitingAdd = new ArrayList();

        void nsBrowser_DidFindService(NetServiceBrowser browser, NetService service, bool moreComing)
        {
            service.DidUpdateTXT += new NetService.ServiceTXTUpdated(ns_DidUpdateTXT);
            service.DidResolveService += new NetService.ServiceResolved(ns_DidResolveService);
            service.DidNotResolveService += new NetService.ServiceNotResolved(service_DidNotResolveService);
            try
            {
                service.StartMonitoring();
            }
            catch
            { 
                
            }
            ServiceList.Add(service);

            if (moreComing)
            {
                waitingAdd.Add(service);
            }
            else
            {
                while (waitingAdd.Count > 0)
                {
                    ServiceList.Add((NetService)waitingAdd[0]);
                    waitingAdd.RemoveAt(0);
                }
            }

            if (ServiceList.Count == 0)
                return;

            Resolve(ServiceList[0]);
        }

        void service_DidNotResolveService(NetService service, DNSServiceException exception)
        {
        }

        void ns_DidUpdateTXT(NetService service)
        {
            NetService selected = ServiceList[0];

            if (service == selected)
            {
                servicelabel = service.Name;

                byte[] txt = service.TXTRecordData;
                IDictionary dict = NetService.DictionaryFromTXTRecordData(txt);

                if (dict == null)
                {
                    txtRecordList.Add(String.Format("No TXT Record Available"));
                }
                else
                {
                   
                }
            }
        }

        private void Resolve(NetService service)
        {
            if (resolvingService != null)
            {
                resolvingService.Stop();
            }
            resolvingService = service;
            service.ResolveWithTimeout(5);
        }

        void ns_DidResolveService(NetService service)
        {
            NetService selected = ServiceList[0];

            if (service == selected)
            {
                hostname = String.Format("Hostname: '{0}'", service.HostName);

                if (service.Addresses == null)
                {
                }
                else
                {
                    addresses.Clear();
                    foreach (System.Net.IPEndPoint ep in service.Addresses)
                    {
                        addresses.Add(ep);
                    }
                }
            }

            if (DidServiceFind != null && DidServiceFind.GetInvocationList().Length > 0)
                DidServiceFind.Invoke(this);
        }
      
    }
}
