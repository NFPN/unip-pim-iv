using Microsoft.Owin.Hosting;
using System;

namespace HostAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: get api from file
            using (WebApp.Start<BlackRiver.API.Startup>("http://localhost:9090"))
            {
                Console.WriteLine("WEB Server ON");
                Console.WriteLine("Pressione qualquer tecla para SAIR");
                Console.ReadKey();
            }
        }
    }
}
