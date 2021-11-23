using BlackRiver.Data;
using BlackRiver.Data.Services;
using BlackRiver.EntityModels;
using System;

namespace ConsoleDatabaseTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var userService = new GenericDataService<Municipio>(new BlackRiverDBContextFactory());

            var create = userService.Create(
                new Municipio
                {
                    Nome = "Test",
                    UF = "XX"
                });

            create.Wait();

            var data = userService.Get(create.Result.Id);
            var removedData = userService.Delete(data.Result.Id);

            Console.WriteLine(data.Result);
            Console.WriteLine(removedData.Result);
        }
    }
}
