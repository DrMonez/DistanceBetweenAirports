namespace DistanceBetweenAirports
{
    internal static class Constants
    {
        public static string GetNullValidationMessage(string objectName)
        {
            return objectName + " should not be null";
        }

        public const string UNCORRECT_AIRPORT_CODE_MESSAGE = "Airport code should not be null and contains only 3 letters";

        public const string DEFAULT_PROVIDER_URL = "https://places-dev.cteleport.com/airports/";

        public const double EARTH_RADIUS_IN_MILES = 3958.8;
    }
}
