using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Clinic.Clases
{
    public class Connection
    {
        public string BaseUrl = "http://192.168.22.146";

        public bool TestConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }

            return false;
        }
    }
}
