using Microsoft.AspNetCore.Http;

namespace PaperaX.Api.Helpers
{
    public static class CookieHelper
    {
        private const string RefreshTokenKey = "refresh_token";
        private const string CookiePath = "/api/auth";

        public static CookieOptions GetRefreshTokenCookieOptions(IWebHostEnvironment env, DateTimeOffset expires)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = !env.IsDevelopment(),
                SameSite = SameSiteMode.Lax,
                Path = CookiePath,
                Expires = expires
            };
        }

        public static CookieOptions GetDeleteCookieOptions(IWebHostEnvironment env)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = !env.IsDevelopment(),
                SameSite = SameSiteMode.Lax,
                Path = CookiePath,
                Expires = DateTimeOffset.UtcNow.AddDays(-1)
            };
        }

        public static void SetRefreshTokenCookie(HttpContext httpContext, IWebHostEnvironment env, string refreshToken, int days = 7)
        {
            httpContext.Response.Cookies.Append(
                RefreshTokenKey,
                refreshToken,
                GetRefreshTokenCookieOptions(env, DateTimeOffset.UtcNow.AddDays(days)));
        }

        public static void DeleteRefreshTokenCookie(HttpContext httpContext, IWebHostEnvironment env)
        {
            httpContext.Response.Cookies.Delete(
                RefreshTokenKey,
                new CookieOptions
                {
                    Path = CookiePath
                });
        }
    }
}
