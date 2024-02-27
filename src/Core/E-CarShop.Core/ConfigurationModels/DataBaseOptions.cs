namespace E_CarShop.Core.ConfigurationModels
{
    public class DataBaseOptions
    {
        public const string SectionName = "DataBaseSettings";
        public string ConnectionString { get; set; }
    }
}