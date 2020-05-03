using Xamarin.Forms;


namespace TizenWearablePageSwitchTests.Tizen4
{
	class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
	{
		protected override void OnCreate()
		{
			base.OnCreate();

			LoadApplication(new App());
		}

		static void Main(string[] args)
		{
			var app = new Program();
			Forms.Init(app);
			Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
			//Tizen.Wearable.CircularUI.Forms.FormsCircularUI.Init();
			app.Run(args);
		}
	}
}
