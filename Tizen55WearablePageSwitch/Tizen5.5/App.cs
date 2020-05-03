using System;

using Newtonsoft.Json;

using Xamarin.Forms;


namespace Tizen55WearablePageSwitch.Tizen5._5
{
	public class App : Application
	{
		public App()
		{
			SwitchPage(new PageParameters(AppPage.Profiles, null, "", default, SwitchPage));
		}

		private void SwitchPage(PageParameters pageParameters)
		{
			IPageBase page;
			switch (pageParameters.TargetPage)
			{
				case AppPage.Profiles:
					page = new ProfilesPage(pageParameters);
					break;
				case AppPage.ProfileEdit:
					page = new ProfileEditPage(pageParameters);
					break;
				default: throw new ArgumentOutOfRangeException(nameof(pageParameters), pageParameters, "bad page switch");
			}

			MainPage = (Page)page;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}



	public class Credentials
	{
		private ushort _port = 80;

		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "host")]
		public string Host { get; set; }

		[JsonProperty(PropertyName = "port")]
		public ushort Port
		{
			get => _port;
			set => _port = (ushort)(value == default ? 80 : value);
		}

		[JsonProperty(PropertyName = "hideAddress")]
		public bool HideAddress { get; set; }

		[JsonProperty(PropertyName = "token")]
		public string Token { get; set; }

		[JsonProperty(PropertyName = "timeout")]
		public int Timeout { get; set; } = 8_000;
	}


	public enum AppPage
	{
		Main,
		Profiles,
		ProfileEdit
	}

	internal interface IPageBase
	{
		void OnResume();
		void OnPause();
		void OnDestroy();
	}
}
