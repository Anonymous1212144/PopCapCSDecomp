using System;
using SexyFramework.Misc;

namespace SexyFramework.Drivers.App
{
	public class ConfigItemBoolean : ConfigItem
	{
		public ConfigItemBoolean()
		{
			this.value = false;
			this.type = ConfigType.Boolean;
		}

		public override void load(Buffer buffer)
		{
			base.load(buffer);
			this.value = buffer.ReadBoolean();
		}

		public override void save(Buffer buffer)
		{
			base.save(buffer);
			buffer.WriteBoolean(this.value);
		}

		public bool value;
	}
}
