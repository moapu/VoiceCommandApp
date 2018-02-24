using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow ()
		{
			InitializeComponent();
		}

		//Enumeration Type - defined set of named integral constants
		public enum Operator { None, Plus, Minus, Times, Divide, Equals }

		//private class variables
		private Operator lastOperator = Operator.None;
		private decimal valueSoFar = 0;
		private bool numberHitSinceLastOperator = false;
		private decimal storedNum = 0;

		private void HandleDigit ( decimal digit )
		{
			//Ternary Operator    
			//assign value = condition evaluate ? True (value) : False (value)    
			//valueSoFar and newValue are local variables
			string valueSoFar = numberHitSinceLastOperator ? textBoxDisplay.Text : "";
			string newValue = valueSoFar + digit.ToString();
			textBoxDisplay.Text = newValue;
			numberHitSinceLastOperator = true;
		}

		private void OnClickOperator ( object sender, RoutedEventArgs e )
		{
			Button btn = sender as Button;

			if ( btn.Tag != null )
			{
				Operator op = (Operator)btn.Tag;
				ExecuteLastOperator(op);
			}
		}

		private void Windows_Loaded_1 ( object sender, RoutedEventArgs e )
		{
			buttonMultiply.Tag = Operator.Times;
			buttonDivide.Tag = Operator.Divide;
			buttonSubtract.Tag = Operator.Minus;
			buttonAdd.Tag = Operator.Plus;
			buttonEqual.Tag = Operator.Equals;
		}

		private void ExecuteLastOperator ( Operator newOperator )
		{
			decimal currentValue = Convert.ToDecimal(textBoxDisplay.Text);
			decimal newValue = currentValue;
			if ( numberHitSinceLastOperator )
			{
				switch ( lastOperator )
				{
					case Operator.Plus:
						textBoxDisplay.Text = currentValue + " + ";
						newValue = valueSoFar + currentValue;
						break;
					case Operator.Minus:
						newValue = valueSoFar - currentValue;
						break;
					case Operator.Times:
						newValue = valueSoFar * currentValue;
						break;
					case Operator.Divide:
						if ( currentValue == 0 )
						{
							newValue = 0;
						}
						else
						{
							newValue = valueSoFar / currentValue;
						}
						break;
					case Operator.Equals:
						newValue = currentValue;
						break;
				}
			}

			valueSoFar = newValue;
			lastOperator = newOperator;
			numberHitSinceLastOperator = false;
			textBoxDisplay.Text = valueSoFar.ToString();
		}

		private void textBoxDisplay_TextChanged ( object sender, TextChangedEventArgs e )
		{

		}

		private void button0_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(0);
		}

		private void button1_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(1);
		}

		private void button2_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(2);
		}

		private void button3_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(3);
		}

		private void button4_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(4);
		}

		private void button5_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(5);
		}

		private void button6_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(6);
		}

		private void button7_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(7);
		}

		private void button8_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(8);
		}

		private void button9_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(9);
		}

		private void buttonC_Click ( object sender, RoutedEventArgs e )
		{
			textBoxDisplay.Clear();
		}

		// it deletes a digit, when there are more than one digit
		private void buttonBack_Click ( object sender, RoutedEventArgs e )
		{
			String str = textBoxDisplay.Text;

			if ( str.Length >= 1 )
			{
				str = str.Substring(0, str.Length - 1);
				textBoxDisplay.Text = str;
			}
		}

		private void buttonRoot_Click ( object sender, RoutedEventArgs e )
		{
			textBoxDisplay.Text = Math.Sqrt(Double.Parse(textBoxDisplay.Text)).ToString();
		}

		private void buttonMSign_Click ( object sender, RoutedEventArgs e )
		{
			textBoxDisplay.Text = (Double.Parse(textBoxDisplay.Text) * -1).ToString();
		}

		private void buttonCE_Click ( object sender, RoutedEventArgs e )
		{
			textBoxDisplay.Clear();
			valueSoFar = 0;
		}

		private void buttonMAdd_Click ( object sender, RoutedEventArgs e )
		{
			storedNum = valueSoFar;
			textBoxDisplay.Clear();
		}

		private void buttonMR_Click ( object sender, RoutedEventArgs e )
		{
			HandleDigit(storedNum);
		}

		private void buttonMC_Click ( object sender, RoutedEventArgs e )
		{
			storedNum = 0;
		}

		// DONE --> TODO: C --> 6 + 5 [C] 4 = 10
		// DONE --> TODO: CE --> Clears everything | Wipe Clean
		// DONE --> TODO: MC --> clear the memory
		// DONE --> TODO: M+ --> store in memory | 6 + 4 = 10 [M+] --> stores 10
		// DONE --> TODO: MR --> Recalls the stored memory
	}
}
