using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Misc;

namespace BejeweledLivePlus.Misc
{
	public class Messager
	{
		public Messager()
		{
			this.mFont = null;
			this.mDefaultLife = 1.0;
			this.mJustification = Messager.EJustification.eJustification_Left;
		}

		public void Init(Font i_font, int iOpt_defaultColor)
		{
			this.Init(i_font, iOpt_defaultColor, 2.5);
		}

		public void Init(Font i_font)
		{
			this.Init(i_font, -1, 2.5);
		}

		public void Init(Font i_font, int iOpt_defaultColor, double iOpt_defaultLife)
		{
			this.mFont = i_font;
			this.mDefaultColor = iOpt_defaultColor;
			this.mDefaultLife = iOpt_defaultLife;
		}

		public void AddMessage(string i_msg, int iOpt_color)
		{
			this.AddMessage(i_msg, iOpt_color, -1.0);
		}

		public void AddMessage(string i_msg)
		{
			this.AddMessage(i_msg, -1, -1.0);
		}

		public void AddMessage(string i_msg, int iOpt_color, double iOpt_life)
		{
			Messager.Msg msg = new Messager.Msg();
			msg.LifeLeft = ((iOpt_life < 0.0) ? this.mDefaultLife : iOpt_life);
			msg.TextColor = ((iOpt_color < 0) ? this.mDefaultColor : iOpt_color);
			msg.Text = i_msg;
			this.mMessages.Add(msg);
		}

		public void Update()
		{
			this.Update(0.01);
		}

		public void Update(double i_deltaT)
		{
			for (int i = this.mMessages.Count - 1; i >= 0; i--)
			{
				this.mMessages[i].LifeLeft -= i_deltaT;
				if (this.mMessages[i].LifeLeft <= 0.0)
				{
					this.mMessages.RemoveAt(i);
				}
			}
		}

		public void Draw(Graphics g, int iOpt_x)
		{
			this.Draw(g, iOpt_x, 0);
		}

		public void Draw(Graphics g)
		{
			this.Draw(g, 0, 0);
		}

		public void Draw(Graphics g, int iOpt_x, int iOpt_y)
		{
			int num = iOpt_y;
			int num2 = iOpt_x;
			g.SetFont(this.mFont);
			for (int i = this.mMessages.Count - 1; i >= 0; i--)
			{
				Messager.Msg msg = this.mMessages[i];
				num -= g.GetFont().GetHeight();
				if (this.mJustification == Messager.EJustification.eJustification_Right)
				{
					num2 = -g.GetFont().StringWidth(msg.Text);
				}
				Color color = default(Color);
				Color color2 = default(Color);
				if (msg.LifeLeft < this.Draw_fadeAt)
				{
					g.SetColorizeImages(true);
					color = new Color(0, (int)(msg.LifeLeft / this.Draw_fadeAt * 255.0));
					color2 = new Color(msg.TextColor, (int)(msg.LifeLeft / this.Draw_fadeAt * 255.0));
				}
				else
				{
					color = new Color(0);
					color2 = new Color(msg.TextColor);
				}
				g.SetColor(color);
				g.DrawString(msg.Text, num2 + 1, num + 1);
				g.SetColor(color2);
				g.DrawString(msg.Text, num2, num);
				g.SetColorizeImages(false);
			}
			g.SetColor(new Color(-1));
		}

		public Messager.EJustification GetJustification()
		{
			return this.mJustification;
		}

		public void SetJustification(Messager.EJustification i_val)
		{
			this.mJustification = i_val;
		}

		private double Draw_fadeAt = (double)ModVal.M(0.5f);

		public Font mFont;

		public List<Messager.Msg> mMessages = new List<Messager.Msg>();

		protected Messager.EJustification mJustification;

		protected double mDefaultLife;

		protected int mDefaultColor;

		public enum EJustification
		{
			eJustification_Left,
			eJustification_Right
		}

		public class Msg
		{
			public string Text = string.Empty;

			public double LifeLeft;

			public int TextColor;
		}
	}
}
