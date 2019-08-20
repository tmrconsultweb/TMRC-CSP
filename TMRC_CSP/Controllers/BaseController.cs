using System.Web.Mvc;
using TMRC_CSP.Logics;

namespace TMRC_CSP.Controllers
{
    public class BaseController 
    {
        /// <summary>
        /// Provides access to the core application services.
        /// </summary>
        private readonly IExplorerProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        protected BaseController(IExplorerProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Provides access to the core application services.
        /// </summary>
        protected IExplorerProvider Provider => provider;
    }
}