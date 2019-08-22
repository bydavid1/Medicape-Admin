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
            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(BaseUrl);

                iNetRequest.Timeout = 5000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
