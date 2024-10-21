

using CalcU;
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
}