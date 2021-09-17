using System;
using System.Collections.Generic;

namespace SexyFramework.Graphics
{
	public class PushedColorStack
	{
		public Color this[int idx]
		{
			get
			{
				if (idx < 0)
				{
					return Color.White;
				}
				return this.mColors[idx];
			}
			set
			{
				if (idx < 0)
				{
					return;
				}
				this.mColors[idx] = value;
			}
		}

		public void CopyFrom(PushedColorStack rhs)
		{
			if (this != rhs)
			{
				this.mColors.Clear();
				foreach (Color color in rhs.mColors)
				{
					this.mColors.Add(color);
				}
			}
		}

		public void Add(Color c)
		{
			this.mColors.Add(c);
		}

		public void Push_back(Color c)
		{
			this.mColors.Add(c);
		}

		public Color Back()
		{
			return this.mColors.back<Color>();
		}

		public void Pop_back()
		{
			this.mColors.RemoveAt(this.mColors.Count);
		}

		public void RemoveAt(int idx)
		{
			if (idx < 0)
			{
				return;
			}
			this.mColors.RemoveAt(idx);
		}

		public int Count
		{
			get
			{
				return this.mColors.Count;
			}
		}

		public int Size()
		{
			return this.mColors.Count;
		}

		private List<Color> mColors = new List<Color>();
	}
}
