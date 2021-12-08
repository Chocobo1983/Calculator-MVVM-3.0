using System.Windows.Input;

namespace Calculator_MVVM.ViewModel
{
    internal class VM : VMbase
    {
        
        string _operand = "0";
        string _operation = "";
        Calculation calculator = new Calculation();
        public string Operand { get { return _operand ?? "0"; } set { _operand = value; OnPropertyChanged(nameof(Operand)); } }
        public string Operation { get { return _operation ?? ""; } set { _operation = value; OnPropertyChanged(nameof(Operation)); } }
        public RelayCommand command { get; private set; }
        #region PrivateMethods
        private void OperandInput(object parameter)
        {
            calculator.Operand(parameter.ToString(), ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
        private void OperationInput(object parameter)
        {
            calculator.Operation(parameter.ToString(), ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
        private void PointInput(object parameter)
        {
            calculator.Point(ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
        private void DeleteUsing(object parameter)
        {
            calculator.Delete(ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
        private void TotalUsing(object o)
        {
            calculator.Total(ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
        private void ClearAll(object o)
        {
            calculator.ClearAll(ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
        private void Clear(object o)
        {
            calculator.Clear(ref _operand);
            Operand = _operand;
        }
        #endregion
        #region Commands
        public ICommand OperandCommand
        {
            get
            {
                command = new RelayCommand(OperandInput);
                return command;
            }
        }

        public ICommand OperationCommand
        {
            get
            {
                command = new RelayCommand(OperationInput);
                return command;
            }
        }

        public ICommand PointCommand
        {
            get
            {
                command = new RelayCommand(PointInput);
                return command;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                command = new RelayCommand(DeleteUsing);
                return command;
            }
        }

        public ICommand TotalCommand
        {
            get
            {
                command = new RelayCommand(TotalUsing);
                return command;
            }
        }

        public ICommand ClearAllCommand
        {
            get
            {
                command = new RelayCommand(ClearAll);
                return command;
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                command = new RelayCommand(Clear);
                return command;
            }
        }
        #endregion


    }
    
}
