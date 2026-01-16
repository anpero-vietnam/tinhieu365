using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Mvc.Controllers;

namespace Security
{
    public static class Store
    {
        public static bool IsInRole(int role)
        {
            try
            {
                return UserProfileControl.IsUserInRole(AppSession.CurentProfile.UserId, role);
            }
            catch (Exception ex)
            {
                Dal.MessengerControl Massage = new Dal.MessengerControl();
                Massage.SendMessagesToRole("addmin",false,"Hệ thống phát hiện lỗi", "Hệ thống tự động phát hiện lỗi tại Security.Store.IsInRole backend code : " + ex.Message,"0");
                return false;
            }

        }
        public static void ReGenerateSessionId()
        {
            var manager = new System.Web.SessionState.SessionIDManager();

            if (HttpContext.Current == null)
            {
                return;
            }

            string oldId = manager.GetSessionID(System.Web.HttpContext.Current);
            string newId = manager.CreateSessionID(System.Web.HttpContext.Current);
            bool isAdd = false, isRedir = false;
            manager.SaveSessionID(System.Web.HttpContext.Current, newId, out isRedir, out isAdd);
            HttpApplication ctx = (HttpApplication)System.Web.HttpContext.Current.ApplicationInstance;
            HttpModuleCollection mods = ctx.Modules;
            System.Web.SessionState.SessionStateModule ssm = (System.Web.SessionState.SessionStateModule)mods.Get("Session");
            System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            System.Web.SessionState.SessionStateStoreProviderBase store = null;
            System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
            foreach (System.Reflection.FieldInfo field in fields)
            {
                if (field.Name.Equals("_store")) store = (System.Web.SessionState.SessionStateStoreProviderBase)field.GetValue(ssm);
                if (field.Name.Equals("_rqId")) rqIdField = field;
                if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
                if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
            }
            object lockId = rqLockIdField.GetValue(ssm);
            if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(System.Web.HttpContext.Current, oldId, lockId);
            rqStateNotFoundField.SetValue(ssm, true);
            rqIdField.SetValue(ssm, newId);
        }
    }
}