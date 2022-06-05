using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Problem1
{
    public class PersonData
    {
        public string Surname { get; private set; }
        public int ApartmentNumber { get; private set; }
        public int InputIndication { get; private set; }
        public int OutputIndication { get; private set; }
        public readonly DateOnly[] MeterReadingDate = new DateOnly[3];

        #region Constructors
        public PersonData() : this("", 0, 0, 0, default, default, default) { }
        public PersonData(string surname, int apartmentNumber, int inputIndication, int outputIndication, params DateOnly[] meterReadingDate)
        {
            Surname = surname ?? "";
            ApartmentNumber = apartmentNumber;
            InputIndication = inputIndication;
            OutputIndication = outputIndication;
            for (int i = 0; i < meterReadingDate.Length; i++)
            {
                MeterReadingDate[i] = meterReadingDate[i];
            }
        }
        #endregion

        #region Methods
        public void Parse(string stringToParse)
        {
            var personData = stringToParse.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (personData.Length != 7)
            {
                throw new InvalidDataException("Inсorrect number of arguments!");
            }
            if (!int.TryParse(personData[0], out int apartmentNumber))
            {
                throw new InvalidDataException("Invalid apartment number!");
            }
            ApartmentNumber = apartmentNumber;
            Surname = personData[1];
            if (!int.TryParse(personData[2], out int inputIndication))
            {
                throw new InvalidDataException("Invalid input indicator of meter!");
            }
            InputIndication = inputIndication;
            if (!int.TryParse(personData[3], out int outputIndication))
            {
                throw new InvalidDataException("Invalid output indicator of meter!");
            }
            OutputIndication = outputIndication;
            for (int i = 4; i < 7; i++)
            {
                if (!DateOnly.TryParse(personData[i], out DateOnly date))
                {
                    throw new InvalidDataException($"Invalid date number {i - 3} of reading the meter!");
                }
                MeterReadingDate[i - 4] = date;
            }
        }
        public override int GetHashCode()
        {
            return ApartmentNumber.GetHashCode() ^
                Surname.GetHashCode() ^
                InputIndication.GetHashCode() ^
                OutputIndication.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            PersonData data = (PersonData)obj;
            return ApartmentNumber.Equals(data.ApartmentNumber) &&
                Surname.Equals(data.Surname) &&
                InputIndication.Equals(data.InputIndication) &&
                OutputIndication.Equals(data.OutputIndication);
        }
        #endregion
    }
}
