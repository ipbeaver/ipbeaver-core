using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPFox
{
    public class Segment
    {
        public uint StartIP { get; set; }

        public uint EndIP { get; set; }

        public string Region { get; set; }

        public RegionDetail Detail
        {
            get { 
                var arr=Region.Split('|');
                string? c = arr[3] != "0" ? arr[3] : null;
                string? s = arr[2]!="0"? arr[2] : null;
                string? p = arr[4] != "0" ? arr[4] : null;
                RegionDetail rd = new RegionDetail() { 
                    Country = arr[0], 
                    State = s,
                    City = c,
                    DataProvider = p };
                return rd;
            }
        }

        public List<Segment> Split()
        {
            var tList = new List<Segment>();
            var sByte = (StartIP >> 24) & 0xFF;
            var eByte = (EndIP >> 24) & 0xFF;

            var nSip = StartIP;
            for (var i = sByte; i <= eByte; i++)
            {
                var sip = (i << 24) | (nSip & 0xFFFFFF);
                var eip = (i << 24) | 0xFFFFFF;

                if (eip < EndIP)
                {
                    nSip = (i + 1) << 24;
                }
                else
                {
                    eip = EndIP;
                }

                tList.Add(new Segment
                {
                    StartIP = sip,
                    EndIP = eip,
                });
            }

            var segList = new List<Segment>();

            foreach (var seg in tList)
            {
                var temp = seg.StartIP & 0xFF000000;
                nSip = seg.StartIP;

                sByte = (seg.StartIP >> 16) & 0xFF;
                eByte = (seg.EndIP >> 16) & 0xFF;

                for (var i = sByte; i <= eByte; i++)
                {
                    var sip = temp | (i << 16) | (nSip & 0xFFFF);
                    var eip = temp | (i << 16) | 0xFFFF;

                    if (eip < seg.EndIP)
                    {
                        nSip = 0;
                    }
                    else
                    {
                        eip = seg.EndIP;
                    }

                    segList.Add(new Segment
                    {
                        StartIP = sip,
                        EndIP = eip,
                        Region = Region,
                    });
                }
            }
            return segList;
        }

        public override string ToString()
        {
            return $"{Util.UInt32ToIpAddress(StartIP)}|{Util.UInt32ToIpAddress(EndIP)}|{Region}";
        }
    }
}
