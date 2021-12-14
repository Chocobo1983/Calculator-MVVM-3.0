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

        public string Data { get; set; }
        public void Total(string operand1, string operand2, string operation)
        {
            double op1 = double.Parse(operand1, _numberFormatInfo);
            double op2 = double.Parse(operand2, _numberFormatInfo);
            switch (operation)
            {
                case "+": Data = (op1 + op2).ToString(_numberFormatInfo); break;
                case "-": Data = (op1 - op2).ToString(_numberFormatInfo); break;
                case "*": Data = (op1 * op2).ToString(_numberFormatInfo); break;
                case "/":
                    {
                        if (op2 == 0) Data = "Error";
                        else Data = (op1 / op2).ToString(_numberFormatInfo); break;
                    }
            }
            
        }


    }
}
