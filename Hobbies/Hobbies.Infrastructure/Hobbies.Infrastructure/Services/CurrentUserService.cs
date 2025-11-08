using System.Security.Claims;
using Hobbies.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Hobbies.Infrastructure.Services;

public class CurrentUserService:ICurrentUserService
{
   private readonly IHttpContextAccessor _httpContextAccessor;

   public CurrentUserService(IHttpContextAccessor httpContextAccessor)
   {
      _httpContextAccessor = httpContextAccessor;
   }
   
   //user id
   public string UserId => 
      _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";

   // IP adres
   public string UserIpAddress => 
      _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

}