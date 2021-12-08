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
               
        string lower = "0";
        string upper = "";
        public string Lower { get { return lower ?? "0"; } set { lower = value; OnPropertyChanged("Lower"); } }
        public string Upper { get { return upper ?? ""; } set { upper = value; OnPropertyChanged("Upper"); } }
        public ICommand ButtonClick
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Click(parameter.ToString(), ref lower, ref upper);
                    Lower = lower;
                    Upper = upper;
                });
            }
        }

        public ICommand Action
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Action(parameter.ToString(), ref lower, ref upper);
                    Lower = lower;
                    Upper = upper;
                });
            }
        }

        public ICommand PointClick
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Point(ref lower, ref upper);
                    Lower = lower;
                    Upper = upper;
                });
            }
        }

        public ICommand Delete
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Delete(ref lower, ref upper);
                    Lower = lower;
                    Upper = upper;
                });
            }
        }

        public ICommand Equal
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.Equal(ref lower, ref upper);
                    Lower = lower;
                    Upper = upper;
                });
            }
        }

        public ICommand C_Button
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.C_Click(ref lower, ref upper);
                    Lower = lower;
                    Upper = upper;
                });
            }
        }
        public ICommand CE_Button
        {
            get
            {
                return new RelayCommand(parameter =>
                {
                    Model.Calculation.CE_Click(ref lower);
                    Lower = lower;
                });
            }
        }

    }
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
    }
}
