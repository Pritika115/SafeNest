using Microsoft.Data.Sqlite;
using SafeNest.Models;
using System;
using System.Collections.Generic;

namespace SafeNest.Data
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(string conn)
        {
            _connectionString = conn;
            EnsureDatabase();
        }

        // Create table if it doesn't exist
        private void EnsureDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS readings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Timestamp DATETIME NOT NULL,
                    SensorType TEXT NOT NULL,
                    Value REAL NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        public List<SensorReading> GetReadings()
        {
            var readings = new List<SensorReading>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Timestamp, SensorType, Value FROM readings";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                readings.Add(new SensorReading
                {
                    Id = reader.GetInt32(0),
                    Timestamp = reader.GetDateTime(1),
                    SensorType = reader.GetString(2),
                    Value = reader.GetDouble(3)
                });
            }

            return readings;
        }
    }
}
