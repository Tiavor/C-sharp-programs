using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace STO_Partikel
{
    public partial class Form3 : Form
    {
        Graphics g;
        internal Form1 thatParentForm { get; set; }
        public Form3()
        {
            InitializeComponent();
        }

        internal void drawPixel(int x, int y, Brush b)
        {
            g = this.CreateGraphics();
            g.FillRectangle(b, x, y, 5, 5);

        }

        //pass clicks through//

        public enum GWL
        {
            ExStyle = -20
        }

        public enum WS_EX
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

      /*  public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }*/

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

       // [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
      //  public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            //SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
        }
        //end of: pass clicks through//
    }
}
