using C8Examples.Data;
using System;
using System.Threading.Tasks;

#nullable enable

namespace C8Examples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new Server(); 
            var client = new Client();

            await (client as IClient).DumpDataAsync();

            
        }
    }
}

#nullable disable