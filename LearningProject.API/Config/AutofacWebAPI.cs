using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace LearningProject.API.Config
{

    /// <summary>
    ///The reason why we have separated these into different files and classes is to make them more maintainable.
    /// Each static class will have static methods to get their job done, and they will be called through the hosting layer once
    /// per application life cycle.We’ll invoke these static methods during integration testing.
    /// </summary>
    public class AutofacWebAPI
    {
        /// <summary>
        /// This initialize method will be called by Hosting Layer
        /// The first Initialize method calls the second one by providing an IContainer instance through the RegisterServices private method, which is where our dependencies are registered.
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config,RegisterServices(new ContainerBuilder()));
        }

        /// <summary>
        /// This will be called through the integration tests by providing an IContainer instance.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="container"></param>
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            // Initialize method a new AutofacWebApiDependencyResolver instance, which accepts an implementation of IContainer through its constructor,
            // is assigned to the passed -in HttpConfiguration instance DependencyResolver property.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // registration goes here
            return builder.Build();
        }
    }
}