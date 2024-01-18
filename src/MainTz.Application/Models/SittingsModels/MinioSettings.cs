namespace MainTz.Application.Models.SittingsModels
{
    public class MinioSettings
    {
        public const string MinioPosition = "MinioSettings";
        public string ROOT_USER { get; set; }
        public string ROOT_PASSWORD { get; set; }
        public string StorageEndPoint { get; set; }
    }
}