using System.Windows;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long number = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10);
            txtDisplay.Text = number.ToString();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 1;
            txtDisplay.Text = number.ToString();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 2;
            txtDisplay.Text = number.ToString();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 3;
            txtDisplay.Text = number.ToString();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 4;
            txtDisplay.Text = number.ToString();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 5;
            txtDisplay.Text = number.ToString();
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 6;
            txtDisplay.Text = number.ToString();
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 7;
            txtDisplay.Text = number.ToString();
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 8;
            txtDisplay.Text = number.ToString();
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            number = (number * 10) + 9;
            txtDisplay.Text = number.ToString();
        }
    }
}
