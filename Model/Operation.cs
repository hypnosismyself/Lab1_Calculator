namespace Lab1_Calculator.Model
{
    public class Operation
    {
        // Класс вычислений операции

        // Поля
        public string Name { get; set; }
        public double LeftNumber { get; set; }
        public double RightNumber { get; set; }
        public double Result { get; set; }

        // Метод выполнения операции
        public void ExecuteOperation()
        {
            switch (Name)
            {
                case "+":
                    Result = LeftNumber + RightNumber;
                    break;
                case "-":
                    Result = LeftNumber - RightNumber;
                    break;
                case "*":
                    Result = LeftNumber * RightNumber;
                    break;
                case "/":
                    Result = RightNumber != 0 ? LeftNumber / RightNumber : 0;
                    break;

                default:
                    Result = 0;
                    break;
            }
        }
    }
}
