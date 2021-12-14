using System.Globalization;
using System.Windows.Input;

namespace Calculator_MVVM.ViewModel
{
    internal class VM : VMbase
    {
        string _operand = "0";
        string _tmp, _act = "";
        string _operation = "";
        bool _operFlag = false, _errorFlag = false, _repeatFlag=false;
        Calculation _calculator = new Calculation();
        public string Operand { get { return _operand ?? "0"; } set { _operand = value; OnPropertyChanged(nameof(Operand)); } }
        public string Operation { get { return _operation ?? ""; } set { _operation = value; OnPropertyChanged(nameof(Operation)); } }
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
            _deleteCommand = new RelayCommand(DeleteUsing);
            _totalCommand = new RelayCommand(TotalUsing);
            _clearAllCommand = new RelayCommand(ClearAll);
            _clearCommand = new RelayCommand(Clear);
        }
        #region PrivateMethods
        private void OperandInput(object o)
        {
            if (_errorFlag) ClearAll(o);
            if (Operand == "0" || _repeatFlag) Operand = o.ToString();
            else Operand += o.ToString();
            _repeatFlag = false;
        }
        private void OperationInput(object o)
        {
            if (_errorFlag) ClearAll(o);
            else if (!_operFlag)
            {
                _act = o.ToString();
                Operation = Operand + _act;
                _tmp = Operand;
                Operand = "";
                _operFlag = true;
            }
            else if(_operFlag && Operand=="")
            {
                _act = o.ToString();
                Operation = Operation.Remove(Operation.Length-1) + _act;
            }
            else if(_operFlag && Operand != "")
            {
                _calculator.Total(_tmp, Operand, _act);
                if (_calculator.Data == "Error")
                {
                    Operand = _calculator.Data;
                    _errorFlag = true;
                    Operation = "";
                    return;
                }
                _act = o.ToString();
                Operand = "";
                Operation = _calculator.Data + _act;
                _tmp = _calculator.Data;
            }
            
        }
        private void PointInput(object o)
        {
            if(_errorFlag||_repeatFlag) ClearAll(o);
            if (Operand.Contains(Calculation._numberFormatInfo.NumberDecimalSeparator)) return;
            if (Operand == "") Operand = "0";
            Operand += Calculation._numberFormatInfo.NumberDecimalSeparator;
        }
        private void DeleteUsing(object o)
        {
            if (_repeatFlag) return;
            if (_errorFlag) ClearAll(o);
            if (Operand.Length > 1) Operand = Operand.Remove(Operand.Length - 1);
            else Operand = "0";
        }
        private void TotalUsing(object o)
        {
            if (_errorFlag) ClearAll(o);
            if (Operation!="" && Operand != "" && _act!="")
            {
               _calculator.Total(_tmp, Operand, _act);
               _tmp = Operand;
            }
            if(Operand!="" && Operation=="" && _act!="") _calculator.Total(Operand, _tmp, _act);
            if(Operand=="" && Operation!="") _calculator.Total(_tmp, _tmp, _act);
            _operFlag = false;
            _repeatFlag = true;
            Update();
        }
        private void ClearAll(object o)
        {
            Operand = "0";
            Operation = "";
            _tmp = _act = "";
            _operFlag = false;
            _errorFlag = false;
            _repeatFlag = false;
            _calculator.Data = "0";
        }
        private void Update()
        {
            Operand = _calculator.Data;
            if (Operand == "Error") _errorFlag = true;
            Operation = "";
            _calculator.Data = "0";
        }
        private void Clear(object o)
        {
            Operand = "0";
        }
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
