using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Column_Inserter.Model
{
    /// <summary>
    /// Defines class responsible for inserting a column into a CSV file.
    /// </summary>
    class InsertColumn
    {
        #region Private Properties

        string _path ="";
        string _columnHeader = "";
        string _columnValue = "";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InsertColumn object.
        /// </summary>
        /// <param name="path">The path to the CSV file.</param>
        /// <param name="columnHeader">The column header string.</param>
        /// <param name="columnValue">The column value string.</param>
        public InsertColumn(string path, string columnHeader, string columnValue)
        {
            _path = path;
            _columnHeader = columnHeader;
            _columnValue = columnValue;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method for executing the insertions of 
        /// </summary>
        public void BeginInsertions()
        {
            try
            {
                string tmpFile = _path + ".tmp";
                using (StreamReader reader = new StreamReader(_path))
                {
                    // Using statement to automatically close the writing file stream.
                    using (StreamWriter writer = new StreamWriter(tmpFile))
                    {
                        string lineIn = ""; // Where we will store the current line read
                        bool isHeader = true; // Flag for knowing if the CSV header is the current line

                        while ((lineIn = reader.ReadLine()) != null) // As long as a line is being read
                        {
                            if (isHeader == true) // If this is the first read / header row
                            {
                                writer.WriteLine(_columnHeader); // Append the final column header "Filename" to the first row
                                isHeader = false;
                            }
                            else // This is just another row
                            {
                                writer.WriteLine(lineIn + "," + _columnValue); // Append the data as the last column value
                            }

                        }
                    }
                }

                // Replace old file with tmp.
                File.Delete(_path);
                File.Move(tmpFile, _path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

    }
}
