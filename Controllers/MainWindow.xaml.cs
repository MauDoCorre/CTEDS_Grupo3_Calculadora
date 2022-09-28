using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string string1 = "";
        string string2 = "";
        string operation = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(txtDisplay.Text != "0")
            {
                txtDisplay.Text += button.Content.ToString();
            }
            else
            {
                txtDisplay.Text = button.Content.ToString();
            }
        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            operation = button.Content.ToString();

            if (string1 == "")
            {
                string1 = txtDisplay.Text;
                txtDisplay.Text = "0";
                txtOngoing.Text = string1 + " " + operation;
            }
            else
            {
                txtOngoing.Text = string1 + " " + operation;
            }
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            int number1, number2;

            string2 = txtDisplay.Text;
            Int32.TryParse(string1, out number1);
            Int32.TryParse(string2, out number2);
            txtOngoing.Text = string1 + " " + operation + " " + string2 + " =";

            switch (operation)
            {
                case "+":
                    txtDisplay.Text = (number1 + number2).ToString();
                    break;

                case "-":
                    txtDisplay.Text = (number1 - number2).ToString();
                    break;

                case "*":
                    txtDisplay.Text = (number1 * number2).ToString();
                    break;

                case "/":
                    txtDisplay.Text = (number1 / number2).ToString();
                    break;
            }

        }
    }
}
