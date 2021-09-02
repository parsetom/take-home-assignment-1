using ACME.Widget.Company.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACME.Widget.Company.Infra
{
    public static class DependencyResolution
    {
        public static void AddACMEServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ACMEDbContext>((options) =>
            {
                options.UseSqlServer("ConnectionStrings.ACME.Database");
            });
        }
    }
}
