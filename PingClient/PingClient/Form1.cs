using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PingClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sessionStart = System.DateTime.Now;
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //arguments.IP = "192.168.0.10";
            //arguments.port = 51717;
            sessionStats.pingsLost = 0;
            sessionStats.pingsSent = 0;
            sessionStats.pingTime = 0;
        }

        DateTime sessionStart;
        int lastwrittenminute = 0;
        int earliestminuteingraph = -20;
        pingstats[] pings = new pingstats[21];
        pingstats sessionStats=new pingstats();
        pingstats[] lastHStats=new pingstats[60];
        internal struct startingArg
        {
            internal string IP;
            internal int port;
            internal int pingPerMin;
        }
        internal struct pingstats
        {
            internal int pingsSent;
            internal int pingsLost;
            internal int pingTime;
        }
        private startingArg arguments = new startingArg();
        private alarm formAlarm = new alarm();
        BackgroundWorker bw = new BackgroundWorker();

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            startingArg arg = (startingArg)e.Argument;
            pingClient client = new pingClient(arg.IP, arg.port);
            pingstats report = new pingstats();
            report.pingsSent = 0;
            report.pingsLost = 0;
            report.pingTime = 0;
            if (!client.connect())
            {
                while (true)
                {
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        int ret = client.ping();
                        report.pingsSent += 1;
                        if (ret < 0)
                            report.pingsLost += 1;
                        //todo:try new connection
                        else
                            report.pingTime = ret;
                        worker.ReportProgress(0, report);
                        report.pingsSent = 0;
                        report.pingsLost = 0;
                        report.pingTime = 0;
                        if (ret > 0 && ret < 60000 / arg.pingPerMin)
                        {
                            Thread.Sleep(60000 / arg.pingPerMin - ret);
                        }
                        if (ret <= 0)
                            Thread.Sleep(60000 / arg.pingPerMin);
                    }
                }
                client.end();
            }
            else
            {
                report.pingsSent = 1;
                report.pingsLost = 1;
                worker.ReportProgress(1, report);
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // This method runs on the main thread.
            pingstats state =
                (pingstats)e.UserState;
            if (state.pingsSent > 0)
            {
                addToData(state.pingsSent, state.pingsLost, state.pingTime);
                updateDataPoints();
                updateGraph();
            }
            if (state.pingsLost > 0)
                alarm();
            if (e.ProgressPercentage == 1)
                buttonStartStop.Text = "Start";
        }

        private void alarm()
        {
            if (checkBoxAlarm.Checked&&!formAlarm.Visible)
                formAlarm.Show();
            labelPinglastmissed.Text = System.DateTime.Now.ToLocalTime().ToShortTimeString();
        }

        private void addToData(int pingsSent, int pingsLost, int pingsTime)
        {
            double minute = (System.DateTime.Now - sessionStart).TotalMinutes;
            if (minute > lastwrittenminute)
            {
                lastwrittenminute = (int)minute;
                updateDataPoints();
            }
            int t = pings[20].pingTime;
            int s = pings[20].pingsSent;
            int l = pings[20].pingsLost;
            if (pingsSent > pingsLost)
                pings[20].pingTime = (t * (s - l) + pingsTime * (pingsSent - pingsLost)) / (s - l + pingsSent - pingsLost);
            pings[20].pingsSent += pingsSent;
            pings[20].pingsLost += pingsLost;
            updateGraph();
            updateLabels();

            t = sessionStats.pingTime;
            s = sessionStats.pingsSent;
            l = sessionStats.pingsLost;
            if (pingsSent > pingsLost)
                sessionStats.pingTime = (t * (s - l) + pingsTime * (pingsSent - pingsLost)) / (s - l + pingsSent - pingsLost);
            sessionStats.pingsSent += pingsSent;
            sessionStats.pingsLost += pingsLost;

        }
        private void updateDataPoints()
        {
            while (earliestminuteingraph < lastwrittenminute - 20)
            {
                Array.Copy(lastHStats, 1, lastHStats, 0, lastHStats.Length - 1);
                lastHStats[59].pingsSent = pings[20].pingsSent;
                lastHStats[59].pingsLost = pings[20].pingsLost;
                lastHStats[59].pingTime = pings[20].pingTime;

                    Array.Copy(pings, 1, pings, 0, pings.Length - 1);
                pings[20].pingsSent = 0;
                pings[20].pingsLost = 0;
                pings[20].pingTime = 0;
                earliestminuteingraph++;
            }
        }

        private void updateLabels()
        {
            int sum = 0;
            int suml = 0;

            //hourly sum
            for (int i = 0; i < 60; i++)
                sum += lastHStats[i].pingsSent;
            labelPingsumh.Text = sum.ToString();
            for (int i = 0; i < 60; i++)
                suml += lastHStats[i].pingsLost;
            if (sum > 0)
                this.labelPingph.Text = ((sum*100 - suml*100) / sum).ToString() + " %";
            //session sum
            labelPingsumall.Text = sessionStats.pingsSent.ToString();
            if (sessionStats.pingsSent>0)
                labelPingpall.Text = ((sessionStats.pingsSent*100 - sessionStats.pingsLost*100) / sessionStats.pingsSent).ToString() + " %";

            labeltotalmissed.Text = sessionStats.pingsLost.ToString();

        }
        internal void updateGraph()
        {
            chart1.Series["pings"].Points.Clear();
            chart1.Series["lostpings"].Points.Clear();
            //chart1.Series["times"].Points.Clear();
            for (int i = 0; i <= 20; i++)
            {
                chart1.Series["pings"].Points.AddXY(i - 20, pings[i].pingsSent);
                chart1.Series["lostpings"].Points.AddXY(i - 20, pings[i].pingsLost);
                //chart1.Series["times"].Points.AddXY(i - 20, pings[i].pingTime);
            }
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            arguments.pingPerMin = Int32.Parse(textBoxPpm.Text);
            arguments.IP = textBoxServerip.Text;
            arguments.port = Int32.Parse(textBoxServerport.Text);
            if (bw.IsBusy != true && buttonStartStop.Text == "Start")
            {
                int ppm = Convert.ToInt32(textBoxPpm.Text);
                if (ppm > 0)
                {
                    arguments.pingPerMin = ppm;
                    buttonStartStop.Text = "Stop";
                    bw.RunWorkerAsync(arguments);
                }
            }
            else
            {
                buttonStartStop.Text = "Start";
                bw.CancelAsync();
            }
        }

        private void buttonPing_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                arguments.pingPerMin = Int32.Parse(textBoxPpm.Text);
                arguments.IP = textBoxServerip.Text;
                arguments.port = Int32.Parse(textBoxServerport.Text);
                bw.RunWorkerAsync(arguments);
                Thread.Sleep(100);
                bw.CancelAsync();
            }
        }

        private void buttonexitserver_Click(object sender, EventArgs e)
        {
            arguments.pingPerMin = Int32.Parse(textBoxPpm.Text);
            arguments.IP = textBoxServerip.Text;
            arguments.port = Int32.Parse(textBoxServerport.Text);
            startingArg arg = arguments;
            pingClient client = new pingClient(arg.IP, arg.port);
            if (!client.connect())
            {
                client.endServer();
            }
        }
    }
}
