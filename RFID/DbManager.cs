using System.Data.SQLite;

namespace RFID
{
    public class DbManager
    {
        private Form1 _instance;
        private string _dbPath = @"URI=file:C:\Users\bulka.ondrej3\source\repos\RFID\RFID\rfid.db";
        private SQLiteConnection _connection;

        public DbManager(Form1 instance)
        {
            _instance = instance;
        }

        public void Init()
        {
            _connection = new SQLiteConnection(_dbPath);
        }

        public void Connect()
        {
            _connection.Open();
        }
    }
}