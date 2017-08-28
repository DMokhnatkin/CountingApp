using Autofac;
using CountingApp.Repositories;

namespace CountingApp
{
    public class App
    {
        public static IContainer Container { get; set; }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SessionsRepository>().As<ISessionsRepository>();

            Container = builder.Build();
        }
    }
}