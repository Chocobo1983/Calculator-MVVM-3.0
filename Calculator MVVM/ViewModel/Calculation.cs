using System;
using System.Data;
using System.Globalization;
using System.Windows;

namespace Calculator_MVVM.ViewModel
{
    internal class Calculation
    {

        public static readonly NumberFormatInfo _numberFormatInfo = NumberFormatInfo.InvariantInfo;
        //Change NumberFormatInfo.InvariantInfo for NumberFormatInfo.CurrentInfo if you wish to use your local numbers formatting
        public double? Operand1 { get; set; } 
        public double? Operand2 { get; set; }
        public char? Operator { get; set; }
        

        private double? Calculate()
        {
            switch (Operator)
            {
                case '+': return Operand1 + Operand2;
                case '-': return Operand1 - Operand2;
                case '*': return Operand1 * Operand2;
                case '/': return (Operand2 == 0) ? null : Operand1 / Operand2;
                default: return null;
            }           
        }
        public double? Equal()
        {
            Operand2 = Operand2 ?? Operand1;
            return Calculate();

        }

    }
}
