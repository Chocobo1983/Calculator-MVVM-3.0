using System.Globalization;
namespace Calculator_MVVM.ViewModel
{
    internal class Calculation
    {
        public static readonly NumberFormatInfo _numberFormatInfo = NumberFormatInfo.InvariantInfo;
        //Change NumberFormatInfo.InvariantInfo for NumberFormatInfo.CurrentInfo if you wish to use your local numbers formatting
        public double? Operand1 { get; set; } 
        public double? Operand2 { get; set; }
        public char? Operator { get; set; }
        public double? Calculate()
        {
            Operand2 = Operand2 ?? Operand1;
            switch (Operator)
            {
                case '+': return Operand1 + Operand2;
                case '-': return Operand1 - Operand2;
                case '*': return Operand1 * Operand2;
                case '/': return (Operand2 == 0) ? null : Operand1 / Operand2;
                default: return null;
            }           
        }
    }
}
