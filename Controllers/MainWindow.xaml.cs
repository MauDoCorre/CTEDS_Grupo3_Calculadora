using Calculadora.Models;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (txtDisplay.Text != "0")
            {
                txtDisplay.Text += button.Content.ToString();
            }
            else
            {
                txtDisplay.Text = button.Content.ToString();
            }
            btnEquals.Focus();
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

            btnEquals.Focus();
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
                    txtDisplay.Text = Math.Round((number1 + number2), 8).ToString();
                    break;

                case "-":
                    txtDisplay.Text = Math.Round((number1 - number2), 8).ToString();
                    break;

                case "*":
                    txtDisplay.Text = Math.Round((number1 * number2), 8).ToString();
                    break;

                case "/":
                    txtDisplay.Text = Math.Round((number1 / number2), 8).ToString();
                    break;
            }
            NewOperation.Id = Guid.NewGuid();
            NewOperation.FullOperation = txtOngoing.Text + " " + txtDisplay.Text;
            NewOperation.Time = DateTime.Now.ToString("dd/MM HH:mm");
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

            btnEquals.Focus();
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
                    if (txtDisplay.Text.Substring(0, 1) != "-" && txtDisplay.Text != "0")
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

            btnEquals.Focus();
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
            btnEquals.Focus();
        }

        private void GetOperations()
        {
            OperationDataGrid.ItemsSource = context.Operations.OrderByDescending(x => x.Time).ToList();
        }

        /// <summary>
        /// Event handler para reconhecer teclas digitadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                //Numérico
                case Key.D0:
                case Key.NumPad0:
                    btn0.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D1:
                case Key.NumPad1:
                    btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D2:
                case Key.NumPad2:
                    btn2.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D3:
                case Key.NumPad3:
                    btn3.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D4:
                case Key.NumPad4:
                    btn4.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D5:
                case Key.NumPad5:
                    btn5.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D6:
                case Key.NumPad6:
                    btn6.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D7:
                case Key.NumPad7:
                    btn7.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D8:
                case Key.NumPad8:
                    btn8.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.D9:
                case Key.NumPad9:
                    btn9.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                //Operações

                case Key.Add:
                case Key.OemPlus:
                    btnPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Subtract:
                case Key.OemMinus:
                    btnMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Divide:
                    btnDivided.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Multiply:
                    btnTimes.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Enter:
                    btnEquals.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                //Outros
                case Key.Escape:
                    btnClear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.Delete:
                case Key.Back:
                    btnBackspace.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.OemComma:
                case Key.OemPeriod:
                case Key.Separator:
                case Key.Decimal:
                    btnComma.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;

                case Key.H:
                    if (mainTabControl.SelectedIndex == 0)
                    {
                        btnHistory.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                    else if (mainTabControl.SelectedIndex == 1)
                    {
                        btnBack.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                    break;

            }
        }
        
        private void DeleteOperation(object s, RoutedEventArgs e)
        {
            var operationToDelete = (s as FrameworkElement).DataContext as Operation;
            context.Operations.Remove(operationToDelete);
            context.SaveChanges();
            GetOperations();
        }
        
    }
}
