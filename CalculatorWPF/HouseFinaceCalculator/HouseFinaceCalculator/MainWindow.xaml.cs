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
            TotalDownPay_Label.Content = "$ " + totalDownPay;
            double leftOverPay = (purchaseValue - totalDownPay);
            LeftOverPay_Label.Content = "$ " + leftOverPay;
           
        }
        private void buttonResetDown_CLick(object sender, RoutedEventArgs e)
        {
            // Clears String Value
            PurchaseValue_Text.Text = String.Empty;
            PercentPay_Text.Text = String.Empty;
            TotalDownPay_Label.Content = String.Empty;
            LeftOverPay_Label.Content = String.Empty;
        }

		private void Calculate_Button ( object sender, RoutedEventArgs e )
		{
			double homeValue = Double.Parse(homeValueTextBox.Text);
			double avgTaxRate = Double.Parse(AvgTaxRateTextBox.Text);

			// equation
			double propertyTaxValue = (homeValue * avgTaxRate) / 100;
			propertyTaxQuantityLabel.Content = "$ " + propertyTaxValue;
		}

        private void buttonResetPropt_Click(object sender, RoutedEventArgs e)
        {
            // Clears String Value
            homeValueTextBox.Text = String.Empty;
            AvgTaxRateTextBox.Text = String.Empty;
            propertyTaxQuantityLabel.Content = String.Empty;
        }

        private void CalculateEstMonthPay_Button(object sender, RoutedEventArgs e)
        {
            double Mortgage = Double.Parse(MortgageAmount_Text.Text);
            MortgageAmount_Text.Text = "$ " + Mortgage;
            double Apr = Double.Parse(APR_Text.Text) / 100;
            APR_Text.Text += "%";
            double loanTerm = Double.Parse(LoanTerm_Text.Text);
            LoanTerm_Text.Text = loanTerm + "-Years";

            //equation
            double interest = (Mortgage * Apr);
            double total = (interest + Mortgage);
            double totalMonths = loanTerm * 12;
            double estimateMonthPayment = total / totalMonths;
            EstimateMonthlyPayLabel.Content = "$ " + estimateMonthPayment;
        }

        private void buttonResetMortgage_CLick(object sender, RoutedEventArgs e)
        {
            // Clears String Value
            MortgageAmount_Text.Text = String.Empty;
            APR_Text.Text = String.Empty;
            LoanTerm_Text.Text = String.Empty;
            EstimateMonthlyPayLabel.Content = String.Empty;
        }

		private void PercentPay_Text_TextChanged ( object sender, TextChangedEventArgs e )
		{

		}
	}
    
}
