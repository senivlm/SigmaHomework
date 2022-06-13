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
    {//хороша робота!
        public static void WriteReportToFile(StreamWriter target, IEnumerable<PersonData>? data, int numberOfQuarter)
        {
            target.WriteLine($"Quarter number: {numberOfQuarter}");

            var table = new ConsoleTable(new ConsoleTableOptions
            {
                Columns = new[] {
                    "Record number",
                    "Apartment number",
                    "Surname of the apartment owner",
                    $"Used electricity for {new DateTimeFormatInfo().GetMonthName(3*(numberOfQuarter-1)+1)}",
                    $"Used electricity for {new DateTimeFormatInfo().GetMonthName(3*(numberOfQuarter-1)+2)}",
                    $"Used electricity for {new DateTimeFormatInfo().GetMonthName(3*(numberOfQuarter-1)+3)}",
                    $"Meter reading date for {new DateTimeFormatInfo().GetMonthName(3*(numberOfQuarter-1)+1)}",
                    $"Meter reading date for {new DateTimeFormatInfo().GetMonthName(3*(numberOfQuarter-1)+2)}",
                    $"Meter reading date for {new DateTimeFormatInfo().GetMonthName(3*(numberOfQuarter-1)+3)}"
                },
                EnableCount = false,
                NumberAlignment = Alignment.Left
            });

            if (data != null)
            {
                var recordNumber = 1;
                foreach (var record in data)
                {
                    if (record != null)
                    {
                        var indications = record.GetMeterIndications().ToArray();
                        var dates = record.GetMeterReadingDates().ToArray();

                        table.AddRow(
                            recordNumber,
                            record.ApartmentNumber,
                            record.Surname,
                            $"{indications[1] - indications[0]} kWh",
                            $"{indications[2] - indications[1]} kWh",
                            $"{indications[3] - indications[2]} kWh",
                            dates[0].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en")),
                            dates[1].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en")),
                            dates[2].ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("en")));//uk-UA - ukrainian culture

                        recordNumber++;
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
                    "Record number",
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
                var recordNumber = 1;
                foreach (var record in data)
                {
                    if (record != null)
                    {
                        var indications = record.GetMeterIndications().ToArray();

                        table.AddRow(
                            recordNumber,
                            record.ApartmentNumber,
                            record.Surname,
                            $"{indications[3] - indications[0]} kWh",
                            $"{(indications[3] - indications[0]) * kilowattPerHourPrice} UAH");

                        recordNumber++;
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
                    "Record number",
                    "Apartment number",
                    "Surname of the apartment owner",
                    "Days have passed since the last time the meter was read"
                },
                EnableCount = false,
                NumberAlignment = Alignment.Left
            });

            if (data != null)
            {
                var recordNumber = 1;
                foreach (var record in data)
                {
                    if (record != null)
                    {
                        var dates = record.GetMeterReadingDates().ToArray();

                        table.AddRow(
                            recordNumber,
                            record.ApartmentNumber,
                            record.Surname,
                            (DateTime.Now - dates[2].ToDateTime(new TimeOnly())).Days);

                        recordNumber++;
                    }
                }
            }

            target.Write(table);
        }
    }
}
