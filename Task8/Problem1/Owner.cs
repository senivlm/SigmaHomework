using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem1
{
    public class Owner
    {
        private int _apartmentNumber;
        public string Surname { get; private set; }
        public int ApartmentNumber
        {
            get => _apartmentNumber;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Apartment number can not be negative!");
                }
                _apartmentNumber = value;
            }
        }
        public Owner() : this("", 0) { }
        public Owner(string surname, int apartmentNumber)
        {
            Surname = surname ?? "";
            ApartmentNumber = apartmentNumber;
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Owner data)
            {
                return ApartmentNumber.Equals(data.ApartmentNumber) &&
                    Surname.Equals(data.Surname);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ApartmentNumber.GetHashCode() ^
                Surname.GetHashCode();
        }
        public override string ToString()
        {
            return $"Apartment number: {ApartmentNumber}\n" +
                $"Surname of the apartment owner: {Surname}\n";
        }
    }
}
