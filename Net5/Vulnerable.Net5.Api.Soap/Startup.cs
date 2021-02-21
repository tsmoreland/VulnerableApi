//
// Copyright � 2021 Terry Moreland
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

using System.ServiceModel.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;
using Vulnerable.Net5.Api.Soap.ServiceContracts;
using Vulnerable.Net5.Api.Soap.Services;
using Vulnerable.Infrastructure;
using Vulnerable.Net5.Infrastructure.DependencyInjection;

namespace Vulnerable.Net5.Api.Soap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            services.AddHttpContextAccessor();

            services.AddDataServices(Configuration);
            services.AddApplicationServices();

            services.AddSoapCore();
            services.AddMvc(x => x.EnableEndpointRouting = false);
            services.AddSingleton<IAddressServiceContact, AddressService>(); // change to scoped if possible, examples all show as singleton but scoped would be more ideal
            services.AddSoapExceptionTransformer((ex) => ex.Message);

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var transportBinding = new HttpTransportBindingElement();
            var textEncodingBinding = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressingAugust2004, System.Text.Encoding.UTF8);
            var soap12Binding = new CustomBinding(transportBinding, textEncodingBinding);

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.UseSoapEndpoint<IAddressServiceContact>("/address.asmx", soap12Binding);
            });

            app.UseMvc();
        }
    }
}
