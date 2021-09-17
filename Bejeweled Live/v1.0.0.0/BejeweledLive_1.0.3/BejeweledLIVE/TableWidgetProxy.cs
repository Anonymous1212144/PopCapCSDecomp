using System;
using Sexy;

namespace BejeweledLIVE
{
	public interface TableWidgetProxy
	{
		void TableDrawRow(Graphics g, int rowIndex, bool selected);

		void TableRowSelected(int rowIndex);

		void TableDeselected();
	}
}
