using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineBookReader.Models;

namespace OnlineBookReader.DB
{
	public class CustomIdentityContext : IdentityDbContext<ApplicationUser>
	{
		public CustomIdentityContext()
			: base("DefaultConnection", false)
		{
		}

		public static CustomIdentityContext Create()
		{
			return new CustomIdentityContext();
		}


	}

	public class AccountInitializer : DropCreateDatabaseAlways<CustomIdentityContext>
	{

		protected override void Seed(CustomIdentityContext context)
		{
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

			CreateDefaultRoles(context);

			CreateDefaultUsers(userManager);

		}

		private static void CreateDefaultUsers(UserManager<ApplicationUser> userManager)
		{
			var adminUser = new ApplicationUser { UserName = "admin" };
			var adminUserCreateResult = userManager.Create(adminUser, "abc123");

			var normalUser = new ApplicationUser { UserName = "luis" };
			var normalUserCreateResult = userManager.Create(normalUser, "luis123");

			if (adminUserCreateResult.Succeeded)
			{
				userManager.AddToRole(adminUser.Id, "admin");
			}

			if (normalUserCreateResult.Succeeded)
			{
				userManager.AddToRole(normalUser.Id, "user");
			}
		}

		private static void CreateDefaultRoles(CustomIdentityContext context)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			if (!roleManager.RoleExists("Admin"))
			{
				roleManager.Create(new IdentityRole("Admin"));
			}

			if (!roleManager.RoleExists("User"))
			{
				roleManager.Create(new IdentityRole("User"));
			}
		}
	}

}