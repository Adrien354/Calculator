using CalculatorWPF.APP.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CalculatorWPF.APP.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> _availableOperations = new List<string> { "+", "-", "/", "*" };
        private string _screenVal;
        public string ScreenVal
        {
            get { return _screenVal; }
            set
            {
                _screenVal = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ScreenVal = "0";
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult);
            UsingBackspaceCommand = new RelayCommand(UsingBackspace);
            GetSquareRootCommand = new RelayCommand(GetSquareRoot);
        }
        public ICommand AddNumberCommand { get; set; }
        public ICommand AddOperationCommand { get; set; }
        public ICommand ClearScreenCommand { get; set; }
        public ICommand GetResultCommand { get; set; }
        public ICommand UsingBackspaceCommand { get; set; }
        public ICommand GetSquareRootCommand { get; set; }

        public string number3;

        private void AddNumber(object obj)
        {
            var number = obj as string;

            if(ScreenVal=="0" && number != ",")
            {
                ScreenVal = String.Empty;
            }
            else if(number == "," && _availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
            {
                number = "0,";
            }

            ScreenVal += number;

            isLastSignAnOperation = false;
        }
        private void AddOperation(object obj)
        {
            if (!isLastSignAnOperation)
            {
                var operation = obj as string;

                ScreenVal += operation;

                isLastSignAnOperation = true;
            }
        }
        private void ClearScreen(object obj)
        {
            ScreenVal = "0";
            isLastSignAnOperation = false;
        }
        private void GetResult(object obj)
        {
            if (!isLastSignAnOperation)
            {
                var result = Math.Round(Convert.ToDouble(_dataTable.Compute(ScreenVal.Replace(",", "."), "")), 2);
                ScreenVal = result.ToString();
            }
        }
        private void UsingBackspace(object obj)
        {
            if (ScreenVal.Length >= 2)
            {
                for (int i = 0; i < ScreenVal.Length - 1; i++)
                {
                    char number2 = ScreenVal[i];
                    number3 += number2.ToString();
                }
                ScreenVal = number3;
                number3 = "";

            }
            else
            {
                ScreenVal = "0";
            }
        }
        private void GetSquareRoot(object obj)
        {
            if (isLastSignAnOperation)
            {
                MessageBox.Show("The square root of the operation cannot be computed");
                for (int i = 0; i < ScreenVal.Length - 1; i++)
                {
                    char number2 = ScreenVal[i];
                    char number1;
                    number3 += number2.ToString();
                }
                ScreenVal = number3;
                number3 = "";
            }
            else
            {
                var results = Math.Sqrt(Convert.ToDouble(_dataTable.Compute(ScreenVal.Replace(",", "."), "")));
                ScreenVal = results.ToString();
            }
        }

        private DataTable _dataTable = new DataTable();

        private bool isLastSignAnOperation;
    }

}
