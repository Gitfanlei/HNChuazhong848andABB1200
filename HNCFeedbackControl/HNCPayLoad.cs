using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HNCAPI;

namespace HNCFeedbackControl
{
    class HNCPayLoad
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime TimeStamp { get; set; }
        public HNCLoadData LoadDataInfo { get; set; }
        public HNCPositionData PositionInfo { get; set; }
    }
}
