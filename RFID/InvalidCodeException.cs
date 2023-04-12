using System;

namespace RFID
{
    class InvalidCodeException : Exception
    {
        public InvalidCodeException()
            : base("Code is invalid!")
        {}
    }
}