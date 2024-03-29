﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Lawn
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[DebuggerNonUserCode]
	internal class Strings
	{
		internal Strings()
		{
		}

		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Strings.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Lawn.Strings", typeof(Strings).Assembly);
					Strings.resourceMan = resourceManager;
				}
				return Strings.resourceMan;
			}
		}

		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return Strings.resourceCulture;
			}
			set
			{
				Strings.resourceCulture = value;
			}
		}

		internal static string CANCEL
		{
			get
			{
				return Strings.ResourceManager.GetString("CANCEL", Strings.resourceCulture);
			}
		}

		internal static string NO
		{
			get
			{
				return Strings.ResourceManager.GetString("NO", Strings.resourceCulture);
			}
		}

		internal static string OK
		{
			get
			{
				return Strings.ResourceManager.GetString("OK", Strings.resourceCulture);
			}
		}

		internal static string Update_Required
		{
			get
			{
				return Strings.ResourceManager.GetString("Update_Required", Strings.resourceCulture);
			}
		}

		internal static string YES
		{
			get
			{
				return Strings.ResourceManager.GetString("YES", Strings.resourceCulture);
			}
		}

		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;
	}
}
