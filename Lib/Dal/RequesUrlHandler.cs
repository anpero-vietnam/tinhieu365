using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace Dal.reqest
{
    class RequesUrlHandler
    {

        public string GetHttpWebRequest(string url)
        {

            System.Net.HttpWebRequest myRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            myRequest.Method = "GET";
            System.Net.WebResponse myResponse = myRequest.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();         

            return result;
           
        }

    }
  



}
