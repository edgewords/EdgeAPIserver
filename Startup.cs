using System;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

namespace AspNetSelfHostDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Can use this to add to the pipeline if you want:
            app.Use(async (context, next) =>
            {
                // Add a Custom Header, just as an example
                context.Response.Headers["Company"] = "Edgewords Ltd";

                // Call next middleware
                await next.Invoke();
            });

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "EdgewordsApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // we remove the default xml formatter & force json formatting as default
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

            //add our filter to enforce basic auth globally
            //we have not done this, as we just decorate the controller we want to appy basic auth to
            //with [BasicAuthentication]

            //config.Filters.Add(new BasicAuthenticationAttribute());

            // Build Web Api
            app.UseWebApi(config);
        }
    }
}
