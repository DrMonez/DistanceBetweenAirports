namespace DistanceBetweenAirports.Models
{
    public class ResultData<T>
    {
        public string error { get; set; }
        public T result { get; set; }
    }
}
