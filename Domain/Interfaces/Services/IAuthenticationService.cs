namespace Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public object Authenticate(string AppName, string AppKey);
    }
}
