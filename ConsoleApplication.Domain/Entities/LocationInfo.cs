namespace ConsoleApplication.Domain.Entities
{
    public class LocationInfo
    {
        public string Town { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

        public override string ToString()
        {
            return $"City: {Town ?? "Unknown"} Province: {County ?? "Unknown"} State: {Country ?? "Unknown"} CAP: {PostCode ?? "Unknown"}";
        }
    }
}