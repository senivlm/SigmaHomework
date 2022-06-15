using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem1
{
    public class PersonData
    {
        public Owner Owner { get; private set; } = new Owner();
        private int[] MeterIndications = new int[4];
        private DateOnly[] MeterReadingDate = new DateOnly[3];

        #region Constructors
        public PersonData() { }
        public PersonData(string lineToParse)
        {
            Parse(lineToParse);
        }
        public PersonData(Owner owner, int[] meterIndications, DateOnly[] meterReadingDate)
        {
            if (!ValidationService.IsPersonDataValid(meterIndications, meterReadingDate, out Exception? exception))
            {
                throw exception!;
            }
            Owner = owner;
            Array.Copy(meterIndications, MeterIndications, MeterIndications.Length);
            Array.Copy(meterReadingDate, MeterReadingDate, MeterReadingDate.Length);
        }
        #endregion

        #region Methods
        public IReadOnlyCollection<int> GetMeterIndications() => Array.AsReadOnly(MeterIndications);
        public IReadOnlyCollection<DateOnly> GetMeterReadingDates() => Array.AsReadOnly(MeterReadingDate);
        public void Parse(string stringToParse)
        {
            var personData = stringToParse.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (personData.Length != 9)
            {
                throw new InvalidDataException("Inсorrect number of arguments!");
            }

            if (!int.TryParse(personData[0], out int apartmentNumber))
            {
                throw new InvalidDataException("Invalid apartment number!");
            }

            for (int i = 2; i < 6; i++)
            {
                if (!int.TryParse(personData[i], out int indication) ||
                    indication < 0 ||
                    (i != 2 && MeterIndications[i - 3] > indication))
                {
                    throw new InvalidDataException($"Invalid meter indication number {i - 1}!");
                }
                MeterIndications[i - 2] = indication;
            }

            for (int i = 6; i < 9; i++)
            {
                if (!DateOnly.TryParse(personData[i], out DateOnly date))
                {
                    throw new InvalidDataException($"Invalid date number {i - 5} of reading the meter!");
                }
                MeterReadingDate[i - 6] = date;
            }
            Owner = new Owner(personData[1], apartmentNumber);
        }
        public override int GetHashCode()
        {
            return Owner.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is PersonData data)
            {
                return Owner.Equals(data.Owner);
            }
            return false;
        }
        public override string ToString()
        {
            var result = Owner.ToString();

            for (int i = 0; i < MeterIndications.Length - 1; i++)
            {
                result += $"Used electricity for {MeterReadingDate[i].ToString("MMMM", CultureInfo.GetCultureInfo("en"))}: {MeterIndications[i + 1] - MeterIndications[i]} kWh\n";
            }

            for (int i = 0; i < MeterReadingDate.Length; i++)
            {
                result += $"Meter reading date for {MeterReadingDate[i].ToString("MMMM", CultureInfo.GetCultureInfo("en"))}: {MeterReadingDate[i].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en"))}\n";
            }

            return result;
        }
        #endregion
    }
}
