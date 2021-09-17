using System;

namespace SexyFramework
{
	public class Ref<STRUCT_TYPE>
	{
		public Ref(STRUCT_TYPE initial)
		{
			this.value = initial;
		}

		public static implicit operator STRUCT_TYPE(Ref<STRUCT_TYPE> obj)
		{
			return obj.value;
		}

		public STRUCT_TYPE value;
	}
}
