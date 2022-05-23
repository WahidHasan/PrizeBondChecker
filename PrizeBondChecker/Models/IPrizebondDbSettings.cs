namespace PrizeBondChecker.Models
{
    public interface IPrizebondDbSettings
    {
        public string PrizebondCheckerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
