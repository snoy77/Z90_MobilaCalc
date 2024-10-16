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

namespace Z90
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //0 - Ввод первого числа
        //  0 -> 1, Если ввод операции. Запоминаем операцию. Записываем в первое число
        //1 - Ввод второго числа
        //  1 -> 2, Если ввод "=". Записываем второе число
        //2 - Вычисление, результат записываем в первое число
        //  2 -> 0, Если встречаем число
        //  2 -> 1, Если встречаем операцию, операцию записываем

        private int status = 0;
        private int Status 
        { 
            get { return this.status; } 
            set { this.status = value; Console.WriteLine("MainWindow.this.status = " + value.ToString()); } 
        }


        private float digit_a;  //Первое число
        private float digit_b;  //Второе число

        private string command; //Выполняемая операция



        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Получаем контент нажатой кнопки
            Button sender_button = (Button)sender;
            string sender_content = sender_button.Content.ToString();
            Console.WriteLine(sender_content);

            //Обрабатываем исходя из контента и состояния
            switch (this.Status)
            {
                case 0:
                    {
                        switch (sender_content)
                        {
                            case "-":
                            case "/":
                            case "x":
                            case "+":
                                {
                                    
                                    this.command = sender_content;
                                    this.digit_a = float.Parse(textBox_down.Text);

                                    textBox_up.Text = textBox_down.Text + " " + this.command;
                                    textBox_down.Text = "0";

                                    this.Status = 1;
                                    break;
                                }
                            default:
                                {
                                    if(textBox_down.Text == "0")
                                    {
                                        textBox_down.Text = sender_content;
                                    }
                                    else
                                    { 
                                        textBox_down.Text += sender_content;
                                    }
                                    break;
                                }
                        }
                    }
                    break;
                    
                case 1:
                    {
                        switch (sender_content)
                        {
                            case "=":
                                {
                                    this.digit_b = float.Parse(textBox_down.Text);

                                    


                                    this.status = 2;
                                    break;
                                }
                            default:
                                {
                                    if (textBox_down.Text == "0")
                                    {
                                        textBox_down.Text = sender_content;
                                    }
                                    else
                                    {
                                        textBox_down.Text += sender_content;
                                    }
                                    break;
                                }
                        }

                        if (status == 2)
                        {
                            goto case 2;
                        }
                        break;
                    }
                    
                case 2:
                    {
                        float c = 0;
                        textBox_up.Text = this.digit_a.ToString() + " " + this.command + " " + this.digit_b.ToString();

                        switch (this.command)
                        {
                            case "+":
                                {
                                    c = this.digit_a + this.digit_b;
                                    break;
                                }
                            case "-":
                                {
                                    c = this.digit_a - this.digit_b;
                                    break;
                                }
                            case "/":
                                {
                                    c = this.digit_a / this.digit_b;
                                    break;
                                }
                            case "x":
                                {
                                    c = this.digit_a * this.digit_b;
                                    break;
                                }
                        }

                        this.digit_a = c;
                        textBox_down.Text = c.ToString();
                    }
                    break;
                default: break;
            }
        }
    }
}
