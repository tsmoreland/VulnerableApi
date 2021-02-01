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
    
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Moreland.VulnerableSoap.Api.Services;
using SoapCore;

namespace Moreland.VulnerableSoap.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            services.AddHttpContextAccessor();

            services.AddSingleton<IVulnerableService, VulnerableService>();

            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddSoapCore();
            services.AddSoapExceptionTransformer((ex) => ex.Message);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var textBindingElement = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressingAugust2004, System.Text.Encoding.UTF8);
            var httpBindingElement = new HttpTransportBindingElement();
            var soap12Binding = new CustomBinding(textBindingElement, httpBindingElement);

            app.UseSoapEndpoint<IVulnerableService>(path: "/", binding: soap12Binding, SoapSerializer.XmlSerializer);
            app.UseMvc();
        }
    }
}
