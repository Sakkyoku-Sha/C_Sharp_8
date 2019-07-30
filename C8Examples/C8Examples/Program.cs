using C8Examples.Data;
using System;
using System.Threading.Tasks;

#nullable enable

namespace C8Examples
{
    class Program
    {
        static async Task Main()
        {
            var client = new Client();
            //var cm = new ClientManager();

            await (client as IClient).DumpDataAsync();

         
        }
    }
}

#nullable disable