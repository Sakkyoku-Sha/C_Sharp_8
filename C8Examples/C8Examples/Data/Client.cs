using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace C8Examples.Data
{
    class Client : IClient
    {
        public IServer Connection { get; private set; }

        public ClientType ClientType { get; private set; }

        public Client()
        {
            Connection = new Server(); //Must have a instantiation in the constructor for the class for the object to be 
        }

        public DateTime? LastConnectionDate => new DateTime();

        public void SwapServer(IServer? server)
        {
            Connection = server; //Can't assign nullable to non-nullable without a warning.
        }
    }
}

#nullable disable