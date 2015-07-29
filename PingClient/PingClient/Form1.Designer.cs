namespace PingClient
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.buttonPing = new System.Windows.Forms.Button();
            this.textBoxServerip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServerport = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPpm = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.labelPinglastmissed = new System.Windows.Forms.Label();
            this.labelPingpall = new System.Windows.Forms.Label();
            this.labelPingsumall = new System.Windows.Forms.Label();
            this.labelPingph = new System.Windows.Forms.Label();
            this.labelPingsumh = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBoxAlarm = new System.Windows.Forms.CheckBox();
            this.buttonexitserver = new System.Windows.Forms.Button();
            this.labeltotalmissed = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(330, 12);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStartStop.TabIndex = 0;
            this.buttonStartStop.Text = "Start";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // buttonPing
            // 
            this.buttonPing.Location = new System.Drawing.Point(330, 41);
            this.buttonPing.Name = "buttonPing";
            this.buttonPing.Size = new System.Drawing.Size(75, 23);
            this.buttonPing.TabIndex = 1;
            this.buttonPing.Text = "1 Ping";
            this.buttonPing.UseVisualStyleBackColor = true;
            this.buttonPing.Click += new System.EventHandler(this.buttonPing_Click);
            // 
            // textBoxServerip
            // 
            this.textBoxServerip.Location = new System.Drawing.Point(94, 12);
            this.textBoxServerip.Name = "textBoxServerip";
            this.textBoxServerip.Size = new System.Drawing.Size(100, 20);
            this.textBoxServerip.TabIndex = 2;
            this.textBoxServerip.Text = "192.168.0.10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "server IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "server port";
            // 
            // textBoxServerport
            // 
            this.textBoxServerport.Location = new System.Drawing.Point(94, 37);
            this.textBoxServerport.Name = "textBoxServerport";
            this.textBoxServerport.Size = new System.Drawing.Size(100, 20);
            this.textBoxServerport.TabIndex = 5;
            this.textBoxServerport.Text = "51717";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "pings last h";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "recieved last h";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "recieved this session";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "pings this session";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "last ping missed";
            // 
            // textBoxPpm
            // 
            this.textBoxPpm.Location = new System.Drawing.Point(94, 63);
            this.textBoxPpm.Name = "textBoxPpm";
            this.textBoxPpm.Size = new System.Drawing.Size(100, 20);
            this.textBoxPpm.TabIndex = 12;
            this.textBoxPpm.Text = "60";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "pings/min";
            // 
            // labelPinglastmissed
            // 
            this.labelPinglastmissed.AutoSize = true;
            this.labelPinglastmissed.Location = new System.Drawing.Point(103, 225);
            this.labelPinglastmissed.Name = "labelPinglastmissed";
            this.labelPinglastmissed.Size = new System.Drawing.Size(10, 13);
            this.labelPinglastmissed.TabIndex = 17;
            this.labelPinglastmissed.Text = "-";
            // 
            // labelPingpall
            // 
            this.labelPingpall.AutoSize = true;
            this.labelPingpall.Location = new System.Drawing.Point(124, 180);
            this.labelPingpall.Name = "labelPingpall";
            this.labelPingpall.Size = new System.Drawing.Size(10, 13);
            this.labelPingpall.TabIndex = 16;
            this.labelPingpall.Text = "-";
            // 
            // labelPingsumall
            // 
            this.labelPingsumall.AutoSize = true;
            this.labelPingsumall.Location = new System.Drawing.Point(124, 156);
            this.labelPingsumall.Name = "labelPingsumall";
            this.labelPingsumall.Size = new System.Drawing.Size(13, 13);
            this.labelPingsumall.TabIndex = 15;
            this.labelPingsumall.Text = "0";
            // 
            // labelPingph
            // 
            this.labelPingph.AutoSize = true;
            this.labelPingph.Location = new System.Drawing.Point(124, 131);
            this.labelPingph.Name = "labelPingph";
            this.labelPingph.Size = new System.Drawing.Size(10, 13);
            this.labelPingph.TabIndex = 14;
            this.labelPingph.Text = "-";
            // 
            // labelPingsumh
            // 
            this.labelPingsumh.AutoSize = true;
            this.labelPingsumh.Location = new System.Drawing.Point(124, 107);
            this.labelPingsumh.Name = "labelPingsumh";
            this.labelPingsumh.Size = new System.Drawing.Size(13, 13);
            this.labelPingsumh.TabIndex = 13;
            this.labelPingsumh.Text = "0";
            // 
            // chart1
            // 
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorGrid.Interval = 5D;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.Maximum = 0D;
            chartArea2.AxisX.Minimum = -20D;
            chartArea2.AxisX.Title = "time(minutes)";
            chartArea2.AxisX2.IsStartedFromZero = false;
            chartArea2.AxisX2.Maximum = 0D;
            chartArea2.AxisX2.Minimum = -20D;
            chartArea2.AxisY.Title = "Pings";
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(172, 107);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            series3.LabelToolTip = "Pings";
            series3.Name = "pings";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Color = System.Drawing.Color.Red;
            series4.LabelToolTip = "Missed Pings";
            series4.Name = "lostpings";
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(243, 146);
            this.chart1.TabIndex = 18;
            this.chart1.Text = "chart1";
            // 
            // checkBoxAlarm
            // 
            this.checkBoxAlarm.AutoSize = true;
            this.checkBoxAlarm.Location = new System.Drawing.Point(214, 16);
            this.checkBoxAlarm.Name = "checkBoxAlarm";
            this.checkBoxAlarm.Size = new System.Drawing.Size(90, 17);
            this.checkBoxAlarm.TabIndex = 19;
            this.checkBoxAlarm.Text = "Alarm on miss";
            this.checkBoxAlarm.UseVisualStyleBackColor = true;
            // 
            // buttonexitserver
            // 
            this.buttonexitserver.Location = new System.Drawing.Point(330, 70);
            this.buttonexitserver.Name = "buttonexitserver";
            this.buttonexitserver.Size = new System.Drawing.Size(75, 23);
            this.buttonexitserver.TabIndex = 20;
            this.buttonexitserver.Text = "exit server";
            this.buttonexitserver.UseVisualStyleBackColor = true;
            this.buttonexitserver.Click += new System.EventHandler(this.buttonexitserver_Click);
            // 
            // labeltotalmissed
            // 
            this.labeltotalmissed.AutoSize = true;
            this.labeltotalmissed.Location = new System.Drawing.Point(124, 203);
            this.labeltotalmissed.Name = "labeltotalmissed";
            this.labeltotalmissed.Size = new System.Drawing.Size(13, 13);
            this.labeltotalmissed.TabIndex = 22;
            this.labeltotalmissed.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "missed this session";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 252);
            this.Controls.Add(this.labeltotalmissed);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonexitserver);
            this.Controls.Add(this.checkBoxAlarm);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.labelPinglastmissed);
            this.Controls.Add(this.labelPingpall);
            this.Controls.Add(this.labelPingsumall);
            this.Controls.Add(this.labelPingph);
            this.Controls.Add(this.labelPingsumh);
            this.Controls.Add(this.textBoxPpm);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxServerport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxServerip);
            this.Controls.Add(this.buttonPing);
            this.Controls.Add(this.buttonStartStop);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Button buttonPing;
        private System.Windows.Forms.TextBox textBoxServerip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServerport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPpm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelPinglastmissed;
        private System.Windows.Forms.Label labelPingpall;
        private System.Windows.Forms.Label labelPingsumall;
        private System.Windows.Forms.Label labelPingph;
        private System.Windows.Forms.Label labelPingsumh;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox checkBoxAlarm;
        private System.Windows.Forms.Button buttonexitserver;
        private System.Windows.Forms.Label labeltotalmissed;
        private System.Windows.Forms.Label label10;
    }
}

