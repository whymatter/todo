using System;
using System.Security.Claims;

namespace TodoAPI.Extensions {
    public static class ClaimExtensions {
        public static int GetUserId(this ClaimsPrincipal principal) {
            var userIdClaim = principal.FindFirst("userId");
            return Convert.ToInt32(userIdClaim.Value);
        }
    }
}