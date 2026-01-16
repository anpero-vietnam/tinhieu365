using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using adminv2._4.Models;

namespace adminv2._4
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
         
            // string facebookappidlogin= system.configuration.configurationmanager.appsettings["facebookappidlogin"];
            //string facebookappsecretlogin = system.configuration.configurationmanager.appsettings["facebookappsecretlogin"];
            //oauthwebsecurity.registerfacebookclient(appid: facebookappidlogin,appsecret: facebookappsecretlogin);

            //string googleclientidlogin = system.configuration.configurationmanager.appsettings["googleclientidlogin"];
            //string googleclientsecreclogin = system.configuration.configurationmanager.appsettings["googleclientsecreclogin"];            
            //var client = new dotnetopenauth.googleoauth2.googleoauth2client(googleclientidlogin, googleclientsecreclogin);
            //var extradata = new dictionary<string, object>();
            //oauthwebsecurity.registerclient(client, "google", extradata);
        }
    }
}
