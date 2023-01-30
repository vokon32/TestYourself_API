using System.Security.Claims;


namespace TestYourself_API.Helper
{
    public static class ClaimPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
