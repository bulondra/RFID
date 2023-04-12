using System;
using System.Diagnostics;

namespace RFID
{
    public class AuthManager
    {
        private Form1 _instance;

        public AuthManager(Form1 instance)
        {
            _instance = instance;
        }

        public void Authorize(string[] buffer)
        {
            AuthorizationData authData = DecodeCode(buffer);
            Debug.WriteLine(authData.code);
        }

        public void Authorize(AuthorizationData authData)
        {
            Debug.WriteLine(authData.code);
        }
        
        public AuthorizationData DecodeCode(string[] buffer)
        {
            Location loc = Location.NIC; // Init default values
            string code = ""; // Init default values
            
            if (buffer[0].Equals("2") && buffer[1].Equals("4")) // If packet has elements of numpad
            {
                if (buffer.Length > 10) // If packet has more than 10 bytes (element of numpad)
                {
                    loc = Location.CISELNIK; // Set location to numpad
                    
                    for (int i = 2; i < 10; i++) // Parse code from packet
                    {
                        code += (char)Convert.ToUInt32(buffer[i], 16);
                    }
                } else // Else (element of numpad reader)
                {
                    loc = Location.CTECKA_CISELNIKU; // Set location to numpad reader
                    
                    for (int i = 2; i < 6; i++) // Parse code from packet
                    {
                        code += (char)Convert.ToUInt32(buffer[i], 16);
                    }
                }
            } else // Else (element of normal reader)
            {
                loc = Location.CTECKA; // Set location to normal reader
                
                for (int i = 1; i < 9; i++) // Parse code from packet
                {
                    code += (char)Convert.ToUInt32(buffer[i], 16);
                }
            }

            return new AuthorizationData { location = loc, code = code }; // Return data
        }
    }
}