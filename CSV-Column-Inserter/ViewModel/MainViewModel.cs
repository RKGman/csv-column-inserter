using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CSV_Column_Inserter.ViewModel
{
    class MainViewModel : BindableBase, INotifyPropertyChanged
    {
        private string _selectedFilePath = "Please selected a CSV file...";        
        private DelegateCommand _browseFileCommand;
        private bool _isValidCSV = false; 

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand BrowseFileCommand
        {
            get
            {
                if (_browseFileCommand == null)
                {
                    _browseFileCommand = new DelegateCommand(ExecuteBrowseFile);
                }
                return _browseFileCommand;
            }
        }
        private void ExecuteBrowseFile()
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.DefaultExt = ".csv";
            openFD.Filter = "csv Files (*.csv)|*.csv|CSV Files (*.CSV)|*.CSV";
            Nullable<bool> result = openFD.ShowDialog();

            if (result.HasValue && result.Value)
            {
                // Set the selected file name
                SelectedFilePath = openFD.FileName;
            }
        }

        public bool IsValidCSV  
        {
            get { return _isValidCSV; }
            set
            {
                _isValidCSV = value;
                OnPropertyChanged("IsValidCSV");
            }
        }

        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set
            {
                _selectedFilePath = value;
                OnPropertyChanged("SelectedFilePath");
            }
        }
    }
}
