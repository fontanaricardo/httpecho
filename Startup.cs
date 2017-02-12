using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Extensions;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Features;

namespace httpecho
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Run(async (context) =>
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"*** Start request received at {DateTime.Now} ***\n");
                
                sb.AppendLine($"URL: {UriHelper.GetDisplayUrl(context.Request)}");
                
                var IpAddress = context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
                sb.AppendLine($"IP: {IpAddress}");
                
                sb.AppendLine($"METHOD: {context.Request.Method}");
                
                sb.AppendLine($"HEADERS:");                
                context.Request.Headers.ToList().ForEach(h => sb.AppendLine($"\t{h.Key}: {h.Value}") );

                StreamReader reader = new StreamReader(context.Request.Body);
                sb.AppendLine($"BODY: \n\t{reader.ReadToEnd()}");

                sb.AppendLine($"\n*** End request received at {DateTime.Now} ***");
                
                Console.WriteLine($"{sb.ToString()}");
                await context.Response.WriteAsync(sb.ToString());
            });
        }
    }
}
