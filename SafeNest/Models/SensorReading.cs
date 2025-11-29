namespace SafeNest.Models
{
    public class SensorReading
    {
        public string SensorType { get; set; } = string.Empty;
        public double Value { get; set; }
        public DateTime Time { get; set; }  // use Time
    }
}
