using System;
using System.Collections.Generic;
using System.Text;

namespace C8Examples.Data
{
    class ClientManager : IClientManager
    {
        public ISet<IClient> Clients => new HashSet<IClient>();

        public void ConnectClient(IClient client)
        {
            if ((client is IClient { ClientType: ClientType.Admin })) //Decompositional pattern
                AdminConnect();
            else
                StandardConnect();
        
        }

        private void AdminConnect()
        {
            ///Connect for an admin
        }

        private void StandardConnect()
        {
            ///Connect for the standard user.
        }

        public int getNum() => 10;
    }
}
