using Xamarin.Forms;


namespace TizenWearablePageSwitchTests.Tizen4
{
	public class CellData : ICellData
	{
		public CellData(string primaryText, string secondaryText, bool dualLines, Command command, object commandParameter)
		{
			CommandParameter = commandParameter;
			Command = command;
			DualLines = dualLines;
			PrimaryText = primaryText;
			SecondaryText = secondaryText;
		}

		public object CommandParameter { get; set; }
		public Command Command { get; set; }
		public string PrimaryText { get; set; }
		public string SecondaryText { get; set; }
		public bool DualLines { get; set; }
	}


	public interface ICellData
	{
		object CommandParameter { get; }
		Command Command { get; }
		string PrimaryText { get; set; }
		string SecondaryText { get; set; }
		bool DualLines { get; }
	}
}