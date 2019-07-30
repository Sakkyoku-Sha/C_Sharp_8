using System;
using System.Collections.Generic;
using System.Text;

namespace C8Examples.Data
{
   enum ServerState
    {
        Accepting,
        Busy
    };

    enum ClientType
    {
        Admin,
        Standard
    };

    enum AccessType
    {
        Allow,
        Deny
    };
}
