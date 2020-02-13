using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Column_Inserter.Model
{
    class CSV_Verifier
    {
        private string _filePath = "";
        private TextFieldParser _parser = null;
        public CSV_Verifier(string filePath) 
        {
            _filePath = filePath;
        }

        public bool IsValidCSV()
        {

        }
    }
}
