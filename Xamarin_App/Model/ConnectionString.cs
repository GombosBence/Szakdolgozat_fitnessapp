using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Szakdolgozat.Model
{
    public class ConnectionString
    {

        // http://10.0.2.2:5016/
        // http://192.168.107.132:45457/
        private string connection = "https://aspcorewebapi.azurewebsites.net/";



        public static string getLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                return ip.ToString();
            }
            throw new Exception("Not network adapters with Ipv4");
        }
        public string getConnection()
        {
            return connection;
        }
    }
}