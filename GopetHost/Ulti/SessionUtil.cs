using GopetHost.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace GopetHost.Ulti
{
    public static class SessionUtil
    { 
        public static bool IsLoginOK(this ControllerBase controller)
        {
            return controller.HttpContext.Session.GetInt32(nameof(UserData.user_id)).HasValue;
        }

        public static bool IsLoginOK(this HttpContext context)
        {
            return context.Session.GetInt32(nameof(UserData.user_id)).HasValue;
        }

        public static void SetLoginOK(this ControllerBase controller, UserData user)
        {
            controller.HttpContext.Session.SetInt32(nameof(UserData.user_id), user.user_id);
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
