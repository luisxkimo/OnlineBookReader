using OnlineBookReader.DB;
using System.Data.Entity.Migrations;

namespace OnlineBookReader.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<CustomIdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OnlineBookReader.Models.CustomIdentityContext";
        }

	    protected override void Seed(CustomIdentityContext customIdentityContext)
	    {

	    }


    }
}
