using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class MainPage : Page
	{
		// add comment here
		public static MainPage mainPage { get; set; }

		// add comment here
		public MainPage()
		{
			InitializeComponent();

			mainPage = this;

			Window.Current.CoreWindow.CharacterReceived += keyPress;
		}

		// add comment here
		private void pageLoaded(object sender, RoutedEventArgs e)
		{
			// window minimum size
			ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(256, 384));

			// enable title bar full customiztion
			CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
			// title bar customization
			ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

			titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
			titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;
		}

		// add comment here
		private void handleClearButtonClick(object sender, RoutedEventArgs e)
		{
			Utils.Utils.handleClearButtonClick();
		}

		// add comment here
		private void handleEqualButtonClick(object sender, RoutedEventArgs e)
		{
			Utils.Utils.handleEqualButtonClick();
		}

		// add comment here
		private async void keyPress(CoreWindow sender, CharacterReceivedEventArgs args)
		{
			if ((args.KeyCode >= 48 && args.KeyCode <= 57) || (args.KeyCode >= 96 && args.KeyCode <= 105))
			{
				uint number;

				if (args.KeyCode >= 48 && args.KeyCode <= 57)
					number = args.KeyCode - 48;             // pressed key 0 ~ 9
				else
					number = args.KeyCode - 96;				// pressed  0 ~ 9 on number keypad

				Utils.Utils.handleClick(number.ToString());
			}
			else if (args.KeyCode == 27)
			{
				Utils.Utils.handleClearButtonClick();
			}
			else if (args.KeyCode == 13)
			{
				Utils.Utils.handleEqualButtonClick();
			}
			else if (args.KeyCode == '/')
			{
				Utils.Utils.handleOperationClick("/");
			}
			else if (args.KeyCode == '*')
			{
				Utils.Utils.handleOperationClick("*");
			}
			else if (args.KeyCode == '+')
			{
				Utils.Utils.handleOperationClick("+");
			}
			else if (args.KeyCode == '-')
			{
				Utils.Utils.handleOperationClick("-");
			}
			else if (args.KeyCode == 8)
			{
				Utils.Utils.handleBackspace();
			}
		}
	}
}