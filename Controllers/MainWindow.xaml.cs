using Calculadora.Models;
using System;
using System.Windows;
using System.Linq;
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
        bool commaUsed = false;
        private readonly Context context;
        Operation NewOperation = new Operation();
        

        public MainWindow(Context context)
        {
            this.context = context;
            InitializeComponent();
        }

        /// <summary>
        /// Função responsável pelas interações com os botões numéricos
        /// </summary>
        /// <param name="sender">Botões de numerais</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Função responsável pelas interações com os botões de operações
        /// </summary>
        /// <param name="sender">Botões de operações (+, -, *, /)</param>
        /// <param name="e"></param>
        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            operation = button.Content.ToString();

            checkComma();

            if (string1 == "")
            {
                string1 = txtDisplay.Text;
                txtDisplay.Text = "0";
                txtOngoing.Text = string1 + " " + operation;
                commaUsed = false;
            }
            else
            {
                txtOngoing.Text = string1 + " " + operation;
            }
        }

        /// <summary>
        /// Função responsável pela interação com o botão de igual
        /// </summary>
        /// <param name="sender">Botão "="</param>
        /// <param name="e"></param>
        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            double number1, number2;

            checkComma();

            string2 = txtDisplay.Text;
            Double.TryParse(string1, out number1);
            Double.TryParse(string2, out number2);
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
            NewOperation.Id = Guid.NewGuid();
            NewOperation.FullOperation = txtOngoing.Text + " " + txtDisplay.Text;
            NewOperation.Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            context.Operations.Add(NewOperation);
            context.SaveChanges();
        }

        /// <summary>
        /// Função responsável pelas interações com os botões de limpeza
        /// </summary>
        /// <param name="sender">Botões "CE", "C" e "DEL"</param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            switch (button.Content.ToString())
            {
                case "CE":
                    txtDisplay.Text = "0";
                    commaUsed = false;
                    break;

                case "C":
                    string1 = "";
                    string2 = "";
                    operation = "";
                    txtDisplay.Text = "0";
                    txtOngoing.Text = "0";
                    commaUsed = false;
                    break;

                case "DEL":
                    if (txtDisplay.Text.Substring(txtDisplay.Text.Length - 1) == ",")
                    {
                        commaUsed = false;
                    }
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
                    break;
            }
        }

        /// <summary>
        /// Função responsável pelas interações com botões diversos
        /// </summary>
        /// <param name="sender">Botões diversos (+-, ",")</param>
        /// <param name="e"></param>
        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            switch (button.Content.ToString())
            {
                case "+-":
                    if(txtDisplay.Text.Substring(0, 1) != "-" && txtDisplay.Text != "0")
                    {
                        txtDisplay.Text = "-" + txtDisplay.Text;
                    }
                    else if (txtDisplay.Text != "0")
                    {
                        txtDisplay.Text = txtDisplay.Text.Substring(1);
                    }
                    break;

                case ",":
                    if (!commaUsed)
                    {
                        txtDisplay.Text = txtDisplay.Text + ",";
                        commaUsed = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// Função para checar existência de vírgula antes de enviar o dado para operações em andamento
        /// </summary>
        private void checkComma()
        {
            if (txtDisplay.Text.Substring(txtDisplay.Text.Length - 1) == ",")
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
        }

        /// <summary>
        /// Função para ir para a tela de histórico de operações
        /// </summary>
        /// <param name="sender">Botão de histórico de operações</param>
        /// <param name="e"></param>
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            GetOperations();
            mainTabControl.SelectedIndex = 1;
        }

        /// <summary>
        /// Função para voltar da tela de histórico de operações
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 0;
        }

        private void GetOperations() 
        {
            OperationDataGrid.ItemsSource = context.Operations.ToList();
        }
    }
}
