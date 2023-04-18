using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID
{
    public partial class Administration : Form
    {
        public Administration(Form1 _instance)
        {
            InitializeComponent();

            mainCycle.Start();
            var logs = _instance.GetDbManager().GetLogs();
            int usersCount = _instance.GetDbManager().GetUsersCount();
            currentUsersText.Text = usersCount.ToString();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            currentDateText.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }
    }
}
