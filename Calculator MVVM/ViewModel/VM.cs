using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows.Input;

namespace Calculator_MVVM.ViewModel
{
    internal class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        string _operand = "0";
        string _operation = "";
        public string Operand { get { return _operand ?? "0"; } set { _operand = value; OnPropertyChanged(nameof(Operand)); } }
        public string Operation { get { return _operation ?? ""; } set { _operation = value; OnPropertyChanged(nameof(Operation)); } }
        public ICommand OperandCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Click(parameter.ToString(), ref _operand, ref _operation);
                    Operand = _operand;
                    Operation = _operation;
                });
            }

        }

        public ICommand OperationCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Action(parameter.ToString(), ref _operand, ref _operation);
                    Operand = _operand;
                    Operation = _operation;
                });
            }
        }

        public ICommand PointCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Point(ref _operand, ref _operation);
                    Operand = _operand;
                    Operation = _operation;
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Delete(ref _operand, ref _operation);
                    Operand = _operand;
                    Operation = _operation;
                });
            }
        }

        public ICommand TotalCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Equal(ref _operand, ref _operation);
                    Operand = _operand;
                    Operation = _operation;
                });
            }
        }

        public ICommand ClearAllCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.C_Click(ref _operand, ref _operation);
                    Operand = _operand;
                    Operation = _operation;
                });
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.CE_Click(ref _operand);
                    Operand = _operand;
                });
            }
        }

    }
    
}
