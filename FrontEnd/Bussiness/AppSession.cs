using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Controllers
{
    public class AppSession
    {
        static Dal.UserProfileControl userControl = new Dal.UserProfileControl();
        public static UserProfile CurentProfile
        {
            get
            {
                try
                {
                    return userControl.GetUserProfileFromCookie();
                }
                catch (Exception)
                {

                    return new UserProfile();
                }

            }

        }
        //Singleton  UserName

        #region commont function
        private static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }
        private static bool ExistValue(string key)
        {
            return (Context != null && Context.Session[key] != null);
        }
        private static object GetValue(string key)
        {
            if (Context != null && Context.Session[key] != null)
            {
                return Context.Session[key];
            }
            else
            {
                return null;
            }
        }

        private static void SetValue(string key, object value)
        {
            if (Context != null && Context.Session != null)
            {
                Context.Session[key] = value;
            }
        }
        #endregion commont function
    }
}