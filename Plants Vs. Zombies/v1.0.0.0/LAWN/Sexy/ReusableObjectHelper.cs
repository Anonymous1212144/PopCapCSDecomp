﻿using System;
using System.Collections.Generic;

namespace Sexy
{
	internal class ReusableObjectHelper<T> where T : IReusable, new()
	{
		public T GetNew()
		{
			if (this.unusedInstances.Count != 0)
			{
				return this.unusedInstances.Pop();
			}
			if (default(T) != null)
			{
				return default(T);
			}
			return new T();
		}

		public void PushOnToReuseStack(T obj)
		{
			obj.Reset();
			this.unusedInstances.Push(obj);
		}

		private Stack<T> unusedInstances = new Stack<T>();
	}
}
