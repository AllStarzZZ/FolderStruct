using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoderStruct
{
    class BadInputFormatException : Exception
    {
        public string WrongPath { get; set; }

        public BadInputFormatException(string wrongPath)
        {
            WrongPath = wrongPath;
        }

        public override string ToString() =>
            $"Path must starts with '/', but can't end with it and can't contains '\\' character.\n({WrongPath})";
    }
}
