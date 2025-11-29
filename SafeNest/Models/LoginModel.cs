namespace SafeNest.Models
{
    public class LoginModel
    {
        public string? Pin { get; set; }


        public bool ValidatePin()
        {
            return Pin == "1234";
        }
    }
}
