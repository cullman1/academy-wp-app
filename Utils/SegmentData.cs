using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2Academy.Utils
{
    public class Segment
    {
        public Segment()
        {
            data = new SegmentObjData[1];
            data[0] = new SegmentObjData();
        }
        public int errorCode { get; set; }
        public string success { get; set; }
        public SegmentObjData[] data { get; set; }
    }

    public class SegmentObjData
    {
        public bool valid { get; set; }
        public string segment { get; set; }
    }
}
