namespace CalculatorCet
{
    public partial class MainPage : ContentPage
    {
        double FirstNumber = 0;
        double Memory = 0;
        bool isFirstNumberAfterOperator = true;
        Operator PreviousOperator = Operator.None;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Digit_Clicked(object sender, EventArgs e)
        {
            Button digitButton = sender as Button;
            if (isFirstNumberAfterOperator)
            {
                Display.Text = digitButton.Text == "." ? "0." : digitButton.Text;
                isFirstNumberAfterOperator = false;
            }
            else
            {
                if (digitButton.Text == "." && Display.Text.Contains(".")) return;
                Display.Text += digitButton.Text;
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e) => SetOperator(Operator.Add);
        private void SubtractButton_Clicked(object sender, EventArgs e) => SetOperator(Operator.Subtract);
        private void MultiplyButton_Clicked(object sender, EventArgs e) => SetOperator(Operator.Multiply);
        private void DivideButton_Clicked(object sender, EventArgs e) => SetOperator(Operator.Divide);

        private void SetOperator(Operator op)
        {
            if (!isFirstNumberAfterOperator)
                DoCalculation();
            PreviousOperator = op;
            isFirstNumberAfterOperator = true;
        }

        private void DoCalculation()
        {
            double currentNumber = double.Parse(Display.Text);

            switch (PreviousOperator)
            {
                case Operator.Add:
                    FirstNumber += currentNumber;
                    break;
                case Operator.Subtract:
                    FirstNumber -= currentNumber;
                    break;
                case Operator.Multiply:
                    FirstNumber *= currentNumber;
                    break;
                case Operator.Divide:
                    if (currentNumber == 0)
                    {
                        Display.Text = "Error";
                        ResetAll();
                        return;
                    }
                    FirstNumber /= currentNumber;
                    break;
                case Operator.None:
                    FirstNumber = currentNumber;
                    break;
            }

            Display.Text = FirstNumber.ToString();
        }

        private void EqualButton_Clicked(object sender, EventArgs e)
        {
            DoCalculation();
            PreviousOperator = Operator.None;
        }

        private void CEButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = "0";
            isFirstNumberAfterOperator = true;
        }

        private void CButton_Clicked(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void MSButton_Clicked(object sender, EventArgs e)
        {
            Memory = double.Parse(Display.Text);
        }

        private void MRButton_Clicked(object sender, EventArgs e)
        {
            Display.Text = Memory.ToString();
            if (PreviousOperator == Operator.None)
            {
                FirstNumber = Memory;
                isFirstNumberAfterOperator = true;
            }
            else
            {
                Display.Text = Memory.ToString();
            }
        }

        private void ResetAll()
        {
            Display.Text = "0";
            FirstNumber = 0;
            PreviousOperator = Operator.None;
            isFirstNumberAfterOperator = true;
        }
    }
}
