namespace Speakers.Api.Models
{
    public interface IAppSettings
    {
        string ConnectionString { get; set; }
        string ApplicationInsightConnectionString { get; set; }
    }
    public class AppSettings: IAppSettings
    {
        public string ConnectionString { get; set; }

        public string ApplicationInsightConnectionString { get; set; }
    }
}
