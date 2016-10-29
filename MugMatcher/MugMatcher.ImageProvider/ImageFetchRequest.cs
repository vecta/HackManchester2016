namespace MugMatcher.ImageProvider
{
    public class ImageFetchRequest
    {
        public GeoLocation Location { get; set; }
        public int SearchRadiusMiles { get; set; }

        public ImageFetchRequest(GeoLocation location, int searchRadiusInMiles = 5)
        {
            Location = location ?? new GeoLocation();
            SearchRadiusMiles = searchRadiusInMiles;
        }
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public GeoLocation()
        {
            Latitude = 53.4807590;
            Longtitude = -2.2426310;
        }
    }
}