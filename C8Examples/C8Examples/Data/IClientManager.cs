using System.Collections.Generic;

namespace C8Examples.Data
{
    internal interface IClientManager
    {
        ISet<IClient> Clients { get; }

        public void ConnectClient(IClient client);

        public int getNum() => 5;
    }
}