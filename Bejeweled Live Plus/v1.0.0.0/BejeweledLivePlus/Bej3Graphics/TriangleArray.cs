using System;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class TriangleArray
	{
		public TriangleArray()
		{
			this.vertices_ = new SexyVertex2D[256, 3];
			this.capacity_ = 256;
			this.next_ = 0;
		}

		public void add(SexyVertex2D v1, SexyVertex2D v2, SexyVertex2D v3)
		{
			if (this.next_ >= this.capacity_)
			{
				this.expand();
			}
			this.vertices_[this.next_, 0] = v1;
			this.vertices_[this.next_, 1] = v2;
			this.vertices_[this.next_, 2] = v3;
			this.next_++;
		}

		public SexyVertex2D[,] toArray()
		{
			return this.vertices_;
		}

		public int count()
		{
			return this.next_;
		}

		public void clear()
		{
			this.next_ = 0;
		}

		private void expand()
		{
			SexyVertex2D[,] array = new SexyVertex2D[this.capacity_ + 256, 3];
			for (int i = 0; i < this.next_; i++)
			{
				array[i, 0] = this.vertices_[i, 0];
				array[i, 1] = this.vertices_[i, 1];
				array[i, 2] = this.vertices_[i, 2];
			}
			this.vertices_ = array;
			this.capacity_ += 256;
		}

		private SexyVertex2D[,] vertices_;

		private int capacity_;

		private int next_;
	}
}
