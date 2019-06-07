using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HNCAPI;
using System.Net;
using System.Net.Sockets;


using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.Configuration;
using System.Configuration;
using System.Timers;
using System.IO;



namespace HNCFeedbackControl
{
    public partial class Form1 : Form
    {
        private UInt16 localPort = 10001;
        private HNCCollector hncCollector;

        private static ABBCollector abbCollector;

        private const Int32 alarmCount = 50;
        public string errText = "0";
        public Int32 alarmNum = 0;
        public Int32 alarmNo = 0;
        public Int32 indexNo = 0;

        public Int32 AccessNum = 0;  //程序执行的授权码


        AlarmHisData[] historyData = new AlarmHisData[HncApi.ALARM_HISTORY_MAX_NUM]; // 获取的历史故障信息最大154个
        Int32 hisAlarmNum = 0;  // 历史报警数量
        Int32 count = 1; // // 设定 需要获取的历史报警数据的数量     


        public Form1()
        {
            InitializeComponent();
            timer2.Enabled = true;
            timer2.Start();
            

        }

        // 初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            ShowMsgBox(HncApi.HNC_NetInit(GetLocalIpAddr(), localPort) == 0 ? true : false, "Initializing ");
            AddCombox();
            
            abbScanner();  //  ABB 扫描
            
        }

        // 连接
        private void ConnectBtn_click(object sender, EventArgs e)
        {
            // 分别判别 不同设备
            if (AccessNum == 0)
            {
                hncCollector = new HNCCollector(cncIpTex.Text, Convert.ToUInt16(portTex.Text));  // 调用Collector 中的 connectCNC 函数
                ShowMsgBox(hncCollector.Connect_CNC(), "Connect");
            }
            else if (AccessNum == 1)
            {
                // abb 驱动 机器人数据读取
                cncIpTex.Text = "Auto";
                portTex.Text = "Auto";
            }
            else if (AccessNum == 2)
            {
                MessageBox.Show("Don not have this device!");
            }
            else
            {
                MessageBox.Show("Device type not match!");
            }


        }
        // 断开
        private void button2_Click(object sender, EventArgs e)
        {
            if (AccessNum==0)
            {
                hncCollector.Disconnect_CNC();
                timer1.Enabled = false;
            }
            else if(AccessNum==1)
            {
                // abb
                timer3.Stop();
                timer3.Enabled = false;
                EndUp();
            }
        }

        private void Form1_FormClosed(object sender, EventArgs e)
        {
            hncCollector.Disconnect_CNC();
            timer1.Enabled = false;
        }

        // 开始  修改  分别驱动 ABB 或者  HNC  判别状态    提示信息 放最后
        private void startButton_Click(object sender, EventArgs e)
        {
            if (AccessNum == 0)
            {
                timer1.Enabled = true;
                timer1.Start();
                startButton.Text = "Monitoring";
                startButton.Enabled = false;
                stopButton.Enabled = true;
            }
            else if (AccessNum == 1)
            {
                // 加入ABB 判别
                timer3.Enabled = true;
                timer3.Start();
            }
            else
            {
                MessageBox.Show("Please Connect!");
            }
        }
        // 停止
        private void stopButton_Click(object sender, EventArgs e)
        {
            if (AccessNum == 0)
            {
                timer1.Stop();
                stopButton.Text = "Stopping";
                startButton.Enabled = true;
                stopButton.Enabled = false;
            }
            else if (AccessNum == 1)
            {
                // abb
                timer3.Stop();
                timer3.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please connect!");
            }

        }

        // combox 元素的补充 可以从空间处设置 也可以通过如下方式：
        private void AddCombox()
        {
            // combox空间元素的添加方式  这种添加方式会默认 索引号 从0 开始
            this.DeviceComboBox.Text = "Choose....";
            this.DeviceComboBox.Items.Add("CNC");
            this.DeviceComboBox.Items.Add("Robot");
            this.DeviceComboBox.Items.Add("AGV");

        }


        // 设备类型选择 更改后发生
        private void DeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 运行之前 加判断语句 判断 设备索引是否对应；
            if (DeviceComboBox.Text.ToString() == "CNC")
            {
                MessageBox.Show("Device_type:" + this.DeviceComboBox.SelectedIndex.ToString());
                AccessNum = Convert.ToInt32(this.DeviceComboBox.SelectedIndex.ToString());
            }
            else if (DeviceComboBox.Text.ToString() == "Robot")
            {
                MessageBox.Show("Device_type:" + this.DeviceComboBox.SelectedIndex.ToString());
                AccessNum = Convert.ToInt32(this.DeviceComboBox.SelectedIndex.ToString());
            }
            else if (DeviceComboBox.Text.ToString() == "AGV")
            {
                MessageBox.Show("Device_type:" + this.DeviceComboBox.SelectedIndex.ToString());
                AccessNum = Convert.ToInt32(this.DeviceComboBox.SelectedIndex.ToString());
            }

        }



        // 输出信息
        private void OutPutAxisInfo()
        {

            string loadInfo = "";
            //GetAlarmInfo(ref alarmNum, ref indexNo, ref alarmNo, ref errText, activeClientNo);

            // 负载信息
            for (int i = 0; i < hncCollector.AxisNum; i++)
            {
                loadInfo += hncCollector.axisName[i];
                loadInfo += "_Load:";
                loadInfo += hncCollector.loadValue[i].ToString("f6");
                loadInfo += "\n";
            }
            axisLoadInfoLabel.Text = loadInfo;

            // 位置信息
            string PositionInfo = "";
            for (int i = 0; i < hncCollector.AxisNum; i++)
            {
                PositionInfo += hncCollector.axisName[i];
                PositionInfo += "：";
                PositionInfo += hncCollector.AxisValueInfo[i];
                PositionInfo += "\n";
            }
            axisPositionInfoLable.Text = PositionInfo;


            //// 报警数目
            //NumLabel.Text = alarmNum.ToString();
            if (loadInfo.Length > 0)
            {
                FileStream dataFile = new FileStream($"../DataSet/HNCLoadData.txt", FileMode.Append, FileAccess.Write);
                StreamWriter dataWrite = new StreamWriter(dataFile, System.Text.Encoding.Default);
                dataWrite.WriteLine(loadInfo);
                dataWrite.Flush();

                dataFile.Close();
                dataFile.Dispose();
            }

            if (PositionInfo.Length > 0)
            {
                FileStream dataFile = new FileStream($"../DataSet/HNCPosData.txt", FileMode.Append, FileAccess.Write);
                StreamWriter dataWrite = new StreamWriter(dataFile, System.Text.Encoding.Default);
                dataWrite.WriteLine(PositionInfo);
                dataWrite.Flush();

                dataFile.Close();
                dataFile.Dispose();
            }
            
        }

        //刷新 报警信息
        private void RefreshAlarm()
        {
            Int32 ret = HncApi.HNC_AlarmRefresh(HNCCollector.ActiveClientNo);
            //  HNCCollector.ActiveClientNo   可以换成 0 
        }

        // 报警数目
        private void GetAlarmNum()
        {
            Int32 curAlarmNum = 0;
            Int32 ret = HncApi.HNC_AlarmGetNum((int)AlarmType.ALARM_TYPE_ALL, (int)AlarmLevel.ALARM_ERR, ref curAlarmNum, HNCCollector.ActiveClientNo);
            if (ret == 0)
            {
                NumLabel.Text = curAlarmNum.ToString();
            }
            else
            {
                MessageBox.Show("failing to get the alarm number!");
            }
        }

        // 报警信息
        private void GetAlarmInfo()
        {
            Int32 alarmID = 0;
            Int32 index = 0;
            string errTxt = string.Empty;
            Int32 ret = HncApi.HNC_AlarmGetData((int)AlarmType.ALARM_TYPE_ALL, (int)AlarmLevel.ALARM_ERR, index, ref alarmID, ref errTxt, HNCCollector.ActiveClientNo);
            if (ret == 0)
            {
                for (int i = 0; i < alarmCount; i++)
                {
                    ListViewItem item = new ListViewItem(index.ToString());
                    item.SubItems.Add(alarmID.ToString());
                    item.SubItems.Add(errTxt.ToString());
                    item.SubItems.Add(DateTime.Now.ToString());
                    this.AlarmlistView.Items.Add(item);
                }
                // MessageBox.Show("AlarmID:" + alarmID.ToString() + "content:" + errTxt.ToString());
            }
            else
            {
                MessageBox.Show("failing to get the alarm info!");
            }
        }

        //历史报警数目
        public void GetHisAlarmNum()
        {

            Int32 ret = HncApi.HNC_AlarmGetHistoryNum(ref hisAlarmNum, HNCCollector.ActiveClientNo);
            if (ret != 0)
            {
                MessageBox.Show("failing to get the history alarmNum!");
            }
        }

        // 历史报警信息
        public void GetHisAlarmInfo()
        {
            Int32 index = 0;
            Int32 ret = HncApi.HNC_AlarmGetHistoryData(index, ref count, historyData, HNCCollector.ActiveClientNo);
            if (ret != 0)
            {
                MessageBox.Show("failing to get the history alarmNum!");
            }
            // 注意报警信息有很多，这种表达方式需要验证。  historyData[i] 
        }

        // 查询历史报警
        private void HisAlarm_Click(object sender, EventArgs e)
        {
            //hisAlarmTxtlabel.Text += "Until " + DateTime.Now.ToString() + " HisAlarmNum : " + hisAlarmNum.ToString() + " ———— " + historyData.ToString();
            //hisAlarmTxtlabel.Text += "\n";

            // foreach 只是为了 验证 输出多个 历史信心的 表达方式
            for (int i = 0; i < count; i++)
            {

                ListViewItem hisitem = new ListViewItem((i + 1).ToString());
                hisitem.SubItems.Add(" HisAlarmNum : " + hisAlarmNum.ToString() + " —— " + historyData[i].ToString() + " Until " + DateTime.Now.ToString());
                this.historyAlarmlistView.Items.Add(hisitem);

            }
            HisAlarm.Enabled = true;
        }

        //// 获取报警信息
        //private void GetAlarmInfo(ref Int32 AlarmNum, ref Int32 index, ref Int32 AlarmNo, ref string errText, Int16 Active)
        //{

        //    Int32 alarmNum = 0;  //报警数目
        //    Int32 alarmNo = 0;   // 报警号
        //    Int32 ret = 0;

        //    // 获取报警数目
        //    ret = HncApi.HNC_AlarmGetNum((Int32)AlarmType.ALARM_TYPE_ALL, (Int32)AlarmLevel.ALARM_ERR, ref alarmNum, activeClientNo);
        //    if (ret != 0)
        //    {
        //        string[] items = new string[4];
        //        items[0] = "No index";
        //        items[1] = "No AlarmNo";
        //        items[2] = "No ErrorText";
        //        items[3] = DateTime.Now.ToString();
        //        ListViewItem item = new ListViewItem(items);
        //        this.AlarmlistView.Items.Add(item);
        //    }
        //    else
        //    {
        //        // 获取报警类型  
        //        for (Int32 i = 0; i < alarmNum; i++)
        //        {
        //            ret = HncApi.HNC_AlarmGetData((Int32)AlarmType.ALARM_TYPE_ALL, (Int32)AlarmLevel.ALARM_ERR, i, ref alarmNo, ref errText, activeClientNo);
        //            if (ret != 0)
        //            {
        //                string[] items = new string[4];
        //                items[0] = "No index";
        //                items[1] = "No AlarmNo";
        //                items[2] = "No ErrorText";
        //                items[3] = DateTime.Now.ToString();
        //                ListViewItem item = new ListViewItem(items);
        //                this.AlarmlistView.Items.Add(item);
        //            }
        //            else
        //            {
        //                // indexNo = i;
        //                // 报警信息
        //                string[] items = new string[4];
        //                items[0] = indexNo.ToString();
        //                items[1] = alarmNo.ToString();
        //                items[2] = errText.ToString();
        //                items[3] = DateTime.Now.ToString();
        //                index++;
        //                ListViewItem item = new ListViewItem(items);
        //                this.AlarmlistView.Items.Add(item);
        //            }
        //            break;

        //        }
        //    }

        //}


        // 异常信息捕获


        private void AbnormalInfoRecord()
        {
            for (int i = 0; i < hncCollector.AxisNum; i++)
            {
                Int32 position = Convert.ToInt32(hncCollector.AxisValueInfo[i]);

                string[] records = new string[1];
                if (position >= 100)
                {
                    records[0] = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    records[1] = "Secondary waring !" + hncCollector.axisName[i] + "-" + hncCollector.AxisValueInfo[i];
                    ListViewItem record = new ListViewItem(records);
                    this.abnormallistView.Items.Add(record);
                }
                else if (hncCollector.loadValue[i] >= 120)
                {
                    records[0] = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    records[1] = hncCollector.axisName[i] + hncCollector.AxisValueInfo[i];
                    ListViewItem record = new ListViewItem(records);
                    this.abnormallistView.Items.Add(record);

                    // Stop running the current NC code
                    Int32 ret = HncApi.HNC_SysCtrlStopProg((Int16)hncCollector.Ch, HNCCollector.ActiveClientNo);
                    if (ret != 0)
                    {
                        MessageBox.Show("Can not execute the order to stop the curNC code!");

                    }
                    else
                    {
                        MessageBox.Show("First level warning ! Will be colliding, the NC process is stopped!");
                    }
                    break;
                }
                else
                {
                    AbnormalInfoRecord();
                }
            }
        }

        //---------------------------------------------------------------------------

        // 时钟刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            hncCollector.GetAxisInfo();
            OutPutAxisInfo();
            GetAlarmNum();
            GetAlarmInfo();
            RefreshAlarm();
            AbnormalInfoRecord();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            GetTime();
        }


        // 补充函数--------------------------------------------------------------------

        // 获取时间
        public void GetTime()
        {
            CurTimeLabel.Text = DateTime.Now.ToString();
        }

        // 上位机IP
        public static string GetLocalIpAddr()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry localHost = Dns.GetHostEntry(hostName);
            IPAddress localIpAddr = null;
            foreach (IPAddress ip in localHost.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && ip.ToString().StartsWith("192.168"))
                {
                    localIpAddr = ip;
                    break;
                }
            }
            return localIpAddr.ToString();

        }

        // 消息盒子
        private void ShowMsgBox(bool flag, string msg)
        {
            if (flag) MessageBox.Show($"{msg} Success！");
            else MessageBox.Show($"{msg} failure！");

        }

        // 获取历史报警数量设置
        private void hisCotNumtxtBox_TextChanged(object sender, EventArgs e)
        {
            count = Convert.ToInt32(hisCotNumtxtBox.Text);
            if (count.GetType() != typeof(int) && count < 1)
            {
                MessageBox.Show("please input correct value!");

            }
            else if (count > 154)
            {
                MessageBox.Show("the Count Num can't out of 154! ");
            }

        }


        // ABB_____________________________________________________________________________________________________________________

        public void abbScanner()
        {
            abbCollector = new ABBCollector();

            NetworkScanner networkScanner = new NetworkScanner();
            networkScanner.Scan();
            ControllerInfoCollection controllers = networkScanner.Controllers;

            foreach (ControllerInfo controller in controllers)
            {
                // 加载扫描的信息

                //// listviewitem 是建立的控件中的单元 以系统SystemName为keyword 建立信息类
                //ListViewItem item = new ListViewItem(controller.SystemName);
                ////获取 IP version 信息
                //item.SubItems.Add(controller.IPAddress.ToString());  // ip
                //item.SubItems.Add(controller.Version.ToString()); // version
                //item.Tag = controller;
                //// 指明 item 归属的控件
                //this.listControllerView.Items.Add(item);
            }

        }

        // 窗口变化 命令

        //private void listControllerView_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // 建立表格view项目类
        //    ListViewItem itemView = listControllerView.SelectedItems[0];
        //    // 判断项目视图
        //    if (itemView.Tag != null)
        //    {

        //        ControllerInfo controllerInfo = (ControllerInfo)itemView.Tag;

        //        Controller ctrl = ControllerFactory.CreateFrom(controllerInfo);
        //        ctrl.Logon(UserInfo.DefaultUser);

        //        // 指明 item 归属的控件
        //        ListViewItem item = new ListViewItem(ctrl.RobotWare.ToString() + "" + ctrl.State.ToString() + "" + ctrl.OperatingMode.ToString());
        //        this.listOutput.Items.Add(item);
        //    }
        //}

        // 点击button 开始数据采集

        //private void Startbutton_Click(object sender, EventArgs e)
        //{
        //    SetUp();

        //    // 打开动态扫描接口

        //    Console.WriteLine("Start collecting the information");

        //    timer3.Enabled = true;
        //    timer3.Start();
        //}




        //public void OnTimedEvent()
        //{

        //    ABBDownLoad abbDownload = new ABBDownLoad
        //    // abbDownload 的属性 定义（程序源加载）
        //    {
        //        ID = collector.SystemID,
        //        IP = collector.SystemIP,
        //        Name = collector.SystemName,
        //        TimeStamp = DateTime.Now,
        //        PositionInfo = collector.PositionInfo
        //    };
        //    // 定义数据输出格式  JsonConvert 表示JSON 格式的转换
        //    string ABBPositionJson = JsonConvert.SerializeObject(abbDownload);

        //    //Console.WriteLine(abbDownload.PositionInfo.ToString());
        //    Console.WriteLine(ABBPositionJson.ToString());

        //}

        //实时轴数据
        public void DataRead()
        {

            ABBDownload abbPositionInfo = new ABBDownload
            {
                ID =abbCollector.SystemID,
                IP = abbCollector.SystemIP,
                Name = abbCollector.SystemName,
                PositionInfo = abbCollector.PositionInfo,
                TimeStamp = DateTime.Now,
                SpeedInfo = abbCollector.SpeedInfo
            };

            var AxisData = abbPositionInfo.PositionInfo;
            axisPositionInfoLable.Text = "Rax_1: " + AxisData.Rax_1.ToString("f2") + " Degree";
            axisPositionInfoLable.Text += "\n";
            axisPositionInfoLable.Text += "Rax_2: " + AxisData.Rax_2.ToString("f2") + " Degree";
            axisPositionInfoLable.Text += "\n";
            axisPositionInfoLable.Text += "Rax_3: " + AxisData.Rax_3.ToString("f2") + " Degree";
            axisPositionInfoLable.Text += "\n";
            axisPositionInfoLable.Text += "Rax_4: " + AxisData.Rax_4.ToString("f2") + " Degree";
            axisPositionInfoLable.Text += "\n";
            axisPositionInfoLable.Text += "Rax_5: " + AxisData.Rax_5.ToString("f2") + " Degree";
            axisPositionInfoLable.Text += "\n";
            axisPositionInfoLable.Text += "Rax_6: " + AxisData.Rax_6.ToString("f2") + " Degree";
            axisPositionInfoLable.Text += "\n";

            var speedData = abbPositionInfo.SpeedInfo;

            // 尝试直接输出
            Console.WriteLine(AxisData.ToString());


            ////格式转换
            //string ABBPositioninfoJson = JsonConvert.SerializeObject(abbPositionInfo);


            FileStream datafile = new FileStream($"../DataSet/{abbPositionInfo.Name}.txt", FileMode.Append, FileAccess.Write);
            StreamWriter datawrite = new StreamWriter(datafile, System.Text.Encoding.Default);
            datawrite.WriteLine(abbPositionInfo.PositionInfo);
            datawrite.Flush();

            datafile.Close();
            datafile.Dispose();

            //Console.WriteLine(AxisData);     终端输出

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            DataRead();
        }
        
        // 解决线程问题  ???
        //private void Stopbutton_Click_1(object sender, EventArgs e)
        //{
        //    timer3.Stop();
        //    timer3.Enabled = false;
        //}
               
        private static void EndUp()
        {
            MessageBox.Show("All processing had Exited!");
            Application.ExitThread();
            Application.Exit();

        }

        //public void WriteData()
        //{
        //    if (AccessNum == 0)
        //    {
        //        FileStream dataFile = new FileStream($"../HNCDataSet.txt", FileMode.Append, FileAccess.Write);
        //        StreamWriter dataWrite = new StreamWriter(dataFile, System.Text.Encoding.Default);
        //        dataWrite.WriteLine()
        //        dataWrite.Flush();

        //        dataFile.Close();
        //        dataFile.Dispose();

        //    }
        //    else if (AccessNum == 1)
        //    {
        //        FileStream dataFile = new FileStream($"../ABBDataSet.txt", FileMode.Append, FileAccess.Write);
        //        StreamWriter dataWrite = new StreamWriter(dataFile, System.Text.Encoding.Default);

        //    }

        //}
                 
    }
}
