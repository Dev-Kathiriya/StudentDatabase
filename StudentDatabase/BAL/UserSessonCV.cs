namespace StudentDatabase.BAL
{
    public static class UserSessonCV
    {
        private static IHttpContextAccessor _contextAccessor;
        static UserSessonCV() => _contextAccessor = new HttpContextAccessor();
        public static string? Username() => (_contextAccessor != null && _contextAccessor.HttpContext != null) ? _contextAccessor.HttpContext.Session.GetString("Username") : null;
        public static string? UserPassword() => (_contextAccessor != null && _contextAccessor.HttpContext != null) ? _contextAccessor.HttpContext.Session.GetString("UserPassword") : null;
        public static string? Email() => (_contextAccessor != null && _contextAccessor.HttpContext != null) ? _contextAccessor.HttpContext.Session.GetString("Email") : null;
        public static int? UserId() => (_contextAccessor != null && _contextAccessor.HttpContext != null) ? _contextAccessor.HttpContext.Session.GetInt32("UserId") : null;
    }
}
