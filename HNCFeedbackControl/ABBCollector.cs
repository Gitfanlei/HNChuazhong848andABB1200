using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.MotionDomain;
using System.Windows.Forms;


namespace HNCFeedbackControl
{
    class ABBCollector
    {
        private Controller ABBController;
        private ControllerInfo ABBControllerInfo;

        private bool chooseSocket = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("chooseSocket"));

        public ABBCollector()
        {
            DynamicCreation();
        }

        public void DynamicCreation()
        {
            // ABB 自带接口扫描类
            NetworkScanner networkScanner = new NetworkScanner();

            // 条件运算符  获取控制器类型
            ControllerInfo[] controllers = networkScanner.GetControllers(chooseSocket ? NetworkScannerSearchCriterias.Virtual : NetworkScannerSearchCriterias.Real);

            if (controllers.Length>0)
            {
                Console.WriteLine(controllers);

                // 获取控制器信息
                ABBControllerInfo = controllers[0];

                // 根据控制器信息 船舰实例
                ABBController = ControllerFactory.CreateFrom(ABBControllerInfo);
                Console.WriteLine($"Found one ABB.System Name is:{SystemName} System ID is:{SystemID} System IP is:{SystemIP}");
                // $ 起到一个占位符的作用内容包含在 {} 中，可以用于获取{}中对应内容的信息,避免了利用{0}的形式占位。  以前占位符 需要  {0}  和 变量 配合使用
            }
            else
            {
                MessageBox.Show("No ABB Robot Found!");

            }
        }

        public string SystemName
        {
            get
            {
                return ABBController.SystemName;
            }
        }

        public string SystemID
        {
            get
            {
                return ABBController.SystemId.ToString();
            }
        }

        public string SystemIP
        {
            get
            {
                return ABBController.IPAddress.ToString();
            }
        }

        public IRapidData SpeedInfo
        {
            get
            {
                return null;   //ABBController.Rapid.GetRapidData().Value;
            }
        }

        public RobJoint PositionInfo
        {
            get
            {
                return ABBController.MotionSystem.MechanicalUnits[0].GetPosition().RobAx;
            }
        }

        
    }
}
