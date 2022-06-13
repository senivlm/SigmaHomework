using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Task8.Problem1
{
    public class Quarter
    {
        private decimal _kilowattPerHourPrice;
        public decimal KilowattPerHourPrice
        {
            get => _kilowattPerHourPrice;
            private set
            {
                if (value < 0)
                {
                    throw new InvalidDataException("Price for kilowatt per hour cannot be negative!");
                }
                _kilowattPerHourPrice = value;
            }
        }
        public int NumberOfQuarter { get; private set; }
        private PersonData[] Records;

        #region Constructors
        public Quarter()
        {
            NumberOfQuarter = 0;
            KilowattPerHourPrice = 0.0m;
            Records = new PersonData[1];
        }
        public Quarter(StreamReader streamReader, decimal kilowattPerHoutPrice)
        {
            KilowattPerHourPrice = kilowattPerHoutPrice;
            var quarterData = streamReader.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToArray();
            if (quarterData == null || quarterData.Length != 2)
            {
                throw new InvalidDataException("Invalid data in 1 line of file");
            }
            Records = new PersonData[quarterData[0]];
            NumberOfQuarter = quarterData[1];
            for (int i = 0; i < Records.Length; i++)
            {
                try
                {
                    string? line = streamReader.ReadLine();
                    if (line == null)
                    {
                        throw new InvalidDataException("Impossible to parse!");
                    }

                    var record = new PersonData(line);

                    var dates = record.GetMeterReadingDates().ToArray();

                    for (int j = 0; j < dates.Length; j++)
                    {
                        if (dates[j].Month != (3 * (NumberOfQuarter - 1) + (j + 1)))
                        {
                            throw new InvalidDataException($"Invalid date number {j + 1} of reading the meter!");
                        }
                    }

                    Records[i] = record;
                }
                catch (InvalidDataException ex)
                {
                    Console.WriteLine(ex.Message + $" Line {i + 2}");
                }
            }
        }
        #endregion

        #region Methods
        public static IEnumerable<PersonData> operator +(Quarter firstQuarter, Quarter secondQuarter)
        {

            if (firstQuarter == null)
            {
                return secondQuarter.Records;
            }
            if (secondQuarter == null)
            {
                return firstQuarter.Records;
            }

            return firstQuarter.Records.Concat(secondQuarter.Records);//Option to implement if you want to return the collection,
                                                                      //not a new object. In this case, you can add data from different
                                                                      //quarters of the year
            
            /*if (firstQuarter == null)//One of possible realizations (operator should return object of type Quarter)
            {
                return secondQuarter;
            }
            if (secondQuarter == null)
            {
                return firstQuarter;
            }

            if (firstQuarter.Equals(secondQuarter))//I think that the possibility of adding information from two classes of
                                                   //electricity metering for a certain quarter of the year should be possible only
                                                   //if the electricity indicators were taken in the same quarter of the year
                                                   //and in both objects the price for electricity is the same
            {
                return new Quarter()
                {
                    NumberOfQuarter = firstQuarter.NumberOfQuarter,
                    KilowattPerHourPrice = firstQuarter.KilowattPerHourPrice,
                    Records = firstQuarter.Records.Union(secondQuarter.Records).ToArray()
                };
            }
            return firstQuarter;*/
        }
        public static Quarter operator -(Quarter firstQuarter, Quarter secondQuarter)
        {
            if (firstQuarter != null && secondQuarter != null)
            {
                firstQuarter.Records = firstQuarter.Records.Except(secondQuarter.Records).ToArray();
            }
            return firstQuarter;
        }
        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Quarter quarter)
            {
                return NumberOfQuarter.Equals(quarter.NumberOfQuarter) &&
                    KilowattPerHourPrice.Equals(quarter.KilowattPerHourPrice);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return NumberOfQuarter.GetHashCode() ^
                KilowattPerHourPrice.GetHashCode();
        }
        public void PrintReportToFile(StreamWriter target)
        {
            ReportCreator.WriteReportToFile(target, Records, NumberOfQuarter);
        }
        public void PrintReportToFile(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintReportToFile(writer);
        }

        public void PrintRecordToFile(StreamWriter target, int apartmentNumber)
        {
            var apartmentInfo = Records?.Where(r => r?.ApartmentNumber == apartmentNumber);
            ReportCreator.WriteReportToFile(target, apartmentInfo, NumberOfQuarter);
        }
        public void PrintRecordToFile(string targetPath, bool append, int apartmentNumber)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintRecordToFile(writer, apartmentNumber);
        }

        public void PrintRecordWithLargestDebt(StreamWriter target)
        {
            var recordWithLargestDebt = Records?.MaxBy(r =>
            {
                if (r != null)
                {
                    var indications = r.GetMeterIndications().ToArray();
                    return indications[3] - indications[0];
                }
                return 0;
            });
            ReportCreator.WriteReportToFile(target, new PersonData[] { recordWithLargestDebt }, NumberOfQuarter);
        }
        public void PrintRecordWithLargestDebt(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintRecordWithLargestDebt(writer);
        }

        public void PrintRecordsWithoutElectricityUsage(StreamWriter target)
        {
            var recordsWithoutElectricityUsage = Records?.Where(r =>
            {
                if (r != null)
                {
                    var indications = r.GetMeterIndications().ToArray();
                    return (indications[3] - indications[0]) == 0;
                }
                return false;
            });
            ReportCreator.WriteReportToFile(target, recordsWithoutElectricityUsage, NumberOfQuarter);
        }
        public void PrintRecordsWithoutElectricityUsage(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintRecordsWithoutElectricityUsage(writer);
        }

        public void PrintExpenseReport(StreamWriter target)
        {
            target.WriteLine($"Quarter number: {NumberOfQuarter}");
            ReportCreator.WriteExpenseReportToFile(target, Records, KilowattPerHourPrice);
        }
        public void PrintExpenseReport(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintExpenseReport(writer);
        }

        public void PrintActivityReport(StreamWriter target)
        {
            target.WriteLine($"Quarter number: {NumberOfQuarter}");
            ReportCreator.WriteActivityReportToFile(target, Records);
        }
        public void PrintActivityReport(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintActivityReport(writer);
        }
        #endregion
    }
}
