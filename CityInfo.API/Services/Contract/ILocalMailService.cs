
namespace CityInfo.API.Services.Contract
{
    public interface ILocalMailService
    {
        bool SendMail(string to, string subject, string body);
    }
}
