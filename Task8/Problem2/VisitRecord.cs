using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem2
{
    public class VisitRecord
    {
        public string IP { get; private set; }
        public TimeOnly Time { get; private set; }
        public DayOfWeek Day { get; private set; }
        public VisitRecord() : this("0.0.0.0", default, default) { }
        public VisitRecord(string ip, TimeOnly time, DayOfWeek day)
        {
            if (!ValidationService.IsValidIpAddress(ip))
            {
                throw new ArgumentException("IP address is not valid!");
            }
            IP = ip;
            Time = time;
            Day = day;
        }
        public void Parse(string? line)
        {
            var splittedLine = line?.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (splittedLine == null || splittedLine.Length != 3)
            {
                throw new ArgumentException("Incorrect number of arguments for record of site visit!");
            }

            if (!ValidationService.IsValidIpAddress(splittedLine[0]))
            {
                throw new FormatException("Invalid IP address!");
            }

            if (!TimeOnly.TryParse(splittedLine[1], out TimeOnly time))
            {
                throw new FormatException("Invalid time!");
            }

            if (!Enum.TryParse(typeof(DayOfWeek), splittedLine[2], true, out object? day))
            {
                throw new FormatException("Invalid day of week!");
            }

            IP = splittedLine[0];
            Time = time;
            Day = (DayOfWeek)day;
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is VisitRecord record)
            {
                return IP.Equals(record.IP) &&
                    Time.Equals(record.Time) &&
                    Day.Equals(record.Day);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return IP.GetHashCode() ^
                Time.GetHashCode() ^
                Day.GetHashCode();
        }
        public override string ToString()
        {
            return $"IP address: {IP}\n" +
                $"Time of visit: {Time}\n" +
                $"Day of visit: {Day}\n";
        }
    }
}
