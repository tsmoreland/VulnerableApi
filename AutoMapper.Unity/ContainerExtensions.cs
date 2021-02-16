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
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Lifetime;

namespace AutoMapper.Unity
{
    public static class ContainerExtensions
    {
        /// <summary>
        /// Registers all <see cref="Profile"/> types in <paramref name="assemblies"/>
        /// with <paramref name="container"/>
        /// </summary>
        public static void RegisterAutoMapper(this IUnityContainer container, params Assembly[] assemblies)
        {
            var mapperConfiguration = new MapperConfiguration(configure =>
            {
                var profiles = assemblies.SelectMany(a => a.GetTypes())
                    .Distinct()
                    .Where(IsProfileType)
                    .Select(t => (Profile?)t.Assembly.CreateInstance(t.FullName!))
                    .Where(p => p != null)
                    .Select(p => p!);

                foreach (var profile in profiles)
                    configure.AddProfile(profile);
                configure.ForAllMaps(ConfigureForAllMaps);
            });
            container.RegisterInstance(mapperConfiguration);

            container.RegisterFactory<IMapper>(c => 
                    c.Resolve<MapperConfiguration>().CreateMapper(), new SingletonLifetimeManager());

            static bool IsProfileType(Type type) =>
                typeof(Profile).IsAssignableFrom(type) &&
                !type.IsAbstract &&
                type.IsGenericTypeDefinition &&
                type.GetConstructor(Type.EmptyTypes) != null &&
                !string.IsNullOrEmpty(type.FullName);

            static void ConfigureForAllMaps(TypeMap map, IMappingExpression expression) =>
                expression.ForAllMembers(options =>
                    options.Condition((source, destination, member) => member != null));
        }
    }
}
