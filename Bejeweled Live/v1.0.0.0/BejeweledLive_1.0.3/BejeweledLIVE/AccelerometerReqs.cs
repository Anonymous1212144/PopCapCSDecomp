using System;

namespace BejeweledLIVE
{
	public enum AccelerometerReqs
	{
		ACCREQ_APP_IS_ACTIVE = 1,
		ACCREQ_NO_MODAL_OPEN,
		ACCREQ_BOARD_RUNNING = 4,
		ACCREQ_SHAKE_ENABLED = 8,
		ACCREQ_ALL_REQS = 15
	}
}
