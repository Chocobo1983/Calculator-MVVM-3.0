﻿using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Calculator_MVVM.ViewModel
{
    internal class VM : VMbase
    {
        string _operand = "0";
        string _operation;
        bool _calculationDone = false, _error = false, _calculationRepeat=false;
        Visibility _visibility = Visibility.Hidden;
        Calculation _calculator = new Calculation();
        public string Operand { get { return _operand; } set { _operand = value; OnPropertyChanged(nameof(Operand)); } }
        public string Operation { get { return _operation; } set { _operation = value; OnPropertyChanged(nameof(Operation)); } }
        public Visibility Visibility { get { return _visibility; } set { _visibility = value; OnPropertyChanged(nameof(Visibility)); } }
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
            if (_error || _calculationDone) ClearAll(o);
            string operand = o.ToString();
            if (Operand == "0") Operand = operand;
            else Operand += operand;
        }
        private void PointInput(object o)
        {
            if (_error || _calculationDone) ClearAll(o);
            else if (Operand == null) Operand = "0";
            else if (Operand.Contains(Calculation._numberFormatInfo.NumberDecimalSeparator)) return;
            Operand += Calculation._numberFormatInfo.NumberDecimalSeparator;
        }
        private void OperationInput(object o)
        {
            if (_error) ClearAll(o);
            char Operator = o.ToString()[0];
            _calculationDone = false;            
            if (!_calculationRepeat)
            {
                _calculator.Operand1 = double.Parse(Operand, Calculation._numberFormatInfo);
                Operation = Operand + Operator;
                Operand = null;
                _calculationRepeat = true;
            }
            else if (_calculationRepeat && Operand == null) Operation = Operation.Remove(Operation.Length - 1) + Operator;
            else
            {
                Equal(o);
                _calculator.Operand1 = double.Parse(Operand, Calculation._numberFormatInfo);
                Operation = Operand + Operator;
                Operand = null;
                _calculationRepeat = true;
                _calculationDone = false;
            }
            _calculator.Operator = Operator;
        }
        private void Equal(object o)
        {
            if (Operation == null && !_calculationDone) return;
            if (!_calculationDone && Operand != null) _calculator.Operand2 = double.Parse(Operand, Calculation._numberFormatInfo);
            else if (_calculationDone && Operand != null) _calculator.Operand1 = double.Parse(Operand, Calculation._numberFormatInfo);
            else _calculator.Operand2 = null;
            double? result = _calculator.Equal();
            if (result == null) 
            { 
                _error = true; 
                Visibility = Visibility.Visible; 
            }
            else Operand = result?.ToString(Calculation._numberFormatInfo);
            Operation = null;
            _calculationRepeat = false;
            _calculationDone = true;
        }
        private void DeleteChar(object o)
        {
            if (_error || _calculationDone) ClearAll(o);
            if (Operand?.Length > 1) Operand = Operand.Remove(Operand.Length - 1);
            else Operand = "0";
        }
        private void Update() => _calculator.Operand1 = _calculator.Operand2 = _calculator.Operator = null;
        private void ClearAll(object o)
        {
            Operation = null;
            _error = _calculationDone = _calculationRepeat = false;
            Visibility = Visibility.Hidden;
            Update();
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
