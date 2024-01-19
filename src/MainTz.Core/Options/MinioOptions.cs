namespace MainTz.Application.Models.OptionsModels
{
    public class MinioOptions
    {
        public const string SectionName = "MinioOptions";
        public string ROOT_USER { get; set; }
        public string ROOT_PASSWORD { get; set; }
        public string StorageEndPoint { get; set; }
    }
}