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

using System.Reflection;
using AutoMapper.Unity;
using MediatR;
using MediatR.Pipeline;
using MediatR.Unity;
using Unity;
using Unity.Lifetime;
using Vulnerable.Application.Contracts.Data;

namespace Vulnerable.Infrastructure
{
    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Registers Application Services with unity container
        /// </summary>
        public static void RegisterApplicationServices(this IUnityContainer container)
        {
            container.RegisterAutoMapper(Assembly.GetExecutingAssembly());

            container.RegisterMediator(new HierarchicalLifetimeManager())
                     .RegisterMediatorHandlers(typeof(ICityRepository).Assembly);

            container.RegisterType(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>), "RequestPreProcessorBehavior");
            container.RegisterType(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>), "RequestPostProcessorBehavior");
            container.RegisterType(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>), "GenericPipelineBehavior");
            container.RegisterType(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>), "GenericRequestPreProcessor");
            container.RegisterType(typeof(IRequestPostProcessor<,>), typeof(GenericRequestPostProcessor<,>), "GenericRequestPostProcessor");
            container.RegisterType(typeof(IRequestPostProcessor<,>), typeof(ConstrainedRequestPostProcessor<,>), "ConstrainedRequestPostProcessor");
        }

    }
}
