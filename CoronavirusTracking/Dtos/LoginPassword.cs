namespace CoronavirusTracking.Dtos
{
    public abstract class LoginPassword
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }

        public class In : LoginPassword
        {
        }
    }
}
