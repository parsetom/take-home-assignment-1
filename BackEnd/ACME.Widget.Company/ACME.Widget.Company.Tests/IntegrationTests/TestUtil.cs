using ACME.Widget.Company.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Tests.IntegrationTests
{
    [SetUpFixture]
    public static class TestUtil
    {
        public static IConfiguration Configuration { get; private set; }
        private static IServiceProvider Services { get; set; }

        public static T Get<T>()
        {
            return Services.GetService<T>();
        }

        [OneTimeSetUp]
        public static void OneTimeSetup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddACMEServices(Configuration);

            Services = services.BuildServiceProvider();
        }
    }
}
