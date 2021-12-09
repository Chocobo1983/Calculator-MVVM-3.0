using System.Windows.Input;

namespace Calculator_MVVM.ViewModel
{
    internal class VM : VMbase
    {
        
        string _operand = "0";
        string _operation = "";
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
            _calculator.Operand(o.ToString());
            Update();
        }
        private void OperationInput(object o)
        {
            _calculator.Operation(o.ToString());
            Update();
        }
        private void PointInput(object o)
        {
            _calculator.Point();
            Update();
        }
        private void DeleteUsing(object o)
        {
            _calculator.Delete();
            Update();
        }
        private void TotalUsing(object o)
        {
            _calculator.Total();
            Update();
        }
        private void ClearAll(object o)
        {
            _calculator.ClearAll();
            Update();
        }
        private void Update()
        {
            Operand = _calculator.Data;
            Operation = _calculator.InterMediateData;
        }
        private void Clear(object o)
        {
            _calculator.Clear();
            Update();
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
