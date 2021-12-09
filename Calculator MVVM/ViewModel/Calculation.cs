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

        public void Operand(string parameter, ref string operand, ref string operation)
        {
            if (operand == "0." && operation == "")
            {
                operand += parameter;
                zeroDivError = true;
            }
            else
            {
                if (operand == "0" || !zeroDivError)
                {
                    operand = "";
                    zeroDivError = true;
                }
                else if (operation != "" && operand != "")
                {
                    char tmp = operation[operation.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        operation = "";
                        operand = "";
                        lastOperation = selfref = result = "";
                        clear = false;
                    }
                }
                operand += parameter;
            }

        }
        public void Operation(string parameter, ref string operand, ref string operationCurrent)
        {
            if (!zeroDivError)
            {
                zeroDivError = true;
                operand = "0";
            }
            if (!point) point = true;
            if (!clear) clear = true;
            operationStr = parameter;
            if (operationCurrent != "" && operand == "")
            {
                char tmp = operationCurrent[operationCurrent.Length - 1];
                if (tmp == '+' || tmp == '-' || tmp == '*' || tmp == '/')
                {
                    operationCurrent.Remove(operationCurrent.Length - 1);
                    operationCurrent = selfref + operationStr;
                }
                operand = "";
            }
            else if (operationCurrent != "" && operand != "" && !repeatOperation)
            {

                operation = operationCurrent[operationCurrent.Length - 1];
                operand1 = double.Parse(operationCurrent.Remove(operationCurrent.Length - 1), numberFormatInfo);
                operand2 = double.Parse(operand, numberFormatInfo);
                result = Calculate(ref operand1, ref operand2, ref operation, ref operationCurrent);
                if(!zeroDivError)
                {
                    operand = "Деление на ноль";
                    return;
                }
                else operand = result;
                operationCurrent = result + operationStr;
                selfref = operand;
                operand = "";
            }
            else
            {
                if (operand[operand.Length - 1] == '.') operand = operand.Remove(operand.Length - 1);
                operationCurrent = operand + operationStr;
                selfref = operand;
                operand = "";
            }
            repeatOperation = false;
        }

        private string Calculate(ref double operand1, ref double operand2, ref char operation, ref string operationCurrent)
        {
            switch (operation)
            {
                case '+': return (operand1 + operand2).ToString(numberFormatInfo);
                case '-': return (operand1 - operand2).ToString(numberFormatInfo);
                case '*': return (operand1 * operand2).ToString(numberFormatInfo);
                case '/':
                    {
                        if (operand2 == 0)
                        {
                            operationCurrent = "";
                            operand1 = operand2 = 0;
                            operation = ' ';
                            zeroDivError = false;
                            return "Деление на ноль";
                        }
                        else return (operand1 / operand2).ToString(numberFormatInfo);
                    }
                default: return "0";

            }
        }
        public void Point(ref string operand, ref string operationCurrent)
        {
            if (point)
            {
                point = false;
                if (operationCurrent != "" && operand != "")
                {
                    char tmp = operationCurrent[operationCurrent.Length - 1];
                    if (tmp != '+' && tmp != '-' && tmp != '*' && tmp != '/')
                    {
                        operationCurrent = "";
                        lastOperation = selfref = result = "";
                        clear = false;
                        operand = "0";
                    }
                }
                else if (operationCurrent != "" && operand == "") operand = "0";
                else if (!zeroDivError) operand = "0";
                operand += ".";
            }
        }
        public void Delete(ref string operand, ref string operation)
        {
            if (operand.Length > 1 && operand != "0")
            {
                if (operand[operand.Length - 1] == '.') point = true;
                operand = operand.Remove(operand.Length - 1);
            }
            else if (operand.Length == 1) operand = "0";
        }

        public void Total(ref string operand, ref string operationCurrent)
        {
            if (!point && operationCurrent != "") point = true;
            if (operand != "" && !repeatOperation && operationCurrent != "")
            {
                operation = operationCurrent[operationCurrent.Length - 1];
                operand1 = double.Parse(operationCurrent.Remove(operationCurrent.Length - 1), numberFormatInfo);
                operand2 = double.Parse(operand, numberFormatInfo);
                operationCurrent += operand;
                lastOperation = operand;
                operand = Calculate(ref operand1, ref operand2, ref operation, ref operationCurrent);
                repeatOperation = true;
            }
            else if (operand != "" && repeatOperation && zeroDivError && clear)
            {
                operand1 = double.Parse(operand, numberFormatInfo);
                operand2 = double.Parse(lastOperation, numberFormatInfo);
                operationCurrent = operand + operationStr + lastOperation;
                operand = Calculate(ref operand1, ref operand2, ref operation, ref operationCurrent);
            }
            else if (operand == "")
            {
                lastOperation = selfref;
                operand1 = double.Parse(selfref, numberFormatInfo);
                operand2 = double.Parse(lastOperation, numberFormatInfo);
                operation = operationStr[0];
                operationCurrent = selfref + operationStr + lastOperation;
                operand = Calculate(ref operand1, ref operand2, ref operation, ref operationCurrent);
                repeatOperation = true;
            }
        }

        public void ClearAll(ref string operand, ref string operationCurrent)
        {
            operationCurrent = "";
            zeroDivError = false;
            operand = "0";
            if (!point) point = true;
            operand1 = operand2 = 0;
            operation = ' ';
            clear = false;
        }

        public void Clear(ref string operand)
        {
            operand = "0";
        }
    }
}
