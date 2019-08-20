using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using TMRC_CSP.Logics;
using Unity;
using Unity.Lifetime;

namespace TMRC_CSP.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
            new Lazy<IUnityContainer>(() =>
            {
                UnityContainer container = new UnityContainer();
                RegisterTypes(container);
                return container;
            });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IExplorerProvider, ExplorerProvider>(new ContainerControlledLifetimeManager());
        }
    }
}