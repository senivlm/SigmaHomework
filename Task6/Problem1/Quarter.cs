using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Task6.Problem1
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
        public int NumberOfQuarter { get; }
        public PersonData[] Records;

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
