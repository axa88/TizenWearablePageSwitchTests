using Tizen.Wearable.CircularUI.Forms;

using Xamarin.Forms;


namespace Tizen55WearablePageSwitch.Tizen5._5
{
	internal class ProfileEditPage : CirclePage, IPageBase
	{
		public ProfileEditPage(PageParameters pageParameters)
		{
			var profileEditView = new StackLayout { Children = { new Label { Text = "Hi" } } };

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