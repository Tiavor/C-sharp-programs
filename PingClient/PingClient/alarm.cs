using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Shell; //needs .net4.6

namespace PingClient
{
    public partial class alarm : Form
    {
        public alarm()
        {
            InitializeComponent();
        }
        internal void changeText(string text){
            label1.Text=text;
        }
        //internal void blink()
        //{
        //    TaskbarItemProgressState.Indeterminate();
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
