/* Copyright 2020 edgewords Ltd
 * www.edgewordstraining.co.uk
 * A simple REST API Server for testing purposes
 */
using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace AspNetSelfHostDemo
{
    class Program
    {
        public static string baseURL = "http://localhost:2002";
        public static string port = "2002";
        public static string uName = "edge";
        public static string uPwd = "edgewords";

        static void Main(string[] args)
        {
            
            if (args.Length == 1)
            {
                baseURL = "http://localhost:" + args[0];
            }
            else if (args.Length == 2)
            {
                uName = args[0];
                uPwd = args[1];
            }
            else if (args.Length == 3)
            {
                baseURL = "http://localhost:" + args[0];
                uName = args[1];
                uPwd = args[2];
            }
   
                
            try
            {
                WebApp.Start<Startup>(baseURL);
                Console.WriteLine("Copyright 2020 Edgewords Ltd.");
                Console.WriteLine("REST Server Started at {0}/api/products or /api/users", baseURL);
                Console.WriteLine("/api/users requires basic_auth user='{0}' password='{1}'", uName,uPwd);
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
