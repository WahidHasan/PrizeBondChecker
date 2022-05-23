namespace PrizeBondChecker.Models
{
    public class PrizebondDbSettings : IPrizebondDbSettings
    {
        public string PrizebondCheckerCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
