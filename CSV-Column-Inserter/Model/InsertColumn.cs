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
