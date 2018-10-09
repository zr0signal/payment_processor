using Autofac;
using Autofac.Integration.WebApi;
using PaymentProcessor.Bussiness;
using PaymentProcessor.Bussiness.Gateway;
using PaymentProcessor.Bussiness.Utilities;
using System.Reflection;
using System.Web.Http;

namespace PaymentProcessor.Api
{
    public static class ContainerConfig
    {
        public static IContainer Container { get; set; }

        public static void Configure()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<PaymentProcessorLogic>().As<IPaymentProcessorLogic>();
            builder.RegisterType<PaymentGatewayCheap>().As<IPaymentGatewayCheap>();
            builder.RegisterType<PaymentGatewayExpensive>().As<IPaymentGatewayExpensive>();
            builder.RegisterType<PaymentGatewayPremium>().As<IPaymentGatewayPremium>();
            builder.RegisterType<PaymentValidator>().As<IPaymentValidator>();
            builder.RegisterType<CreditCardValidator>().As<ICreditCardValidator>();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            Container = container;
        }
    }
}