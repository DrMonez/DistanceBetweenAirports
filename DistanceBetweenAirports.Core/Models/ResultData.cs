namespace DistanceBetweenAirports.Core.Models
{
    /// <summary>
    /// Data returned by API.
    /// </summary>
    /// <typeparam name="T">Type of returned data.</typeparam>
    public class ResultData<T>
    {
        public string? Error { get; set; }
        public T? Result { get; set; }
    }
}
