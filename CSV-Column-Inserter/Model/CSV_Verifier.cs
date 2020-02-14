using Microsoft.VisualBasic.FileIO;
using System;
using System.Windows;

namespace CSV_Column_Inserter.Model
{
    /// <summary>
    /// Defines class responsible for the verification of a CSV file.
    /// </summary>
    class CSV_Verifier
    {
        #region Private Properties

        private string _filePath = "";
        private TextFieldParser _parser = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of a CSV_Verifier object.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        public CSV_Verifier(string filePath) 
        {
            _filePath = filePath;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Function to attempt to open the CSV file with TextFieldParser.  If exception is thrown, message is displayed and the function returns false.
        /// </summary>
        /// <returns>True if TextFieldParser successfully creates an object given the set path.</returns>
        public bool IsValidCSV()
        {
            bool testValue = true;
            try
            {
                _parser = new TextFieldParser(_filePath);
                _parser.TextFieldType = FieldType.Delimited;
                _parser.SetDelimiters("\t");
                _parser.Close();
            }
            catch (Exception e)
            {
                // TODO: Should this be in the Model, or caught and handled by ViewModel?
                MessageBoxResult errorMessage = MessageBox.Show(e.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                testValue = false;
            }

            return testValue;
        }

        #endregion
    }
}
