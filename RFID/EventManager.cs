using System;
using System.Diagnostics;
using System.Timers;

namespace RFID
{
    public class EventManager
    {
        private Form1 _instance;

        public EventManager(Form1 instance)
        {
            _instance = instance;
        }
        
        public void MainCycle_Elapsed(object sender, ElapsedEventArgs e)
        {
            int bytesToRead = _instance.GetPortManager().GetPort().BytesToRead;
            string[] buffer = new string[bytesToRead];
            // 2-    42-37-39-32-34-31-32-34  -d-a-3-15
            // 2-4-  42-37-39-32-34-31-32-34  -d-a-3-
            // 2-4-  42-37-39-32-34-31-32-34  -d-a-3-
            // 2-4-  30-30-30-30              -d-a-3-
            // 2-4-  30-30-30-30              -d-a-3-
            _instance.PauseTimer();
            for (int i = 0; i < bytesToRead; i++)
            {
                int b = _instance.GetPortManager().GetPort().ReadByte();
                buffer[i] = Convert.ToString(b, 16);
            }
            if (buffer.Length > 0) _instance.GetAuthManager().Authorize(buffer);
            _instance.ResumeTimer();

        }
    }
    
    // Normální čtečka
    // 2-42-37-39-32-34-31-32-34-d-a-3-15

    // Číselníková čtečka
    // 2-4-42-37-39-32-34-31-32-34-d-a-3

    // Číselník
    // 2-4-30-30-30-30-d-a-3
    // 2-4-30-32-41-36-d-a-3
    
    /*
    String readedLine = port.ReadLine();
    char[] chars = readedLine.ToCharArray();

    char special = chars[0];
    chars.Take(0);

    foreach (char ch in chars) {
        int ascii = (int)ch;
        Debug.Write(ascii);
    }
    Debug.Write("\n");
    */
}