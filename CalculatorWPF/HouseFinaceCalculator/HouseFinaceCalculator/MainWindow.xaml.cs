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
            textBoxDisplay2.Text = (Double.Parse(textBoxDisplay1.Text) * Double.Parse(textBoxDisplay.Text)).ToString();
            textBoxDisplay3.Text = (Double.Parse(textBoxDisplay.Text) - Double.Parse(textBoxDisplay2.Text)).ToString();
        }
    }
    
}
