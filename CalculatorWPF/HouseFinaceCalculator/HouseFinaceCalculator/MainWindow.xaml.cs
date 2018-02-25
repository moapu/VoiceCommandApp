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

namespace HouseFinaceCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonCal_CLick(object sender, RoutedEventArgs e)
        {
            double purchaseValue = Double.Parse(PurchaseValue_Text.Text);
            PurchaseValue_Text.Text = "$ " + purchaseValue;
            double percentPay = Double.Parse(PercentPay_Text.Text) / 100;
            PercentPay_Text.Text += "%";

            // equation
            double totalDownPay = (purchaseValue * percentPay);
            TotalDownPay_Text.Text = "$ " + totalDownPay;
            double leftOverPay = (purchaseValue - totalDownPay);
            LeftOverPay_Text.Text = "$ " + leftOverPay;
           
        }
        private void buttonResetDown_CLick(object sender, RoutedEventArgs e)
        {
            // Clears String Value
            PurchaseValue_Text.Text = String.Empty;
            PercentPay_Text.Text = String.Empty;
            TotalDownPay_Text.Text = String.Empty;
            LeftOverPay_Text.Text = String.Empty;
        }

		private void Calculate_Button ( object sender, RoutedEventArgs e )
		{
			double homeValue = Double.Parse(homeValueTextBox.Text);
			double avgTaxRate = Double.Parse(AvgTaxRateTextBox.Text);

			// equation
			double propertyTaxValue = (homeValue * avgTaxRate) / 100;
			propertyTaxQuantityLabel.Content = "$ " + propertyTaxValue;
		}

        private void Reset_Button(object sender, RoutedEventArgs e)
        {
            homeValueTextBox.Text = String.Empty;
            AvgTaxRateTextBox.Text = String.Empty;
            propertyTaxQuantityLabel.Content = String.Empty;
        }
    }
    
}
