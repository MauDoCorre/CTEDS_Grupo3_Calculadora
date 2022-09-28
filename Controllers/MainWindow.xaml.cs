using System.Windows;
using System.Windows.Controls;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string number1 = "0";
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
    }
}
