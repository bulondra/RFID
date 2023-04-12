using System.Data.SQLite;

namespace RFID
{
    public class DbManager
    {
        private Form1 _instance;
        private string _dbPath = @"URI=file:C:\Users\bulka.ondrej3\source\repos\RFID\RFID\rfid.db"; // Path to database
        private SQLiteConnection _connection;

        public DbManager(Form1 instance)
        {
            _instance = instance;
        }

        public void Init() => _connection = new SQLiteConnection(_dbPath); // Initialize connection

        public void Connect() => _connection.Open(); // Open connection

        public void Disconnect() => _connection.Close(); // Close connection

        public CodeModel GetExactCode(string code)
        {
            string statement = "SELECT * FROM codes WHERE code = '" + code + "';"; // Create statement
            
            var command = new SQLiteCommand(statement, _connection); // Create command
            
            SQLiteDataReader reader = command.ExecuteReader(); // Create reader

            return reader.Read() ? new CodeModel { code_id = reader.GetInt32(0), code = reader.GetString(1), valid = reader.GetBoolean(2) } : null; // Return CodeModel or null
        }
        
    }
}