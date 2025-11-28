using Microsoft.Data.Sqlite;
using SafeNest.Models;
using System.Collections.Generic;

namespace SafeNest.Data
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
            Initialize();
        }

        private void Initialize()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText =
            """
            CREATE TABLE IF NOT EXISTS SensorReading (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Timestamp TEXT NOT NULL,
                SensorType TEXT NOT NULL,
                Value REAL
            );
            """;
            cmd.ExecuteNonQuery();
        }

        public void Add(SensorReading r)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText =
            """
            INSERT INTO SensorReading (Timestamp, SensorType, Value)
            VALUES ($ts, $type, $val);
            """;

            cmd.Parameters.AddWithValue("$ts", r.Timestamp.ToString("o"));
            cmd.Parameters.AddWithValue("$type", r.SensorType);
            cmd.Parameters.AddWithValue("$val", r.Value);

            cmd.ExecuteNonQuery();
        }

        public List<SensorReading> GetAll()
        {
            var list = new List<SensorReading>();

            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Timestamp, SensorType, Value FROM SensorReading ORDER BY Id;";

            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new SensorReading
                {
                    Id = rdr.GetInt32(0),
                    Timestamp = DateTime.Parse(rdr.GetString(1)),
                    SensorType = rdr.GetString(2),
                    Value = rdr.GetDouble(3)
                });
            }

            return list;
        }
    }
}
