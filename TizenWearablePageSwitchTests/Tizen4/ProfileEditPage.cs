using Tizen.Wearable.CircularUI.Forms;

using Xamarin.Forms;


//#nullable disable

namespace TizenWearablePageSwitchTests.Tizen4
{
	internal class ProfileEditPage : CirclePage, IPageBase
	{
		public ProfileEditPage(PageParameters pageParameters)
		{
			var profileEditView = new StackLayout
								{
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									VerticalOptions = LayoutOptions.CenterAndExpand,
									Children = { new Label { HorizontalTextAlignment = TextAlignment.Center, Text = "v4 ok - v5.5 NOT ok" } }
								};

			var profileEditViewScroll = new CircleScrollView { Content = profileEditView };

			Content = profileEditViewScroll;
		}

		#region Implementation of IPageBase
		public void OnResume() { }
		public void OnPause() { }
		public void OnDestroy() { }
		#endregion
	}
}