using System;
using System.Windows.Forms;

namespace RFID
{
    class InvalidCodeException : Exception
    {
        public InvalidCodeException()
            : base("Code is invalid!")
        {}
    }
}