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
                    SensorType TEXT NOT NULL,
                    Value REAL NOT NULL,
                    Time DATETIME NOT NULL
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
            command.CommandText = "SELECT SensorType, Value, Time FROM readings";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                readings.Add(new SensorReading
                {
                    SensorType = reader.GetString(0),
                    Value = reader.GetDouble(1),
                    Time = reader.GetDateTime(2)
                });
            }

            return readings;
        }

        // Optional: Add a reading to database
        public void AddReading(SensorReading reading)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO readings (SensorType, Value, Time)
                VALUES ($sensorType, $value, $time);
            ";
            command.Parameters.AddWithValue("$sensorType", reading.SensorType);
            command.Parameters.AddWithValue("$value", reading.Value);
            command.Parameters.AddWithValue("$time", reading.Time);

            command.ExecuteNonQuery();
        }
    }
}
