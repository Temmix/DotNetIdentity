using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace CustomizedUsers.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstname(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Firstname");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserLastname(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Lastname");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserTelephone(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Telephone");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}