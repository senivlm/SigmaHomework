using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.Problem2
{
    public class SiteTrafficStatistics
    {
        private List<VisitRecord> visitRecords = new List<VisitRecord>();

        #region Constructors
        public SiteTrafficStatistics()
        {
            visitRecords = new List<VisitRecord>();
        }
        public SiteTrafficStatistics(StreamReader sourceReader)
        {
            for (int lineCounter = 1; !sourceReader.EndOfStream; lineCounter++)
            {
                try
                {
                    string? line = sourceReader.ReadLine();

                    var record = new VisitRecord();
                    record.Parse(line);

                    visitRecords.Add(record);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + $" Line {lineCounter}");
                }
            }
        }
        #endregion

        #region Methods
        public Dictionary<string, int> GetNumberOfVisitsPerWeekForEveryUser()
        {
            return CreateDictionaryFromCollectionByIp(visitRecords, g => g.Count());//O(2n),
                                                                                    //where n - number of records
        }
        public Dictionary<string, DayOfWeek?> GetMostPopularDayOfWeekForEveryUser()
        {
            return CreateDictionaryFromCollectionByIp(visitRecords, g => g
                         .GroupBy(vr => vr.Day)//2n
                         .Select(g => new { Day = g.Key, Count = g.Count() })//3n
                         .MaxBy(g => g.Count)?.Day);//Worst case: O(3n+7m),
                                                    //where n - number of records,
                                                    //m - number of unique IP addresses,
                                                    //7 - max number of unique days of week for every IP address
        }
        public Dictionary<string, string?> GetMostPopularTimeIntervalForEveryUser()
        {
            return CreateDictionaryFromCollectionByIp(
                visitRecords,
                g => GetMostPopularTimeInterval(g));//Worst case: O(3n+24m),
                                                    //where n - number of records,
                                                    //m - number of unique IP addresses,
                                                    //24 - max number of unique time intervals for each IP address
        }
        public string? GetMostPopularTimeIntervalForSite()
        {
            return GetMostPopularTimeInterval(visitRecords);//O(2n+m),
                                                            //where n - number of records,
                                                            //m - number of unique time intervals (max = 24)
        }
        private static Dictionary<string, TResult> CreateDictionaryFromCollectionByIp<TResult>(IEnumerable<VisitRecord> collection, Func<IEnumerable<VisitRecord>, TResult> value)
        {
            return collection
                .GroupBy(vr => vr.IP)//n
                .ToDictionary(g => g.Key, g => value(g));//Depends on what Func<> is
        }
        private static string? GetMostPopularTimeInterval(IEnumerable<VisitRecord> collection)
        {
            return collection
                .GroupBy(vr => vr.Time.Hour)//n
                .Select(g => new { Time = $"{g.Key}:00:00 - {(g.Key + 1 == 24 ? 00 : g.Key + 1)}:00:00", Count = g.Count() })//2n
                .MaxBy(g => g.Count)?.Time;//O(2n+m),
                                           //where n - number of records,
                                           //m - number of unique time intervals (max = 24)
        }
        #endregion
    }
}
