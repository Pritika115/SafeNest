namespace SafeNest.Models
{
    public class SensorReading
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string SensorType { get; set; } = "";
        public double Value { get; set; }
    }
}
