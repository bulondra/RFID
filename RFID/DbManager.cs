using System.Collections.Generic;
using System.Data.SQLite;

namespace RFID
{
    public class DbManager
    {
        private Form1 _instance;
        private string _dbPath;
        private SQLiteConnection _connection;

        public DbManager(Form1 instance)
        {
            _instance = instance;
            _dbPath = @"URI=file:" + System.IO.Directory.GetCurrentDirectory()  + @"\rfid.db"; // Path to database
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

        public void LogAction(string code, string place, LogType logType, long time)
        {
            string statement = "INSERT INTO logs " +
                "(code, place, action, created_at) VALUES " +
                "('" + code + "', '" + place +
                "', '" + logType.ToString() + "', " + time + ");"; // Create statement

            var command = new SQLiteCommand(statement, _connection); // Create command

            command.ExecuteNonQuery(); // Perform command
        }

        public int GetUsersCount()
        {
            string statement = "SELECT COUNT(*) FROM users;"; // Create statement

            var command = new SQLiteCommand(statement, _connection); // Create command

            SQLiteDataReader reader = command.ExecuteReader(); // Create reader

            return reader.Read() ? reader.GetInt32(0) : 0;
        }

        public List<DbLogModel> GetLogs()
        {
            List<DbLogModel> logList = new List<DbLogModel>();
            string statement = "SELECT * FROM logs;"; // Create statement

            var command = new SQLiteCommand(statement, _connection); // Create command

            SQLiteDataReader reader = command.ExecuteReader(); // Create reader

            while (reader.Read())
            {
                logList.Add(new DbLogModel { log_id = reader.GetInt32(0), code = reader.GetString(1), place = reader.GetString(2), action = reader.GetString(3), created_at = reader.GetInt64(4) });
            }

            return logList;
        }

        public bool GetExactAccess(string code, string place, int performationTime)
        {
            string statement = "SELECT access_id FROM accesses " +
                               "INNER JOIN codes c on accesses.code_id = c.code_id " +
                               "INNER JOIN places p on accesses.place_id = p.place_id " +
                               "WHERE code = '" + code + "' " +
                               "AND title = '" + place + "'  " +
                               "AND available_from <= " + performationTime + " " +
                               "AND available_until >= " + performationTime + ";"; // Create statement
            
            var command = new SQLiteCommand(statement, _connection); // Create command
            
            SQLiteDataReader reader = command.ExecuteReader(); // Create reader

            return reader.Read(); // Return
        }
        
    }
}