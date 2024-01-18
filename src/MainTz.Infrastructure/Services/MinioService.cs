using Microsoft.Extensions.Logging;
using MainTz.Application.Services;
using Minio.DataModel.Args;
using Minio;

namespace MainTz.Infrastructure.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClientFactory _minioClientFactory;
        private readonly ILogger<MinioService> _logger;
        public MinioService(IMinioClientFactory minioClientFactory, ILogger<MinioService> logger)
        {
            _minioClientFactory = minioClientFactory;
            _logger = logger;
        }
        //public async Task<bool> AddObjectToBucketAsync(string path, FileStream file)
        //{
        //    string bucketName;
        //    string objectName;
        //    GetBucketNameAndFileNameByPath(path, out bucketName, out objectName);
        //    try
        //    {
        //        var args = new PutObjectArgs()
        //            .WithBucket(bucketName)
        //            .WithObject(objectName);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return false;
        //    }
        //}
        public async Task<string> GetObjectAsync(string path)
        {
            string bucketName = Path.GetDirectoryName(path);
            string objectName = Path.GetFileName(path);
            byte[] objectBytes = null;
            try
            {
                using (var client = _minioClientFactory.CreateClient())
                {
                    var args = new GetObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(objectName)
                        .WithFile(objectName)
                        .WithCallbackStream(async (stream) => // колбек стрим, который конвертирует пикчу в массив байт
                        {
                            await using (var ms = new MemoryStream())
                            {
                                stream.CopyTo(ms);
                                objectBytes = ms.ToArray();
                            }
                        });
                    await client.GetObjectAsync(args);
                    return Convert.ToBase64String(objectBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<string>> GetBucketsAsync()
        {
            try
            {
                using (var client = _minioClientFactory.CreateClient())
                {
                    var buckets = await client.ListBucketsAsync();
                    var bucketNames = buckets.Buckets.Select(b => b.Name).ToList();
                    return bucketNames;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Не удалось получить список бакетов");
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            try
            {
                using(var client = _minioClientFactory.CreateClient())
                {
                    var beArgs = new BucketExistsArgs().WithBucket(bucketName);
                    if (!await client.BucketExistsAsync(beArgs).ConfigureAwait(false))
                    {
                        var mb = new MakeBucketArgs().WithBucket(bucketName);
                        await client.MakeBucketAsync(mb).ConfigureAwait(false);
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ошибка при создании бакета [{ex.Message}]");
                return false;
            }
        }
        //public async Task<bool> RemoveObjectInBucketAsync(string path)
        //{
        //    string bucketName;
        //    string objectName;
        //    try
        //    {
        //        GetBucketNameAndFileNameByPath(path, out bucketName, out objectName);
        //        using (var client = _minioClientFactory.CreateClient())
        //        {
        //            var args = new RemoveObjectArgs()
        //                .WithBucket(bucketName)
        //                .WithObject(objectName);
        //            await client.RemoveObjectAsync(args).ConfigureAwait(false);
                    
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}
        private void GetBucketNameAndFileNameByPath(string filePath, out string bucketName, out string fileName)
        {
            fileName = Path.GetFileName(filePath);
            bucketName = Path.GetDirectoryName(filePath);
        }
    }
}