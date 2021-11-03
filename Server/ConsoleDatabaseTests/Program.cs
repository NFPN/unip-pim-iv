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

            var create = userService.CreateData(
                new Municipio
                {
                    Nome = "Test",
                    UF = "XX"
                });

            create.Wait();

            var data = userService.GetData(create.Result.Id);
            var removedData = userService.DeleteData(data.Result.Id);

            Console.WriteLine(data.Result);
            Console.WriteLine(removedData.Result);
        }
    }
}
