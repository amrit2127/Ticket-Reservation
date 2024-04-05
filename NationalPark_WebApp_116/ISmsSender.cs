namespace NationalPark_WebApp_116
{
    public interface ISmsSender
    {
        Task SendSMSAsync(string phoneNumber, string message);
    }
}
