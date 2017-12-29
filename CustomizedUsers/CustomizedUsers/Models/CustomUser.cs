using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

// Replace all ApplicationUser in IdentityModels.cs and IdentityConfig.cs files 
// You can add more profile property on this file
// Added IdentityExtension.cs in Extensions folder 

namespace CustomizedUsers.Models
{
    public class CustomUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Telephone { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CustomUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Firstname", this.Firstname));
            userIdentity.AddClaim(new Claim("Lastname", this.Lastname));
            userIdentity.AddClaim(new Claim("Telephone", this.Telephone));
            userIdentity.AddClaim(new Claim("Email", this.Email));

            return userIdentity;
        }
    }
}