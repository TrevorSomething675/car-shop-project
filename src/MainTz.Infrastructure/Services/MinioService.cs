using MainTz.Application.Services;
using Minio;

namespace MainTz.Infrastructure.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minioClient;
        public MinioService(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task AddImageToBucket(byte[] bytes, string bucketName)
        {

        }

        public async Task<string> GetObjectByNameAndBucket(string bucketName, string objectName, int expiryDuration = 900)
        {
            //var minioClient = new MinioClient()
            //.WithEndpoint("192.168.120.225:9000")
            //.WithCredentials("adminadmin", "adminadmin")
            //                .Build();


            //var statObjectArgs = new StatObjectArgs()
            //    .WithBucket(bucketName)
            //    .WithObject(objectName);
            //await _minioClient.StatObjectAsync(statObjectArgs);

            //var listOfBuckets = await _minioClient.ListBucketsAsync();
            return "Ok";
        }

        public Task GetBuckets()
        {
            throw new NotImplementedException();
        }
    }
}