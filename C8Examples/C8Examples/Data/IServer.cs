using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace C8Examples.Data
{
    interface IServer
    {
        IClientManager ClientManager { get; }
        ServerState ServerState { get; }

        AccessType GetAccess(ClientType type);

        public async IAsyncEnumerable<Byte> GetDataStream()
        {
            var randomData = new byte[10000];
            var r = new Random();
            r.NextBytes(randomData);

            foreach (var data in randomData)
            {
                await Task.Delay(200);
                yield return data;
            }
        }
    }
}
