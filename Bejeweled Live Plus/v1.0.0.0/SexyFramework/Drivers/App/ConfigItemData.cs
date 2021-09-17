using System;
using SexyFramework.Misc;

namespace SexyFramework.Drivers.App
{
	public class ConfigItemData : ConfigItem
	{
		public ConfigItemData()
		{
			this.value = null;
			this.type = ConfigType.Data;
		}

		public override void load(Buffer buffer)
		{
			base.load(buffer);
			int num = buffer.ReadInt32();
			this.value = new byte[num];
			buffer.ReadBytes(ref this.value, num);
		}

		public override void save(Buffer buffer)
		{
			base.save(buffer);
			buffer.WriteInt32(this.value.Length);
			buffer.WriteBytes(this.value, this.value.Length);
		}

		public byte[] value;
	}
}
