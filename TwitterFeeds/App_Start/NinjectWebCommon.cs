using System.Reflection;
using TwitterFeeds.Repository;
using TwitterFeeds.Repository.Interface;
using TwitterFeeds.Service;
using TwitterFeeds.Service.Interface;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TwitterFeeds.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(TwitterFeeds.App_Start.NinjectWebCommon), "Stop")]

namespace TwitterFeeds.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IApiService>().To<ApiService>().InRequestScope();
            kernel.Bind<IUrlBuilder>().To<UrlBuilder>().InRequestScope();
            kernel.Bind<ITwitterRepository>().To<TwitterRepository>().InRequestScope();
            kernel.Bind<IApplicationService>().To<ApplicationService>().InRequestScope();
            kernel.Load(Assembly.GetExecutingAssembly());
        }        
    }
}
