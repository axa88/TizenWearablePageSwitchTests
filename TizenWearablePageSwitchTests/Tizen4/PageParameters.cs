using System;


namespace TizenWearablePageSwitchTests.Tizen4
{
	public class PageParameters
	{
		internal PageParameters(AppPage targetPage, object pageData, string message, int messageLength, Action<PageParameters> pageSwitch)
		{
			TargetPage = targetPage;
			PageData = pageData;
			Message = message;
			MessageLength = messageLength;
			PageSwitch = pageSwitch;
		}

		internal AppPage TargetPage { get; }
		internal object PageData { get; }
		internal string Message { get; }
		internal int MessageLength { get; }
		internal Action<PageParameters> PageSwitch { get; }
	}
}
