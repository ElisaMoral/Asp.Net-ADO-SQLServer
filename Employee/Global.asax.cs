using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Employee
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            //Get the exception details and log it in the database or event viewer
           // Exception ex = Server.GetLastError();
            // Clear the exception
           // Server.ClearError();
            // Redirect user to Error page
          //  Response.Redirect("~/error.aspx");

        }

    }
}
