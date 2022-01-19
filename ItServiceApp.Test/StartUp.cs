using ItServiceApp.MapperProfiles;
using ItServiceApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace ItServiceApp.Test
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPaymentService, IyzicoPaymentService>();
            services.AddAutoMapper(options =>
            {
                options.AddProfile(typeof(PaymentProfile));
            });
        }
    }
}
