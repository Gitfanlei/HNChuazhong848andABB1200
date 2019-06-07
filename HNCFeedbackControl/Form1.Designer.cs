namespace HNCFeedbackControl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ConnetBtn_click = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AlarmlistView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NumLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CurTimeLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cncipLabel = new System.Windows.Forms.Label();
            this.cncIpTex = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTex = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.axisPositionInfoLable = new System.Windows.Forms.Label();
            this.axisLoadInfoLabel = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.abnormalBox = new System.Windows.Forms.GroupBox();
            this.abnormallistView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.historyAlarmlistView = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HisAlarm = new System.Windows.Forms.Button();
            this.DeviceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.hisCotNumtxtBox = new System.Windows.Forms.TextBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.abnormalBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnetBtn_click
            // 
            this.ConnetBtn_click.Location = new System.Drawing.Point(10, 12);
            this.ConnetBtn_click.Name = "ConnetBtn_click";
            this.ConnetBtn_click.Size = new System.Drawing.Size(75, 23);
            this.ConnetBtn_click.TabIndex = 0;
            this.ConnetBtn_click.Text = "Connect";
            this.ConnetBtn_click.UseVisualStyleBackColor = true;
            this.ConnetBtn_click.Click += new System.EventHandler(this.ConnectBtn_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 727);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "CurAlarmNum:";
            // 
            // AlarmlistView
            // 
            this.AlarmlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.AlarmlistView.Location = new System.Drawing.Point(6, 20);
            this.AlarmlistView.Name = "AlarmlistView";
            this.AlarmlistView.Size = new System.Drawing.Size(624, 157);
            this.AlarmlistView.TabIndex = 2;
            this.AlarmlistView.UseCompatibleStateImageBehavior = false;
            this.AlarmlistView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            this.columnHeader1.Width = 71;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "AlarmNo";
            this.columnHeader2.Width = 145;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "AlarmText";
            this.columnHeader3.Width = 281;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "TimeTag";
            this.columnHeader4.Width = 129;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(551, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Disconnect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AlarmlistView);
            this.groupBox1.Location = new System.Drawing.Point(12, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 183);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AlarmInfo";
            // 
            // NumLabel
            // 
            this.NumLabel.AutoSize = true;
            this.NumLabel.Location = new System.Drawing.Point(86, 728);
            this.NumLabel.Name = "NumLabel";
            this.NumLabel.Size = new System.Drawing.Size(11, 12);
            this.NumLabel.TabIndex = 5;
            this.NumLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 727);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "CurrentTime:";
            // 
            // CurTimeLabel
            // 
            this.CurTimeLabel.AutoSize = true;
            this.CurTimeLabel.Location = new System.Drawing.Point(285, 727);
            this.CurTimeLabel.Name = "CurTimeLabel";
            this.CurTimeLabel.Size = new System.Drawing.Size(11, 12);
            this.CurTimeLabel.TabIndex = 7;
            this.CurTimeLabel.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cncipLabel
            // 
            this.cncipLabel.AutoSize = true;
            this.cncipLabel.Location = new System.Drawing.Point(206, 54);
            this.cncipLabel.Name = "cncipLabel";
            this.cncipLabel.Size = new System.Drawing.Size(47, 12);
            this.cncipLabel.TabIndex = 8;
            this.cncipLabel.Text = "CNC IP:";
            // 
            // cncIpTex
            // 
            this.cncIpTex.Location = new System.Drawing.Point(253, 50);
            this.cncIpTex.Name = "cncIpTex";
            this.cncIpTex.Size = new System.Drawing.Size(86, 21);
            this.cncIpTex.TabIndex = 9;
            this.cncIpTex.Text = "192.168.1.175";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(344, 54);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(35, 12);
            this.portLabel.TabIndex = 10;
            this.portLabel.Text = "Port:";
            // 
            // portTex
            // 
            this.portTex.Location = new System.Drawing.Point(379, 50);
            this.portTex.Name = "portTex";
            this.portTex.Size = new System.Drawing.Size(44, 21);
            this.portTex.TabIndex = 11;
            this.portTex.Text = "10001";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(436, 50);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(551, 50);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 13;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.axisPositionInfoLable);
            this.groupBox2.Controls.Add(this.axisLoadInfoLabel);
            this.groupBox2.Location = new System.Drawing.Point(18, 454);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 235);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AxisInfo";
            // 
            // axisPositionInfoLable
            // 
            this.axisPositionInfoLable.AutoSize = true;
            this.axisPositionInfoLable.Font = new System.Drawing.Font("宋体", 10F);
            this.axisPositionInfoLable.Location = new System.Drawing.Point(219, 34);
            this.axisPositionInfoLable.Name = "axisPositionInfoLable";
            this.axisPositionInfoLable.Size = new System.Drawing.Size(63, 14);
            this.axisPositionInfoLable.TabIndex = 1;
            this.axisPositionInfoLable.Text = "PosiInfo";
            // 
            // axisLoadInfoLabel
            // 
            this.axisLoadInfoLabel.AutoSize = true;
            this.axisLoadInfoLabel.Font = new System.Drawing.Font("宋体", 10F);
            this.axisLoadInfoLabel.Location = new System.Drawing.Point(14, 34);
            this.axisLoadInfoLabel.Name = "axisLoadInfoLabel";
            this.axisLoadInfoLabel.Size = new System.Drawing.Size(63, 14);
            this.axisLoadInfoLabel.TabIndex = 0;
            this.axisLoadInfoLabel.Text = "loadData";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // abnormalBox
            // 
            this.abnormalBox.Controls.Add(this.abnormallistView);
            this.abnormalBox.Location = new System.Drawing.Point(436, 454);
            this.abnormalBox.Name = "abnormalBox";
            this.abnormalBox.Size = new System.Drawing.Size(212, 235);
            this.abnormalBox.TabIndex = 15;
            this.abnormalBox.TabStop = false;
            this.abnormalBox.Text = "AbnormalRecord";
            // 
            // abnormallistView
            // 
            this.abnormallistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.abnormallistView.Location = new System.Drawing.Point(6, 20);
            this.abnormallistView.Name = "abnormallistView";
            this.abnormallistView.Size = new System.Drawing.Size(200, 209);
            this.abnormallistView.TabIndex = 0;
            this.abnormallistView.UseCompatibleStateImageBehavior = false;
            this.abnormallistView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "TimeTAG";
            this.columnHeader5.Width = 69;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Info";
            this.columnHeader6.Width = 127;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.historyAlarmlistView);
            this.groupBox3.Location = new System.Drawing.Point(12, 279);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(642, 140);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HisAlarmInfo";
            // 
            // historyAlarmlistView
            // 
            this.historyAlarmlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.historyAlarmlistView.Location = new System.Drawing.Point(6, 20);
            this.historyAlarmlistView.Name = "historyAlarmlistView";
            this.historyAlarmlistView.Size = new System.Drawing.Size(624, 114);
            this.historyAlarmlistView.TabIndex = 1;
            this.historyAlarmlistView.UseCompatibleStateImageBehavior = false;
            this.historyAlarmlistView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Index";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "HistoryAlarm";
            this.columnHeader8.Width = 99;
            // 
            // HisAlarm
            // 
            this.HisAlarm.Location = new System.Drawing.Point(529, 425);
            this.HisAlarm.Name = "HisAlarm";
            this.HisAlarm.Size = new System.Drawing.Size(119, 23);
            this.HisAlarm.TabIndex = 16;
            this.HisAlarm.Text = "Query HisAlarm";
            this.HisAlarm.UseVisualStyleBackColor = true;
            this.HisAlarm.Click += new System.EventHandler(this.HisAlarm_Click);
            // 
            // DeviceComboBox
            // 
            this.DeviceComboBox.FormattingEnabled = true;
            this.DeviceComboBox.Location = new System.Drawing.Point(93, 50);
            this.DeviceComboBox.Name = "DeviceComboBox";
            this.DeviceComboBox.Size = new System.Drawing.Size(91, 20);
            this.DeviceComboBox.TabIndex = 17;
            this.DeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.DeviceComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "Device Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(402, 430);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "Count Num:";
            // 
            // hisCotNumtxtBox
            // 
            this.hisCotNumtxtBox.Location = new System.Drawing.Point(473, 427);
            this.hisCotNumtxtBox.Name = "hisCotNumtxtBox";
            this.hisCotNumtxtBox.Size = new System.Drawing.Size(38, 21);
            this.hisCotNumtxtBox.TabIndex = 20;
            this.hisCotNumtxtBox.TextChanged += new System.EventHandler(this.hisCotNumtxtBox_TextChanged);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 746);
            this.Controls.Add(this.hisCotNumtxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DeviceComboBox);
            this.Controls.Add(this.HisAlarm);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.abnormalBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.portTex);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.cncIpTex);
            this.Controls.Add(this.cncipLabel);
            this.Controls.Add(this.CurTimeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NumLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConnetBtn_click);
            this.Name = "Form1";
            this.Text = "cncIP";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.abnormalBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnetBtn_click;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView AlarmlistView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label NumLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurTimeLabel;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label cncipLabel;
        private System.Windows.Forms.TextBox cncIpTex;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTex;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label axisPositionInfoLable;
        private System.Windows.Forms.Label axisLoadInfoLabel;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.GroupBox abnormalBox;
        private System.Windows.Forms.ListView abnormallistView;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button HisAlarm;
        private System.Windows.Forms.ComboBox DeviceComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView historyAlarmlistView;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox hisCotNumtxtBox;
        private System.Windows.Forms.Timer timer3;
    }
}