﻿//This sets all the configuration for the web service
//wrapped up in a 'Startip' object, called by the Main Program.cs
using System;
using System.Web.Http;
using EdgeAPIserver;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
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
            // force either XML or JSON as a response format
            //you could comment all this out & it will automatically return xml or json depending on the Accept or Content-Type header
            if (Program.resFormat == "xml")
            {
                //we remove the json formatter to force response to XML
                config.Formatters.Remove(config.Formatters.JsonFormatter);
            }
            else
            {
                // we remove the default xml formatter & force json formatting as default
                config.Formatters.Remove(config.Formatters.XmlFormatter);
                config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            }

            //add our filter to enforce basic auth globally (if below uncommented)
            //we have not done this, as we just decorate the controller we want to appy basic auth to with [BasicAuthentication]
            //config.Filters.Add(new BasicAuthenticationAttribute());

            // Build Web Api configuration
            app.UseWebApi(config);

            // File Server setup
            // use the default url to provide an index.html with a couple of files you can download
            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = { "index.html" } },
                FileSystem = new PhysicalFileSystem("Assets"),
                StaticFileOptions = { ContentTypeProvider = new CustomContentTypeProvider() }
            };

            app.UseFileServer(options);
        }
    }
}
