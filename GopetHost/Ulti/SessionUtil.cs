using GopetHost.Models;
using Microsoft.AspNetCore.Mvc;

namespace GopetHost.Ulti
{
    public static class SessionUtil
    { 
        public static bool IsLoginOK(this ControllerBase controller)
        {
            return !string.IsNullOrEmpty(controller.HttpContext.Session.GetString(nameof(UserData.username)));
        }

        public static void SetLoginOK(this ControllerBase controller, string username)
        {
            controller.HttpContext.Session.SetString(nameof(UserData.username), username);
        }

        public static void LogOut(this ControllerBase controller)
        {
            controller.CleanSession();
        }

        public static void CleanSession(this ControllerBase controller)
        {
            controller.HttpContext.Session.Clear();
        }
    }
}
