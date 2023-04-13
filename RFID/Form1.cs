using System.Timers;
using System.Windows.Forms;

namespace RFID
{
    public partial class Form1 : Form
    {
        // Declaration of managers, cycle & testing var
        private AuthManager _authManager;
        private EventManager _eventManager;
        private PortManager _portManager;
        private DbManager _dbManager;
        private TestManager _testManager;
        private System.Timers.Timer mainCycle;
        private bool _testing;

        public Form1()
        {
            // Initialization of managers
            _authManager = new AuthManager(this);
            _eventManager = new EventManager(this);
            _portManager = new PortManager(this);
            _dbManager = new DbManager(this);
            _testManager = new TestManager(this);

            _testing = true; // Testing variable

            InitializeComponent();

            if (!_testing) GetPortManager().OpenPort(); // Open port
            
            GetDbManager().Init(); // Init database
            GetDbManager().Connect(); // Connect database

            GetTestManager().TestAuthorization(GetTestManager().GetTestData()[4]); // Testing

            if (_testing) return;
            
            // Initialize & start main cycle
            mainCycle = new System.Timers.Timer();
            mainCycle.Elapsed += new ElapsedEventHandler(GetEventManager().MainCycle_Elapsed);
            mainCycle.Interval = 1;
            mainCycle.Enabled = true;
        }

        // Manager getters
        public AuthManager GetAuthManager() => _authManager;
        public EventManager GetEventManager() => _eventManager;
        public PortManager GetPortManager() => _portManager;
        public DbManager GetDbManager() => _dbManager;
        public TestManager GetTestManager() => _testManager;
        
        
        public void PauseTimer() => mainCycle.Stop(); // Pause main cycle
        public void ResumeTimer() => mainCycle.Start(); // Resume main cycle
    }
}
