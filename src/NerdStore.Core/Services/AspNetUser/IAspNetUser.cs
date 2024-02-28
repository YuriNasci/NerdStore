using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace NerdStore.Core.Services.AspNetUser
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        string GetUserRefreshToken();
        bool IsAuthenticated();
        bool HasRole(string role);
        IEnumerable<Claim> GetClaims();
        HttpContext GetHttpContext();
    }
}
