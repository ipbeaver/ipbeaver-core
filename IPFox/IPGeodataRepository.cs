using LiteDB;
using LiteDB.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPFox
{
    public class IPGeodataRepository
    {
        const string dataFolder = "/app/";
        const string dbFileName = "ip.db";
        static IPGeodataRepository()
        {
            if (!File.Exists(dataFolder+dbFileName))
            {
                InitLiteDB();
            }
        }
        static List<Segment> segments = null;
        static private void InitLiteDB()
        {
            if (segments != null)
            {
                return;
            }
            segments = new List<Segment>();
            using (TextReader reader = new StreamReader(dataFolder+"data_ip.merge.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var seg = Util.GetSegment(line);
                    segments.Add(seg);
                }
            }
            using (var db = new LiteDatabase(dataFolder+ dbFileName))
            {
                var col = db.GetCollection<Segment>("IPSegments");
                col.Insert(segments);
                col.EnsureIndex(x => x.StartIP);
            }
        }

        public static async Task<RegionDetail> GetIPLocationAsync(string ip)
        {
            if (!Util.ValidateIPv4(ip))
            {
                throw new ArgumentException($"{ip} is not a valid IP address");
            }
            using (var db = new LiteDatabaseAsync(dataFolder + dbFileName))
            {
                var col = db.GetCollection<Segment>("IPSegments");
                var ipInt = Util.IpAddressToUInt32(ip);
                var segment = await col.FindOneAsync(x => x.StartIP <= ipInt && ipInt<= x.EndIP);
                return segment.Detail;
            }
        }
    }
}
