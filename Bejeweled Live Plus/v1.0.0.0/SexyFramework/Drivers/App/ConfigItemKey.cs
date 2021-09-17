using System;
using System.Collections.Generic;
using SexyFramework.Misc;

namespace SexyFramework.Drivers.App
{
	public class ConfigItemKey : ConfigItem
	{
		public ConfigItemKey()
		{
			this.keys = new List<ConfigItem>();
			this.type = ConfigType.Key;
		}

		public override void load(Buffer buffer)
		{
			base.load(buffer);
			int num = buffer.ReadInt32();
			bool flag = false;
			int num2 = 0;
			while (num2 < num && !flag)
			{
				ConfigItem configItem = null;
				switch (buffer.ReadInt32())
				{
				case 0:
					configItem = new ConfigItemKey();
					break;
				case 1:
					configItem = new ConfigItemString();
					break;
				case 2:
					configItem = new ConfigItemInt();
					break;
				case 3:
					configItem = new ConfigItemBoolean();
					break;
				case 4:
					configItem = new ConfigItemData();
					break;
				default:
					flag = true;
					break;
				}
				if (!flag)
				{
					configItem.load(buffer);
					this.keys.Add(configItem);
				}
				num2++;
			}
		}

		public override void save(Buffer buffer)
		{
			base.save(buffer);
			buffer.WriteInt32(this.keys.Count);
			foreach (ConfigItem configItem in this.keys)
			{
				configItem.save(buffer);
			}
		}

		public bool create(string name, ConfigType type)
		{
			bool result = false;
			ConfigItem configItem = this[name];
			if (configItem != null)
			{
				if (configItem.type == type)
				{
					result = true;
				}
			}
			else
			{
				switch (type)
				{
				case ConfigType.Key:
					configItem = new ConfigItemKey();
					break;
				case ConfigType.String:
					configItem = new ConfigItemString();
					break;
				case ConfigType.Int:
					configItem = new ConfigItemInt();
					break;
				case ConfigType.Boolean:
					configItem = new ConfigItemBoolean();
					break;
				case ConfigType.Data:
					configItem = new ConfigItemData();
					break;
				}
				if (configItem != null)
				{
					configItem.name = name;
					this.keys.Add(configItem);
					result = true;
				}
			}
			return result;
		}

		public ConfigItem this[string name]
		{
			get
			{
				ConfigItem result = null;
				foreach (ConfigItem configItem in this.keys)
				{
					if (configItem.name == name)
					{
						result = configItem;
						break;
					}
				}
				return result;
			}
		}

		public List<ConfigItem> keys;
	}
}
