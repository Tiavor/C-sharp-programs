using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;


namespace STO_Partikel
{
    public partial class Form1 : Form
    {
       private Form2 f2 = new Form2();
       internal Form3 f3 = new Form3();
       
       [DllImport("user32.dll", SetLastError = true)]
       private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
       [DllImport("User32.dll")]
       public static extern IntPtr GetDC(IntPtr hwnd);
       [DllImport("User32.dll")]
       public static extern void ReleaseDC(IntPtr dc);
       [DllImport("User32.dll")]
       static extern int SetForegroundWindow(IntPtr point);

       BackgroundWorker bw = new BackgroundWorker();
       BackgroundWorker bw2 = new BackgroundWorker();
       BackgroundWorker bw3 = new BackgroundWorker();

       Brush aBrushY = (Brush)Brushes.Yellow;
       Brush aBrushG = (Brush)Brushes.LawnGreen;


       private int status = -1, //-1 stop, 0 wait, 1 selection, 2 scanning, 3 game prepare, 4 game
                   turns = 0, lastdelay = 0, interf = 0, lastlaneactive = -1, turnsS3 = 0;
       private bool bwIsRunning = false;


       //default fullscreen 1920x1200
       private Point[] points = {/*0*/new Point(831,771),/*pixel1 selection*/
                                  /*1*/new Point(760,440),/*pixel2 gamewindow*/
                                  /*2*/new Point(880,640),/*startbutton*/
                                  /*3*/new Point(938,552),/*lane1 select*/
                                  /*4*/new Point(740,560),/*lane1 mouse*/
                                  /*5*/new Point(938,613),/*lane2 select*/
                                  /*6*/new Point(740,610),/*lane2 mouse*/
                                  /*7*/new Point(938,672),/*lane3 select*/
                                  /*8*/new Point(740,677),/*lane3 mouse*/
                                  /*9*/new Point(938,744),/*lane4 select*/
                                 /*10*/new Point(740,735),/*lane4 mouse*/
                                 /*11*/new Point(1269,613),/*game finished*/
                                 /*12*/new Point(897,671),/*take reward*/
                                 /*13*/new Point(1324,175)/*k7 transport*/
                                 };
       private Point[] pointsBackup;
       //default -> voyager palette
       private int[] colors = { -16300000, //0 p1 >= mouseover selection
                                  -2500000, //1 p2 gamewindow
                                 -11800000, //2 p9 end
                                 -16300000, //3 p11 close docking mode
                               };
       private int[] colorsOld = {-16300000, //0 pixel1 selection
                                  -2500000, //1 pixel1 >= mouseover selection
                                 -11800000, //2 pixel2 gamewindow
                                 -14000000, //3 game particles
                                 -16300000, //4 selection interferance
                                 -15200000, //5 collect rewards
                                  -5000000  //6 close docking mode
                               };
       private int[] colorsBackup;

       private int[] dif = { 600000,// pixel1 value1
                              500000,// pixel2
                              200000// reward
                            };

        
        ////////////////////////////Initialization here///////////////////////////
        

        public Form1()
        {
            InitializeComponent();
            bw.WorkerSupportsCancellation = true;
            bw2.WorkerSupportsCancellation = true;
            bw3.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(runShowPixels);
            bw2.DoWork += new DoWorkEventHandler(runShowPixelInfo);
            bw3.DoWork += new DoWorkEventHandler(runOmegaMain);
            f2.thatParentForm = this;
            f3.thatParentForm = this;
            pointsBackup = points;
            colorsBackup = colors;
            resetPoints();
        }


        private void runOmegaMain(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (status>=0){

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
            switch (status)
            {
                case 0: //start bot
                    if (turns > 100)
                    {
                        status = -1;
                        break;
                    }
                    status = 1;
                    turns++;
                    for (int i = 0; i < 30; i++)
                        Thread.Sleep(100);
                    break;
                case 1: //wait for selection
                    checkK7();
                    if (checkPixel(0, 0, 0) || checkPixel(0, 1, -1)) //check for scanable particles
                    {
                        SimulateMouseMove(points[0].X, points[0].Y);
                        Thread.Sleep(300);
                        Thread.Sleep(50 + random1());
                        SimulateMouseMove(points[0].X + random2() + 2, points[0].Y + random1());
                        Thread.Sleep(300);
                        if (status == -1) break;
                        SimulateMouseClick();
                        status = 2;
                        break;
                    }
                    SimulateMouseMove(points[0].X + random2() + 5, points[0].Y + random1() + 5);
                    if (status == -1) break;
                    if (checkPixel(1, 2, 1)) { status = 2; break; }
                    for (int i = 0; i < 10; i++)
                        Thread.Sleep(100);
                    break;

                case 2: //scanning
                    status = 3;
                    SimulateMouseClick();
                    for (int i = 0; i < 40; i++)
                        Thread.Sleep(100);
                    checkK7();
                    turnsS3 = 0;
                    break;

                case 3: //start the game
                     if (checkPixel(1, 2, 1))
                     {
                         Thread.Sleep(50 + random1());
                         SimulateMouseMove(points[1].X, points[1].Y);
                         Thread.Sleep(50 + random1());
                         SimulateMouseClick();
                         Thread.Sleep(50 + random1());
                         SimulateMouseMove(points[2].X, points[2].Y);
                         Thread.Sleep(50 + random1());
                         SimulateMouseMove(points[2].X + random2(), points[2].Y + random1());
                         Thread.Sleep(50 + random1());
                         if (status == -1) break;
                         SimulateMouseClick();
                         for (int i = 0; i < 35; i++)
                             Thread.Sleep(100);
                         status = 4;
                         break;
                     }
                     for (int i = 0; i < 10; i++)
                         Thread.Sleep(100);
                     checkK7();
                     if (turnsS3 > 3)//if game did not start
                         status = 1;
                     else
                         turnsS3++;
                    break;
                case 4: //game running
                    for (int i = 3; i < 10; i += 2)
                    {
                        if (checkPixel(i, 3, -1))
                        {
                            SimulateMouseMove(points[i + 1].X, points[i + 1].Y);
                        }
                    }
                    interf++;
                    if (interf > 50)
                    {
                        interf = 0;
                        if (checkPixel(0, 4, 0)) // interference
                        {
                            SimulateMouseClick();
                        }
                    }
                    if (interf % 10 == 9)
                        if (checkPixel(11, 5, 2))
                            status = 5;
                    break;

                case 5: //collect rewards
                    for (int i = 0; i < 10; i++)
                        Thread.Sleep(100);
                    if (status == -1)
                        break;
                    SimulateMouseMove(points[12].X, points[12].Y);
                    Thread.Sleep(50 + random1());
                    SimulateMouseMove(points[12].X + random1(), points[12].Y + random1());
                    Thread.Sleep(50 + random1());
                    SimulateMouseClick();
                    // continue?
                    if (f2.numericUpDown1.Value <= 1)
                    {
                        status = -1;
                        f2.numericUpDown1.Value = 1;
                    }
                    else
                    {
                        f2.numericUpDown1.Value--;
                        status = 0;
                    }
                    break;
            }
            Thread.Sleep(10);
            }

            button1.Text = "Start";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (status != -1){//is running
                status = -1; // stop bot
                button1.Text = "Start";
            }
            else
            {   
                int delay = Decimal.ToInt32(f2.numericUpDown1.Value);
                if (delay != lastdelay)
                {
                    int d = delay - lastdelay;
                    points[3].X += d;
                    points[5].X += d;
                    points[7].X += d;
                    points[9].X += d;
                    lastdelay = delay;
                }

                Process p = Process.GetProcessesByName("GameClient").FirstOrDefault();
                if (p != null)
                {
                    savePoints(0);
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h);
                    bw3.RunWorkerAsync();
                    status = 0;// start bot
                    button1.Text = "Stop";
                }
                else
                    status = -1;
            }
        }


       static Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
       static Graphics grp = Graphics.FromImage(bmp);
        int ret = 0;

        //Pixelverarbeitung
        private int getPixel(int X, int Y)
        {
            ret = 0;
            grp.CopyFromScreen(new Point(X, Y), Point.Empty, new Size(1, 1));
            grp.Save();
            ret = bmp.GetPixel(0, 0).ToArgb();
            grp.Flush();
            return ret;
        }

        private bool checkPixel(int p, int c, int d)
        {
            //f2.labelX.Text = points[p].X.ToString();
            //f2.labelY.Text = points[p].Y.ToString();
            //f2.labelRGB.Text = getPixel(points[p].X, points[p].Y).ToString();
            if (d == -1)
                return getPixel(points[p].X, points[p].Y) >= colors[c];
            else
                return getPixel(points[p].X, points[p].Y) >= colors[c] && getPixel(points[p].X, points[p].Y) <= colors[c] + dif[d];
        }

        private void checkK7()
        {
            if (f2.checkBoxP11.Checked)
            if (checkPixel(13, 6, 0)) //docking check
            {
                SimulateMouseMove(points[13].X, points[13].Y);
                Thread.Sleep(10 + random1());
                SimulateMouseClick();
            }
        }




        //random
        private int random1()
        {
            return System.DateTime.Now.Minute % 5 - 2;
        }
        private int random2()
        {
            return System.DateTime.Now.Minute % 10 - 5;
        }


        //Mausfunktionen


        struct MouseInput
        {
            public int X; // X - Koordinate
            public int Y; // Y - Koordinate
            public uint MouseData; // Mausdaten, z.B. für Mausrad
            public uint DwFlags; // weitere Mausdaten, z.B. für Mausbuttons
            public uint Time; // Zeit des Events
            public IntPtr DwExtraInfo; // weitere Informationen
        }
        struct Input
        {
            public int Type; // Typ des Inputs, 0 für Maus  
            public MouseInput Data; // Mausdaten
        }
        // Konstanten für Mausflags
        const uint MOUSE_LEFTDOWN = 0x0002; // linken Mausbutton drücken
        const uint MOUSE_LEFTUP = 0x0004; // linken Mausbutton loslassen
        const uint MOUSE_ABSOLUTE = 0x8000; // ganzen Bildschirm ansprechen, nicht nur aktuelles Fenster
        const uint MOUSE_MOVE = 0x0001; // Maus bewegen

        private MouseInput CreateMouseInput(int x, int y, uint data, uint time, uint flag)
        {
            // aus gegebenen Daten Objekt vom Typ MouseInput erstellen, welches dann gesendet werden kann
            MouseInput Result = new MouseInput();
            Result.X = x;
            Result.Y = y;
            Result.MouseData = data;
            Result.Time = time;
            Result.DwFlags = flag;
            return Result;
        }
        private void SimulateMouseClick()
        {
            // Linksklick simulieren: Maustaste drücken und loslassen
            Input[] MouseEvent = new Input[1];
            MouseEvent[0].Type = 0;
            MouseEvent[0].Data = CreateMouseInput(0, 0, 0, 0, MOUSE_LEFTDOWN);

            SendInput((uint)MouseEvent.Length, MouseEvent, Marshal.SizeOf(MouseEvent[0].GetType()));

            Thread.Sleep(100);
            MouseEvent[0].Type = 0; // INPUT_MOUSE;
            MouseEvent[0].Data = CreateMouseInput(0, 0, 0, 0, MOUSE_LEFTUP);

            SendInput((uint)MouseEvent.Length, MouseEvent, Marshal.SizeOf(MouseEvent[0].GetType()));

        }

        private void SimulateMouseMove(int x, int y)
        {
            Input[] MouseEvent = new Input[1];
            MouseEvent[0].Type = 0;
            int Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            x = x * 65536/Width+1;
            if (x < 0) x = 0;
            if (x > 65536) x = 65536;
            y = y * 65536/Height+1;
            if (y < 0) y = 0;
            if (y > 65536) y = 65536;
            // Maus bewegen: Flags ABSOLUTE (ganzen Bildschirm verfügbar machen) und MOVE (bewegen)
            MouseEvent[0].Data = CreateMouseInput(x, y, 0, 0, MOUSE_ABSOLUTE | MOUSE_MOVE);
            SendInput((uint)MouseEvent.Length, MouseEvent, Marshal.SizeOf(MouseEvent[0].GetType()));
        }

        bool form2Active;
        private void button2_Click_1(object sender, EventArgs e) //extra button
        {
            if (form2Active)
            {
                f2.Hide();
                f3.Hide();
                form2Active = false;
            }
            else {
                form2Active = true;
                f2.Show();
                f3.Show();
                f2.Left = this.Left + this.Width;
                f2.Top = this.Top;
            }
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            if(form2Active)
                f2.Location = new Point(this.Left + this.Width, this.Top);
        }

        internal void savePoints()
        {
            savePoints(0);
            pointsBackup = points;
            colorsBackup = colors;
        }
        internal void savePoints(int tmp)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = f2.getCoords(i);
                int index=convertIndex(i);
                if (index>=0)
                    colors[index] = f2.getColor(i,true);
                //todo - save to config
            }
        }
        private int convertIndex(int i)
        {
            if (i < 2)
                return i;
            if (i == 8)
                return 2;
            if (i == 10)
                return 3;
            return -1;
        }
        private void loadConfig()
        {
            //todo - load from config
            resetPoints();
        }
        internal void showPoints()
        {
            if (bw.IsBusy)
                bw.CancelAsync();
            else
                bw.RunWorkerAsync();
        }
        internal void showPixelInfo()
        {
            if (bw2.IsBusy)
                bw2.CancelAsync();
            else
                bw2.RunWorkerAsync();
        }
        private void runShowPixelInfo(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (f2.showC)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                int x = Control.MousePosition.X,
                    y = Control.MousePosition.Y;
                f2.setPixelInfo(x, y, getPixel(x, y));
                
                Thread.Sleep(100);
            }
        }


        private void runShowPixels(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (f2.showPoints)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                for (int i = 0; i < 13; i++)
                {
                    f3.drawPixel(points[i].X, points[i].Y, aBrushG);
                }
                Thread.Sleep(10);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw2.IsBusy)
                bw2.CancelAsync();
            bw2.Dispose();
            if (bw3.IsBusy)
                bw3.CancelAsync();
            bw3.Dispose();
            f3.Dispose();
            f2.Dispose();
        }

        internal void resetPoints()
        {
            if (pointsBackup!=null)
                points = pointsBackup;
            if (colorsBackup != null)
                colors = colorsBackup;
            for (int i = 0; i < points.Length; i++)
            {
                f2.setCoords(i, points[i]);
            }
            for (int i = 0; i < colors.Length; i++)
            {
                f2.setColor(i, colors[i], false);
            }
        }
    }
}
