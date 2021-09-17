using System;

namespace ZumasRevenge
{
	public interface NewUserDialogListener
	{
		void BlankNameEntered();

		void NameIsAllSpaces();

		void FinishedNewUser(bool canceled);
	}
}
