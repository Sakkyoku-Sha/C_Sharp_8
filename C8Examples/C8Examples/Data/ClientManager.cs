using System;
using System.Collections.Generic;
using System.Text;

namespace C8Examples.Data
{
    class ClientManager : IClientManager
    {
        public ISet<IClient> Clients => throw new NotImplementedException();

        public void ConnectClient(IClient client)
        {
            if ((client is IClient { ClientType: ClientType.Admin })) 
                AdminConnect();
            else
                StandardConnect();
        
        }

        private void AdminConnect()
        {
            ///
        }

        private void StandardConnect()
        {
            ///
        }

           
    }
}
