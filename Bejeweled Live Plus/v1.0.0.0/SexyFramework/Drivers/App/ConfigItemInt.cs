using System;
using SexyFramework.Misc;

namespace SexyFramework.Drivers.App
{
	public class ConfigItemInt : ConfigItem
	{
		public ConfigItemInt()
		{
			this.value = 0;
			this.type = ConfigType.Int;
		}

		public override void load(Buffer buffer)
		{
			base.load(buffer);
			this.value = buffer.ReadInt32();
		}

		public override void save(Buffer buffer)
		{
			base.save(buffer);
			buffer.WriteInt32(this.value);
		}

		public int value;
	}
}
