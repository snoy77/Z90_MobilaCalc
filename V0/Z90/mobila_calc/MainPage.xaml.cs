

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
            string digit_b = this.calc.Digit_b.ToString();



            //Обрабатываем исходя из контента и состояния
            this.calc.DoWithSymbol(sender_content);

            if(this.calc.Status == 0)
            {
                this.textBox_up.Text = "";
                this.textBox_down.Text = this.calc.digit_actual;
                
            }
            else if (this.calc.Status == 1){
                this.textBox_up.Text = this.calc.Digit_a.ToString() + " " + this.calc.Command;
                this.textBox_down.Text = this.calc.digit_actual;
            }
            else if (this.calc.Status == 2)
            {
                this.textBox_up.Text = digit_a + " " + this.calc.Command + " " + this.calc.Digit_b.ToString();
                this.textBox_down.Text = this.calc.Digit_a.ToString();
            }

            if(this.textBox_down.Text == "")
            {
                this.textBox_down.Text = "0";
            }
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

        private int status = 0;
        public int Status { get { return this.status; } }


        public string digit_actual;
        private float digit_a;  //Первое число
        private float digit_b;  //Второе число
        //private float digit_r;  //результат число

        public float Digit_a { get { return this.digit_a; } }
        public float Digit_b { get { return this.digit_b; } }
        //public float Digit_r { get { return this.digit_r; } }

        private string command; //Выполняемая операция
        public string Command { get { return this.command; } }

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
        }
        public void DoWithSymbol(string symbol)
        {
            //Получаем контент нажатой кнопки
            //Button sender_button = (Button)sender;
            //string sender_content = sender_button.Text.ToString();
            //Console.WriteLine(sender_content);

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
                                    this.digit_a = float.Parse(this.digit_actual);
                                    this.digit_actual = "";
                                    this.command = symbol;

                                    this.status = 1;
                                    break;
                                }
                            default:
                                {
                                    this.digit_actual += symbol;   
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
                                    this.digit_b = float.Parse(this.digit_actual);
                                    
                                    this.DoCommand();

                                    this.status = 2;

                                    break;
                                }
                            case "+":
                            case "-":
                            case "x":
                            case "/":
                                {
                                    if (digit_actual == "")
                                    {
                                        this.command = symbol;
                                    }
                                    else 
                                    {
                                        this.digit_b = float.Parse(this.digit_actual);

                                        this.DoCommand();

                                        this.command = symbol;
                                        this.digit_actual = "";
                                    }
                                    break;
                                }
                            default:
                                {
                                    this.digit_actual += symbol;
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
                                    this.digit_actual = "";
                                    break;
                                }
                            case "+":
                            case "-":
                            case "x":
                            case "/":
                                {
                                    this.command = symbol;
                                    this.digit_actual = "";

                                    this.status = 1;
                                    break;
                                }
                            default:
                                {
                                    this.digit_actual = symbol;
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