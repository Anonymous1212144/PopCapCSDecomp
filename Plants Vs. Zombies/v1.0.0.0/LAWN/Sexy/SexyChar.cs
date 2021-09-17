﻿using System;

namespace Sexy
{
	internal struct SexyChar
	{
		public SexyChar(char c)
		{
			this.value_type = c;
		}

		public static bool operator ==(SexyChar a, KeyCode b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(SexyChar a, KeyCode b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			if (obj is SexyChar)
			{
				return this.value_type == ((SexyChar)obj).value_type;
			}
			if (obj is KeyCode)
			{
				KeyCode keyCode = (KeyCode)obj;
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.value_type.GetHashCode();
		}

		public override string ToString()
		{
			return this.value_type.ToString();
		}

		public char value_type;
	}
}
