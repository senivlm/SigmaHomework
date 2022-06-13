using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Problem1
{// Інформацію про персону і про заміри краще розділяти в різні класи.
    public class PersonData
    {
        public string Surname { get; private set; } = "";
        public int ApartmentNumber { get; private set; } = 0;
        private int[] MeterIndications = new int[4];
        private DateOnly[] MeterReadingDate = new DateOnly[3];

        #region Constructors
        public PersonData() { }
        public PersonData(string lineToParse)
        {
            Parse(lineToParse);
        }
        public PersonData(string surname, int apartmentNumber, int[] meterIndications, DateOnly[] meterReadingDate)
        {// Валідацію даних краще винести в інший файл.
            Surname = surname ?? "";
            ApartmentNumber = apartmentNumber;

            if (MeterIndications.Length != 4)
            {// Виняток краще породити свій. Можна гнучкіше перехоплювати.
                throw new ArgumentException("Invalid number of meter indications!");
            }
            for (int i = 0; i < 4; i++)
            {
                if (meterIndications[i] < 0 || (i != 0 && meterIndications[i - 1] > meterIndications[i]))
                {
                    throw new InvalidDataException($"Invalid meter indication number {i + 1}");
                }
                MeterIndications[i] = meterIndications[i];
            }

            if (MeterReadingDate.Length != 3)
            {
                throw new ArgumentException("Invalid number of meter reading dates!");
            }
            for (int i = 0; i < 3; i++)
            {
                MeterReadingDate[i] = meterReadingDate[i];
            }
        }
        #endregion

        #region Methods
        public IReadOnlyCollection<int> GetMeterIndications() => Array.AsReadOnly(MeterIndications);
        public IReadOnlyCollection<DateOnly> GetMeterReadingDates() => Array.AsReadOnly(MeterReadingDate);
        public void Parse(string stringToParse)
        {// тут теж!!!
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

            ApartmentNumber = apartmentNumber;
            Surname = personData[1];
        }
        public override int GetHashCode()
        {
            return ApartmentNumber.GetHashCode() ^
                Surname.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            PersonData data = (PersonData)obj;
            return ApartmentNumber.Equals(data.ApartmentNumber) &&
                Surname.Equals(data.Surname);
        }
        #endregion
    }
}
