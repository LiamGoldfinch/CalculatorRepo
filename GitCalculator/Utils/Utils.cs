using System;
using System.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Calculator.Utils
{
	public static class Utils
	{
		// This method evaluates a mathematical expression given as a string and returns the result as a 16-bit integer
		private static double executeExpression(string text)
		{
			string number = new DataTable().Compute(text, "").ToString();
			double numberDouble = Convert.ToDouble(number);

			return numberDouble;
		}

		// This method checks whether a given character is a numeric digit.
		private static bool isNumeric(char character)
		{
			return int.TryParse(character.ToString(), out _);
		}

		//
		public static void handleClick(string number)
		{
			TextBlock primaryDisplay = MainPage.mainPage.primaryDisplay;
			TextBlock secondaryDisplay = MainPage.mainPage.secondaryDisplay;
			TextBlock errorDisplay = MainPage.mainPage.errorDisplay;

			// the number cannot have more than 6 digits
			if (primaryDisplay.Text.Length < 6)
			{
				if (!(primaryDisplay.Text.Length == 0 && number == "0"))
				{
					if (secondaryDisplay.Text.Length + primaryDisplay.Text.Length <= 50)
					{
						primaryDisplay.Text += number;
					}
					else
					{
						errorDisplay.Text = "The exp. can have until 50 digits";
						FlyoutBase.ShowAttachedFlyout(primaryDisplay);
					}
				}
			}
			else
			{
				errorDisplay.Text = "The number can have until 6 digits";
				FlyoutBase.ShowAttachedFlyout(primaryDisplay);
			}
		}

		// This method handles what happens when an user clicks a operation (+, -, *, or /) 
		public static void handleOperationClick(string operation)
		{
			TextBlock primaryDisplay = MainPage.mainPage.primaryDisplay;
			TextBlock secondaryDisplay = MainPage.mainPage.secondaryDisplay;

			if (primaryDisplay.Text.Length + secondaryDisplay.Text.Length < 50)
			{
				if (secondaryDisplay.Text.Length != 0)
				{
					if (!isNumeric(secondaryDisplay.Text[secondaryDisplay.Text.Length - 1]) && primaryDisplay.Text.Length == 0)
						secondaryDisplay.Text = secondaryDisplay.Text.Substring(0, secondaryDisplay.Text.Length - 1) + operation;
					else
						secondaryDisplay.Text += primaryDisplay.Text + operation;
				}
				else if (primaryDisplay.Text.Length != 0)
				{
					secondaryDisplay.Text += primaryDisplay.Text + operation;
				}

				primaryDisplay.Text = "";
			}
		}

		// This method handles what happens when an user clicks the clear button, it will clear all text from the display 
		public static void handleClearButtonClick()
		{
			MainPage.mainPage.primaryDisplay.Text = "";
			MainPage.mainPage.secondaryDisplay.Text = "";
		}

		// This method handles what happens when the user clicks the "equals" (=) button,
		// it then attempts to evaluate the mathematical expression displayed on the screen
		// and then updates the display with the result or shows an error message if the evaluation fails.
		public static void handleEqualButtonClick()
		{
			TextBlock primaryDisplay = MainPage.mainPage.primaryDisplay;
			TextBlock secondaryDisplay = MainPage.mainPage.secondaryDisplay;
			TextBlock errorDisplay = MainPage.mainPage.errorDisplay;

			try
			{
				primaryDisplay.Text = executeExpression(secondaryDisplay.Text + primaryDisplay.Text).ToString();  //

				if (primaryDisplay.Text.Length > 6)
					primaryDisplay.FontSize = 48;
				secondaryDisplay.Text = "";
			}
			catch
			{
				errorDisplay.Text = "There's an error in the exp.";
				FlyoutBase.ShowAttachedFlyout(primaryDisplay);
			}
		}

		// This method handles what happens when an user hit backspace  
		public static void handleBackspace()
		{
			TextBlock primaryDisplay = MainPage.mainPage.primaryDisplay;

			if (primaryDisplay.Text.Length > 0)
				primaryDisplay.Text = primaryDisplay.Text.Substring(0, primaryDisplay.Text.Length - 1);
		}
	}
}