﻿using System;
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
        Calculation calculator = new Calculation();
        public string Operand { get { return _operand ?? "0"; } set { _operand = value; OnPropertyChanged(nameof(Operand)); } }
        public string Operation { get { return _operation ?? ""; } set { _operation = value; OnPropertyChanged(nameof(Operation)); } }
        public RelayCommand command { get; private set; }  
        public void OperandInput(object parameter)
        {
            calculator.Operand(parameter.ToString(), ref _operand, ref _operation);
            Operand = _operand;
            Operation = _operation;
        }
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
                return new RelayCommand(parameter =>
                {
                    calculator.Operation(parameter.ToString(), ref _operand, ref _operation);
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
                    calculator.Point(ref _operand, ref _operation);
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
                    calculator.Delete(ref _operand, ref _operation);
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
                    calculator.Total(ref _operand, ref _operation);
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
                    calculator.ClearAll(ref _operand, ref _operation);
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
                    calculator.Clear(ref _operand);
                    Operand = _operand;
                });
            }
        }

    }
    
}
