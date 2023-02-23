using TimeZoneConverter;
using System.Collections.ObjectModel;

namespace background_service;

public class TimeService
{
    public ReadOnlyCollection<City> _readOnlyCities;
    private readonly IConfiguration _configuration;
    public readonly Guid _guid;
    public TimeService(IConfiguration configuration)
    {
        _guid = Guid.NewGuid();
        List<City> cities = new();
        cities.Add(
            new City(){
                Name = "Bogota",
                TimeZone = "America/Bogota"
            }
        );
        cities.Add(
            new City(){
                Name = "Chicago",
                TimeZone = "America/Chicago"
            }
        );
        cities.Add(
            new City(){
                Name = "Argentina",
                TimeZone = "America/Argentina/Buenos_Aires"
            }
        );
        cities.Add(
            new City(){
                Name = "Detroit",
                TimeZone = "America/Detroit"
            }
        );
        cities.Add(
            new City(){
                Name = "London",
                TimeZone = "Europe/London"
            }
        );
        _readOnlyCities = new(cities);
        _configuration = configuration;
    }

    public string convertAllTimeZones()
    {
        try
        {   
            string tzStringInfo = default;
            using (var db = new DatabaseContext(_configuration))
            {
                List<City> cities = db.Cities.ToList<City>();
                foreach (var city in cities)
                {
                    string windowsTZ = TZConvert.IanaToWindows(city.TimeZone);
                    TimeZoneInfo tzInfo = TimeZoneInfo.FindSystemTimeZoneById(windowsTZ);
                    DateTime time = TimeZoneInfo.ConvertTime(DateTime.Now, tzInfo);
                    string timeString = time.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFzzz");
                    tzStringInfo += $"City: {city.Name}\n"+
                    $"TimeZone: {city.TimeZone}\n"+
                    $"Time: {timeString}\n";
                }
            }
            return tzStringInfo;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}