using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ssl_aimbot
{
	// Token: 0x02000003 RID: 3
	public partial class Form1 : Form
	{
		// Token: 0x06000005 RID: 5
		[DllImport("user32.dll")]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

		// Token: 0x06000006 RID: 6
		[DllImport("user32.dll")]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		// Token: 0x06000007 RID: 7 RVA: 0x000020C4 File Offset: 0x000002C4
		protected override bool ProcessDialogKey(Keys keyData)
		{
			bool result;
			if (Control.ModifierKeys == Keys.None && keyData == Keys.Escape)
			{
				base.Close();
				result = true;
			}
			else
			{
				result = base.ProcessDialogKey(keyData);
			}
			return result;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 33554432;
				return createParams;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002130 File Offset: 0x00000330
		public Form1()
		{
			this.TopMost = true;
			this.TopLevel = true;
			Form1.RegisterHotKey(base.Handle, 1, 2, 87);
			Form1.RegisterHotKey(base.Handle, 2, 2, 83);
			Form1.RegisterHotKey(base.Handle, 3, 2, 65);
			Form1.RegisterHotKey(base.Handle, 4, 2, 68);
			Form1.RegisterHotKey(base.Handle, 5, 2, 81);
			Form1.RegisterHotKey(base.Handle, 6, 2, 0x48); // Rebound from C to H
			Form1.RegisterHotKey(base.Handle, 7, 2, 76);
			Form1.RegisterHotKey(base.Handle, 8, 2, 74);
			Form1.RegisterHotKey(base.Handle, 9, 2, 73);
			Form1.RegisterHotKey(base.Handle, 10, 2, 75);
			Form1.RegisterHotKey(base.Handle, 11, 2, 69);
			Form1.RegisterHotKey(base.Handle, 12, 2, 90);
			Form1.RegisterHotKey(base.Handle, 13, 2, 70);
			this.InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.FormBorderStyle = FormBorderStyle.None;
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				this.Capture = false;
				Message msg = Message.Create(this.Handle, 0XA1, new IntPtr(2), IntPtr.Zero);
				this.WndProc(ref msg);
			}
		}


		// Token: 0x0600000A RID: 10 RVA: 0x0000222C File Offset: 0x0000042C
		private void Form1_Shown(object sender, EventArgs e)
		{
			this.h = base.Height;
			this.w = base.Width;
			this.x = this.w / 2;
			this.y = this.h / 2;
			this.angle = 85;
			this.degree = 85;
			this.power = 100;
			this.g = 9.8;
			this.v = 1.317;
			this.r = 20.0;
			this.wind = 0;
			this.ww = 0.0125;
			this.ph = 145;
			this.len = 200;
			this.label1.Text = this.power.ToString();
			this.label2.Text = this.degree.ToString() + "°";
			this.label3.Text = this.v.ToString();
			this.label4.Text = this.ww.ToString();
			this.label5.Text = this.r.ToString();
			this.label6.Text = this.ph.ToString();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002374 File Offset: 0x00000574
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			Pen pen = new Pen(Color.FromArgb(47, 68, 86), 1f);
			graphics.DrawLine(pen, this.x, this.y + this.len, this.x, this.y - this.len);
			graphics.DrawLine(pen, this.x + this.len, this.y, this.x - this.len, this.y);
			SolidBrush solidBrush = new SolidBrush(Color.FromArgb(15, 219, 18));
			graphics.FillEllipse(solidBrush, this.x - 4, this.y - 4, 8, 8);
			double num = (double)this.angle * 0.017453292519943295;
			float num2 = (float)Convert.ToInt16(this.r * Math.Cos(num));
			float num3 = (float)Convert.ToInt16(this.r * Math.Sin(num));
			SolidBrush solidBrush2 = new SolidBrush(Color.White);
			for (float num4 = 0f; num4 < 50f; num4 += 0.05f)
			{
				float num5 = Convert.ToSingle((double)this.x + (double)this.power * this.v * (double)num4 * Math.Cos(num) + 0.5 * (double)this.wind * this.ww * (double)num4 * (double)num4);
				float num6 = Convert.ToSingle((double)this.y - (double)this.power * this.v * (double)num4 * Math.Sin(num) + 0.5 * this.g * (double)num4 * (double)num4);
				if (num6 <= (float)(this.h - this.ph))
				{
					graphics.FillEllipse(solidBrush2, num5 + num2 - 1f, num6 - num3 - 1f, 2f, 2f);
				}
			}
			graphics.Dispose();
			pen.Dispose();
			solidBrush.Dispose();
			solidBrush2.Dispose();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025A0 File Offset: 0x000007A0
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 786)
			{
				switch (m.WParam.ToInt32())
				{
				case 1:
					this.y--;
					break;
				case 2:
					this.y++;
					break;
				case 3:
					this.x--;
					break;
				case 4:
					this.x++;
					break;
				case 5:
					base.Close();
					break;
				case 6:
					if (base.Visible)
					{
						base.Visible = false;
					}
					else
					{
						base.Visible = true;
					}
					break;
				case 7:
					this.angle--;
					this.degree = this.angle;
					if (this.degree > 90)
					{
						this.degree = 180 - this.degree;
					}
					if (this.degree < -90)
					{
						this.degree = -180 - this.degree;
					}
					SendKeys.SendWait("{RIGHT}");
					this.label2.Text = this.degree.ToString() + "°";
					break;
				case 8:
					this.angle++;
					this.degree = this.angle;
					if (this.degree > 90)
					{
						this.degree = 180 - this.degree;
					}
					if (this.degree < -90)
					{
						this.degree = -180 - this.degree;
					}
					SendKeys.SendWait("{LEFT}");
					this.label2.Text = this.degree.ToString() + "°";
					break;
				case 9:
					if (this.power < 100)
					{
						this.power++;
					}
					SendKeys.SendWait("{UP}");
					this.label1.Text = this.power.ToString();
					break;
				case 10:
					if (this.power > 0)
					{
						this.power--;
					}
					SendKeys.SendWait("{DOWN}");
					this.label1.Text = this.power.ToString();
					break;
				case 11:
					this.x = Cursor.Position.X + 8;
					this.y = Cursor.Position.Y + 8;
					break;
				case 12:
					this.x = this.w / 2;
					this.y = this.h / 2;
					this.angle = 85;
					this.degree = 85;
					this.power = 100;
					this.wind = 0;
					this.label2.Text = this.degree.ToString() + "°";
					this.label1.Text = this.power.ToString();
					break;
				case 13:
				{
							try
                            {
								string value = Interaction.InputBox("Wind = ", "Wind", "0", this.w / 2 - 170, this.h / 2 - 50);
								this.wind = (int)Convert.ToInt16(value);
								break;
							} catch (Exception e)
                            {
								MessageBox.Show("Invalid wind input", "Error", MessageBoxButtons.OK);
								break;
                            }
				}
				case 14:
					this.v += 0.001;
					this.label3.Text = this.v.ToString();
					break;
				case 15:
					this.v -= 0.001;
					this.label3.Text = this.v.ToString();
					break;
				case 16:
					this.ww += 0.0001;
					this.label4.Text = this.ww.ToString();
					break;
				case 17:
					this.ww -= 0.0001;
					this.label4.Text = this.ww.ToString();
					break;
				case 18:
					this.r += 0.1;
					this.label5.Text = this.r.ToString();
					break;
				case 19:
					this.r -= 0.1;
					this.label5.Text = this.r.ToString();
					break;
				case 20:
					this.ph++;
					this.label6.Text = this.ph.ToString();
					break;
				case 21:
					this.ph--;
					this.label6.Text = this.ph.ToString();
					break;
				}
				base.Invalidate();
				base.Update();
			}
			base.WndProc(ref m);
		}

		// Token: 0x04000003 RID: 3
		private const int CTRL_UP = 1;

		// Token: 0x04000004 RID: 4
		private const int CTRL_DOWN = 2;

		// Token: 0x04000005 RID: 5
		private const int CTRL_LEFT = 3;

		// Token: 0x04000006 RID: 6
		private const int CTRL_RIGHT = 4;

		// Token: 0x04000007 RID: 7
		private const int EXIT = 5;

		// Token: 0x04000008 RID: 8
		private const int TOGGLE = 6;

		// Token: 0x04000009 RID: 9
		private const int DEG_RIGHT = 7;

		// Token: 0x0400000A RID: 10
		private const int DEG_LEFT = 8;

		// Token: 0x0400000B RID: 11
		private const int POW_UP = 9;

		// Token: 0x0400000C RID: 12
		private const int POW_DOWN = 10;

		// Token: 0x0400000D RID: 13
		private const int ADJUST = 11;

		// Token: 0x0400000E RID: 14
		private const int RESET = 12;

		// Token: 0x0400000F RID: 15
		private const int WIND = 13;

		// Token: 0x04000010 RID: 16
		private const int V_UP = 14;

		// Token: 0x04000011 RID: 17
		private const int V_DOWN = 15;

		// Token: 0x04000012 RID: 18
		private const int WW_UP = 16;

		// Token: 0x04000013 RID: 19
		private const int WW_DOWN = 17;

		// Token: 0x04000014 RID: 20
		private const int R_UP = 18;

		// Token: 0x04000015 RID: 21
		private const int R_DOWN = 19;

		// Token: 0x04000016 RID: 22
		private const int PH_UP = 20;

		// Token: 0x04000017 RID: 23
		private const int PH_DOWN = 21;

		// Token: 0x04000018 RID: 24
		private int h;

		// Token: 0x04000019 RID: 25
		private int w;

		// Token: 0x0400001A RID: 26
		private int x;

		// Token: 0x0400001B RID: 27
		private int y;

		// Token: 0x0400001C RID: 28
		private int angle;

		// Token: 0x0400001D RID: 29
		private int degree;

		// Token: 0x0400001E RID: 30
		private int power;

		// Token: 0x0400001F RID: 31
		private int wind;

		// Token: 0x04000020 RID: 32
		private int ph;

		// Token: 0x04000021 RID: 33
		private int len;

		// Token: 0x04000022 RID: 34
		private double g;

		// Token: 0x04000023 RID: 35
		private double v;

		// Token: 0x04000024 RID: 36
		private double r;

		// Token: 0x04000025 RID: 37
		private double ww;
	}
}
