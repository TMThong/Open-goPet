using GopetHost.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace GopetHost.Ulti
{
    public static class SessionUtil
    {
        public const string SESSION_2FA = "2FA_LOGIN";
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
            controller.HttpContext.Session.SetInt32(nameof(UserData.role), user.role);
        }

        public static void SetRole(this ControllerBase controller, UserData user)
        {
            controller.HttpContext.Session.SetInt32(nameof(UserData.role), user.role);
        }

        public static int GetUserRole(this HttpContext context)
        {
            return context.Session.GetInt32(nameof(UserData.role)).Value;
        }

        public static void LogOut(this ControllerBase controller)
        {
            controller.CleanSession();
        }

        public static void CleanSession(this ControllerBase controller)
        {
            controller.HttpContext.Session.Clear();
        }

        public static void SetSecretKey(this ControllerBase controller, string secretKey)
        {
            controller.HttpContext.Session.SetString("secretKey", secretKey);
        }

        public static string GetSecretKey(this ControllerBase controller)
        {
            return controller.HttpContext.Session.GetString("secretKey");
        }

        public static string GetSecretKey(this HttpContext context)
        {
            return context.Session.GetString("secretKey");
        }

        public static void SetHasLogin2FAOK(this HttpContext context, UserData user)
        {
            if (user.secretKey != null)
            {
                context.Session.SetInt32(SESSION_2FA, 0);
            }
        }

        public static void SetLogin2FAOK(this HttpContext context, UserData user)
        {
            if (user.secretKey != null)
            {
                context.Session.SetInt32(SESSION_2FA, 1);
            }
        }

        public static bool IsNeedLogin2FA(this HttpContext context)
        {
            return context.Session.GetInt32(SESSION_2FA).HasValue && context.Session.GetInt32(SESSION_2FA).Value == 0;
        }
    }
}
