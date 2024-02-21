namespace DistanceBetweenAirports.Services
{
    internal static class Constants
    {
        public static string GetNullValidationMessage(string objectName)
        {
            return objectName + " should not be null";
        }

        public const string DefaultProviderUrl = "https://places-dev.cteleport.com/airports/";

        public const double EarthRadiusInMiles = 3958.8;
    }
}
