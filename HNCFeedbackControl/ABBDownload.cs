using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABB.Robotics.Controllers.RapidDomain;

namespace HNCFeedbackControl
{
    class ABBDownload
    {
        // 定义相关属性成员：来实现对某些私有属性的访问和提取  与ABBCollecter中对应
        public string ID { set; get; }
        public string IP { set; get; }
        public string Name { set; get; }
        public DateTime TimeStamp { set; get; }
        public RobJoint PositionInfo { set; get; }
        public IRapidData SpeedInfo { get; set; }
    }
}
