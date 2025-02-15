using System.Windows.Forms;

namespace Lab1_Calculator.View
{
    public class MainInputChecker
    {
        // проверка возможности установить операцию
        public bool CanSetOperation(TextBox t, string o)
        {
            if (o != null && t.Text.EndsWith(","))
            {
                return false;
            }
            else if (o == null)
            {
                return true; 
            }

            if (IsInputEmpty(t)) return false;
            if (t.Text.EndsWith(",")) return false;

            return true;
        }

        // проверка возможности провести операцию
        public bool CanExecuteOperation(TextBox t, Label l)
        {
            if (IsInputEmpty(t) || IsOperationHistoryEmpty(l)) return false;
            if (t.Text.EndsWith(",") || l.Text.EndsWith(",")) return false;
            
            return true;
        }

        // проверка поля ввода на пустоту
        public bool IsInputEmpty(TextBox t)
        {
            return t.Text == "";
        }

        // проверка возможности ввода цифр
        public bool CanInputNumbers(TextBox t, Label l, string o)
        {
            bool first_is_zero = IsFirstZero(t);

            if (t.Text.Length >= 13) return false;

            if (IsOperationHistoryEmpty(l) || o != null)
            {
                if (!first_is_zero || (first_is_zero && HasComma(t)))
                {
                    return true;
                }
            }

            return false;
        }

        // проверка текущего результата на пустоту
        public bool IsOperationHistoryEmpty(Label l)
        {
            return l.Text == "";
        }

        // проверка на наличие ведущего нуля
        public bool IsFirstZero(TextBox t)
        {
            if (t.Text.Length == 1)
            {
                return t.Text[0] == '0';
            }
            else if (t.Text.Length == 2)
            {
                if (t.Text[0] == '-' && t.Text[1] == '0')
                    return true;
            }
            return false;
        }
        
        // проверка налия запятой
        public bool HasComma(TextBox t)
        {
            return t.Text.Contains(",");
        }

        public bool IsFirstMinus(TextBox t)
        {
            return t.Text.Contains("-");
        }
    }
}

