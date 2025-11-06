namespace Speakers.Api.Models
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
    }
    public class AppSettings: IAppSettings
    {
        public string ConnectionString { get; set; }
    }
}
