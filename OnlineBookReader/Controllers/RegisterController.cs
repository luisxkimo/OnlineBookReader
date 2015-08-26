using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineBookReader.Models;

namespace OnlineBookReader.Controllers
{
    public class RegisterController : Controller
    {
		private ApplicationUserManager _userManager;

	    public RegisterController()
	    {
	    }

	    public RegisterController(ApplicationUserManager userManager)
	    {
		    _userManager = userManager;
	    }

	    [Authorize(Roles = "Admin")]
		public ActionResult Register()
		{
			return View();
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser() { UserName = model.UserName};
				IdentityResult result = await UserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					ViewBag.Registered = "Registered Success!";
					return View("");
				}
				else
				{
					AddErrors(result);
				}
			}
			return View(model);
		}

		private ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			set
			{
				_userManager = value;
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}
    }
}