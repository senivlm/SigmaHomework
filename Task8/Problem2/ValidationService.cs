using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem2
{
    public static class ValidationService
    {
        public static bool IsValidIpAddress(string ipAddress)
        {З адресою все складніше
            if (ipAddress != null && ipAddress.Count(ch => ch == '.') == 3)
            {
                return IPAddress.TryParse(ipAddress, out _);
            }
            return false;
        }
    }
}
