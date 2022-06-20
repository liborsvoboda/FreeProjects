using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPublicIPOwner.Classes
{
    class IPList
    {
        public string ip { get; set; }

    }

    class Config
    {
        public string checkOwnerIpUrl { get; set; } = "http://ip-api.com/json/";

    }
}
