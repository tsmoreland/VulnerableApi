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
using System.Reflection;
using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Moreland.VulnerableSoap.Api.Address
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAddressService(this IServiceCollection services, IConfiguration configuration, Action<AddressServiceOptions> optionsBuilder)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var options = new AddressServiceOptions();
            optionsBuilder.Invoke(options);

            var @namespace = configuration[options.SettingsKey];
            if (@namespace is not {Length: >0})
                return services;

            foreach (var (type, path) in options.TypePathPairs)
                SetNamespace(type, @namespace, path);

            return services;

            static void SetNamespace(Type type, string namespaceUrl, string path)
            {
                var contract = type.GetCustomAttribute<ServiceContractAttribute>();
                if (contract == null)
                    return;
                contract.Namespace = namespaceUrl + path;
            }
        }
    }
}
