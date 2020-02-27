/**************************************************************************************************************************************************************

Copyright © Roni Garayanala

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

The Software is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, 
fitness for a particular purpose and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages or other liability, 
whether in an action of contract, tort or otherwise, arising from, out of or in connection with the Software or the use or other dealings in the Software. 
 
**************************************************************************************************************************************************************/

using CSV_Column_Inserter.Model;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows;
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
        private string _columnHeaderString = "New Column Header";
        private string _columnValueString = "New Column Values";
        private bool _isValidCSV = false;
        private bool _useUserField = false;
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
                ColumnValueString = SelectedFilePath;
            }
        }

        /// <summary>
        /// Method that executes the action of inserting values into the CSV file.
        /// </summary>
        private void ExecuteUpdateFile()
        {
            try
            {
                InsertColumn insert = new InsertColumn(SelectedFilePath, ColumnHeaderString, ColumnValueString);
                insert.BeginInsertions();
                MessageBoxResult confirmationMessage = MessageBox.Show("The operation completed without error", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }            
            catch (Exception e)
            {
                MessageBoxResult errorMessage = MessageBox.Show(e.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    _updateCSVCommand = new DelegateCommand(ExecuteUpdateFile);
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

                if (_setFileNameValue == false)
                {
                    // Use user input string
                    UseUserField = true;
                    ColumnValueString = _columnValueString;
                }
                else
                {
                    // Use the file name
                    UseUserField = false;
                    ColumnValueString = _selectedFilePath;
                }

                OnPropertyChanged("SetFileNameValue");
            }
        }

        /// <summary>
        /// Gets or sets a flag indicating whether or not to use the user defined value.
        /// </summary>
        public bool UseUserField
        {
            get { return _useUserField; }
            set
            {
                _useUserField = value;
                OnPropertyChanged("UseUserField");
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

        /// <summary>
        /// Gets or sets the column header string.
        /// </summary>
        public string ColumnHeaderString
        {
            get { return _columnHeaderString; }
            set
            {
                _columnHeaderString = value;
                OnPropertyChanged("ColumnHeaderString");
            }
        }

        /// <summary>
        /// Gets or sets the column value string.
        /// </summary>
        public string ColumnValueString
        {
            get { return _columnValueString; }
            set
            {
                _columnValueString = value;
                OnPropertyChanged("ColumnValueString");
            }
        }

        #endregion
    }
}
