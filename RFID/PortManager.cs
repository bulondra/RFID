using System.IO.Ports;

namespace RFID
{
    public class PortManager
    {
        private Form1 _instance;
        private SerialPort _port;

        public PortManager(Form1 instance)
        {
            _instance = instance;
            _port = new SerialPort("COM11", 19200, Parity.None, 8, StopBits.One);
        }

        public void OpenPort()
        {
            _port.Open();
        }

        public SerialPort GetPort()
        {
            return _port;
        }
    }
}