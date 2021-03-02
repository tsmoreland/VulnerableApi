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

#if NET48
using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR;
using Vulnerable.Application.Queries.Cities;
using Vulnerable.Domain.Contracts.Data;

using Vulnerable.Net48.Infrastructure.Data;
using Vulnerable.Net48.Infrastructure.Data.Repositories;

namespace Vulnerable.Infrastructure
{
    public static class AutoFacExtensions
    {
        public static void RegisterApplicationServcies(this ContainerBuilder builder)
        {
            // see https://github.com/jbogard/MediatR/wiki#setting-up
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.Resolve(t);
            });
            builder
                .RegisterAssemblyTypes(typeof(ICityRepository).Assembly, typeof(GetCitiesQueryHandler).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void RegisterDataServices(this ContainerBuilder builder)
        {
            builder
                .RegisterType<AddressDbContext>()
                .InstancePerRequest();

            builder
                .RegisterType<CityRepository>()
                .As<ICityRepository>()
                .InstancePerRequest();
            builder
                .RegisterType<ProvinceRepository>()
                .As<IProvinceRepository>()
                .InstancePerRequest();

            builder
                .RegisterType<AddressDbContextOptions>()
                .As<IDbContextOptions>()
                .SingleInstance();
        }
    }
}
#endif
