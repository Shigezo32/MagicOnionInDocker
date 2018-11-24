using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Server;
using MagicOnionInDocker.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MagicOnionInDocker.HttpGateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var service = MagicOnionEngine.BuildServerServiceDefinition(new Assembly[] { Assembly.GetAssembly(typeof(MyFirstService)) }, new MagicOnionOptions(true)
            {
                MagicOnionLogger = new MagicOnionLogToGrpcLogger()
            });
            // Add MagicOnionServiceDefinition for reference from Startup.
            services.Add(new ServiceDescriptor(typeof(MagicOnionServiceDefinition), service));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var magicOnion = app.ApplicationServices.GetService<MagicOnionServiceDefinition>();
            var xmlPath = Path.Combine(AppContext.BaseDirectory, "MagicOnionInDocker.ServiceDefinition.xml");
            app.UseMagicOnionSwagger(magicOnion.MethodHandlers, new MagicOnion.HttpGateway.Swagger.SwaggerOptions("MagicOnionInDocker.Server", "Swagger Integration Test", "/")
            {
                XmlDocumentPath = xmlPath
            });
            app.UseMagicOnionHttpGateway(magicOnion.MethodHandlers, new Channel("magiconionindocker.server", 12345, ChannelCredentials.Insecure));
        }
    }
}
