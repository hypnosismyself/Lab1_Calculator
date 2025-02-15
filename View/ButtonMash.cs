using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab1_Calculator
{
    internal class ButtonMash
    {
        // Класс для работы с нажатием с клавитуры

        // Словарь цифр (ключ - текст)
        public readonly Dictionary<Keys, string> DIGITS = new Dictionary<Keys, string>()
        {
            { Keys.NumPad0, "0" },
            { Keys.D0, "0"},
            { Keys.NumPad1, "1"},
            { Keys.D1, "1"},
            { Keys.NumPad2, "2"},
            { Keys.D2, "2"},
            { Keys.NumPad3, "3"},
            { Keys.D3, "3"},
            { Keys.NumPad4, "4"},
            { Keys.D4, "4"},
            { Keys.NumPad5, "5"},
            { Keys.D5, "5"},
            { Keys.NumPad6, "6"},
            { Keys.D6, "6"},
            { Keys.NumPad7, "7"},
            { Keys.D7, "7"},
            { Keys.NumPad8, "8"},
            { Keys.D8, "8"},
            { Keys.NumPad9, "9"},
            { Keys.D9, "9"}
        };

        // Словарь операций (ключ - текст)
        public readonly Dictionary<Keys, string> OPERATIONS = new Dictionary<Keys, string>()
        {
            { Keys.Add, "+"},
            { Keys.Oemplus, "+"},
            { Keys.Subtract, "-"},
            { Keys.OemMinus, "-"},
            { Keys.Multiply, "*"},
            { Keys.Divide, "/"}
        };

        // Словарь служб (ключ - текст)
        public readonly Dictionary<Keys, string> SERVICE = new Dictionary<Keys, string>()
        {
            { Keys.Space, "="},
            { Keys.Enter, "="},
            { Keys.Back, "backspace"},
            { Keys.Delete, "delete"},
            { Keys.Oemcomma, ","},
            { Keys.OemPeriod, ","},
        };

        // Метод определения типа нажатой кнопки
        public string[] GetType(Keys key)
        {
            string[] result = { string.Empty, string.Empty };

            if (DIGITS.TryGetValue(key, out string digit))
            {
                result[0] = digit;
                result[1] = "digit";
            }

            if (OPERATIONS.TryGetValue(key, out string operation))
            {
                result[0] = operation;
                result[1] = "operation";
            }

            if (SERVICE.TryGetValue(key, out string service))
            {
                result[0] = service;
                result[1] = "service";
            }

            return result;
        }
    }
}
