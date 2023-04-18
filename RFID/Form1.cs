using System;
using System.Drawing;
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
        private Button _button;
        private bool _testing;

        public Form1()
        {
            // Initialization of managers
            _authManager = new AuthManager(this);
            _eventManager = new EventManager(this);
            _portManager = new PortManager(this);
            _dbManager = new DbManager(this);
            _testManager = new TestManager(this);

            _testing = false; // Testing variable

            InitializeComponent();

            if (!_testing) GetPortManager().OpenPort(); // Open port
            
            GetDbManager().Init(); // Init database
            GetDbManager().Connect(); // Connect database

            if (_testing)
            {
                _button = new Button();
                _button.Name = "Button";
                _button.Text = "Button";
                _button.Location = new Point(10, 70);
                _button.Size = new Size(200, 100);
                _button.Font = new Font(FontFamily.GenericSansSerif, 20.25F);
                _button.Click += new EventHandler(GetEventManager().Button_Click);
                Controls.Add(_button);

                textBox1.Visible = true;
                textBox2.Visible = true;
            }

            Administration adminForm = new Administration(this);
            adminForm.Show();
            
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
        public string GetTextbox1Text() => textBox1.Text;
        public string GetTextbox2Text() => textBox2.Text;
        
        
        public void PauseTimer() => mainCycle.Stop(); // Pause main cycle
        public void ResumeTimer() => mainCycle.Start(); // Resume main cycle
    }
}
