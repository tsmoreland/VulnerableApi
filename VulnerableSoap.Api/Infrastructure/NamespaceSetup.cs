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

namespace Moreland.VulnerableSoap.Api.Infrastructure
{
    public record TypePathPair(Type Interface, string Path);

    // todo: rework this to use services to setupt his up with injected logger and configurtion
    public static class NamespaceSetup
    {
        public static void Configure(IConfiguration configuration, params TypePathPair[] typePathPairs)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var @namespace = configuration["SoapSettings:Namespace"] ?? string.Empty;
            if (@namespace is not {Length: > 0})
                return;

            foreach (var (type, path) in typePathPairs)
                SetNamespace(type, @namespace, path);
        }

        private static void SetNamespace(Type type, string namespaceUrl, string path)
        {
            var contract = type.GetCustomAttribute<ServiceContractAttribute>();
            if (contract == null)
                return;
            contract.Namespace = namespaceUrl + path;
        }
    }
}
