using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Security;

namespace Windsong.VersionManager.Services
{
    public class VersionUtilities : IVersionUtilities
    {
        private readonly IAuthenticationService _authenticationService;

        public VersionUtilities(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public string GetUser()
        {
            var user = _authenticationService.GetAuthenticatedUser();
            return (user == null) ? string.Empty : user.UserName;
        }
    }
}