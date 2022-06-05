using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Task6.Problem1
{
    public static class ReportCreator
    {
        public static void WriteReportToFile(StreamWriter target, IEnumerable<PersonData>? data)
        {
            var table = new ConsoleTable(new ConsoleTableOptions
            {
                Columns = new[] {
                    "Apartment number",
                    "Surname of the apartment owner",
                    "Input reading of the electric meter",
                    "Output reading of the electric meter",
                    "Meter reading date for the first month of the quarter",
                    "Meter reading date for the second month of the quarter",
                    "Meter reading date for the third month of the quarter"
                },
                EnableCount = false,
                NumberAlignment = Alignment.Left
            });

            if (data != null)
            {
                foreach (var record in data)
                {
                    if (record != null && !record.Equals(new PersonData()))
                    {
                        table.AddRow(
                            record.ApartmentNumber,
                            record.Surname,
                            $"{record.InputIndication} kWh",
                            $"{record.OutputIndication} kWh",
                            record.MeterReadingDate[0].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en")),
                            record.MeterReadingDate[1].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en")),
                            record.MeterReadingDate[2].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en")));//uk-UA - ukrainian culture
                    }
                }
            }

            target.Write(table);
        }

        public static void WriteExpenseReportToFile(StreamWriter target, IEnumerable<PersonData>? data, decimal kilowattPerHourPrice)
        {
            target.WriteLine($"Price for kilowatt per hour: {kilowattPerHourPrice} UAH");
            var table = new ConsoleTable(new ConsoleTableOptions
            {
                Columns = new[] {
                    "Apartment number",
                    "Surname of the apartment owner",
                    "Electricity consumption for the current quarter",
                    "Debt for the current quarter"
                },
                EnableCount = false,
                NumberAlignment = Alignment.Left
            });

            if (data != null)
            {
                foreach (var record in data)
                {
                    if (record != null && !record.Equals(new PersonData()))
                    {
                        table.AddRow(
                            record.ApartmentNumber,
                            record.Surname,
                            $"{record.OutputIndication - record.InputIndication} kWh",
                            $"{(record.OutputIndication - record.InputIndication) * kilowattPerHourPrice} UAH");
                    }
                }
            }

            target.Write(table);
        }
        public static void WriteActivityReportToFile(StreamWriter target, IEnumerable<PersonData>? data)
        {
            var table = new ConsoleTable(new ConsoleTableOptions
            {
                Columns = new[] {
                    "Apartment number",
                    "Surname of the apartment owner",
                    "Days have passed since the last time the meter was read"
                },
                EnableCount = false,
                NumberAlignment = Alignment.Left
            });

            if (data != null)
            {
                foreach (var record in data)
                {
                    if (record != null && !record.Equals(new PersonData()))
                    {
                        table.AddRow(
                            record.ApartmentNumber,
                            record.Surname,
                            (DateTime.Now - record.MeterReadingDate[2].ToDateTime(new TimeOnly())).Days);
                    }
                }
            }

            target.Write(table);
        }
    }
}
