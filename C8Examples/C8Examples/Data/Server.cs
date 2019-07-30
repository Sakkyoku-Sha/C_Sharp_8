using System;
using System.Collections.Generic;
using System.Text;

namespace C8Examples.Data
{
    class Server : IServer
    {
        public IClientManager ClientManager => throw new NotImplementedException();

        public ServerState ServerState { get; private set; }
       
        public AccessType GetAccess(ClientType type)
        {
            switch (ServerState, type)
            {
                case (_, ClientType.Standard):
                    break;
              
            };


            return (ServerState, type) switch
            {
                (ServerState.Accepting, _) => AccessType.Allow,             
                (ServerState.Busy, ClientType.Admin) => AccessType.Allow,
                (ServerState.Busy, ClientType.Standard) => AccessType.Deny,
                _ => AccessType.Deny
            };
        }
       
    }
}
