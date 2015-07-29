using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Collections.Specialized;
using System.Threading;


namespace STO_Partikel
{
    public partial class Form2 : Form
    {
        internal bool showC=false, showPoints;
        private int picture_ID=0, pixelinfoX,pixelinfoY,pixelinfoRGB, mode;
        private bool lockVar;
        internal Form1 thatParentForm { get; set; }
        private NumericUpDown[] pointSelectionListX;
        private NumericUpDown[] pointSelectionListY;
        private Label[] colorSelectList;

        public Form2()
        {
            InitializeComponent();
            setPic();
            pointSelectionListX=new NumericUpDown[] {numericUDp1x, numericUDp2x, numericUDp3x, numericUDp4x,
                numericUDp5x, numericUDp6x, numericUDp7x, numericUDp8x, numericUDp9x, numericUDp10x, numericUDp11x};
            pointSelectionListY = new NumericUpDown[] {numericUDp1y, numericUDp2y, numericUDp3y, numericUDp4y,
                numericUDp5y, numericUDp6y, numericUDp7y, numericUDp8y, numericUDp9y, numericUDp10y, numericUDp11y};
            colorSelectList = new Label[] {labelP1color,labelP2color,labelP9color,labelP11color};
        }
        private void buttonPixelInfoClick(object sender, EventArgs e)
        {
            if (showC){
                showC = false;
                thatParentForm.showPixelInfo();
                buttonShowPixelinfo.Text = "unfreeze";
            }
            else {
                showC = true;
                thatParentForm.showPixelInfo();
                buttonShowPixelinfo.Text = "freeze info";
            }
        }
        internal void setPixelInfo(int x, int y, int color)
        {
            if (!lockVar) { 
                pixelinfoX = x;
                pixelinfoY = y;
                pixelinfoRGB = color;
            }
        }

        private void buttonShowPoints_Click(object sender, EventArgs e)
        {

            if (showPoints)
            {
                if (thatParentForm.f3.Visible)
                    thatParentForm.f3.Hide();
                buttonShowPoints.Text = "show points";
                showPoints = false;
            }
            else
            {
                if (!thatParentForm.f3.Visible)
                    thatParentForm.f3.Show();
                buttonShowPoints.Text = "hide points";
                showPoints = true;
            }
        }

        private void buttonTogglePicture_Click(object sender, EventArgs e)
        {
            if (picture_ID < 4)
                picture_ID++;
            else picture_ID = 0;
            setPic();

        }

        private void setPic()
        {
            switch (picture_ID)
            {
                case 0: //no pic
                    this.Size = new Size(491, 537);
                    pictureBoxP1.Visible = false;
                    pictureBoxP2toP9.Visible = false;
                    pictureBoxP11.Visible = false;
                    break;
                case 1:
                    this.Size = new Size(673, 537);
                    pictureBoxP1.Visible = true;
                    break;
                case 2:
                    this.Size = new Size(890, 537);
                    pictureBoxP1.Visible = false;
                    pictureBoxP2toP9.Visible = true;
                    break;
                case 3:
                    this.Size = new Size(763, 537);
                    pictureBoxP2toP9.Visible = false;
                    pictureBoxP10.Visible=true;
                    break;
                case 4:
                    this.Size = new Size(686, 537);
                    pictureBoxP10.Visible = false;
                    pictureBoxP11.Visible=true;
                    break;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            thatParentForm.savePoints();
            buttonSave.Text = "saved";
        }

        private void numericUDp1x_ValueChanged(object sender, EventArgs e)
        {
            buttonSave.Text = "save points";
        }
        private void buttonSetColorClick(object sender, EventArgs e)
        {
            setNextBoxChecked();
        }
        private int getCheckedBox()
        {
            foreach (int indexChecked in checkedListBox1.CheckedIndices) {
                return indexChecked;
            }
            return -1;
        }
        private void setNextBoxChecked()
        {
            int i = getCheckedBox();
            if (i >= 0)
                checkedListBox1.SetItemChecked(i, false);
            i++;
            if (i<pointSelectionListX.Length-1)
                checkedListBox1.SetItemChecked(i,true);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lockVar = true;
            labelX.Text = pixelinfoX.ToString();
            labelY.Text = pixelinfoY.ToString();
            labelRGB.Text = pixelinfoRGB.ToString();
            int i = getCheckedBox();
            if (i>=0&&i<11){
                pointSelectionListX[i].Value = pixelinfoX;
                pointSelectionListY[i].Value = pixelinfoY;
                if (i < 2)
                    colorSelectList[i].BackColor = Color.FromArgb(pixelinfoRGB);

                if (i > 7)
                    colorSelectList[i-6].BackColor = Color.FromArgb(pixelinfoRGB);
            }
            lockVar = false;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {
                if (indexChecked!=e.Index)
                    checkedListBox1.SetItemChecked(indexChecked, false);
            }
        }
        internal void setCoords(int index, Point p)
        {
            if (index > pointSelectionListX.Length-1)
                return;
            pointSelectionListX[index].Value = p.X;
            pointSelectionListY[index].Value = p.Y;
        }
        internal void setColor(int index, int color,bool translation)
        {
            if (!translation) { 
                if (index >= 0 && index < colorSelectList.Length)
                    colorSelectList[index].BackColor = Color.FromArgb(color);
            }
            else{
                if (index>=0&&index<3)
                    colorSelectList[index].BackColor = Color.FromArgb(color);
                if(index==8)
                    colorSelectList[2].BackColor = Color.FromArgb(color);
                if(index==10)
                    colorSelectList[3].BackColor = Color.FromArgb(color);
           }
        }
        internal Point getCoords(int index)
        {
            if (index >= pointSelectionListX.Length)
                return new Point(0, 0);
            return new Point(Decimal.ToInt32(pointSelectionListX[index].Value), Decimal.ToInt32(pointSelectionListY[index].Value));
        }
        internal int getColor(int index,bool translation)
        {
            if (!translation) { 
                if (index>=0 && index<colorSelectList.Length)
                  return colorSelectList[index].BackColor.ToArgb();
            }
            else{
                int i=convertIndex(index);
                if (i>=0)
                    return colorSelectList[convertIndex(index)].BackColor.ToArgb();
            }
            return 0;
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
        private void buttonRest_Click(object sender, EventArgs e)
        {
            thatParentForm.resetPoints();
        }
    }
}
