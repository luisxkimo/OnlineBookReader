using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using OnlineBookReader.DB;
using OnlineBookReader.Services;
using Owin;
using System;
using OnlineBookReader.Models;

namespace OnlineBookReader
{
    public partial class Startup
    {
	    public void ConfigureAuth(IAppBuilder app)
	    {
		    app.CreatePerOwinContext(CustomIdentityContext.Create);
		    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
		    app.UseCookieAuthentication(new CookieAuthenticationOptions
		    {
			    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			    LoginPath = new PathString("/Account/Login"),
			    Provider = new CookieAuthenticationProvider
			    {
				    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
					    validateInterval: TimeSpan.FromMinutes(30),
						regenerateIdentity: (manager, user) => IdendityUserPropertiesService.GenerateUserIdentityAsync(user, manager))
			    }
		    });


			// Para agregar cookies de servicios externos tipo Google, twitter, etc
		    //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
	    }
    }
}