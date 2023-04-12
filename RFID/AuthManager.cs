using System;
using System.Diagnostics;

namespace RFID
{
    public class AuthManager
    {
        private Form1 _instance;

        /*
CODES:
0585DF14
57192BFA
A56B691A
B7924124
2BFF628A
        */

        public AuthManager(Form1 instance)
        {
            _instance = instance;
        }

        public void Authorize(string[] buffer)
        {
            AuthorizationData authData = DecodeCode(buffer);
            Debug.WriteLine(authData.code);
        }
        
        public AuthorizationData DecodeCode(string[] buffer)
        {
            Location loc = Location.NIC;
            string code = "";
            if (buffer[0].Equals("2") && buffer[1].Equals("4"))
            {
                if (buffer.Length > 10)
                {
                    loc = Location.CISELNIK;
                    for (int i = 2; i < 10; i++)
                    {
                        code += (char)Convert.ToUInt32(buffer[i], 16);
                    }
                } else
                {
                    loc = Location.CTECKA_CISELNIKU;
                    for (int i = 2; i < 6; i++)
                    {
                        code += (char)Convert.ToUInt32(buffer[i], 16);
                    }
                }
            } else
            {
                loc = Location.CTECKA;
                for (int i = 1; i < 9; i++)
                {
                    code += (char)Convert.ToUInt32(buffer[i], 16);
                }
            }

            AuthorizationData authData = new AuthorizationData();
            authData.location = loc;
            authData.code = code;


            return authData;
        }
    }
}