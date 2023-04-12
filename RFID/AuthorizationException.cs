using System;

namespace RFID
{
    class AuthorizationException : Exception
    {
        public AuthorizationException()
            : base("Authorization unsuccessful!")
        {

        }
    }
}