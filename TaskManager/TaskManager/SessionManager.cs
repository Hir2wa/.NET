using System;

namespace TaskManager
{
    public static class SessionManager
    {
        public static string CurrentUserEmail { get; set; }
        public static string CurrentUsername { get; set; }
        public static bool IsAdmin { get; set; }
        public static bool IsLoggedIn { get; set; }

        public static void Login(string email, string username, bool isAdmin)
        {
            CurrentUserEmail = email;
            CurrentUsername = username;
            IsAdmin = isAdmin;
            IsLoggedIn = true;
        }

        public static void Logout()
        {
            CurrentUserEmail = null;
            CurrentUsername = null;
            IsAdmin = false;
            IsLoggedIn = false;
        }

        public static string GetDisplayName()
        {
            if (!string.IsNullOrEmpty(CurrentUsername))
                return CurrentUsername;
            return CurrentUserEmail ?? "User";
        }
    }
}
