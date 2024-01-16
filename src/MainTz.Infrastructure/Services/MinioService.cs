using MainTz.Application.Models.SittingsModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MainTz.Application.Services;
using Minio;

namespace MainTz.Infrastructure.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClientFactory _minioClientFactory;
        private readonly MinioSettings _minioSettings;
        private readonly ILogger<MinioService> _logger;
        public MinioService(IMinioClientFactory minioClientFactory, IOptions<MinioSettings> minioSettings, ILogger<MinioService> logger)
        {
            _minioClientFactory = minioClientFactory;
            _minioSettings = minioSettings.Value;
            _logger = logger;
        }

        public async Task<bool> AddImageToBucketAsync(string bucketName)
        {
            try
            {
                var image = 
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> GetObjectByNameAndBucketAsync(string bucketName, string objectName, int expiryDuration = 900)
        {
            try
            {
                using (var client = _minioClientFactory.CreateClient())
                {
                    var jija = await client.ListBucketsAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public Task GetBuckets()
        {
            throw new NotImplementedException();
        }
        private byte[] ConvertImageToByte(System.Drawing.Image image)
        {
            using(var ms = new MemoryStream())
            {
                image.Save(ms, image);
                return ms.ToArray();
            }
        }
    }
}