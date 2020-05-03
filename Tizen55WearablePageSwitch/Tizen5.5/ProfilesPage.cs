using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

using Newtonsoft.Json.Linq;

using Tizen.Wearable.CircularUI.Forms;

using Xamarin.Forms;

using Color = Xamarin.Forms.Color;
using Label = Xamarin.Forms.Label;


namespace Tizen55WearablePageSwitch.Tizen5._5
{
	internal class ProfilesPage : CirclePage, IPageBase
	{
		internal const string ProfilesNode = "profiles";
		protected CircleListView ListView;
		protected readonly PageParameters PageParameters;

		public ProfilesPage(PageParameters pageParameters)
		{
			PageParameters = pageParameters;

			const string json = @"{
""profiles"":
	{
		""home"":
		{
			""credentials"":
				{
					""name"": ""home"",
					""host"": ""hostname"",
					""token"": ""49Vm8WRwZifK"",
					""timeout"": 100
				}
		},
		""name"":
		{
			""credentials"":
				{
					""name"": ""job"",
					""host"": ""ipaddress"",
					""token"": ""fhchfjfhjffj"",
					""timeout"": 100
				}
		}
	}
}";

			var jFile = JObject.Parse(json);
			var jCredentials = jFile[ProfilesNode]?.Values();
			var credentials = jCredentials?.Select(jToken => jToken?[nameof(Credentials).ToLower()]?.ToObject<Credentials>()).ToList() ?? new List<Credentials>();
			var cellData = (from credential in credentials
										let command = new Command<Credentials>(ListCellTap)
										select new CellData(credential.Name, credential.Host, credential.HideAddress, command, credential))
									.OrderBy(data => ((Credentials)data.CommandParameter).Name).ToList();

			ListView = new CircleListView
						{
							Header = new Label
									{
										FontSize = 20,
										FontAttributes = FontAttributes.Bold,
										VerticalOptions = LayoutOptions.Center,
										HorizontalOptions = LayoutOptions.Center,
										HorizontalTextAlignment = TextAlignment.Center,
										TextColor = Color.Default,
										Text = "Homes 🏘️",
									},
							BarColor = Color.Default,
							ItemTemplate = new ListCellDataTemplateSelector(),
							ItemsSource = new ProfileListModelView(cellData).ItemCollection,
						};

			Content = ListView;
			RotaryFocusObject = ListView;

			void ListCellTap(object commandParameter)
			{
				var editPageParameters = new PageParameters(AppPage.ProfileEdit, (Credentials)commandParameter, "", default, PageParameters.PageSwitch);
				PageParameters.PageSwitch(editPageParameters);
			}
		}

		protected override bool OnBackButtonPressed() => false;

		#region Implementation of IPageBase
		public void OnResume() { }
		public void OnPause() { }
		public void OnDestroy() { }
		#endregion
	}


	public class ProfileListModelView
	{
		public ProfileListModelView(ICollection<CellData> profiles)
		{
			ItemCollection = new ObservableCollection<LabelButtonCellViewModel>();

			foreach (var profile in profiles)
			{
				var listItem = new LabelButtonCellViewModel(profile);
				listItem.PropertyChanged += (sender, args) =>
											{
												switch (args.PropertyName)
												{
													case nameof(listItem.PrimaryText):
														break;

													case nameof(listItem.SecondaryText):
														break;

													case nameof(listItem.DualLines):
														break;

													case nameof(listItem.ButtonState):
														break;

													case nameof(listItem.CellCommand):
														break;

													case nameof(listItem.CellCommandParameter):
														break;
												}
											};

				ItemCollection.Add(listItem);
			}
		}

		public ObservableCollection<LabelButtonCellViewModel> ItemCollection { get; }
	}


	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public class LabelButtonCellViewModel : INotifyPropertyChanged
	{
		private readonly CellData _cellData;
		private bool _buttonState;
		public event PropertyChangedEventHandler PropertyChanged;

		public LabelButtonCellViewModel(ICellData cellData) => _cellData = (CellData)cellData;

		public string PrimaryText
		{
			get => _cellData.PrimaryText;
			set
			{
				if (_cellData.PrimaryText != value)
				{
					_cellData.PrimaryText = value;
					OnPropertyChanged();
				}
			}
		}

		public string SecondaryText
		{
			get => _cellData.SecondaryText;
			set
			{
				if (_cellData.SecondaryText != value)
				{
					_cellData.SecondaryText = value;
					OnPropertyChanged();
				}
			}
		}

		public bool DualLines
		{
			get => _cellData.DualLines;
			set
			{
				if (_cellData.DualLines != value)
				{
					_cellData.DualLines = value;
					OnPropertyChanged();
				}
			}
		}

		public Command CellCommand
		{
			get => _cellData.Command;
			set
			{
				if (_cellData.Command != value)
				{
					_cellData.Command = value;
					OnPropertyChanged();
				}
			}
		}

		public object CellCommandParameter
		{
			get => _cellData.CommandParameter;
			set
			{
				if (!_cellData.CommandParameter.Equals(value))
				{
					_cellData.CommandParameter = value;
					OnPropertyChanged();
				}
			}
		}

		public bool ButtonState
		{
			get => _buttonState;
			set
			{
				if (_buttonState && !value) // trigger on release only
				{
					_buttonState = false;
					OnPropertyChanged();
				}
				else
					_buttonState = value;
			}
		}

		public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
 }


	// ReSharper disable ClassNeverInstantiated.Global
	public class OneLabelButtonCell : LabelButtonCell
	{
		public OneLabelButtonCell() : base(false, false) {}
	}

	public class TwoLabelButtonCell : LabelButtonCell
	{
		public TwoLabelButtonCell() : base(true, false) {}
	}


	public class LabelButtonCell : ViewCell
	{
		private const ushort ScreenWidth = 360;
		private const byte ButtonWidth = 100;
		private const byte ImageButtonWidth = 80;
		private const byte CellHeight = 100;
		private const float PrimaryRatio = .66f;
		private const byte PrimaryDefaultTextSize = 18;
		private const byte SecondaryDefaultTextSize = 8;
		private const byte MinimumTextSize = 8;

		protected LabelButtonCell(bool dualLines, bool imageButton)
		{
			var textWidth = (ushort)(imageButton ? ScreenWidth - ImageButtonWidth : ScreenWidth - ButtonWidth);

			StackLayout textStack;

			var primaryText = new Label
			{
				//FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
				FontSize = PrimaryDefaultTextSize,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				HeightRequest = dualLines ? CellHeight * PrimaryRatio : CellHeight
			};
			primaryText.SetBinding(Label.TextProperty, nameof(LabelButtonCellViewModel.PrimaryText));

			if (dualLines)
			{
				var secondaryText = new Label
				{
					FontSize = SecondaryDefaultTextSize,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center,
					HeightRequest = CellHeight - primaryText.HeightRequest
				};
				secondaryText.SetBinding(Label.TextProperty, nameof(LabelButtonCellViewModel.SecondaryText));

				textStack = new StackLayout
				{
					WidthRequest = textWidth,
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Orientation = StackOrientation.Vertical,
					Children = { primaryText, secondaryText }
				};
			}
			else
			{
				textStack = new StackLayout
				{
					WidthRequest = textWidth,
					HorizontalOptions = LayoutOptions.Start,
					VerticalOptions = LayoutOptions.Center,
					Orientation = StackOrientation.Vertical,
					Children = { primaryText }
				};
			}

			View button;
			BindableProperty bindableProperty;
			if (imageButton)
			{
				button = new ImageButton
				{
					Padding = new Thickness(10, 10),
					WidthRequest = ImageButtonWidth,
					HeightRequest = CellHeight,
					HorizontalOptions = LayoutOptions.StartAndExpand,
					VerticalOptions = LayoutOptions.Center,
					Source = "trash.png"
				};
				bindableProperty = ImageButton.IsPressedProperty;
			}
			else
			{
				button = new Button
				{
					WidthRequest = ButtonWidth,
					HeightRequest = CellHeight,
					HorizontalOptions = LayoutOptions.StartAndExpand,
					VerticalOptions = LayoutOptions.Center,
					BackgroundColor = Color.Transparent,
					FontSize = 16,
					Text = "🗑️"
				};
				bindableProperty = Button.IsPressedProperty;
			}
			button.SetBinding(bindableProperty, nameof(LabelButtonCellViewModel.ButtonState));

			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty,
				nameof(LabelButtonCellViewModel.CellCommand));
			tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty,
				nameof(LabelButtonCellViewModel.CellCommandParameter));
			textStack.GestureRecognizers.Add(tapGestureRecognizer);

			// ToDo add secondary font size handling? why long text get cut off on each end...

			View = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Horizontal,
				Children = { textStack, button }
			};
		}
	}

	public class ListCellDataTemplateSelector : DataTemplateSelector
	{
		private readonly DataTemplate _oneLabelButtonCell = new DataTemplate(typeof(OneLabelButtonCell));
		private readonly DataTemplate _twoLabelButtonCell = new DataTemplate(typeof(TwoLabelButtonCell));

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return ((LabelButtonCellViewModel)item).DualLines ? _twoLabelButtonCell : _oneLabelButtonCell;
		}
	}
}