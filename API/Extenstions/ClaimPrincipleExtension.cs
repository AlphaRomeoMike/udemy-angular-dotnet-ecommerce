using System.Security.Claims;

namespace API.Extenstions
{
  public static class ClaimPrincipleExtension
  {
    public static string RetrieveEmailFromPrinciple(this ClaimsPrincipal user)
    {
      return user?.FindFirst(ClaimTypes.Email).Value;
    }
  }
}