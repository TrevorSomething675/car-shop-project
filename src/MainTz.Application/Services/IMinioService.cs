namespace MainTz.Application.Services
{
    public interface IMinioService
    {
        public Task<bool> GetObjectByNameAndBucketAsync(string bucketName, string objectName, int expiryDuration = 900);
        public Task<bool> AddImageToBucketAsync(string bucketName);
        public Task GetBuckets();
    }
}