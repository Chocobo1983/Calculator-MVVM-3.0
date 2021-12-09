using System;
using System.Data;
using System.Globalization;
using System.Windows;

namespace Calculator_MVVM.ViewModel
{
    internal class Calculation
    {
        string _operand1="0", _operand2 = "0", _operation; //три поля для манипуляции данными
        bool _errorFlag = false, _repeatTotal = false, _repeatOperation = false, _pointFlag = false;
        public string Data { get; private set; } //данные для отправки во ViewModel (нижняя панель)
        public string InterMediateData { get; private set; } //промежуточные данные для отправки во ViewModel  (верхняя панель)


        public void Operand(string parameter)
        {
            if (_errorFlag)
            {
                ClearAll();
                _errorFlag = false;
            }
            if(Data == "0") Data = "";
            if(_repeatTotal)           
            {
                _operand1 = _operand2 = _operation = Data = InterMediateData = "";
                _repeatTotal = false;
            }
            Data += parameter;
            _operand1 = Data;
        }
        public void Operation(string parameter)
        {
            if(Data != "" && _operation != "" && InterMediateData!="") Total();
            if(_repeatTotal)
            {
                _operand1 = _operand2;
                _operand2 = "";
                _repeatTotal = false;
            }
            if (!_repeatOperation)
            {
                _operation = parameter;
                InterMediateData = _operand1 + parameter;
                _operand2 = _operand1;
                _operand1 = Data = "";
                _repeatOperation = true;
            }
            else
            {
                _operation = parameter;
                InterMediateData = _operand2 + parameter;
                _operand1 = Data = "";
            }
            _pointFlag = false;
        }

        public void Point()
        {
            if (_errorFlag)
            {
                ClearAll();
                _errorFlag = false;
            }
            if(_repeatTotal)
            {
                ClearAll();
                Data = "0.";
                _pointFlag = true;
                _operand1 = Data;
                _repeatTotal=false;
            }
            if (!_pointFlag) 
            {
                if (Data == "") Data = "0";
                Data += ".";
                _operand1 = Data;
            }
            _pointFlag = true;
            
        }
        public void Delete()
        {
            if(Data.Length > 1 && Data != "0")
            {
                if (Data[Data.Length - 1] == '.') _pointFlag = false;
                Data = _operand1 = Data.Remove(Data.Length - 1);
            }
            else if (Data.Length == 1) Data = "0";
        }

        public void Total()
        {
            if (_operation == "") return;
            if (_operation == "/" && (_operand1 == "0" || _operand1 == "0."))
            {
                ClearAll();
                Data = "Zero division error";
                _errorFlag = true;
                return;
            }
            else
            {
                if (_operand1 == "") _operand1 = _operand2;
                if (_operand1.IndexOf('.') == -1 && _operand2.IndexOf('.') == -1) _operand1 += ".";
                try
                {
                    _operand2 = new DataTable().Compute(_operand2 + _operation + _operand1, null).ToString().Replace(',', '.'); // =)
                }
                catch (Exception ex) { }
                InterMediateData = "";
            }
            Data = _operand2;
            _repeatTotal = true;
            _repeatOperation = false;
            _pointFlag = false;
        }

        public void ClearAll()
        {
            _operand1 = _operand2 = Data = "0"; _operation = "";
            _errorFlag = _repeatTotal = _repeatOperation = _pointFlag = false;
            InterMediateData = "";
        }

        public void Clear()
        {
            Data = _operand1 = "0";
            _pointFlag = false;
        }
    }
}
