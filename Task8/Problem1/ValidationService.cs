using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem1
{
    public static class ValidationService
    {
        public static bool IsPersonDataValid(int[] meterIndications, DateOnly[] meterReadingDate, out Exception? exception)
        {
            if (meterIndications != null && meterIndications.Length != 4)
            {
                exception = new ArgumentException("Invalid number of meter indications!");
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if (meterIndications[i] < 0 || (i != 0 && meterIndications[i - 1] > meterIndications[i]))
                {
                    exception = new InvalidDataException($"Invalid meter indication number {i + 1}");
                    return false;
                }
            }
            if (meterReadingDate.Length != 3)
            {
                exception = new ArgumentException("Invalid number of meter reading dates!");
                return false;
            }
            exception = null;
            return true;
        }
    }
}
