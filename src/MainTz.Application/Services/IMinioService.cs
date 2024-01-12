namespace MainTz.Application.Services
{
    public interface IMinioService
    {
        public Task<string> GetObjectByNameAndBucket(string bucketName, string objectName, int expiryDuration = 900);
        public Task GetBuckets();
        public Task AddImageToBucket(byte[] bytes, string bucketName);
    }
}