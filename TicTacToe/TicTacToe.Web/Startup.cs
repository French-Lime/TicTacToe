using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(TicTacToe.Web.Startup))]
namespace TicTacToe.Web
{

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;
    using System.Reflection;
    using System.Web.Http;

    using TicTacToe.Data;
    using TicTacToe.GameLogic;
    using TicTacToe.Web.Infrastructure;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            // Install-Package Microsoft.AspNet.SignalR in the console to match versions
            //app.MapSignalR();

            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });

            ConfigureAuth(app);
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);
            
        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterMappings(kernel);
            return kernel;
        }

        private static void RegisterMappings(StandardKernel kernel)
        {
            kernel.Bind<ITicTacToeData>().To<TicTacToeData>()
                .WithConstructorArgument("context", c => new TicTacToeDbContext());

            kernel.Bind<IGameResultValidator>().To<GameResultValidator>();

            kernel.Bind<TicTacToe.Web.Infrastructure.IUserIdProvider>().To<AspNetUserIdProvider>();
        }
    }
}
