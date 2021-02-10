//
// Copyright © 2021 Terry Moreland
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vulnerable.Net5.Data.Infrastructure;

namespace Vulnerable.Net5.Data
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAdressRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContextFactory<AddressDbContext>(BuildDbContextOptions);
            services.AddDbContext<AddressDbContext>(BuildDbContextOptions);

            void BuildDbContextOptions(DbContextOptionsBuilder options)
            {
                options.UseSqlite(configuration.GetConnectionString("AddressDatabase"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(AddressDbContext).Assembly.GetName().Name));
                options.LogTo(Console.WriteLine, LogLevel.Information);
            }

            services.AddScoped(provider =>
                provider.GetRequiredService<IDbContextFactory<AddressDbContext>>().CreateDbContext());


            return services;
        }
    }
}
