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
                    Records[i] = new PersonData();
                    string? line = streamReader.ReadLine();
                    if (line == null)
                    {
                        throw new InvalidDataException("Impossible to parse!");
                    }
                    Records[i].Parse(line);
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
            target.WriteLine($"Quarter number: {NumberOfQuarter}");
            ReportCreator.WriteReportToFile(target, Records);
        }
        public void PrintReportToFile(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintReportToFile(writer);
        }

        public void PrintRecordToFile(StreamWriter target, int apartmentNumber)
        {
            target.WriteLine($"Quarter number: {NumberOfQuarter}");
            var apartmentInfo = Records?.Where(r => r?.ApartmentNumber == apartmentNumber);
            ReportCreator.WriteReportToFile(target, apartmentInfo);
        }
        public void PrintRecordToFile(string targetPath, bool append, int apartmentNumber)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintRecordToFile(writer, apartmentNumber);
        }

        public void PrintRecordWithLargestDebt(StreamWriter target)
        {
            target.WriteLine($"Quarter number: {NumberOfQuarter}");
            var recordWithLargestDebt = Records?.MaxBy(r => r.OutputIndication - r.InputIndication);
            ReportCreator.WriteReportToFile(target, new PersonData[] { recordWithLargestDebt });
        }
        public void PrintRecordWithLargestDebt(string targetPath, bool append)
        {
            using var writer = new StreamWriter(targetPath, append);
            PrintRecordWithLargestDebt(writer);
        }

        public void PrintRecordsWithoutElectricityUsage(StreamWriter target)
        {
            target.WriteLine($"Quarter number: {NumberOfQuarter}");
            var recordsWithoutElectricityUsage = Records?.Where(r => r.OutputIndication - r.InputIndication == 0);
            ReportCreator.WriteReportToFile(target, recordsWithoutElectricityUsage);
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
