using System.Reflection;
using System.Web.Http;

using Microsoft.Owin;

using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

using Owin;

using TicTacToe.Data;
    
[assembly: OwinStartup(typeof(TicTacToe.Web.Startup))]

namespace TicTacToe.Web
{
    using TicTacToe.GameLogic;
    using TicTacToe.Web.Infrastructure;
    using Microsoft.AspNet.SignalR;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            // Install-Package Microsoft.AspNet.SignalR in the console to match versions
            app.MapSignalR();

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
