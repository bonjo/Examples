using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomWinForm
{
    public partial class Form1 : Form
    {
        bool isTopFormDragged = false;
        bool isTopBorderDragged = false;
        bool isLeftBorderDragged = false;
        bool isRightBorderDragged = false;
        bool isBottomBorderDragged = false;
        bool isMaximized = false;
        Point offset;
        Size _normalSize;
        Point _normalLocation = Point.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void TopBorderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isTopBorderDragged = true;
            else
                isTopBorderDragged = false;
        }

        private void TopBorderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Y < this.Location.Y)
                if (isTopBorderDragged)
                    if (this.Height < 50)
                    {
                        this.Height = 50;
                        isTopBorderDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y + e.Y);
                        this.Height -= e.Y;
                    }
        }

        private void TopBorderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isTopBorderDragged = false;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopFormDragged = true;
                Point pointStartPosition = this.PointToScreen(new Point(e.X, e.Y));
                offset = new Point(this.Location.X - pointStartPosition.X, this.Location.Y - pointStartPosition.Y);
            }
            else
                isTopFormDragged = false;
            if(e.Clicks==2)
            {
                isTopFormDragged = false;
                _MaxButton_Click(sender, e);
            }
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(isTopFormDragged)
            {
                Point newPoint = TopPanel.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(offset);
                this.Location = newPoint;
                if(this.Location.X>2||this.Location.Y>2)
                    if(this.WindowState==FormWindowState.Maximized)
                    {
                        this.Location = _normalLocation;
                        this.Size = _normalSize;
                        toolTip1.SetToolTip(_MaxButton, "Maximize");
                        _MaxButton.CFormState = MinMaxButton.CustomFormState.Normal;
                        isMaximized = false;
                    }
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isTopFormDragged = false;
            if(this.Location.Y<=5)
                if(!isMaximized)
                {
                    _normalSize = this.Size;
                    _normalLocation = this.Location;
                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    this.Location = new Point(0, 0);
                    this.Size = new Size(rect.Width, rect.Height);
                    toolTip1.SetToolTip(_MaxButton, "Restore Down");
                    _MaxButton.CFormState = MinMaxButton.CustomFormState.Maximize;
                    isMaximized = true;
                }
        }

        private void LeftPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Location.X <= 0 || e.X < 0)
            {
                isLeftBorderDragged = false;
                this.Location = new Point(10, this.Location.Y);
            }
            else
                if (e.Button == MouseButtons.Left)
                {
                    isLeftBorderDragged = true;
                }
                else
                {
                    isLeftBorderDragged = false;
                }
        }

        private void LeftPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < this.Location.X)
                if (isLeftBorderDragged)
                {
                    if (this.Width < 100)
                    {
                        this.Width = 100;
                        isLeftBorderDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X + e.X, this.Location.Y);
                        this.Width = this.Width - e.X;
                    }
                }
        }

        private void LeftPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isLeftBorderDragged = false;
        }

        private void RightPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isRightBorderDragged = true;
            else
                isRightBorderDragged = false;
        }

        private void RightPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRightBorderDragged)
                if (this.Width < 100)
                {
                    this.Width = 100;
                    isRightBorderDragged = false;
                }
                else
                    this.Width = this.Width + e.X;
        }

        private void RightPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isRightBorderDragged = false;
        }

        private void BottomPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isBottomBorderDragged = true;
            else
                isBottomBorderDragged = false;
        }

        private void BottomPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isBottomBorderDragged)
            {
                if (this.Height < 50)
                {
                    this.Height = 50;
                    isBottomBorderDragged = false;
                }
                else
                    this.Height = this.Height + e.Y;
            }
        }

        private void BottomPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isBottomBorderDragged = false;
        }

        private void _MinButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void _MaxButton_Click(object sender, EventArgs e)
        {
            if (isMaximized)
            {
                this.Location = _normalLocation;
                this.Size = _normalSize;
                toolTip1.SetToolTip(_MaxButton, "Maximize");
                _MaxButton.CFormState = MinMaxButton.CustomFormState.Normal;
                isMaximized = false;
            }
            else
            {
                _normalSize = this.Size;
                _normalLocation = this.Location;

                Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(0, 0);
                this.Size = new Size(rect.Width, rect.Height);
                toolTip1.SetToolTip(_MaxButton, "Restore Down");
                _MaxButton.CFormState = MinMaxButton.CustomFormState.Maximize;
                isMaximized = true;
            }
        }

        private void _CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}