using EP.CursoMvc.Infra.CrossCutting.IoC;

[assembly: WebActivator.PostApplicationStartMethod(typeof(EP.CursoMvc.UI.Web.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace EP.CursoMvc.UI.Web.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            SimpleInjectorBootstrapper.Register(container);
        }
    }
}