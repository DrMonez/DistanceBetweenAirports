namespace DistanceBetweenAirports.API.Helpers
{
    /// <summary>
    /// Helper for validation.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validation of 3-letter IATA code.
        /// </summary>
        /// <param name="code">3-letter IATA code</param>
        /// <returns>Is valid code</returns>
        public static bool IsValidAirportCode(string code)
        {
            if (string.IsNullOrEmpty(code) || code.Trim().Length != 3)
            {
                return false;
            }
            return true;
        }
    }
}
