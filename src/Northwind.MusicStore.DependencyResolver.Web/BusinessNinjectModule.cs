using Ninject.Modules;
using Northwind.Infrastructure.Log4NetProvider;
using Northwind.Infrastructure.WebMatrixSecurity;
using Northwind.MusicStore.BusinessInterfaces;
using log4net;

namespace Northwind.MusicStore.DependencyResolver.Web
{
    public class BusinessNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWebSecurityProvider>().To<WebMatrixWebSecurityProvider>();
            Bind<ISessionProvider>().To<HttpSessionSessionProvider>();

            Bind<ILogProvider>().To<Log4NetLogProvider>();
            Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(BusinessNinjectModule)));

            Container.Initialize(Kernel);
        }

    }
}