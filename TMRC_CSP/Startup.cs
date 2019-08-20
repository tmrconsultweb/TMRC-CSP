[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(TMRC_CSP.Startup))]
namespace TMRC_CSP
{
    using global::Owin;
    /// <summary>
    /// Manages the application start up.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configured the application.
        /// </summary>
        /// <param name="application">The application.</param>
        public void Configuration(IAppBuilder application)
        {
            this.ConfigureAuth(application);
        }
    }
}