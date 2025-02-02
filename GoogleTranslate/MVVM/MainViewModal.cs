using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BLL.Service;

namespace GoogleTranslate.MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputText;
        private string _outputText;
        private string _selectedInputLanguage;
        private string _selectedOutputLanguage;
        public List<string> Languages { get; private set; }

        public string SelectedInputLanguage
        {
            get => _selectedInputLanguage;
            set { _selectedInputLanguage = value; OnPropertyChanged(); }
        }

        public string SelectedOutputLanguage
        {
            get => _selectedOutputLanguage;
            set { _selectedOutputLanguage = value; OnPropertyChanged(); }
        }

        public string InputText
        {
            get => _inputText;
            set { _inputText = value; OnPropertyChanged(); }
        }

        public string OutputText
        {
            get => _outputText;
            set { _outputText = value; OnPropertyChanged(); }
        }

        public ICommand TranslateCommand { get; }

        public MainViewModel()
        {
            TranslateCommand = new RelayCommand(Translate);
            Languages = (List<string>?)ServiceGoogleTranslate.GetLanguageToTranslate();

            // За замовчуванням вибираємо першу мову
            SelectedInputLanguage = Languages[0];
            SelectedOutputLanguage = Languages[1];
        }

        private async void Translate(object obj) {
            OutputText = ServiceGoogleTranslate.TranslateText(InputText, SelectedInputLanguage, SelectedOutputLanguage);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged { add { } remove { } }
    }
}
