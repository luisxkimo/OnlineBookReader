using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineBookReader.DB;

namespace OnlineBookReader
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			Database.SetInitializer(new AccountInitializer());
			Database.SetInitializer(new BookStoreInitializer());

			var customIdentityContext = new CustomIdentityContext();
			customIdentityContext.Database.Initialize(true);

			var bookContext = new BookStoreContext();
			bookContext.Database.Initialize(true);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
