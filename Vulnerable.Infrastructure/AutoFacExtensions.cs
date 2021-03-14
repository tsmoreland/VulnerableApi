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
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR;
using Vulnerable.Application.Queries.Cities;
using Vulnerable.Domain.Contracts.Commands;
using Vulnerable.Domain.Contracts.Queries;
using Vulnerable.Net48.Infrastructure.Data;
using Vulnerable.Net48.Infrastructure.Data.Repositories.Commands;
using Vulnerable.Net48.Infrastructure.Data.Repositories.Queries;

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

            var assemblies = new List<Assembly> {Assembly.GetExecutingAssembly()};
            var referencedAssemblies = assemblies
                .SelectMany(a => a.GetReferencedAssemblies())
                .Where(a => a != null)
                .Select(a =>
                {
                    try
                    {
                        return Assembly.Load(a);
                    }
                    catch (Exception)
                    {
                        return (Assembly?) null;
                    }
                })
                .Where(a => a != null)
                .Select(a => a!)
                .ToArray();
            assemblies.AddRange(referencedAssemblies);

            builder.RegisterAutoMapper(assemblies.Distinct().ToArray());
        }

        public static void RegisterDataServices(this ContainerBuilder builder)
        {
            builder
                .RegisterType<AddressDbContextFactory>()
                .As<IDbContextFactory<AddressDbContext>>()
                .SingleInstance();

            RegisterCommandDataServices();
            RegisterQueryDataServices();

            void RegisterCommandDataServices()
            {
                builder
                    .RegisterType<CityUnitOfWork>()
                    .As<ICityUnitOfWork>()
                    .InstancePerDependency();
            }

            void RegisterQueryDataServices()
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
}
#endif
