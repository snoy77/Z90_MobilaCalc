﻿

using Android.Hardware.Camera2;

namespace mobila_calc
{
    public partial class MainPage : ContentPage
    {

        private Calc calc;

        public MainPage()
        {
            InitializeComponent();
            this.calc = new Calc();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Получаем контент нажатой кнопки
            Button sender_button = (Button)sender;
            string sender_content = sender_button.Text.ToString();
            Console.WriteLine(sender_content);

            string digit_a = this.calc.Digit_a.ToString();

            //Обрабатываем исходя из контента и состояния
            this.calc.DoWithSymbol(sender_content);

            if(this.calc.Status == 0)
            {
                this.textBox_up.Text = "";
                this.textBox_down.Text = this.calc.Input_actual;
                
            }
            else if (this.calc.Status == 1){
                this.textBox_up.Text = this.calc.Digit_a.ToString() + " " + this.calc.Command;
                this.textBox_down.Text = this.calc.Input_actual;
            }
            else if (this.calc.Status == 2)
            {
                this.textBox_up.Text = digit_a + " " + this.calc.Command + " " + this.calc.Digit_b.ToString();
                this.textBox_down.Text = this.calc.Digit_a.ToString();
            }
        }

        private void ContentPage_Loaded(object sender, EventArgs e)
        {
            this.button_negativ.Text    = Calc.input_negative;
            this.button_separator.Text  = Calc.input_separator;
        }
    }



    public class Calc
    {
        //0 - Ввод первого числа
        //  0 -> 1, Если ввод операции. Запоминаем операцию. Записываем в первое число
        //1 - Ввод второго числа
        //  1 -> 2, Если ввод "=". Записываем второе число, выполняем вычисление
        //  1 -> 1, Если ввод операции: записываем второе число, выполняем вычисление, записываем команду, обнуляем актуальный ввод
        //2 - Работа с запомненными числами
        //  2 -> 2, Если встречаем "=":         выполняем вычисление.
        //  2 -> 1, Если встречаем операцию:    записываем операцию, обнуляем актуальный ввод
        //  2 -> 1, Если встерчаем число:       Обнуляем актуальный ввод
        //  2 -> 0, Если встречаем инверсию:    Ставим инверсию и начинаем новый ввод
        //  2 -> 0, Если встречаем разделитель: Обнуляем ввод, ставим разделитель, начинаем нвоый ввод

        private int status;

        private string command;             //Выполняемая операция

        private string input_actual;        //Актуальный ввод
        private string input_actual_null;   //Ввод по-умолчанию
        private float digit_a;              //Первое число
        private float digit_b;              //Второе число


        public const string input_separator = ",";
        public const string input_negative = "-+";

        public int Status           { get { return this.status; } }
        public string Command       { get { return this.command; } }
        public float Digit_a        { get { return this.digit_a; } }
        public float Digit_b        { get { return this.digit_b; } }
        public string Input_actual  { get { return this.input_actual; } }
        private float Digit_actual  { get { return float.Parse(this.input_actual); } }
        
        public Calc()
        {
            this.status = 0;
            this.input_actual_null = "0";

            digit_actual_reset();
        }

        public void digit_actual_reset() { this.input_actual = this.input_actual_null; }

        public void DoCommand()
        {
            switch (this.command)
            {
                case "+":
                    {
                        this.digit_a = this.digit_a + this.digit_b;
                        break;
                    }
                case "-":
                    {
                        this.digit_a = this.digit_a - this.digit_b;
                        break;
                    }
                case "/":
                    {
                        this.digit_a = this.digit_a / this.digit_b;
                        break;
                    }
                case "x":
                    {
                        this.digit_a = this.digit_a * this.digit_b;
                        break;
                    }
            }
            this.input_actual = this.digit_a.ToString();
        }
        private bool CanAddSeparator()
        {
            for (int i = 0; i < this.input_actual.Length; i++)
            {
                if (this.input_actual[i].ToString() == Calc.input_separator)
                {
                    return false;
                }
            }
            return true;
        }
        private void InputSeparator(string symbol)
        {
            if (this.CanAddSeparator())
            {
                this.input_actual += symbol;
            }
        }
        private void InputDigit(string symbol)
        {
            if (this.input_actual == this.input_actual_null)
            {
                this.input_actual = symbol;
            }
            else
            {
                this.input_actual += symbol;
            }
        }

        public void DoWithSymbol(string symbol)
        {
            switch (this.Status)
            {
                case 0:
                    {
                        switch (symbol)
                        {
                            case "-":
                            case "/":
                            case "x":
                            case "+":
                                {
                                    this.digit_a = this.Digit_actual;
                                    this.digit_actual_reset();
                                    this.command = symbol;

                                    this.status = 1;
                                    break;
                                }
                            case input_separator:
                                {
                                    this.InputSeparator(symbol);
                                    break;
                                }
                            case input_negative:
                                {
                                    if (this.input_actual != this.input_actual_null)
                                    {
                                        if (this.input_actual[0] != '-')
                                        {
                                            this.input_actual = "-" + this.input_actual;
                                        }
                                        else
                                        {
                                            this.input_actual = this.input_actual.Substring(1);
                                        }
                                    }
                                    break;
                                }
                            case "=":
                                { break; }
                            default:
                                {
                                    this.InputDigit(symbol);
                                    break;
                                }
                        }
                    }
                    break;

                case 1:
                    {
                        switch (symbol)
                        {
                            case "=":
                                {
                                    this.digit_b = this.Digit_actual;
                                    
                                    this.DoCommand();

                                    this.status = 2;

                                    break;
                                }
                            case "+":
                            case "-":
                            case "x":
                            case "/":
                                {
                                    this.digit_b = this.Digit_actual;

                                    this.DoCommand();

                                    this.command = symbol;
                                    this.digit_actual_reset();

                                    break;
                                }
                            case input_separator:
                                {
                                    this.InputSeparator(symbol);
                                    break;
                                }
                            case input_negative:
                                {
                                    if (this.input_actual != this.input_actual_null)
                                    {
                                        if (this.input_actual[0] != '-')
                                        {
                                            this.input_actual = "-" + this.input_actual;
                                        }
                                        else
                                        {
                                            this.input_actual = this.input_actual.Substring(1);
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    this.InputDigit(symbol);
                                    break;
                                }
                        }

                        break;
                    }
                case 2:
                    {
                        switch (symbol)
                        {
                            case "=":
                                {
                                    this.DoCommand();
                                    break;
                                }
                            case "+":
                            case "-":
                            case "x":
                            case "/":
                                {
                                    this.command = symbol;
                                    this.digit_actual_reset();

                                    this.status = 1;
                                    break;
                                }
                            case input_separator:
                                {
                                    this.input_actual = this.input_actual_null + Calc.input_separator;
                                    this.status = 0;
                                    break;
                                }
                            case input_negative:
                                {
                                    if (this.input_actual != this.input_actual_null)
                                    {
                                        if (this.input_actual[0] != '-')
                                        {
                                            this.input_actual = "-" + this.input_actual;
                                        }
                                        else
                                        {
                                            this.input_actual = this.input_actual.Substring(1);
                                        }
                                    }

                                    this.status = 0;
                                    break;
                                }
                            default:
                                {
                                    this.input_actual = symbol;
                                    this.status = 0;
                                    break;
                                }
                        }
                        break;
                    }

                default: break;
            }
        }
    }
}