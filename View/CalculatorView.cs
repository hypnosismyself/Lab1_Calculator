using Lab1_Calculator.Model;
using Lab1_Calculator.View;
using System;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace Lab1_Calculator
{

    public partial class CalculatorView : Form
    {
        // Свойства представления
        public double LeftNumber { get; set; }
        public double RightNumber { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }

        // Инициализация чекера
        private MainInputChecker checker = new MainInputChecker();
        private MainInputChecker Checker { get => checker; set => checker = value; }

        // Инициализация палитры
        private readonly ViewColors ViewColors = new ViewColors();

        // Инициализация презентера
        private Presenter Presenter;

        // Инициализация обработчика нажатий
        private ButtonMash btn_masher = new ButtonMash();
        private ButtonMash BtnMasher { get => btn_masher; set => btn_masher = value; }

        // Инициализация контролов
        public CalculatorView()
        {
            InitializeComponent();
        }

        // При клике на цифры (0-9)
        private void ButtonNumberClick(object sender, EventArgs e)
        {
            HighlightOperationButtons(false);
            Button button = (Button)sender;

            string input_number = button.Text;

            if (Checker.CanInputNumbers(MainInputTextBox, OperationHistoryLabel, Operation))
                MainInputTextBox.Text += input_number;
        }

        // При клике "AC"
        private void ClearAllClick(object sender, EventArgs e)
        {
            HighlightOperationButtons(false);

            MainInputTextBox.Text = "";
            OperationHistoryLabel.Text = "";
            SetViewMode(true);
            Operation = null;
        }

        // При клике "<-"
        private void BackspaceClick(object sender, EventArgs e)
        {
            string current_text = MainInputTextBox.Text;

            if (!Checker.IsInputEmpty(MainInputTextBox))
            {
                MainInputTextBox.Text = current_text.Remove(current_text.Length - 1);
            }
            else if (!Checker.IsOperationHistoryEmpty(OperationHistoryLabel))
            {
                OperationHistoryLabel.Text = LeftNumber.ToString();
                Operation = null;
            }
        }

        // При клике на операцию (+-*/)
        private void SetOperationClick(object sender, EventArgs e)
        {
            HighlightOperationButtons(false);

            Button button = (Button)sender;

            string CurrentOperation = button.Text;

            if (!Checker.CanSetOperation(MainInputTextBox, Operation) 
                && Checker.IsInputEmpty(MainInputTextBox) 
                && CurrentOperation == "-")
            {
                // если не можем пооставить операцию и инпут пустой
                // вводим '-'
                MainInputTextBox.Text += "-";
            }
            
            else if (Checker.CanExecuteOperation(MainInputTextBox, OperationHistoryLabel))
            {
                // если можем выполнить операцию, то стучимся в БЛ
                try
                {
                    RightNumber = Convert.ToDouble(MainInputTextBox.Text);
                    ExecuteOperation();

                    Operation = CurrentOperation;
                    OperationHistoryLabel.Text = $"{this.LeftNumber} {this.Operation}";
                }
                catch { }
            }
            else if (Checker.CanSetOperation(MainInputTextBox, Operation))
            {
                // если можем установить операцию
                try
                {
                    if (!Checker.IsInputEmpty(MainInputTextBox))
                    {
                        LeftNumber = Convert.ToDouble(MainInputTextBox.Text);
                        OperationHistoryLabel.Text = $"{MainInputTextBox.Text} {CurrentOperation}";
                    }
                    else
                    {
                        OperationHistoryLabel.Text = $"{LeftNumber} {CurrentOperation}";
                    }

                    Operation = CurrentOperation;
                    MainInputTextBox.Clear();
                }
                catch { }
            }
        }

        // При клике +/-
        private void PlusMinusInputButtonClick(object sender, EventArgs e)
        {
            string temp = MainInputTextBox.Text;
            bool HasMinus = Checker.IsFirstMinus(MainInputTextBox);

            if (checker.CanInputNumbers(MainInputTextBox, OperationHistoryLabel, Operation))
            {
                if (HasMinus)
                {
                    MainInputTextBox.Text = temp.Remove(0, 1);
                }
                else
                {
                    MainInputTextBox.Text = "-" + temp;
                }
            }
        }

        // При клике ","
        private void CommaInputButtonClick(object sender, EventArgs e)
        {
            if (!Checker.IsInputEmpty(MainInputTextBox) && !Checker.HasComma(MainInputTextBox))
            {
                MainInputTextBox.Text += CommaInputButton.Text;
            }
        }

        // При клике "="
        private void EqualityInputButtonClick(object sender, EventArgs e)
        {
            if (Checker.CanExecuteOperation(MainInputTextBox, OperationHistoryLabel))
            {
                // если можем провести операцию
                ExecuteOperation();
            }
            else
            {
                // иначе подсвечиваем кнопки операций
                HighlightOperationButtons(true);
            }

        }

        // Метод вызова расчета с БЛ
        private void ExecuteOperation()
        {
            try
            {
                RightNumber = Convert.ToDouble(MainInputTextBox.Text);
                if (RightNumber == 0 && Operation == "/")
                {
                    MainInputTextBox.Clear();
                    OperationHistoryLabel.Text = "Error";
                    SetViewMode(false);
                }
                else
                {
                    Presenter = new Presenter(new Operation(), this);
                    Presenter.UpdateResult();
                }
            }
            catch { }
        }

        // Метод обновления представление
        public void UpdateView(double Result)
        {
            OperationHistoryLabel.Text = Result.ToString();
            MainInputTextBox.Clear();
            this.Result = Result;
            this.LeftNumber = Result;
            Operation = null;
        }

        // Служебный метод управления состоянием кнопок формы
        private void SetViewMode(bool command)
        {
            ZeroInputButton.Enabled = command;
            OneInputButton.Enabled = command;
            TwoInputButton.Enabled = command;
            ThreeInputButton.Enabled = command;
            FourInputButton.Enabled = command;
            FiveInputButton.Enabled = command;
            SixInputButton.Enabled = command;
            SevenInputButton.Enabled = command;
            EightInputButton.Enabled = command;
            NineInputButton.Enabled = command;
            DivisionInputButton.Enabled = command;
            AdditionInputButton.Enabled = command;
            SubtractionInputButton.Enabled = command;
            MultiplicationInputButton.Enabled = command;
            PlusMinusInputButton.Enabled = command;
            EqualityInputButton.Enabled = command;
            CommaInputButton.Enabled = command;
            BackspaceInputButton.Enabled = command;
        }

        // Служебный метод подсветки кнопок операций
        private void HighlightOperationButtons(bool option)
        {
            if (option)
            {
                AdditionInputButton.BackColor = ViewColors.WarningColor;
                SubtractionInputButton.BackColor = ViewColors.WarningColor;
                MultiplicationInputButton.BackColor = ViewColors.WarningColor;
                DivisionInputButton.BackColor = ViewColors.WarningColor;
            }
            else
            {
                AdditionInputButton.BackColor = ViewColors.BaseControlColor;
                SubtractionInputButton.BackColor = ViewColors.BaseControlColor;
                MultiplicationInputButton.BackColor = ViewColors.BaseControlColor;
                DivisionInputButton.BackColor = ViewColors.BaseControlColor;
            }
        }

        // При вводе с клавиатуры
        private void CalculatorViewKeyDown(object sender, KeyEventArgs e)
        {
            string[] input_type = BtnMasher.GetType(e.KeyCode);

            switch (input_type[1])
            {
                case "digit":
                    EmulateDigitButtonClick(input_type[0]);
                    break;
                case "operation":
                    EmulateOperationButtonClick(input_type[0]);
                    break;
                case "service":
                    EmulateServiceButtonClick(input_type[0]);
                    break;

                default:
                    break;

            }
        }

        // Метод эмуляция нажатия на цифры
        private void EmulateDigitButtonClick(string Digit)
        {
            switch (Digit)
            {
                case "0":
                    ZeroInputButton.PerformClick();
                    break;
                case "1":
                    OneInputButton.PerformClick();
                    break;
                case "2":
                    TwoInputButton.PerformClick();
                    break;
                case "3":
                    ThreeInputButton.PerformClick();
                    break;
                case "4":
                    FourInputButton.PerformClick();
                    break;
                case "5":
                    FiveInputButton.PerformClick();
                    break;
                case "6":
                    SixInputButton.PerformClick();
                    break;
                case "7":
                    SevenInputButton.PerformClick();
                    break;
                case "8":
                    EightInputButton.PerformClick();
                    break;
                case "9":
                    NineInputButton.PerformClick();
                    break;
            }
        }

        // Метод эмуляция нажатия на операции
        private void EmulateOperationButtonClick(string operation)
        {
            switch (operation)
            {
                case "+":
                    AdditionInputButton.PerformClick();
                    break;
                case "-":
                    SubtractionInputButton.PerformClick();
                    break;
                case "*":
                    MultiplicationInputButton.PerformClick();
                    break;
                case "/":
                    DivisionInputButton.PerformClick();
                    break;
            }
        }

        // Метод эмуляция нажатия на службы
        private void EmulateServiceButtonClick(string service)
        {
            switch (service)
            {
                case "=":
                    EqualityInputButton.PerformClick();
                    break;
                case "backspace":
                    BackspaceInputButton.PerformClick();
                    break;
                case "delete":
                    ClearAllInputButton.PerformClick();
                    break;
                case ",":
                    CommaInputButton.PerformClick();
                    break;
            }
        }
    }
}
