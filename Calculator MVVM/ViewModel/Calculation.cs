using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_MVVM.ViewModel
{
    internal class Calculation
    {
        NumberFormatInfo numberFormatInfo = NumberFormatInfo.InvariantInfo;
        string operationStr = "", lastOperation = "", selfref = "", result = "";
        bool repeatOperation = false;
        bool zeroDivError = true;
        bool point = true;
        bool clear = true;
        double operand1, operand2;
        char operation;

        public void Click(string parameter, ref string lower_txt, ref string upper_txt)
        {
            if (lower_txt == "0." && upper_txt == "")
            {
                lower_txt += parameter;
                zeroDivError = true;
            }
            else
            {
                if (lower_txt == "0" || !zeroDivError)
                {
                    lower_txt = "";
                    zeroDivError = true;
                }
                else if (upper_txt != "" && lower_txt != "")
                {
                    char tmp = upper_txt[upper_txt.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        upper_txt = "";
                        lower_txt = "";
                        lastOperation = selfref = result = "";
                        clear = false;
                    }
                }
                lower_txt += parameter;
            }

        }
        public void Action(string parameter, ref string lower_txt, ref string upper_txt)
        {
            if (!zeroDivError)
            {
                zeroDivError = true;
                lower_txt = "0";
            }
            if (!point) point = true;
            if (!clear) clear = true;
            operationStr = parameter;
            if (upper_txt != "" && lower_txt == "")
            {
                char tmp = upper_txt[upper_txt.Length - 1];
                if (tmp == '+' || tmp == '-' || tmp == '*' || tmp == '/')
                {
                    upper_txt.Remove(upper_txt.Length - 1);
                    upper_txt = selfref + operationStr;
                }
                lower_txt = "";
            }
            else if (upper_txt != "" && lower_txt != "" && !repeatOperation)
            {

                operation = upper_txt[upper_txt.Length - 1];
                operand1 = double.Parse(upper_txt.Remove(upper_txt.Length - 1), numberFormatInfo);
                operand2 = double.Parse(lower_txt, numberFormatInfo);
                result = Calculate(ref operand1, ref operand2, ref operation, ref upper_txt);
                if(!zeroDivError)
                {
                    lower_txt = "Деление на ноль";
                    return;
                }
                else lower_txt = result;
                upper_txt = result + operationStr;
                selfref = lower_txt;
                lower_txt = "";
            }
            else
            {
                if (lower_txt[lower_txt.Length - 1] == '.') lower_txt = lower_txt.Remove(lower_txt.Length - 1);
                upper_txt = lower_txt + operationStr;
                selfref = lower_txt;
                lower_txt = "";
            }
            repeatOperation = false;
        }

        private string Calculate(ref double dec1, ref double dec2, ref char act, ref string upper_txt)
        {
            switch (act)
            {
                case '+': return (dec1 + dec2).ToString(numberFormatInfo);
                case '-': return (dec1 - dec2).ToString(numberFormatInfo);
                case '*': return (dec1 * dec2).ToString(numberFormatInfo);
                case '/':
                    {
                        if (dec2 == 0)
                        {
                            upper_txt = "";
                            dec1 = dec2 = 0;
                            act = ' ';
                            zeroDivError = false;
                            return "Деление на ноль";
                        }
                        else return (dec1 / dec2).ToString(numberFormatInfo);
                    }
                default: return "0";

            }
        }
        public void Point(ref string lower_txt, ref string upper_txt)
        {
            if (point)
            {
                point = false;
                if (upper_txt != "" && lower_txt != "")
                {
                    char tmp = upper_txt[upper_txt.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        upper_txt = "";
                        lastOperation = selfref = result = "";
                        clear = false;
                        lower_txt = "0";
                    }
                }
                else if (upper_txt != "" && lower_txt == "") lower_txt = "0";
                else if (!zeroDivError) lower_txt = "0";
                lower_txt += ".";
            }
        }
        public void Delete(ref string lower_txt, ref string upper_txt)
        {
            if (lower_txt.Length > 1 && lower_txt != "0")
            {
                if (lower_txt[lower_txt.Length - 1] == '.') point = true;
                lower_txt = lower_txt.Remove(lower_txt.Length - 1);
            }
            else if (lower_txt.Length == 1) lower_txt = "0";
        }

        public void Equal(ref string lower_txt, ref string upper_txt)
        {
            if (!point && upper_txt != "") point = true;
            if (lower_txt != "" && !repeatOperation && upper_txt != "")
            {
                operation = upper_txt[upper_txt.Length - 1];
                operand1 = double.Parse(upper_txt.Remove(upper_txt.Length - 1), numberFormatInfo);
                operand2 = double.Parse(lower_txt, numberFormatInfo);
                upper_txt += lower_txt;
                lastOperation = lower_txt;
                lower_txt = Calculate(ref operand1, ref operand2, ref operation, ref upper_txt);
                repeatOperation = true;
            }
            else if (lower_txt != "" && repeatOperation && zeroDivError && clear)
            {
                operand1 = double.Parse(lower_txt, numberFormatInfo);
                operand2 = double.Parse(lastOperation, numberFormatInfo);
                upper_txt = lower_txt + operationStr + lastOperation;
                lower_txt = Calculate(ref operand1, ref operand2, ref operation, ref upper_txt);
            }
            else if (lower_txt == "")
            {
                lastOperation = selfref;
                operand1 = double.Parse(selfref, numberFormatInfo);
                operand2 = double.Parse(lastOperation, numberFormatInfo);
                operation = operationStr[0];
                upper_txt = selfref + operationStr + lastOperation;
                lower_txt = Calculate(ref operand1, ref operand2, ref operation, ref upper_txt);
                repeatOperation = true;
            }
        }

        public void C_Click(ref string lower_txt, ref string upper_txt)
        {
            upper_txt = "";
            zeroDivError = false;
            lower_txt = "0";
            if (!point) point = true;
            operand1 = operand2 = 0;
            operation = ' ';
            clear = false;
        }

        public void CE_Click(ref string lower_txt)
        {
            lower_txt = "0";
        }
    }
}
