using System;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class GraphicsOperationChain
	{
		public GraphicsOperationChain()
		{
			this.operationRef_ = new GraphicsOperationRef();
		}

		public GraphicsOperationRef alloc(Graphics g, GraphicsOperation.IMAGE_TYPE type, int timestamp)
		{
			int num;
			if (this.count_ < 120000)
			{
				this.count_++;
				num = this.next_++;
				if (this.next_ >= 120000)
				{
					this.next_ -= 120000;
				}
			}
			else
			{
				num = this.head_++;
				if (this.head_ >= 120000)
				{
					this.head_ -= 120000;
				}
				this.next_ = this.head_;
			}
			this.content_[num].mType = type;
			this.content_[num].mDrawMode = g.mDrawMode;
			this.content_[num].mColor = (g.mColorizeImages ? g.mColor : Color.White);
			this.content_[num].mTimestamp = timestamp;
			this.operationRef_.prepare(this.content_, num);
			return this.operationRef_;
		}

		public int count()
		{
			return this.count_;
		}

		public void executeFrom(int timestamp, Graphics g)
		{
			bool flag = false;
			for (int i = 0; i < this.count_; i++)
			{
				int num = this.head_ + i;
				if (num >= 120000)
				{
					num -= 120000;
				}
				if (!flag)
				{
					if (this.content_[num].mTimestamp >= timestamp)
					{
						flag = true;
						i--;
					}
				}
				else
				{
					this.content_[num].Execute(g);
				}
			}
		}

		public void clearFrom(int timestamp)
		{
			int num = 0;
			for (int i = this.count_ - 1; i >= 0; i--)
			{
				int num2 = this.head_ + i;
				if (num2 >= 120000)
				{
					num2 -= 120000;
				}
				if (this.content_[num2].mTimestamp < timestamp)
				{
					break;
				}
				num++;
			}
			this.count_ -= num;
			this.next_ -= num;
			if (this.next_ < 0)
			{
				this.next_ += 120000;
			}
		}

		public void clearTo(int timestamp)
		{
			int num = 0;
			for (int i = 0; i < this.count_; i++)
			{
				int num2 = i + this.head_;
				if (num2 >= 120000)
				{
					num2 -= 120000;
				}
				GraphicsOperation graphicsOperation = this.content_[num2];
				if (graphicsOperation.mTimestamp > timestamp)
				{
					break;
				}
				num++;
			}
			this.count_ -= num;
			this.head_ += num;
			if (this.head_ >= 120000)
			{
				this.head_ -= 120000;
			}
		}

		public int lastTimestamp()
		{
			int result = -1;
			if (this.count_ > 0)
			{
				int num = this.next_ - 1;
				if (num < 0)
				{
					num += 120000;
				}
				result = this.content_[num].mTimestamp;
			}
			return result;
		}

		public int firstTimestamp()
		{
			int result = -1;
			if (this.count_ > 0)
			{
				result = this.content_[this.head_].mTimestamp;
			}
			return result;
		}

		private const int OPERATION_MAX = 120000;

		private GraphicsOperation[] content_ = new GraphicsOperation[120000];

		private int head_;

		private int next_;

		private int count_;

		private GraphicsOperationRef operationRef_;
	}
}
