using System;

namespace XUniversity.Utils
{
    public class UserSession
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public static class SessionManager
    {
        public static UserSession CurrentUser { get; set; }
    }
} 