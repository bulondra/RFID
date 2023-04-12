using System.Timers;
using System.Windows.Forms;

namespace RFID
{
    public partial class Form1 : Form
    {
        private AuthManager _authManager;
        private EventManager _eventManager;
        private PortManager _portManager;
        private DbManager _dbManager;
        private System.Timers.Timer mainCycle;


        public Form1()
        {
            _authManager = new AuthManager(this);
            _eventManager = new EventManager(this);
            _portManager = new PortManager(this);
            _dbManager = new DbManager(this);

            InitializeComponent();

            GetPortManager().OpenPort();
            
            GetDbManager().Init();

            mainCycle = new System.Timers.Timer();
            mainCycle.Elapsed += new ElapsedEventHandler(GetEventManager().MainCycle_Elapsed);
            mainCycle.Interval = 1;
            mainCycle.Enabled = true;
        }

        public AuthManager GetAuthManager() => _authManager;
        public EventManager GetEventManager() => _eventManager;
        public PortManager GetPortManager() => _portManager;
        public DbManager GetDbManager() => _dbManager;
        public void PauseTimer() => mainCycle.Stop();
        public void ResumeTimer() => mainCycle.Start();
    }
}
