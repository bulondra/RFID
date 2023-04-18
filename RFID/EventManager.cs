using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace RFID
{
    // Example of packets
    // 2-    42-37-39-32-34-31-32-34  -d-a-3-15  Normální čtečka
    // 2-4-  42-37-39-32-34-31-32-34  -d-a-3-    Číselníková čtečka
    // 2-4-  42-37-39-32-34-31-32-34  -d-a-3-    Číselníková čtečka
    // 2-4-  30-30-30-30              -d-a-3-    Číselník
    // 2-4-  30-30-30-30              -d-a-3-    Číselník
    
    public class EventManager
    {
        private Form1 _instance;

        public EventManager(Form1 instance)
        {
            _instance = instance;
        }
        
        public void MainCycle_Elapsed(object sender, ElapsedEventArgs e)
        {
            int bytesToRead = _instance.GetPortManager().GetPort().BytesToRead; // Get count of bytes to read
            string[] buffer = new string[bytesToRead]; // Init buffer with the size of count of bytes
            
            _instance.PauseTimer(); // Pause main cycle
            
            for (int i = 0; i < bytesToRead; i++) // Read bytes
            {
                int b = _instance.GetPortManager().GetPort().ReadByte(); // Get byte
                buffer[i] = Convert.ToString(b, 16); // Save byte to buffer
            }
            
            if (buffer.Length > 0) _instance.GetAuthManager().Authorize(buffer); // If buffer contains data -> perform authorization
            
            _instance.ResumeTimer(); // Resume main cycle
        }

        public void Button_Click(object sender, EventArgs e)
        {
            string code = _instance.GetTextbox1Text();
            string loc = _instance.GetTextbox2Text();
            Location.TryParse(loc, out Location loca);
            _instance.GetTestManager().TestAuthorization(new AuthorizationData{code = code, location = loca});
        }
    }
}