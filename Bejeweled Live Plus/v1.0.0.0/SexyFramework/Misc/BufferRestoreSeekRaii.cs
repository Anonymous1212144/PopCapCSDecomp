using System;

namespace SexyFramework.Misc
{
	public class BufferRestoreSeekRaii : IDisposable
	{
		public BufferRestoreSeekRaii(Buffer buffer)
		{
			this.mBuffer = buffer;
			this.mOrigReadPos = buffer.GetCurrReadBytePos();
			this.mOrigWritePos = buffer.GetCurrWriteBytePos();
		}

		public void Dispose()
		{
			this.mBuffer.SeekReadByte(this.mOrigReadPos);
			this.mBuffer.SeekWriteByte(this.mOrigWritePos);
		}

		private Buffer mBuffer;

		private int mOrigReadPos;

		private int mOrigWritePos;
	}
}
