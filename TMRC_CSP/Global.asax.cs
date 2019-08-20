using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TMRC_CSP.App_Start;
using TMRC_CSP.Logics;
using TMRC_CSP.ViewModel.PartnerRepository;

using Unity;

namespace TMRC_CSP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IExplorerProvider provider;

            try
            {

                AreaRegistration.RegisterAllAreas();
                GlobalConfiguration.Configure(WebApiConfig.Register);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);

                System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

                // intialize our application domain PartnerCenterClient and PortalLocalization
                Task.Run(() => ApplicationDomain.BootstrapAsync()).Wait();


                // intialize our User domain PartnerCenterClient and PortalLocalization
                //Task.Run(() => ApplicationDomain.UserAsync()).Wait();


                Database.SetInitializer<ViewModel.Context.ConnectionStringsContext>(null);


                provider = UnityConfig.Container.Resolve<IExplorerProvider>();

                // intialize our application domain
                Task.Run(() => ApplicationDomain.InitializeAsync()).Wait();
            }
            finally
            {
                provider = null;
            }
        }
    }
}
