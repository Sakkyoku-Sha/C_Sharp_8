using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



#nullable enable

namespace C8Examples.Data
{
    interface IClient
    {
        IServer Connection { get; }
        DateTime? LastConnectionDate { get; }
        ClientType ClientType { get; }

        void SwapServer(IServer server);

        async Task DumpDataAsync()
        {        
            await foreach (var data in Connection.GetDataStream())
            {
                System.Console.WriteLine(data);
            }           
        }    
    }
}

#nullable restore