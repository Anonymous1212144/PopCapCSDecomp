﻿using System;

namespace SexyFramework
{
	public enum MessageBoxFlags
	{
		MSGBOX_OK,
		MSGBOX_OKCANCEL,
		MSGBOX_ABORTRETRYIGNORE,
		MSGBOX_YESNOCANCEL,
		MSGBOX_YESNO,
		MSGBOX_RETRYCANCEL,
		MSGBOX_CANCELTRYCONTINUE,
		MSGBOX_ICONERROR = 16,
		MSGBOX_ICONQUESTION = 32,
		MSGBOX_ICONWARNING = 48,
		MSGBOX_ICONINFORMATION = 64
	}
}
