using CSV_Column_Inserter.Model;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace CSV_Column_Inserter.ViewModel
{
    /// <summary>
    /// Defines the class responsible for defining the Main View's ViewModel.  Contains functions for selecting a file and executing updates given user defined variables.
    /// </summary>
    class MainViewModel : BindableBase, INotifyPropertyChanged
    {
        #region Private Properties

        private string _selectedFilePath = "Please selected a CSV file...";        
        private bool _isValidCSV = false;
        private bool _setFileNameValue = true;
        private DelegateCommand _browseFileCommand;
        private DelegateCommand _updateCSVCommand;

        #endregion

        #region Constructors

        public MainViewModel()
        { // I guess this isn't necessary...
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method for notifying the view that a property has been updated.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method that executes the action of an OpenFileDialog object and processing the result.
        /// </summary>
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
                CSV_Verifier csvVerifier = new CSV_Verifier(SelectedFilePath);
                IsValidCSV = csvVerifier.IsValidCSV();
            }
        }

        #endregion

        #region Public Properties

        // Public event for handling the notification of a property being updated.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the command to trigger an execution of browsing for a file.
        /// </summary>
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

        /// <summary>
        /// Gets the command to trigger an execution of updating a file.
        /// </summary>
        public ICommand UpdateCSVCommand
        {
            get
            {
                if (_updateCSVCommand == null)
                {
                    _updateCSVCommand = new DelegateCommand(ExecuteBrowseFile);
    }
                return _updateCSVCommand;
            }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether or not to use the default setting of using the file name as the new column value.
        /// </summary>
        public bool SetFileNameValue
        {
            get { return _setFileNameValue; }
            set
            {
                _setFileNameValue = value;
                OnPropertyChanged("SetFileNameValue");
            }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether or not the CSV if valid.
        /// </summary>
        public bool IsValidCSV  
        {
            get { return _isValidCSV; }
            set
            {
                _isValidCSV = value;
                OnPropertyChanged("IsValidCSV");
            }
        }

        /// <summary>
        /// Gets or sets the selected file path string.
        /// </summary>
        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set
            {
                _selectedFilePath = value;
                OnPropertyChanged("SelectedFilePath");
            }
        }

        #endregion
    }
}
