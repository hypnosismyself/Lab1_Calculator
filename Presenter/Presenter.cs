namespace Lab1_Calculator.Model
{ 
    public class Presenter
    {
        // Презентер вычислений

        // Интерфейс презентера
        public CalculatorView View { get => _view; set => _view = value; }
        private CalculatorView _view;

        public Operation Model { get => _model; set => _model = value; }
        private Operation _model;

        // Конструктор презентера
        public Presenter(Operation model, CalculatorView view)
        {
            this.View = view;
            this.Model = model;
        }

        // Презентер операции
        public void UpdateResult()
        {
            Operation operation = new Operation
            {
                LeftNumber = View.LeftNumber,
                RightNumber = View.RightNumber,
                Name = View.Operation
            };

            // выполняем операцию
            operation.ExecuteOperation();

            // перерисовываем представление
            View.UpdateView(operation.Result);
        }
    }
}