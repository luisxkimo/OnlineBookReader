using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OnlineBookReader.Models;

namespace OnlineBookReader.Services
{
	public class IdendityUserPropertiesService
	{
		public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser user, UserManager<ApplicationUser> manager)
		{
			manager.UserValidator = new UserValidator<ApplicationUser>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = false
			};
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}
	}
}