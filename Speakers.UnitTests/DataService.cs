using AppSpeakers.Domain;

namespace Speakers.UnitTests
{
    public class DataService
    {
        public static IList<Speaker> GetSpeakers()
        {
            var _speakers = new List<Speaker>()
            {
                new Speaker()
                {
                    Id = "03b4119d-e616-cbcb-fa71-d4a79e07ebc6",
                    Name = "Lynn Mayert",
                    Bio = "Unit Test",
                    WebSite = "Unittest.com"
                },
                new Speaker()
                {
                    Id = "044fe831-3c9f-99b1-3fcc-4f265972b370",
                    Name = "Cathy Ruecker",
                    Bio = "Unit Test",
                    WebSite = "Unittest.com"
                },
            };


            return _speakers;
        }

        public static Speaker GetSpeaker()
        {
            return new Speaker()
            {
                Id = "0a0ba06d-4135-b0c0-b223-63af9d798236",
                Name = "Winston McGlynn",
                Bio = "Unit Test",
                WebSite = "Unittest.com"
            };
        }

    }
}
