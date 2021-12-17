using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Calculator_MVVM.ViewModel
{
    internal class VM : VMbase
    {
        string _operand = "0", _operation;
        bool _IsCalculationDone = false, _hasError = false, _IsCalculationRepeat=false;
        Calculation _calculator = new Calculation();
        public string Operand { get { return _operand; } set { _operand = value; OnPropertyChanged(nameof(Operand)); } }
        public string Operation { get { return _operation; } set { _operation = value; OnPropertyChanged(nameof(Operation)); } }
        public bool HasError { get { return _hasError; } set { if (value == _hasError) return; _hasError = value; OnPropertyChanged(nameof(HasError)); } }
        RelayCommand _operandCommand;
        RelayCommand _operationCommand;
        RelayCommand _pointCommand;
        RelayCommand _deleteCommand;
        RelayCommand _totalCommand;
        RelayCommand _clearAllCommand;
        RelayCommand _clearCommand;
        public VM()
        {
            _operandCommand = new RelayCommand(OperandInput);
            _operationCommand = new RelayCommand(OperationInput);
            _pointCommand = new RelayCommand(PointInput);
            _deleteCommand = new RelayCommand(DeleteChar);
            _totalCommand = new RelayCommand(Equal);
            _clearAllCommand = new RelayCommand(ClearAll);
            _clearCommand = new RelayCommand(Clear);
        }
        #region PrivateMethods
        private void OperandInput(object o)
        {
            if (_hasError || _IsCalculationDone) ClearAll(o);
            if (Operand == "0") Operand = o.ToString();
            else Operand += o.ToString();
        }
        private void PointInput(object o)
        {
            if (_hasError || _IsCalculationDone) ClearAll(o);
            else if (Operand == null) Operand = "0";
            else if (Operand.Contains(Calculation._numberFormatInfo.NumberDecimalSeparator)) return;
            Operand += Calculation._numberFormatInfo.NumberDecimalSeparator;
        }
        private void OperationInput(object o)
        {
            if (_hasError) ClearAll(o);
            char interOperator = o.ToString()[0];
            _IsCalculationDone = false;
            if (!_IsCalculationRepeat || Operand != null)
            {
                if (_IsCalculationRepeat)
                {
                    Equal(o);
                    Operation = _calculator.Operand1.ToString() + interOperator;
                    _IsCalculationDone = false;
                }
                _calculator.Operand1 = double.Parse(Operand, Calculation._numberFormatInfo);
                Operand = null;
                _IsCalculationRepeat = true;
            }            
            Operation = _calculator.Operand1.ToString() + interOperator;
            _calculator.Operator = interOperator;
        }
        private void Equal(object o)               
        {
            if (Operation == null && !_IsCalculationDone) return;
            if(Operand != null)
            {
                double operandParsed = double.Parse(Operand, Calculation._numberFormatInfo);
                if (!_IsCalculationDone) _calculator.Operand2 = operandParsed;
                else _calculator.Operand1 = operandParsed;
            }                
            else _calculator.Operand2 = null;
            double? result = _calculator.Calculate();
            if (result == null) HasError = true; 
            else Operand = result?.ToString(Calculation._numberFormatInfo);
            Operation = null;
            _IsCalculationRepeat = false;
            _IsCalculationDone = true;
        }
        private void DeleteChar(object o)
        {
            if (_hasError || _IsCalculationDone) ClearAll(o);
            if (Operand?.Length > 1) Operand = Operand.Remove(Operand.Length - 1);
            else Operand = "0";
        }
        private void ClearAll(object o)
        {
            Operation = null;
            HasError = _IsCalculationDone = _IsCalculationRepeat = false;
            _calculator.Reset();
            Clear(o);
        }
        private void Clear(object o) => Operand = "0";
        #endregion
        #region Commands
        public ICommand OperandCommand { get { return _operandCommand; } }
        public ICommand OperationCommand { get { return _operationCommand; } }
        public ICommand PointCommand { get { return _pointCommand; } }
        public ICommand DeleteCommand { get { return _deleteCommand; } }
        public ICommand TotalCommand { get { return _totalCommand; } }
        public ICommand ClearAllCommand { get { return _clearAllCommand; } }
        public ICommand ClearCommand { get { return _clearCommand; } }
        #endregion
    }    
}
