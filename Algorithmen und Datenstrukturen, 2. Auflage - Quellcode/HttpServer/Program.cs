using System;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            var localPath = args[1];
 
            Console.WriteLine("Starting 'Very Simple Webserver' at port {0}, path \"{1}\"", port, localPath);

            new HttpServer(port, localPath).Start();
        }
    }
}
