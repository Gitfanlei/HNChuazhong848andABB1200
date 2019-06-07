using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HNCAPI;
using System.Net;


namespace HNCFeedbackControl
{
    class HNCCollector
    {
        public HNCPayLoad hncPayload;
        public static Int16 ActiveClientNo = -1;
        
        private string cncIp;  //  外部输入
        private ushort cncPort; // 外部输入
        public bool State = false;


        public string[] AxisValueInfo = new string[32];
        public Int32 AxisNum = -1;
        public string[] axisName = new string[32];   // 传递变量 AxisName

        private const Int32 AxisTypesCount = 9;   // 规定轴的类型数目
        private const Int32 axisDisplayX = 0x00000001; //   unit =  mm  进制
        
        private Int32[] axisId = new Int32[32];
        public Double[] loadValue = new Double[32];  // 传递变量 LoadValue
        public Int32[] axisValue = new Int32[32];
        private Int32 mch = 0;
        public Int32 Ch = 0;
        //public Int32 alarmNum = 0;
        //public Int32 alarmNo = 0;  // 报警号 数组 1 2 3 4....
        //public string[] errText = new string[64];
        private const Int32 alarmCount = 50;

        public HNCCollector(string cncIp,ushort cncPort)
        {
            this.cncIp = cncIp;  // cncIp 从私有变量cncIp中读取         从Form1 中读取
            this.cncPort = cncPort;  //cncPort 从私有变量cncPort中读取
        }

        // Conneting
        public bool Connect_CNC()
        {
            ActiveClientNo = HNCAPI.HncApi.HNC_NetConnect(cncIp, cncPort);
            if ((ActiveClientNo < 0) || (ActiveClientNo > 255)) return false;
            else
            {
                State = true;
                return true;
            }
        }

        // Disconneting
        public void Disconnect_CNC()
        {
            HncApi.HNC_NetExit();
            State=false;
            ActiveClientNo=-1;
        }

        public void GetAxisInfo()
        {
            // mch 值
            Int32 ret = HncApi.HNC_SystemGetValue((Int32)HncSystem.HNC_SYS_ACTIVE_CHAN, ref mch, ActiveClientNo);
            //MessageBox.Show(Convert.ToString(ret));  // 信息类别查看  ret=0
            
            UInt32 AxisMask = AcquireAxisType(mch, ref AxisNum, ActiveClientNo);
            AcquireAxisType((Int16)Ch, ref AxisNum, ActiveClientNo);
            AxisInfoGotItems(AxisMask, ref axisName, ref axisId, ref axisValue,ref loadValue, ActiveClientNo);  // ref 通过引用传递参数获取值
            
            //GetAlarmInfo(ref alarmNum, ref alarmNo, ref errText, ActiveClientNo);
            GetAxisStrInfo(AxisNum, axisId, axisValue, ref AxisValueInfo);                                     // AxisValueinfo  是单位换算之后的值

            // kafka 数据输出点 
            hncPayload = new HNCPayLoad
            {
                ID = "1",
                IP = $"{cncIp}:{cncPort}",
                Name = "HNC1",
                TimeStamp = DateTime.Now,
                LoadDataInfo = new HNCLoadData
                {
                    AxLoad_1 = $"{axisName[0]}:{loadValue[0].ToString("f6")}",
                    AxLoad_2 = $"{axisName[1]}:{loadValue[1].ToString("f6")}",
                    AxLoad_3 = $"{axisName[2]}:{loadValue[2].ToString("f6")}",
                    AxLoad_4 = $"{axisName[3]}:{loadValue[3].ToString("f6")}",
                    AxLoad_5 = $"{axisName[4]}:{loadValue[4].ToString("f6")}"
                },
                PositionInfo =new HNCPositionData
                {
                    Ax_1 = $"{axisName[0]}:{AxisValueInfo[0]}",
                    Ax_2 = $"{axisName[1]}:{AxisValueInfo[1]}",
                    Ax_3 = $"{axisName[2]}:{AxisValueInfo[2]}",
                    Ax_4 = $"{axisName[3]}:{AxisValueInfo[3]}",
                    Ax_5 = $"{axisName[4]}:{AxisValueInfo[4]}"
                }
                
            };
                      
        }

        // 轴的类型的获取  类型 通道号（ch） 索引号 通道值（mask）  clientNo 网络号
        public UInt32 AcquireAxisType(Int32 ch, ref Int32 axisnum, Int16 clientNo)
        {
            Int32 mask = 0;  //
            HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_AXES_MASK, ch, 0, ref mask, clientNo); // 类型 - 通道号 - 索引号 - 值 - 连接号
            int num = 0;
            for (Int32 i = 0; i < AxisTypesCount; i++)
            {
                if (((axisDisplayX << i) & mask) != 0)
                    num++;
            }
            axisnum = num;
            return (UInt32)mask;
        }

        // 获取轴信息的变量类型
        public bool AxisInfoGotItems(UInt32 AxisMask, ref string[] AxisName, ref Int32[] AxisId,ref Int32[] axisValue ,ref Double[] loadValue, Int16 clientNo)
        {
            bool flag = true;
            Int32 index = 0; 
            Int32 ret = 0;  // 引用变量
            if (AxisMask == 0)
            {
                flag = false;
                return flag;
            }

            for (Int32 i = 0; i < AxisTypesCount; i++)
            {
                if ((AxisMask >> i & 1) == 1)  // 位与运算   i 与 1 都是 1 时 返回1   >> 右移 符号（转换为2进制向右移动第二个操作数的位数）
                {
                    ret += HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_NAME, i, ref AxisName[index], clientNo);  //  类型 轴号 轴值 网络号
                    AxisId[index] = i;
                    // 获取位置信息
                    ret += HncApi.HNC_AxisGetValue((Int32)HncAxis.HNC_AXIS_ACT_POS, AxisId[index], ref axisValue[index], ActiveClientNo);
                    // 获取负载电流
                    ret += HncApi.HNC_AxisGetValue((Int32)HncAxis.HNC_AXIS_LOAD_CUR, AxisId[index], ref loadValue[index], ActiveClientNo);
                    index++;
                    if (ret != 0)
                    {
                        flag = false;
                        break;
                    }
                }

            }
            return flag;
        }


        //public Int32 index = 0;
        //// 获取报警信息
        //private void GetAlarmInfo(ref Int32 AlarmNum, ref Int32 AlarmNo, ref string[] errText, Int16 clientNo)
        //{
        //    Int32 alarmNum = 0;  //报警数目
        //    Int32 alarmNo = 0;   // 报警号
            
        //    Int32 ret = 0;

        //    // 获取报警数目
        //    ret = HncApi.HNC_AlarmGetNum((Int32)AlarmType.ALARM_TYPE_ALL, (Int32)AlarmLevel.ALARM_ERR, ref alarmNum, ActiveClientNo);

        //    if (ret != 0)
        //    {
        //        for (int i = 0; i < alarmCount; i++)
        //        {
        //            string[] items = new string[4];
        //            items[0] = "No index";
        //            items[1] = "No AlarmNo";
        //            items[2] = "No ErrorText";
        //            items[3] = DateTime.Now.ToString();
        //            ListViewItem item = new ListViewItem(items);
        //            this.AlarmlistView.Items.Add(item);

        //        }
        //        return;
        //    }

        //    // 获取报警类型 注意每一个报警对应的数据不同
        //    int index = 0;
        //    for (int i = 0; i < alarmNum; i++)
        //    {
        //        ret = HncApi.HNC_AlarmGetData((Int32)AlarmType.ALARM_TYPE_ALL, (Int32)AlarmLevel.ALARM_ERR, i, ref alarmNo, ref errText, ActiveClientNo);
        //        if (ret != 0)
        //        {
        //            //
        //        }
        //        index++;
        //    }
        //}
        
        
        // 位移单位转换
        private void GetAxisStrInfo(int count, Int32[] AxisId, Int32[] axisValue, ref string[] AxisValueInfo)
        {
            Int32 ret = 0;
            Int32 lax = 0;
            Int32 axistype = 0;
            Int32 metric = 0;
            Int32 unit = 100000;
            Int32 diameter = 0;
            for (int i = 0; i < count; i++)
            {
                ret = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_LAX, mch, AxisId[i], ref lax, ActiveClientNo);
                // 直半径处理
                ret = HncApi.HNC_ChannelGetValue((int)HncChannel.HNC_CHAN_DIAMETER, mch, 0, ref diameter, ActiveClientNo);
                if (0 == lax && 1 == diameter)
                {
                    axisValue[i] *= 2;
                }
                ret = HncApi.HNC_AxisGetValue((int)HncAxis.HNC_AXIS_TYPE, lax, ref axistype, ActiveClientNo);
                if (axistype == 1)//直线轴
                {
                    ret = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_MOVE_UNIT, ref unit, ActiveClientNo);
                    ret = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_METRIC_DISP, ref metric, ActiveClientNo);
                    if (0 == metric) // 英制
                        unit = (Int32)(unit * 25.4);
                }
                else
                {
                    ret = HncApi.HNC_SystemGetValue((int)HncSystem.HNC_SYS_TURN_UNIT, ref unit, ActiveClientNo);
                }

                if (Math.Abs(unit) - 0.00001 <= 0.0) //除零保护
                {
                    AxisValueInfo[i] = "0";
                }
                else
                {
                    AxisValueInfo[i] = ((double)axisValue[i] / unit).ToString("F4"); // 实际输出的轴的位置   单位换算
                }

                if (axistype == 1)
                {
                    if (0 == metric) // 英制
                    {
                        AxisValueInfo[i] += " inch";
                    }
                    else
                    {
                        AxisValueInfo[i] += " mm";
                    }
                }
                else
                {
                    AxisValueInfo[i] += " D";
                }
            }
        }
    }
}
