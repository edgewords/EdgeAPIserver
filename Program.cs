using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace AspNetSelfHostDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseURL = "http://localhost:2002";
            try
            {
                WebApp.Start<Startup>(baseURL);
                Console.WriteLine("REST Server Started at {0}/api/products or /api/users", baseURL);
                Console.WriteLine("/api/users requires basic_auth user='edge' password='edgewords'");
                Console.WriteLine("Close Window to stop listening and exit.");
                Console.ReadKey();
            }
            catch (AggregateException aggEx)
            {
                Console.WriteLine("Error opening server. Error: " + aggEx.Message);
                Console.WriteLine();
                Console.WriteLine("Try running Visual Studio as Administrator by right-clicking on the ");
                Console.WriteLine("Visual Studio icon, and selecting \"Run as Administrator\"");
            }
        }

    }
}
