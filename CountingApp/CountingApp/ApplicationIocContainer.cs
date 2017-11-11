using Autofac;
using CountingApp.Services;
using Xamarin.Auth;

namespace CountingApp
{
    public static class ApplicationIocContainer
    {
        public static IContainer CurrentContainer { get; private set; }

        public static void BuildIocContainer()
        {
            var bld = new ContainerBuilder();
            bld.Register(x => AccountStore.Create()).As<AccountStore>();
            bld.RegisterType<AuthService>().As<IAuthService>().SingleInstance();

            CurrentContainer = bld.Build();
        }
    }
}
